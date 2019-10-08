using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Template.Entities.Database.Models;
using Template.Entities.Identity;
using Template.Services.Base;
using Template.Services.Services.Contracts;

namespace Template.API.Filters
{
    /// <summary>
    /// Authentication filter
    /// </summary>
    public class SessionTokenAuthenticateFilter : IAsyncAuthorizationFilter
    {
        private class TokenModel
        {
            public string Scheme { get; set; }
            public string Token { get; set; }
            public SessionToken DbEntity { get; set; }
        }

        private readonly IServiceProvider _services;
        private readonly ISessionTokenService _sessionTokenService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="services"></param>
        public SessionTokenAuthenticateFilter(IServiceProvider services)
        {
            _services = services;
            _sessionTokenService = _services.GetSessionTokenService();
        }

        /// <summary>
        /// Default method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            context.HttpContext.User = await GetUserByContext(context);
            await ExtendIfNeedTo(context);
        }

        private async Task<ClaimsPrincipal> GetUserByContext(AuthorizationFilterContext context)
        {
            var token = RetreiveTokenFromRequest(context.HttpContext.Request);

            if (token == null)
                return null;

            token.DbEntity = await GetSessionTokenFromDb(token);

            if (token.DbEntity == null)
                return null;

            var claims = await _services.GetUserClaimRepository().Get(_services.GetSessionTokenService().ParseUserId(token.Token));

            var identity = new SessionTokenIdentity(token.DbEntity, claims);

            return new ClaimsPrincipal(identity);
        }

        private TokenModel RetreiveTokenFromRequest(HttpRequest request)
        {
            var headers = request.Headers;
            var query = request.Query;

            // Try extracting token
            headers.TryGetValue("Authorization", out StringValues authStrVals);
            if (authStrVals.Any())
                return ParseToken(authStrVals.ToString());

            // Try extracting token from URL request (in case it's public, f.e., for PDF/XLS export)
            query.TryGetValue("Authorization", out authStrVals);
            if (authStrVals.Any())
                return ParseToken(authStrVals.ToString());

            return null;
        }

        private TokenModel ParseToken(string paramValue)
        {
            var stringArray = paramValue.Split(' ');
            return new TokenModel
            {
                Scheme = stringArray.FirstOrDefault(),
                Token = stringArray.LastOrDefault(),
            };
        }

        private async Task<SessionToken> GetSessionTokenFromDb(TokenModel token)
        {
            if (token.Scheme.ToLower() != SessionTokenIdentity.SESSION_TOKEN_AUTH_SCHEME.ToLower())
                return null;

            if (!_services.GetSessionTokenService().IsValidFormat(token.Token))
                return null;

            var userId = _services.GetSessionTokenService().ParseUserId(token.Token);
            var userType = _services.GetSessionTokenService().ParseUserType(token.Token);

            return await _services.GetSessionTokenRepository().Get(userId, token.Token, userType);
        }

        private async Task ExtendIfNeedTo(AuthorizationFilterContext context)
        {
            var passScheme = context.HttpContext.User.Identity.IsAuthenticated && context.HttpContext.User.Identity is SessionTokenIdentity;
            if (!passScheme)
                return;

            var identity = (SessionTokenIdentity)context.HttpContext.User.Identity;
            if (!identity.IsValid)
                return;

            var token = identity.SessionToken;

            var config = _services.GetConfig();
            var lifeTimeMinutesConfig = config.SessionToken.LifetimeMinutes;
            uint minutesLeftToExtend = (uint)(lifeTimeMinutesConfig / 100m * 90m);
            var minutesLeftOfToken = (uint)(token.Expires - DateTime.UtcNow).TotalMinutes;
            if (minutesLeftOfToken < minutesLeftToExtend)
            {
                token.Expires = DateTime.UtcNow.AddMinutes(lifeTimeMinutesConfig);
                await _services.GetSessionTokenRepository().Update(token);
            }
        }
    }
}
