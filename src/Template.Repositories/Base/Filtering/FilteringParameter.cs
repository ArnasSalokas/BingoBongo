using Dapper;
using Template.Common.Extensions;
using Template.Common.Helpers;
using Template.Entities.API.WebParams.Base;

namespace Template.Repositories.Base.Filtering
{
    public class FilteringParameter
    {
        public ComparisonOperator Operation { get; private set; }
        public string ColumnName { get; private set; }
        public string ParameterName { get; private set; }
        public string ParameterizedName { get; }
        public LogicalOperator LogicalOperator { get; set; }
        public virtual object Value { get; private set; }
        public ComparisonType ComparisonType { get; private set; }

        public FilteringParameter(ComparisonOperator operation, string dbPropertyName, string parameterName, object value, LogicalOperator logicalOperator, ComparisonType comparisonType = ComparisonType.Default)
        {
            Operation = operation;
            ColumnName = dbPropertyName;
            ParameterName = parameterName;
            Value = value;
            ParameterizedName = ParameterizeName();
            LogicalOperator = logicalOperator;
            ComparisonType = comparisonType;
        }

        public virtual string ToClause(string tableName = null)
        {
            var table = tableName != null ? tableName + "." : string.Empty;

            if (Operation == ComparisonOperator.DoesNotEqual && Value == null)
                return $" {table}{ColumnName.Bracketed()} IS NOT NULL";

            if (Operation == ComparisonOperator.Equals && Value == null)
                return $" {table}{ColumnName.Bracketed()} IS NULL";

            return $" {table}{ColumnName.Bracketed()} {GetOperationString()} {ParameterizedName}";
        }

        public virtual void AddToParameters(DynamicParameters parms)
        {
            if (Value == null)
                return;

            parms.Add(ParameterizedName, Value);
        }

        public static FilteringParameter ToParameter(object obj, string columnName, ComparisonOperator operation = ComparisonOperator.Equals, LogicalOperator logicalOperator = LogicalOperator.AND, ComparisonType comparisonType = ComparisonType.Default)
        {
            if (comparisonType == ComparisonType.StDistance)
                return new FilteringStDistanceParameter(columnName, columnName, obj, comparisonType, operation, logicalOperator);

            if (operation == ComparisonOperator.Contains)
                return new FilteringParameter(operation, columnName, columnName, $"%{obj}%", logicalOperator);

            if (operation == ComparisonOperator.In)
                return new FilteringInParameter(columnName, columnName, obj);

            if (operation == ComparisonOperator.Bitwise)
                return new FilteringBitwiseParameter(columnName, columnName, obj, logicalOperator);

            if (operation == ComparisonOperator.RestrictedBitwise)
                return new FilteringRestrictredBitwiseParameter(columnName, columnName, obj, logicalOperator);

            return new FilteringParameter(operation, columnName, columnName, obj, logicalOperator);
        }

        public string GetOperationString() => Operation.GetDescription();

        protected virtual string ParameterizeName() => $"@{ParameterName.ToUpper()}{RandomHelper.RandomString(6)}";
    }
}
