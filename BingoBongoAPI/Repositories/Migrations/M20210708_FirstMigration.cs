using BingoBongoAPI.Entities;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories.Migrations
{
    [Migration(201807201046)]
    public class M20210708_FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table(nameof(User))
                .WithIdColumn()
                .WithColumn(nameof(User.Name)).AsMaxString().Nullable()
                .WithColumn(nameof(User.Email)).AsMaxString().Nullable()
                .WithColumn(nameof(User.Created)).AsDateTime2().NotNullable()
                .WithColumn(nameof(User.Email)).AsMaxString().NotNullable()
                .WithColumn(nameof(User.SlackUserId)).AsMaxString().Nullable();

            Create.Table(nameof(Event))
                .WithIdColumn()
                .WithColumn(nameof(Event.Name)).AsMaxString()
                .WithColumn(nameof(Event.Place)).AsMaxString()
                .WithColumn(nameof(Event.Time)).AsDateTime2()
                .WithColumn(nameof(Event.UserId)).AsInt32();

            Create.Table(nameof(UserEvent))
                .WithIdColumn()
                .WithColumn(nameof(UserEvent.UserId)).AsInt32()
                .WithColumn(nameof(UserEvent.EventId)).AsInt32();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }

    }
}
