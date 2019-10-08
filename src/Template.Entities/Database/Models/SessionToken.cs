using System;
using System.ComponentModel.DataAnnotations;
using Template.Entities.Database.Base;
using Template.Entities.Enums;

namespace Template.Entities.Database.Models
{
    public class SessionToken : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Token { get; set; }

        // For mobile apps
        public string PhoneNumber { get; set; }

        public DateTime Expires { get; set; }
        public DateTime Started { get; set; }

        public SessionTokenType TokenType { get; set; }

        public string NotificationToken { get; set; }

        public int? UserId { get; set; }
        public int? ClientId { get; set; }

        public SessionToken() { }

        public static SessionToken FormAppToken(int clientId, string phoneNumber, int lifetimeMinutes)
        {
            return new SessionToken
            {
                Started = DateTime.UtcNow,
                TokenType = SessionTokenType.App,
                Expires = DateTime.UtcNow.AddMinutes(lifetimeMinutes),
                ClientId = clientId,
                PhoneNumber = phoneNumber
            };
        }

        public static SessionToken FormAdminToken(int userId, int lifetimeMinutes)
        {
            return new SessionToken
            {
                Started = DateTime.UtcNow,
                TokenType = SessionTokenType.Admin,
                Expires = DateTime.UtcNow.AddMinutes(lifetimeMinutes),
                UserId = userId
            };
        }
    }
}
