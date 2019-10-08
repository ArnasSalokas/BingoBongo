using System;
using Template.Common.Extensions;
using Template.Entities.Enums;

namespace Template.Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class XlsFormatAttribute : Attribute
    {
        /// <summary>
        /// Header of the column
        /// </summary>
        public string Header { get; }

        /// <summary>
        /// Goes directly into Styling of Excel sheet cell
        /// </summary>
        public string Format { get; }

        /// <summary>
        /// Order of the column
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Xls format
        /// </summary>
        /// <param name="header"></param>
        /// <param name="format"></param>
        /// <param name="order"></param>
        public XlsFormatAttribute(string header, string format, int order)
        {
            Header = header;
            Format = format;
            Order = order;
        }

        /// <summary>
        /// Xls format
        /// </summary>
        /// <param name="header"></param>
        /// <param name="format"></param>
        /// <param name="order"></param>
        public XlsFormatAttribute(string header, ExcelCellType format, int order)
        {
            Header = header;
            Format = ExtractCellType(format);
            Order = order;
        }

        private string ExtractCellType(ExcelCellType cellType) => cellType.GetDescription();
    }
}
