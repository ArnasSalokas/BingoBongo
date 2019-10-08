using System.ComponentModel;

namespace Template.Entities.Enums
{
    public enum ExcelCellType
    {
        [Description("General")]
        General = 0,

        [Description("0")]
        Integer = 1,

        [Description("0.00")]
        Decimal = 2,

        // 1 - 100%, 0.01 - 1%
        [Description("0.00%")]
        Percent = 10,

        [Description("yyyy-MM-dd")]
        Date = 20,

        [Description("HH:mm:ss")]
        Time = 21,

        [Description("yyyy-MM-dd HH:mm:ss")]
        DateTime = 22
    }
}
