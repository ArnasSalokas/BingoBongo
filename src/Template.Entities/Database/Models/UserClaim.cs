using System;
using System.ComponentModel.DataAnnotations;
using Template.Entities.Database.Base;
using Template.Entities.Enums;

namespace Template.Entities.Database.Models
{
    public class UserClaim : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int? AddedBy { get; set; }
        public DateTime? AddedDate { get; set; }

        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int UserId { get; set; }

        public string Value { get; set; }

        public AdminClaimType ClaimType { get; set; }

        public UserClaim() { }

        public UserClaim(int userId, AdminClaimType claimType, string value)
        {
            UserId = userId;
            Value = value;
            ClaimType = claimType;
        }

        public static UserClaim NewActiveClaim(int userId, AdminClaimType type)
        {
            return new UserClaim
            {
                UserId = userId,
                Value = bool.TrueString,
                ClaimType = type
            };
        }
    }
}
