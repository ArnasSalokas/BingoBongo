using Template.Common.Extensions;
using Template.Entities.API.WebParams.Base;

namespace Template.Repositories.Base.Filtering
{
    public class FilteringBitwiseParameter : FilteringParameter
    {
        public override object Value => (int)base.Value;

        public FilteringBitwiseParameter(string dbPropertyName, string parameterName, object value,
            LogicalOperator logicalOperator, ComparisonType comparisonType = ComparisonType.Default) : base(
            ComparisonOperator.Bitwise, dbPropertyName, parameterName, value, logicalOperator, comparisonType)
        {
        }

        public override string ToClause(string tableAlias = null) => $" {tableAlias}{ParameterName.Bracketed()} {Operation.GetDescription()} {ParameterizedName} <> 0";
    }
}
