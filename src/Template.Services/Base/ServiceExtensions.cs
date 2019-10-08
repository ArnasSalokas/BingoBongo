using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Common.Configuration;
using Template.Repositories.Base;
using Template.Repositories.Base.Contracts;
using Template.Repositories.Repositories;
using Template.Repositories.Repositories.Contracts;
using Template.Services.Exporting;
using Template.Services.Exporting.Contracts;
using Template.Services.Services;
using Template.Services.Services.Contracts;

namespace Template.Services.Base
{
    public static class ServiceExtensions
    {
        // Services;
        public static ISessionTokenService GetSessionTokenService(this IServiceProvider services) => services.GetRequiredService<ISessionTokenService>();
        public static IRngService GetRngService(this IServiceProvider services) => services.GetRequiredService<IRngService>();
        public static IPasswordService GetPasswordService(this IServiceProvider services) => services.GetRequiredService<IPasswordService>();

        public static IExcelExportService GetExcelExportService(this IServiceProvider services) => services.GetRequiredService<IExcelExportService>();

        // Repositories
        public static IUserRepository GetUserRepository(this IServiceProvider services) => services.GetRequiredService<IUserRepository>();
        public static ISessionTokenRepository GetSessionTokenRepository(this IServiceProvider services) => services.GetRequiredService<ISessionTokenRepository>();
        public static IUserClaimRepository GetUserClaimRepository(this IServiceProvider services) => services.GetRequiredService<IUserClaimRepository>();
        public static IRequestLogRepository GetRequestLogRepository(this IServiceProvider services) => services.GetRequiredService<IRequestLogRepository>();
        // Other
        public static ConfigurationSettings GetConfig(this IServiceProvider services) => services.GetRequiredService<ConfigurationSettings>();
        public static RNGCryptoServiceProvider GetRngCrypto(this IServiceProvider service) => service.GetRequiredService<RNGCryptoServiceProvider>();
        public static ITransactionStore GetTransactionStore(this IServiceProvider services) => services.GetRequiredService<ITransactionStore>();
        public static IRandomGeneratorService GetRandomGeneratorService(this IServiceProvider services) => services.GetRequiredService<IRandomGeneratorService>();

        // Called on API startup. DI configuration.
        public static void ConfigureServices(IServiceCollection services, IConfigurationRoot config)
        {
            // Services
            services.AddScoped<ISessionTokenService, SessionTokenService>();
            services.AddScoped<IRngService, RngService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IExcelExportService, ExcelExportService>();

            // Repositories
            services.AddTransient<IDataStore, DataStore>();

            services.AddScoped<ITransactionStore, TransactionStore>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionTokenRepository, SessionTokenRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();

            // Other
            services.AddScoped<IRandomGeneratorService, RandomGeneratorService>();
            services.AddSingleton<RNGCryptoServiceProvider>();
        }
    }
}
