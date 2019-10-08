using System.Collections.Generic;
using System.Linq;
using Dapper;
using Template.Common.Extensions;

namespace Template.Repositories.Base
{
    public class SelectQueryBuilder
    {
        private const string DESCENDING_ORDER = "DESC";
        private const string ROW_NUMBER = "ROWNUMBER";

        public readonly QueryConfiguriation queryConfiguration;

        public SelectQueryBuilder(QueryConfiguriation queryConfiguration)
        {
            this.queryConfiguration = queryConfiguration;
        }

        public string BuildQuery(DynamicParameters parameters, bool useOrOperators = false)
        {
            string filters = null;

            if (queryConfiguration.FilterParameters.Any())
            {
                filters = queryConfiguration.FilterParameters.ToClause();
                queryConfiguration.FilterParameters.AddToParameters(parameters);
            }

            var sql = string.Empty;

            if (queryConfiguration.Paging != null && queryConfiguration.Paging.PageNumber != 0)
            {
                // If we use SQL function as parameterized view, we can't 'AS' it.
                var orderedAs = "Ordered" + queryConfiguration.TableName.Split('(')[0];
                var originalAs = "Original" + queryConfiguration.GetTableName().BracketsRemoved().Split('(')[0];

                sql =
                    $"SELECT {queryConfiguration.FieldMapper.BuildSelectProjection(true)} " +
                    $"FROM (SELECT {queryConfiguration.FieldMapper.BuildSelectProjection(true)}, ROW_NUMBER() OVER ({BuildSort(queryConfiguration.SortParameters)}) AS {ROW_NUMBER} " +
                    $"FROM (SELECT {queryConfiguration.FieldMapper.BuildSelectProjection()} " +
                    $"FROM {queryConfiguration.GetTableName()} {BuildFilters(filters)}) AS {originalAs}) AS {orderedAs} " +
                    $"WHERE {BuildPaging(queryConfiguration.Paging.PageSize, queryConfiguration.Paging.PageNumber)}";
            }

            else
            {
                var pageSize = queryConfiguration.Paging?.PageSize != 0 ? " TOP " + queryConfiguration.Paging.PageSize : string.Empty;
                sql =
                    $"SELECT {pageSize} {queryConfiguration.FieldMapper.BuildSelectProjection()} " +
                    $"FROM {queryConfiguration.GetTableName()}" +
                    $"{BuildFilters(filters)} {BuildSort(queryConfiguration.SortParameters)}";
            }

            return sql;
        }

        //Builds SQL query that selects rows of a SINGLE DISTINCT column.
        public string BuildQueryDistinct(DynamicParameters parameters, string distinctParameter, bool userOrOperators = false)
        {
            string filters = null;

            if (queryConfiguration.FilterParameters.Any())
            {
                filters = queryConfiguration.FilterParameters.ToClause();
                queryConfiguration.FilterParameters.AddToParameters(parameters);
            }

            var sql = string.Empty;
            // If we use SQL function as parameterized view, we can't 'AS' it.
            var orderedAs = "Ordered" + queryConfiguration.TableName.Split('(')[0];
            var originalAs = "Original" + queryConfiguration.GetTableName().BracketsRemoved().Split('(')[0];

            if (queryConfiguration.Paging != null && queryConfiguration.Paging.PageNumber != 0)
            {
                sql =
                    $"SELECT {queryConfiguration.FieldMapper.BuildSelectProjection(true)} " +
                    $"FROM (SELECT {queryConfiguration.FieldMapper.BuildSelectProjection(true)}, ROW_NUMBER() OVER ({BuildSort(queryConfiguration.SortParameters)}) AS {ROW_NUMBER} " +
                    $"FROM (SELECT {queryConfiguration.FieldMapper.BuildSelectProjection()}, ROW_NUMBER() OVER(PARTITION BY [{distinctParameter}] ORDER BY [{distinctParameter}]) AS {ROW_NUMBER} " +
                    $"FROM {queryConfiguration.GetTableName()} {BuildFilters(filters)}) AS {originalAs} WHERE {originalAs}.{ROW_NUMBER} = 1) AS {orderedAs} " +
                    $"WHERE {BuildPaging(queryConfiguration.Paging.PageSize, queryConfiguration.Paging.PageNumber)}";
            }

            else
            {
                var pageSize = queryConfiguration.Paging?.PageSize != 0 ? " TOP " + queryConfiguration.Paging.PageSize : string.Empty;
                sql =
                    $"SELECT {pageSize} {queryConfiguration.FieldMapper.BuildSelectProjection()} " +
                    $"FROM (SELECT {queryConfiguration.FieldMapper.BuildSelectProjection()}, ROW_NUMBER() OVER(PARTITION BY [{distinctParameter}] ORDER BY [{distinctParameter}]) AS {ROW_NUMBER} " +
                    $"FROM {queryConfiguration.GetTableName()} {BuildFilters(filters)}) AS {originalAs}" +
                    $"{BuildFilters(filters)} {BuildSort(queryConfiguration.SortParameters)}" +
                    (string.IsNullOrEmpty(filters) ? "WHERE " : string.Empty) + $"{ originalAs}.{ROW_NUMBER} = 1";
            }

            return sql;
        }

        private string BuildPaging(int? pageSize, int? page)
        {
            if (!pageSize.HasValue && !page.HasValue)
                return string.Empty;

            var skip = pageSize * (page - 1);
            var take = pageSize;

            if (skip == null)
                return string.Empty;

            var paging = new List<string>();

            paging.Add($"ROWNUMBER > {skip}");

            if (take.HasValue)
            {
                var maxRow = skip.HasValue ? take.Value + skip.Value : take.Value;

                paging.Add($"ROWNUMBER <= {maxRow}");
            }

            return string.Join(" AND ", paging);
        }

        private string BuildFilters(string filters) => (!string.IsNullOrWhiteSpace(filters)) ? $" WHERE {filters}" : null;

        private string BuildSort(IEnumerable<(string property, string direction)> sortOptions)
        {
            if (sortOptions == null || sortOptions.Count() == 0)
                return string.Empty;

            return $"ORDER BY {string.Join(", ", sortOptions.Select(t => ($"{t.property.Bracketed()} {(t.direction == DESCENDING_ORDER ? DESCENDING_ORDER : string.Empty)}").Trim()))}";
        }
    }
}
