using System;
using Microsoft.Extensions.Configuration;

namespace Template.Common.Helpers
{
    // Wrong namespace for convenience
    namespace Microsoft.Extensions.Configuration
    {
        public static class ConfigurationHelper
        {
            private const string DEFAULT_CONNECTION = "DefaultConnection";

            public static string GetDefaultConnection(this IConfigurationRoot config) => config.GetConnectionString(DEFAULT_CONNECTION);

            /// <summary>
            /// Separator is :
            /// </summary>
            public static T Get<T>(this IConfigurationRoot config, string key) where T : IConvertible => ConversionHelper.ConvertTo<T>(config[key]);

            public static T Get<T>(this IConfigurationRoot config, params string[] keys) where T : IConvertible => Get<T>(config, string.Join(':', keys));
        }
    }
}
