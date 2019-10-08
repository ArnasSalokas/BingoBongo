using Template.Common.Extensions;
using Template.Entities.API.WebParams.Base;

namespace Template.Repositories.Base.Filtering
{
    public class FilteringRestrictredBitwiseParameter : FilteringParameter
    {
        public override object Value => (int)base.Value;

        public FilteringRestrictredBitwiseParameter(string dbPropertyName, string parameterName, object value,
            LogicalOperator logicalOperator, ComparisonType comparisonType = ComparisonType.Default) : base(
            ComparisonOperator.RestrictedBitwise, dbPropertyName, parameterName, value, logicalOperator, comparisonType)
        {
        }

        public override string ToClause(string tableAlias = null) => $" ({tableAlias}{ParameterName.Bracketed()} {Operation.GetDescription()} {ParameterizedName} <> 0 AND {tableAlias}{ ParameterName.Bracketed()} >= {ParameterizedName})";
    }
}
