using System.Collections.Generic;
using System.Linq;
using Dapper;
using Template.Common.Extensions;
using Template.Entities.API.WebParams.Base;

namespace Template.Repositories.Base.Filtering
{
    public class FilterParameters : List<FilteringParameter>
    {
        private const string AND_OPERATOR = "AND";
        private const string OR_OPERATOR = "OR";

        public IEnumerable<FilteringParameter> FromWebParameters(WebParameters parameters)
        {
            HashSet<FilteringParameter> result = new HashSet<FilteringParameter>();
            var type = parameters.GetType();

            foreach (var propertyInfo in type.GetProperties())
            {
                var propertyValue = propertyInfo.GetValue(parameters);
                if (propertyValue == null)
                    continue;

                var propertyName = string.Empty;
                var propertyOperator = string.Empty;
                ComparisonOperator propertyOperatorEnum = ComparisonOperator.Equals;
                ComparisonType comparisonType = ComparisonType.Default;

                foreach (var attr in propertyInfo.GetCustomAttributes(typeof(ExpressionParsingAttribute), false))
                {
                    var expressionAttribute = (ExpressionParsingAttribute)attr;
                    propertyName = expressionAttribute.PropertyName;
                    propertyOperatorEnum = expressionAttribute.Operator;
                    propertyOperator = propertyOperatorEnum.GetDescription();
                    comparisonType = expressionAttribute.ComparisonType ?? comparisonType;
                }

                if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(propertyOperator))
                    continue;

                result.Add(FilteringParameter.ToParameter(propertyValue, propertyName, propertyOperatorEnum, LogicalOperator.AND, comparisonType));
            }

            return result;
        }

        public string ToClause(string tableAlias = null)
        {
            // Split into ANDs and ORs
            var andClause = AndParameterClause(tableAlias);
            var orClause = OrParameterClause(tableAlias);

            var finalClause = andClause;

            if (!string.IsNullOrWhiteSpace(andClause))
                finalClause += " AND ";

            finalClause += orClause;

            finalClause = finalClause.TrimEnd(" AND ");

            return finalClause;
        }

        private string AndParameterClause(string tableAlias = null)
        {
            var result = string.Empty;

            var andParameters = this.Where(t => t.LogicalOperator == LogicalOperator.AND).ToList();

            for (int i = 0; i < andParameters.Count(); i++)
            {
                var param = andParameters[i];
                var paramClause = param.ToClause(tableAlias);

                if (!string.IsNullOrWhiteSpace(paramClause))
                {
                    if (i > 0)
                        result += $" {param.LogicalOperator.ToString()}";

                    result += paramClause;
                }
            }

            return result;
        }

        private string OrParameterClause(string tableAlias = null)
        {
            var result = string.Empty;

            var orParameters = this.Where(t => t.LogicalOperator == LogicalOperator.OR).ToList();
            var orCount = orParameters.Count();

            for (int i = 0; i < orCount; i++)
            {
                var param = orParameters[i];
                var paramClause = param.ToClause(tableAlias);

                if (!string.IsNullOrWhiteSpace(paramClause))
                {
                    if (i > 0)
                        result += $" {param.LogicalOperator.ToString()}";

                    result += paramClause;
                }
            }

            return orCount > 0 ? $"({result})" : string.Empty;
        }

        public void AddToParameters(DynamicParameters parameters)
        {
            for (int i = 0; i < Count; i++)
                this[i].AddToParameters(parameters);
        }
    }
}
