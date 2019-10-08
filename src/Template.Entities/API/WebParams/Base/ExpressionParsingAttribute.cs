using System;

namespace Template.Entities.API.WebParams.Base
{
    public class ExpressionParsingAttribute : Attribute
    {
        public readonly string PropertyName;
        public readonly string NamesofCandidateProperties;
        public readonly ComparisonOperator Operator;
        public readonly ComparisonType? ComparisonType;

        public ExpressionParsingAttribute(string propertyName, ComparisonOperator expressionOperator, string namesofCandidateProperties)
        {
            PropertyName = propertyName;
            Operator = expressionOperator;
            NamesofCandidateProperties = namesofCandidateProperties;
        }

        public ExpressionParsingAttribute(string propertyName, ComparisonOperator expressionOperator)
        {
            PropertyName = propertyName;
            Operator = expressionOperator;
        }

        public ExpressionParsingAttribute(string propertyName, ComparisonOperator expressionOperator, ComparisonType comparisonType)
        {
            PropertyName = propertyName;
            Operator = expressionOperator;
            ComparisonType = comparisonType;
        }
    }
}
