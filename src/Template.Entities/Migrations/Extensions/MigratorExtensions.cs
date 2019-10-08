using FluentMigrator.Builders.Alter.Table;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Builders.Execute;
using Template.Entities.Database.Base;

namespace Template.Entities.Migrations.Extensions
{
    public static class MigratorExtensions
    {
        private const string ID = nameof(IEntity.Id);
        private const string ADDED_DATE = nameof(IAdded.AddedDate);
        private const string ADDED_BY = nameof(IAdded.AddedBy);
        private const string MODIFIED_DATE = nameof(IModified.ModifiedDate);
        private const string MODIFIED_BY = nameof(IModified.ModifiedBy);

        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn(ID)
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
                .Identity();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithAddedDateColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn(ADDED_DATE)
                .AsDateTime()
                .Nullable();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithAddedByColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax, string table, string column)
        {
            return tableWithColumnSyntax
                .WithColumn(ADDED_BY)
                .AsInt32()
                .Nullable()
                .ForeignKey(table, column);
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithModifiedDateColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn(MODIFIED_DATE)
                .AsDateTime()
                .Nullable();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithModifiedByColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax, string table, string column)
        {
            return tableWithColumnSyntax
                .WithColumn(MODIFIED_BY)
                .AsInt32()
                .Nullable()
                .ForeignKey(table, column);
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax AsMaxString(this ICreateTableColumnAsTypeSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .AsString(int.MaxValue);
        }

        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AddIdColumn(this IAlterTableAddColumnOrAlterColumnOrSchemaSyntax syntax)
        {
            return syntax
                .AddColumn(ID)
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
                .Identity();
        }

        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AddAddedDateColumn(this IAlterTableAddColumnOrAlterColumnOrSchemaSyntax syntax)
        {
            return syntax
                .AddColumn(ADDED_DATE)
                .AsDateTime()
                .Nullable();
        }

        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AddAddedByColumn(this IAlterTableAddColumnOrAlterColumnOrSchemaSyntax syntax, string table, string column)
        {
            return syntax
                .AddColumn(ADDED_BY)
                .AsInt32()
                .Nullable()
                .ForeignKey(table, column);
        }

        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AddModifiedDateColumn(this IAlterTableAddColumnOrAlterColumnOrSchemaSyntax syntax)
        {
            return syntax
                .AddColumn(MODIFIED_DATE)
                .AsDateTime()
                .Nullable();
        }

        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AddModifiedByColumn(this IAlterTableAddColumnOrAlterColumnOrSchemaSyntax syntax, string table, string column)
        {
            return syntax
                .AddColumn(MODIFIED_BY)
                .AsInt32()
                .Nullable()
                .ForeignKey(table, column);
        }

        public static void AlterTableAddGeography(this IExecuteExpressionRoot expressionRoot, string table, string column, bool isNullable)
        {
            var nullability = isNullable ? "NULL" : "NOT NULL" ;

            expressionRoot.Sql($"ALTER TABLE [{table}] ADD [{column}] geography {nullability}");
        }
    }
}
