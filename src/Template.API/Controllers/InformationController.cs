using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Template.API.Controllers.Base;

namespace Template.API.Controllers
{
    /// <summary>
    /// Controller for general information (allows anonymous requests)
    /// </summary>
    [Route("api/[controller]")]
    public class InformationController : BaseWebController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cache"></param>
        public InformationController(IServiceProvider services, IMemoryCache cache) : base(services, cache)
        {
        }

        /// <summary>
        /// This is here so Application Insights don't go crazy with 404 errors.
        /// </summary>
        [HttpGet("/")]
        public IActionResult Get() => Ok();
    }
}
