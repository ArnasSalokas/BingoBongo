using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Template.Entities.API.WebParams.Base;
using Template.Repositories.Base.Filtering;

namespace Template.Repositories.Base.Contracts
{
    public interface IDataStore
    {
        void ResetFilters();

        Task<IEnumerable<T>> Get<T>();
        Task<IEnumerable<T>> GetDistinct<T>(string distinctColumn);
        Task<T> FirstOrNull<T>();
        Task<T> Aggregate<T>(IEnumerable<AggregateColumn> columns);

        Task<T> Add<T>(T newItem, params object[] otherColumnValues) where T : class, new();
        Task AddMany<T>(IEnumerable<T> newItems) where T : class, new();

        Task Update(object updatedItem);

        Task Delete<T>(object key);
        Task Delete<T>(IEnumerable<object> key);

        Task<IEnumerable<T>> Query<T>(string sql, dynamic parameters = null);
        Task Execute(string sql, dynamic parameters);

        IDataStore As<T>(string aliasedTableName = null);
        IDataStore WithFunctionParameters(params object[] parameters);
        IDataStore Alias(string tableAlias);
        IDataStore Filtered(FilteringParameter parameter);
        IDataStore Filtered(string propertyName, object value, ComparisonOperator operation = ComparisonOperator.Equals, LogicalOperator logicalOperator = LogicalOperator.AND);
        IDataStore Filtered(WebParameters webParameters, bool isFull = true);

        IDataStore Paged(int? pageNumber, int? pageSize);
        IDataStore Paged(WebParameters webParameters);
        IDataStore Paged(PagingParameters webParameters);

        IDataStore Sorted(string propertyName, string direction);
        IDataStore Sorted(string propertyName, ListSortDirection direction = ListSortDirection.Ascending);
        IDataStore Sorted(WebParameters webParameters);
    }
}
