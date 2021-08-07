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
    [Migration(202107201259)]
    public class M20210709_ThirdMigration : Migration
    {
        public override void Up()
        {
            Alter.Table(nameof(Event))
                .AddColumn(nameof(Event.CreatorAvatar)).AsString();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }

    }
}
