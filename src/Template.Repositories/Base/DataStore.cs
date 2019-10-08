using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Template.Common.Extensions;
using Template.Common.Helpers;
using Template.Entities.API.WebParams.Base;
using Template.Entities.Database.Base;
using Template.Entities.Database.Models;
using Template.Repositories.Base.Contracts;
using Template.Repositories.Base.Filtering;
using Template.Repositories.Base.Mapper;

namespace Template.Repositories.Base
{
    public class DataStore : IDataStore
    {
        private readonly IServiceProvider _services;

        public SqlConnection Connection { get; set; }
        public QueryConfiguriation QueryConfiguration;

        #region Constructors

        public DataStore(IServiceProvider services)
        {
            _services = services;
            Connection = _services.GetService<SqlConnection>();
            QueryConfiguration = new QueryConfiguriation();
        }

        #endregion

        #region Reads

        public async Task<IEnumerable<T>> Get<T>()
        {
            if (QueryConfiguration.FieldMapper == null)
                QueryConfiguration.FieldMapper = FieldMapper.Get<T>();

            var parameters = new DynamicParameters();

            var commandSql = new SelectQueryBuilder(QueryConfiguration).BuildQuery(parameters);

            return await HandleQueryAsync<T>(commandSql, parameters);
        }

        public async Task<IEnumerable<T>> GetDistinct<T>(string distinctColumn)
        {
            if (QueryConfiguration.FieldMapper == null)
                QueryConfiguration.FieldMapper = FieldMapper.Get<T>();

            var parameters = new DynamicParameters();

            var commandSql = new SelectQueryBuilder(QueryConfiguration).BuildQueryDistinct(parameters, distinctColumn);

            return await HandleQueryAsync<T>(commandSql, parameters);
        }

        public async Task<T> FirstOrNull<T>()
        {
            T entity = default(T);

            if (entity.IsDefaultValue())
            {
                // Enforce paging before querying
                Paged(0, 1);
                entity = (await Get<T>()).FirstOrDefault();
            }

            return entity;
        }

        public async Task<T> Aggregate<T>(IEnumerable<AggregateColumn> columns)
        {
            var parameters = new DynamicParameters();

            var commandSql = new AggregateQueryBuilder(QueryConfiguration).BuildQuery(parameters, columns);

            return (await HandleQueryAsync<T>(commandSql, parameters)).FirstOrDefault();
        }

        #endregion

        #region Writes

        public async Task<T> Add<T>(T newItem, params object[] otherColumnValues) where T : class, new()
        {
            if (newItem == null)
                return null;

            if (QueryConfiguration.FieldMapper == null)
                QueryConfiguration.FieldMapper = FieldMapper.Get<T>();

            var queryParts = this.BuildInsertQuery(newItem, otherColumnValues);

            var query = $"INSERT INTO {GetTableName()} ({queryParts.Item1}) OUTPUT INSERTED.{queryParts.Item3} VALUES ({queryParts.Item2})";

            T inserted = null;

            inserted = (await HandleQueryAsync<T>(query, queryParts.Item4)).FirstOrDefault();
            if (inserted == null)
                return null;

            ResetFilters();

            Filtered(queryParts.Item3, inserted.GetType().GetProperty(queryParts.Item3).GetValue(inserted));

            inserted = await FirstOrNull<T>();

            return inserted;
        }

        public async Task AddMany<T>(IEnumerable<T> newItems) where T : class, new()
        {
            if (QueryConfiguration.FieldMapper == null)
                QueryConfiguration.FieldMapper = FieldMapper.Get<T>();

            foreach (var newItem in newItems)
            {
                var queryParts = this.BuildInsertQuery(newItem, null);

                var query = $"INSERT INTO {GetTableName()} ({queryParts.Item1}) OUTPUT INSERTED.{queryParts.Item3} VALUES ({queryParts.Item2})";

                await HandleQueryAsync<T>(query, queryParts.Item4);
            }
        }

        public async Task Update(object updatedItem)
        {
            if (updatedItem == null)
                return;

            if (QueryConfiguration.FieldMapper == null)
                QueryConfiguration.FieldMapper = FieldMapper.Get(updatedItem.GetType());

            var queryParts = BuildUpdateQuery(updatedItem);
            var query = $"UPDATE {GetTableName()} SET {queryParts.Item1} WHERE {queryParts.Item2} = @KEY";

            await HandleExecuteAsync(query, queryParts.Item3);
        }

