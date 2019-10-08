using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Template.Common.Configuration;

namespace Template.Services.Base
{
    public abstract class BaseService
    {
        protected readonly IServiceProvider _services;
        protected readonly ILogger _logger;
        protected readonly ConfigurationSettings _config;

        public BaseService(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
            _logger = _services.GetService<ILoggerFactory>().CreateLogger(this.GetType());
            _config = serviceProvider.GetService<ConfigurationSettings>();
        }

        public BaseService()
        {

        }
    }
}
