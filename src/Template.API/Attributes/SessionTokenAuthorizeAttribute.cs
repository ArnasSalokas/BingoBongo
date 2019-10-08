using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Template.Entities.Enums;

namespace Template.API.Attributes
{
    /// <summary>
    /// Has header Authorization: SessionToken eyJ0eXAiOiJKV1 or in develop cookie[Authorization] = "SessionToken eyJ0eXAiOiJKV1"
    /// </summary>
    public class SessionTokenAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Required claims for authorization
        /// </summary>
        public readonly HashSet<AdminClaimType> RequiredClaims;

        /// <summary>
        /// Token user type
        /// </summary>
        public readonly SessionTokenType TokenType;

        /// <summary>
        /// Default and only constructor
        /// </summary>
        /// <param name="tokenType"></param>
        /// <param name="requiredPermissions"></param>
        public SessionTokenAuthorizeAttribute(SessionTokenType tokenType, params AdminClaimType[] requiredPermissions) : base()
        {
            RequiredClaims = requiredPermissions.ToHashSet();
            TokenType = tokenType;
        }
    }
}
