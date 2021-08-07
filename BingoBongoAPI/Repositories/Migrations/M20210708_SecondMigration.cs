using BingoBongoAPI.Entities;
using FluentMigrator;
using FluentMigrator.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories.Migrations
{
    [Migration(202107201046)]
    public class M20210708_SecondMigration : Migration
    {
        public override void Up()
        {
            Alter.Table(nameof(User))
                .AddColumn(nameof(User.Type)).AsInt16();

            Alter.Table(nameof(Event))
                .AddColumn(nameof(Event.DeadlineDuration)).AsDateTimeOffset();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }

    }
}
