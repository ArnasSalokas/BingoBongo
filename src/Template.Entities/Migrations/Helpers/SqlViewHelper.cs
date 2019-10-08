namespace Template.Entities.Migrations.Helpers
{
    public static class SqlViewHelper
    {
        private static string CreateView(string viewName, string viewSql) => $"CREATE VIEW {viewName} AS {viewSql}";

        private static string DropView(string viewName) => $"DROP VIEW {viewName}";

        // Supports older SQL servers
        public static string RecreateView(string viewName, string viewSql)
        {
            return $@"
IF Object_ID('dbo.{viewName}', 'V') IS NOT NULL
    {DropView(viewName)}
GO

{CreateView(viewName, viewSql)}
";
        }
    }
}
