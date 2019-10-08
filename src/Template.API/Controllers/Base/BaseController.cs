using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Template.Common.Configuration;
using Template.Entities.Database.Models;
using Template.Entities.Enums;
using Template.Entities.Identity;

namespace Template.API.Controllers.Base
{
    /// <summary>
    /// Base controller
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Services
        /// </summary>
        protected readonly IServiceProvider _services;

        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Environment
        /// </summary>
        protected readonly IHostingEnvironment _env;

        /// <summary>
        /// Configuration
        /// </summary>
        protected readonly ConfigurationSettings _config;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cache"></param>
        public BaseController(IServiceProvider services, IMemoryCache cache)
        {
            _services = services;
            _logger = _services.GetRequiredService<ILoggerFactory>().CreateLogger(this.GetType());
            _env = _services.GetRequiredService<IHostingEnvironment>();
            _config = services.GetService<ConfigurationSettings>();
        }

        /// <summary>
        /// Return current user session token
        /// </summary>
        [NonAction]
        public SessionToken GetCurrentUserSessionToken() => GetCurrentUserSessionTokenIdentity()?.SessionToken;

        /// <summary>
        /// Get Identity. Used all over the controller.
        /// </summary>
        [NonAction]
        public SessionTokenIdentity GetCurrentUserSessionTokenIdentity()
        {
            var identity = HttpContext?.User?.Identity;

            if (identity == null)
                return null;

            if (!(identity is SessionTokenIdentity))
                return null;

            var sesionTokenIdentity = (SessionTokenIdentity)identity;

            return sesionTokenIdentity;
        }

        /// <summary>
        /// Gets user type
        /// </summary>
        [NonAction]
        public AdminClaimType? GetUserType()
        {
            var identity = GetCurrentUserSessionTokenIdentity();
            var token = identity?.SessionToken;

            // Should have only one custom claim type
            return identity.UserClaims.Select(v => v.ClaimType).FirstOrDefault();
        }

        /// <summary>
        /// Async copies a byte stream from the Request body into a byte array
        /// </summary>
        [NonAction]
        public async Task<byte[]> StreamToArray(Stream body)
        {
            var destination = new MemoryStream();
            byte[] buffer = new byte[2048];

            await body.CopyToAsync(destination);

            using (destination)
                return destination.ToArray();
        }
    }
}
