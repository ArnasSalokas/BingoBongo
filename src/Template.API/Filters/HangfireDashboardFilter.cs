using Hangfire.Dashboard;

namespace Template.API.Filters
{
    /// <summary>
    /// Hangfire authorization filter
    /// </summary>
    public class HangfireDashboardFilter : IDashboardAuthorizationFilter
    {
        /// <summary>
        /// Filter authorize
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool Authorize(DashboardContext context)
        {
            return context.Request.RemoteIpAddress == "::1" || context.Request.RemoteIpAddress == "88.119.97.225";
        }
    }
}
