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
                .WithColumn(nameof(User.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(User.Name)).AsMaxString().Nullable()
                .WithColumn(nameof(User.Email)).AsMaxString().Nullable()
                .WithColumn(nameof(User.Created)).AsDateTime().NotNullable()
                .WithColumn(nameof(User.Picture)).AsMaxString().Nullable()
                .WithColumn(nameof(User.SlackUserId)).AsMaxString().Nullable();

            Create.Table(nameof(Event))
                .WithColumn(nameof(Event.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(Event.Name)).AsMaxString()
                .WithColumn(nameof(Event.Place)).AsMaxString()
                .WithColumn(nameof(Event.Time)).AsDateTime()
                .WithColumn(nameof(Event.UserId)).AsInt32()
            .WithColumn(nameof(Event.Description)).AsMaxString()
            .WithColumn(nameof(Event.Created)).AsDateTime()
            .WithColumn(nameof(Event.ChannelId)).AsMaxString();

            Create.Table(nameof(UserEvent))
                .WithColumn(nameof(UserEvent.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserEvent.UserId)).AsInt32()
                .WithColumn(nameof(UserEvent.EventId)).AsInt32();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }

    }
}
