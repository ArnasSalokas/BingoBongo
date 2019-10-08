using System.ComponentModel;

namespace Template.Entities.API.WebParams.Base
{
    public enum ComparisonOperator
    {
        [Description("<IGNORE>")]
        NoComparing,
        [Description("=")]
        Equals,
        [Description("<>")]
        DoesNotEqual,
        [Description("@>")]
        IsPartOfList,
        [Description("IN")]
        In,
        [Description("LIKE")]
        Contains,
        [Description("LIKE")]
        CandidatesContain,
        [Description(">")]
        GreaterThan,
        [Description(">=")]
        GreaterThanOrEquals,
        [Description("<")]
        LessThan,
        [Description("<=")]
        LessThanOrEquals,
        [Description("&")]
        Bitwise,
        [Description("&")]
        RestrictedBitwise
    }
}
