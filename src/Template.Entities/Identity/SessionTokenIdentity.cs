using System;
using System.Collections.Generic;
using System.Security.Claims;
using Template.Entities.Database.Models;

namespace Template.Entities.Identity
{
    public class SessionTokenIdentity : ClaimsIdentity
    {
        public const string SESSION_TOKEN_AUTH_SCHEME = "Bearer";
        public override string AuthenticationType => SESSION_TOKEN_AUTH_SCHEME;
        public override bool IsAuthenticated { get; }
        public override string Name { get; }
        public SessionToken SessionToken { get; set; }
        public IEnumerable<UserClaim> UserClaims { get; set; }
        /// <summary>
        /// Did not expire yet
        /// </summary>
        public bool IsValid => SessionToken != null && DateTime.UtcNow <= SessionToken.Expires;

        public SessionTokenIdentity(SessionToken ent, IEnumerable<UserClaim> claims) : base()
        {
            SessionToken = ent;
            IsAuthenticated = ent != null;
            Name = null;
            UserClaims = claims;

            AddStandardClaims(claims);
        }

        private void AddStandardClaims(IEnumerable<UserClaim> claims)
        {
            if (claims != null)
            {
                foreach (var claim in claims)
                {
                    AddClaim(new Claim(claim.ClaimType.ToString(), claim.Value));
                }
            }
        }
    }
}
