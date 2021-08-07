using BingoBongoAPI.Entities.Interfaces;
using FluentMigrator.Builders.Alter.Table;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories.Migrations
{
    public static class MigratorExtensions
    {
        private const string Id = nameof(IEntity.Id);

        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn(Id)
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
                .Identity();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax AsMaxString(this ICreateTableColumnAsTypeSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .AsString(int.MaxValue);
        }

        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AddIdColumn(this IAlterTableAddColumnOrAlterColumnOrSchemaSyntax syntax)
        {
            return syntax
                .AddColumn(Id)
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
                .Identity();
        }
    }
}
