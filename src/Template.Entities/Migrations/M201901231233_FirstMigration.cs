using FluentMigrator;
using Template.Entities.Database.Models;
using Template.Entities.Migrations.Extensions;
using Template.Entities.Migrations.Helpers;

namespace Template.Entities.Migrations
{
    [Migration(201901231233)]
    public class M201901231233_FirstMigration : BaseMigration
    {
        public override void Up()
        {
            Create.Table(nameof(User))
                .WithIdColumn()
                .WithModifiedByColumn(nameof(User), nameof(User.Id))
                .WithModifiedDateColumn()
                .WithAddedByColumn(nameof(User), nameof(User.Id))
                .WithAddedDateColumn()
                .WithColumn(nameof(User.FirstName)).AsMaxString().Nullable()
                .WithColumn(nameof(User.LastName)).AsMaxString().Nullable()
                .WithColumn(nameof(User.PhoneNumber)).AsMaxString().NotNullable()
                .WithColumn(nameof(User.Email)).AsMaxString().Nullable()
                .WithColumn(nameof(User.PasswordResetToken)).AsMaxString().Nullable()
                .WithColumn(nameof(User.PasswordResetTokenExpiration)).AsDateTime().Nullable()
                .WithColumn(nameof(User.EmailConfirmed)).AsBoolean().Nullable()
                .WithColumn(nameof(User.Password)).AsMaxString().Nullable();

            Create.Table(nameof(UserClaim))
                .WithIdColumn()
                .WithModifiedByColumn(nameof(User), nameof(User.Id))
                .WithModifiedDateColumn()
                .WithAddedByColumn(nameof(User), nameof(User.Id))
                .WithAddedDateColumn()
                .WithColumn(nameof(UserClaim.UserId)).AsInt32().NotNullable().ForeignKey(nameof(User), nameof(User.Id))
                .WithColumn(nameof(UserClaim.Value)).AsMaxString().Nullable()
                .WithColumn(nameof(UserClaim.ClaimType)).AsInt32().NotNullable();

            Create.Table(nameof(SessionToken))
                .WithIdColumn()
                .WithColumn(nameof(SessionToken.Token)).AsMaxString().NotNullable()
                .WithColumn(nameof(SessionToken.Started)).AsDateTime().NotNullable()
                .WithColumn(nameof(SessionToken.Expires)).AsDateTime().NotNullable()
                .WithColumn(nameof(SessionToken.TokenType)).AsInt32().NotNullable()
                .WithColumn(nameof(SessionToken.NotificationToken)).AsMaxString().Nullable()
                .WithColumn(nameof(SessionToken.UserId)).AsInt32().NotNullable().ForeignKey(nameof(User), nameof(User.Id));

            Create.Table(nameof(RequestLog))
                .WithIdColumn()
                .WithColumn(nameof(RequestLog.RequestSentDate)).AsDateTime().NotNullable()
                .WithColumn(nameof(RequestLog.RequestUri)).AsMaxString().NotNullable()
                .WithColumn(nameof(RequestLog.Request)).AsMaxString().NotNullable()
                .WithColumn(nameof(RequestLog.ResponseReceivedDate)).AsDateTime().Nullable()
                .WithColumn(nameof(RequestLog.ResponseElapsedMilliseconds)).AsInt64().NotNullable()
                .WithColumn(nameof(RequestLog.Response)).AsMaxString().NotNullable()
                .WithColumn(nameof(RequestLog.ResponseUri)).AsMaxString().NotNullable()
                .WithColumn(nameof(RequestLog.ResponseStatus)).AsMaxString().NotNullable()
                .WithColumn(nameof(RequestLog.ResponseCode)).AsMaxString().NotNullable()
                .WithColumn(nameof(RequestLog.ErrorMessage)).AsMaxString().Nullable()
                .WithColumn(nameof(RequestLog.ErrorException)).AsMaxString().Nullable();
        }

        public override void Down()
        {
            // No need for down for now
        }
    }
}