using System;
using System.ComponentModel.DataAnnotations;
using Template.Entities.Database.Base;

namespace Template.Entities.Database.Models
{
    public class User : IEntity, IAdded, IModified
    {
        [Key]
        public int Id { get; set; }

        public int? AddedBy { get; set; }
        public DateTime? AddedDate { get; set; }

        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordResetToken { get; set; }

        public DateTime? PasswordResetTokenExpiration { get; set; }

        public bool? EmailConfirmed { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