        public async Task Delete<T>(object key)
        {
            if (key == null)
                return;

            var parameters = new DynamicParameters();
            var keyProperty = ReflectionHelper.GetKeyProperty(typeof(T));

            parameters.Add("@KEY", key);

            var query = $"DELETE FROM {GetTableName()} WHERE {keyProperty.Name} = @KEY";

            await HandleExecuteAsync(query, parameters);
        }

        public async Task Delete<T>(IEnumerable<object> keys)
        {
            if (keys == null || !keys.Any())
                return;

            var parameters = ToDynamicParameters(keys, "KEY");
            var paramNames = string.Join(", ", parameters.ParameterNames);
            var keyProperty = ReflectionHelper.GetKeyProperty(typeof(T));
            var query = $"DELETE FROM {GetTableName()} WHERE {keyProperty.Name} IN ({paramNames})";

            await HandleExecuteAsync(query, parameters);
        }

        #endregion

        public async Task<IEnumerable<T>> Query<T>(string sql, dynamic parameters = null)
        {
            return await HandleQueryAsync(sql, parameters);

            // TODO: data mapper
            //if (QueryConfiguration.DataMapper != null)
            //await QueryConfiguration.DataMapper.Map(queryResults);
        }

        public async Task Execute(string sql, dynamic parameters = null)
        {
            await HandleExecuteAsync(sql, parameters);
        }

        private string GetTableName() => QueryConfiguration.GetTableName();

        #region Query builders

        private Tuple<string, string, string, DynamicParameters> BuildInsertQuery(object newItem, object[] otherColumnValues)
        {
            var columns = new List<string>();
            var values = new List<string>();
            var parameters = new DynamicParameters();
            var type = newItem.GetType();
            var keyProperty = ReflectionHelper.GetKeyProperty(type).Name;

            foreach (var field in QueryConfiguration.FieldMapper.InsertFields)
            {
                columns.Add(field.ColumnName.Bracketed());

                if (!field.IsGeography)
                {
                    var parameterName = $"@{field.PropertyName.ToUpper()}";
                    values.Add(parameterName);
                    parameters.Add(parameterName, field.GetColumnValue(newItem));
                }

                else
                {
                    values.Add(ToGeographyInsertSqlQuery(nameof(GeographyPoint.Latitude), nameof(GeographyPoint.Longitude)));
                }
            }

            if (otherColumnValues != null)
            {
                for (int i = 0; i < otherColumnValues.Length - 1; i += 2)
                {
                    var columnName = (string)otherColumnValues[i];

                    columns.Add(columnName.Bracketed());

                    var parameterName = $"@{columnName.ToUpper()}";

                    values.Add(parameterName);

                    parameters.Add(parameterName, otherColumnValues[i + 1]);
                }
            }

            return Tuple.Create(string.Join(", ", columns), string.Join(", ", values), keyProperty, parameters);
        }

        private Tuple<string, string, DynamicParameters> BuildUpdateQuery(object updatedItem)
        {
            var parameters = new DynamicParameters();
            var pieces = new List<string>();
            var type = updatedItem.GetType();
            var keyProperty = ReflectionHelper.GetKeyProperty(type);

            parameters.Add("@KEY", keyProperty.GetValue(updatedItem));

            foreach (var field in QueryConfiguration.FieldMapper.UpdateFields)
            {
                if (!field.IsGeography)
                {
                    var parameterName = $"@{field.PropertyName.ToUpper()}";
                    pieces.Add($"{field.ColumnName.Bracketed()} = {parameterName}");
                    parameters.Add(parameterName, field.GetColumnValue(updatedItem));
                }

                else
                {
                    pieces.Add(ToGeographyUpdateSqlQuery(field.ColumnName, nameof(GeographyPoint.Latitude), nameof(GeographyPoint.Longitude)));
                }
            }

            return Tuple.Create(string.Join(", ", pieces), keyProperty.Name, parameters);
        }

        #endregion

        #region Query object construction

        public IDataStore As<T>(string aliasedTableName = null)
        {
            if (!string.IsNullOrWhiteSpace(aliasedTableName))
                QueryConfiguration.TableName = aliasedTableName;

            else
                QueryConfiguration.TableName = typeof(T).Name;

            QueryConfiguration.FieldMapper = FieldMapper.Get<T>();

            return this;
        }

        /// <summary>
        /// Method add parameters to function. It works properly with strings and numbers, with other data type it might not work.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IDataStore WithFunctionParameters(params object[] parameters)
        {
            var sqlParameters = string.Empty;

            foreach (var parameter in parameters)
            {
                var stringValue = parameter.ToString();

                if (double.TryParse(stringValue, out var number))
                    sqlParameters += $"{stringValue},";

                else
                    sqlParameters += $"'{stringValue}',";
            }

            sqlParameters = sqlParameters.TrimEnd(new[] {','});

            QueryConfiguration.ViewParameters = $"({sqlParameters})";

            return this;
        }

