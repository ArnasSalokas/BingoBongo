using System.Collections;
using System.Linq;
using Dapper;
using Template.Entities.API.WebParams.Base;

namespace Template.Repositories.Base.Filtering
{
    public class FilteringInParameter : FilteringParameter
    {
        public override object Value
        {
            get
            {
                var baseValue = base.Value;

                if (baseValue is string)
                    return (baseValue as string).Split(new[] { ',' }).Select(s => s.Trim()).Cast<object>().ToArray();

                return (baseValue as IEnumerable).Cast<object>().ToArray();
            }
        }

        public FilteringInParameter(string propertyName, string parameterName, object value)
            : base(ComparisonOperator.In, propertyName, parameterName, value, LogicalOperator.AND)
        {
        }

        public override void AddToParameters(DynamicParameters parameters)
        {
            var values = Value as object[];
            var upperName = ParameterName.ToUpper();

            for (int i = 1; i <= values.Length; i++)
                parameters.Add($"{upperName}{i}", values[i - 1]);
        }

        protected override string ParameterizeName()
        {
            var values = Value as object[];
            var i = 1;
            var upperName = ParameterName.ToUpper();

            var vals = values.Select(v => $"@{upperName}{i++}");
            return $"({string.Join(',', vals)})";
        }

        public override string ToClause(string tableAlias = null)
        {
            var baseValue = base.Value;

            if (baseValue == null)
                return string.Empty;

            if (baseValue is string && string.IsNullOrEmpty(baseValue.ToString()))
                return string.Empty;

            if (!(baseValue as IEnumerable).Cast<object>().Any())
                return string.Empty;

            return base.ToClause(tableAlias);
        }
    }
}
