using System.Collections.Generic;
using Template.Common.Extensions;
using Template.Repositories.Base.Filtering;
using Template.Repositories.Base.Mapper;

namespace Template.Repositories.Base
{
    public class QueryConfiguriation
    {
        internal PagingConfiguration Paging { get; set; }
        internal FilterParameters FilterParameters { get; set; }
        internal IList<(string, string)> SortParameters { get; set; }
        internal IFieldMapper FieldMapper { get; set; }
        internal string TableName { get; set; }
        internal string TableAlias { get; set; }
        internal string ViewParameters { get; set; }

        public QueryConfiguriation()
        {
            FilterParameters = new FilterParameters();
            SortParameters = new List<(string, string)>();
            Paging = new PagingConfiguration();
        }

        internal string GetTableName()
        {
            var tableName = TableName.Bracketed(); // Bracketed because of reserved keywords

            if (!string.IsNullOrWhiteSpace(ViewParameters))
                tableName = $"{tableName} {ViewParameters}";

            if (!string.IsNullOrWhiteSpace(TableAlias))
                tableName = $"{tableName} {TableAlias}";

            return tableName;
        }
    }

    internal class PagingConfiguration
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
