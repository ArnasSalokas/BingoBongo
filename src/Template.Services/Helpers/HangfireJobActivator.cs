using System;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Services.Helpers
{
    /// <summary>
    /// Hangfire job main activator
    /// </summary>
    public class HangfireJobActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="services"></param>
        public HangfireJobActivator(IServiceCollection services) => _serviceProvider = services.BuildServiceProvider();

        /// <summary>
        /// Job activate
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override object ActivateJob(Type type) => _serviceProvider.GetRequiredService(type);
    }
}
