using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Template.API.Attributes;
using Template.Common.Exceptions;
using Template.Common.Helpers;
using Template.Entities.Database.Models;
using Template.Entities.Enums;
using Template.Entities.Identity;
using Template.Services.Base;

namespace Template.API.Filters
{
    /// <summary>
    /// Filter for token authorization
    /// </summary>
    public class SessionTokenAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _services;
        private FilterContext _context;
        private List<SessionTokenAuthorizeAttribute> AuthAttributes;
        private List<SessionTokenAuthorizeAttribute> ControllerAttributes;
        private List<SessionTokenAuthorizeAttribute> MethodAttributes;
        private bool MethodHasAllowAnonymous { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="services"></param>
        /// <param name="loggerFactory"></param>
        public SessionTokenAuthorizeFilter(IServiceProvider services, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _services = services;
            AuthAttributes = new List<SessionTokenAuthorizeAttribute>();
            ControllerAttributes = new List<SessionTokenAuthorizeAttribute>();
            MethodAttributes = new List<SessionTokenAuthorizeAttribute>();
        }

        /// <summary>
        /// Main authorization method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
#pragma warning disable 1998
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            _context = context;

            GetAttributes();

            if (DoNeedAuthorize())
                Authorize();
        }
#pragma warning restore 1998

        private bool DoNeedAuthorize()
        {
            var method = ((ControllerActionDescriptor)_context.ActionDescriptor).MethodInfo;

            // Anonymous overrides controller attributes
            return MethodHasAllowAnonymous ? false : AuthAttributes.Any();
        }

        /// <summary>
        /// Checks:
        /// if IIdentity is SessionTokenIdentity
        /// if SessionToken didn't expire
        /// if user has claims mentioned in SessionTokenAuthorizeAttribute attributes
        /// if SessionToken user type is correct
        /// </summary>
        private void Authorize()
        {
            var passScheme = _context.HttpContext.User.Identity.IsAuthenticated && _context.HttpContext.User.Identity is SessionTokenIdentity;
            if (!passScheme)
                throw new MpException(MpExceptionCode.General.Unauthorized);

            var identity = (SessionTokenIdentity)_context.HttpContext.User.Identity;
            if (!identity.IsValid)
                throw new MpException(MpExceptionCode.General.Unauthorized);

            var validIdentityUserClaims = identity.UserClaims.Where(i => !string.IsNullOrWhiteSpace(i.Value) && !ConversionHelper.InterpretFalseBool(i.Value));

            var requiredControllerClaims = ControllerAttributes.Where(v => v.RequiredClaims != null).SelectMany(v => v.RequiredClaims);

            CheckAttributes(requiredControllerClaims, validIdentityUserClaims);

            var requiredMethodClaims = MethodAttributes.Where(v => v.RequiredClaims != null).SelectMany(v => v.RequiredClaims);

            CheckAttributes(requiredMethodClaims, validIdentityUserClaims);
        }

        private void CheckAttributes(IEnumerable<AdminClaimType> claimTypes, IEnumerable<UserClaim> validIdentityUserClaims)
        {
            if (!claimTypes.Any())
                return;

            var hasCommonClaim = claimTypes.Intersect(validIdentityUserClaims.Select(v => v.ClaimType)).Any();
            if (!hasCommonClaim)
                throw new MpException(MpExceptionCode.General.Unauthorized);
        }

        private void CheckUserType(SessionTokenIdentity identity)
        {
            var requiredUserTypes = AuthAttributes.Select(v => v.TokenType).ToHashSet();

            foreach (var tokenType in requiredUserTypes)
            {
                if (_services.GetSessionTokenService().ParseUserType(identity.SessionToken.Token) != tokenType)
                    throw new MpException(MpExceptionCode.General.Unauthorized);
            }
        }

        /// <summary>
        /// Gets all SessionTokenAuthorizeAttribute attributes from controller and method
        /// </summary>
        private void GetAttributes()
        {
            var actionDescriptor = (ControllerActionDescriptor)_context.ActionDescriptor;

            var controller = actionDescriptor.ControllerTypeInfo;
            var controllerAttributes = controller.GetCustomAttributes(typeof(SessionTokenAuthorizeAttribute), true);

            var method = actionDescriptor.MethodInfo;
            var methodAttributes = method.GetCustomAttributes(typeof(SessionTokenAuthorizeAttribute), true);

            MethodHasAllowAnonymous = method.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Count() > 0;

            foreach (var atr in controllerAttributes)
            {
                AuthAttributes.Add((SessionTokenAuthorizeAttribute)atr);
                ControllerAttributes.Add((SessionTokenAuthorizeAttribute)atr);
            }

            foreach (var atr in methodAttributes)
            {
                AuthAttributes.Add((SessionTokenAuthorizeAttribute)atr);
                MethodAttributes.Add((SessionTokenAuthorizeAttribute)atr);
            }
        }
    }
}
