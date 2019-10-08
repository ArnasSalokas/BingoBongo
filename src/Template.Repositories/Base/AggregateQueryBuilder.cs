using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Template.Repositories.Base
{
    public class AggregateQueryBuilder
    {
        private QueryConfiguriation queryConfiguration;

        public AggregateQueryBuilder(QueryConfiguriation queryConfiguration)
        {
            this.queryConfiguration = queryConfiguration;
        }

        public string BuildQuery(DynamicParameters parameters, IEnumerable<AggregateColumn> columns)
        {
            string filters = null;

            if (queryConfiguration.FilterParameters.Any())
            {
                filters = queryConfiguration.FilterParameters.ToClause();
                queryConfiguration.FilterParameters.AddToParameters(parameters);
            }

            if (columns == null || columns.Count() == 0)
                return $"SELECT 0 FROM {queryConfiguration.GetTableName()} WHERE 0 = 1";

            var sqlQuery = $"SELECT {ConstructAggregates(columns)} FROM {queryConfiguration.GetTableName()} {BuildFilters(filters)}";

            return sqlQuery;
        }

        private string ConstructAggregates(IEnumerable<AggregateColumn> columns)
        {
            var selects = new List<string>();

            foreach (var column in columns)
                selects.Add($@"{ParseAggregateType(column.AggregateType)}{column.ColumnName})
                            AS {(string.IsNullOrWhiteSpace(column.ColumnAlias) ? column.ColumnName : column.ColumnAlias)}");

            return string.Join(",", selects);
        }

        private string BuildFilters(string filters)
        {
            if (!string.IsNullOrWhiteSpace(filters))
                return $"WHERE {filters.Trim()}";

            return string.Empty;
        }

        private string ParseAggregateType(AggregateType type)
        {
            switch (type)
            {
                case AggregateType.SUM_DISTINCT:
                case AggregateType.COUNT_DISTINCT:
                    var aggr = type.ToString().Split("_");
                    return $"{aggr[0]}({aggr[1]} ";

                case AggregateType.AVG:
                case AggregateType.COUNT:
                case AggregateType.SUM:
                default:
                    return $"{type.ToString()}(";
            }
        }
    }

    public class AggregateColumn
    {
        public AggregateType AggregateType { get; }
        public string ColumnName { get; }
        public string ColumnAlias { get; }

        public AggregateColumn(AggregateType aggregateType, string columnName, string columnAlias = null)
        {
            AggregateType = aggregateType;
            ColumnName = columnName;
            ColumnAlias = columnAlias;
        }
    }

    public enum AggregateType
    {
        COUNT,
        SUM,
        AVG,

        COUNT_DISTINCT,
        SUM_DISTINCT
    }
}