        public IDataStore Alias(string tableAlias)
        {
            QueryConfiguration.TableAlias = tableAlias;

            return this;
        }

        public IDataStore Filtered(FilteringParameter parameter)
        {
            QueryConfiguration.FilterParameters.Add(parameter);

            return this;
        }

        public IDataStore Filtered(string propertyName, object value, ComparisonOperator operation = ComparisonOperator.Equals, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            QueryConfiguration.FilterParameters.Add(FilteringParameter.ToParameter(value, GetColumnName(propertyName), operation, logicalOperator));

            return this;
        }

        public IDataStore Filtered(WebParameters webParameters, bool isFull = true)
        {
            QueryConfiguration.FilterParameters.AddRange(QueryConfiguration.FilterParameters.FromWebParameters(webParameters));

            if (isFull)
            {
                Paged(webParameters);
                Sorted(webParameters);
            }

            return this;
        }

        public IDataStore Paged(int? pageNumber = 0, int? pageSize = 100)
        {
            QueryConfiguration.Paging.PageNumber = pageNumber.Value;
            QueryConfiguration.Paging.PageSize = pageSize.Value;

            return this;
        }

        public IDataStore Paged(WebParameters webParameters)
        {
            QueryConfiguration.Paging.PageNumber = webParameters.Page.GetValueOrDefault();
            QueryConfiguration.Paging.PageSize = webParameters.PerPage.GetValueOrDefault();

            return this;
        }

        public IDataStore Paged(PagingParameters pagingParameters)
        {
            QueryConfiguration.Paging.PageNumber = pagingParameters.Page.GetValueOrDefault();
            QueryConfiguration.Paging.PageSize = pagingParameters.PerPage.GetValueOrDefault();

            return this;
        }

        public IDataStore Sorted(string propertyName, string direction = null)
        {
            QueryConfiguration.SortParameters.Add((GetColumnName(propertyName), direction));

            return this;
        }

        public IDataStore Sorted(string propertyName, ListSortDirection direction = ListSortDirection.Ascending)
        {
            QueryConfiguration.SortParameters.Add((GetColumnName(propertyName), direction == ListSortDirection.Ascending ? string.Empty : "DESC"));

            return this;
        }

        public IDataStore Sorted(WebParameters webParameters)
        {
            QueryConfiguration.SortParameters.Add((webParameters.Sort, webParameters.SortDir));

            return this;
        }

        #endregion

        public void ResetFilters() { QueryConfiguration.FilterParameters.Clear(); }

        #region Connection/transaction/query handling

        private async Task HandleExecuteAsync(string query, DynamicParameters parameters)
        {
            if (Connection.State == ConnectionState.Broken || Connection.State == ConnectionState.Closed)
            {
                await Connection.OpenAsync();
            }

            var trStore = _services.GetRequiredService<ITransactionStore>();
            await SqlMapper.ExecuteAsync(Connection, query, parameters, trStore.Transaction);
        }

        private async Task<IEnumerable<T>> HandleQueryAsync<T>(string query, DynamicParameters parameters)
        {
            if (Connection.State == ConnectionState.Broken || Connection.State == ConnectionState.Closed)
            {
                await Connection.OpenAsync();
            }

            var trStore = _services.GetRequiredService<ITransactionStore>();
            return await SqlMapper.QueryAsync<T>(Connection, query, parameters, trStore.Transaction);
        }

        #endregion

        private string GetColumnName(string propertyName)
        {
            return QueryConfiguration.FieldMapper?.Fields.FirstOrDefault(f => f.PropertyName.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase))?.ColumnName ?? propertyName;
        }

        private static DynamicParameters ToDynamicParameters(IEnumerable<object> values, string baseName)
        {
            var parameters = new DynamicParameters();

            for (int i = 0; i < values.Count(); ++i)
                parameters.Add($"@@{baseName}{i}", values.ElementAt(i));

            return parameters;
        }

        private string ToGeographyInsertSqlQuery(string latitude, string longitude) =>
            $"geography::Point(@{latitude.ToUpper()}, @{longitude.ToUpper()}, 4326)";

        private string ToGeographyUpdateSqlQuery(string columnName, string latitude, string longitude) => $"{columnName.Bracketed()} = geography::Point(@{latitude.ToUpper()}, @{longitude.ToUpper()}, 4326)";
    }
}
