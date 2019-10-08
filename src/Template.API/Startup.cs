using System;
using System.Data.SqlClient;
using FluentMigrator.Runner;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Template.API.Filters;
using Template.API.Helpers;
using Template.API.Middleware;
using Template.Common.Configuration;
using Template.Common.Helpers.Microsoft.Extensions.Configuration;
using Template.Entities.Migrations;
using Template.Services.Base;
using Template.Services.Helpers;

namespace Template.API
{
    /// <summary>
    /// API startup
    /// </summary>
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env) => _env = env;

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Add config file as singleton
            services.AddScoped(v => new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build());

            var config = services.BuildServiceProvider().CreateScope().ServiceProvider.GetService<IConfigurationRoot>();

            services.Configure<ConfigurationSettings>(config);
            services.AddTransient(s => s.GetService<IOptions<ConfigurationSettings>>().Value);
            // Repos and services
            ServiceExtensions.ConfigureServices(services, config);
            services.AddApplicationInsightsTelemetry(config);
            services.AddMemoryCache();

            services.AddCors();

            var mvc = services.AddMvc(v =>
            {
                v.Filters.Add<SessionTokenAuthenticateFilter>();
                v.Filters.Add<SessionTokenAuthorizeFilter>();
            });

            mvc.AddJsonOptions(
                opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            // SQL
            services.AddScoped(v =>
            {
                return new SqlConnection(config.GetDefaultConnection());
            });

            // Fluent migrator   
            services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer2012()
                    .WithGlobalConnectionString(config.GetDefaultConnection())
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(M201901231233_FirstMigration).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

            // Migrate
            var servicesMigrationScope = services.BuildServiceProvider().CreateScope().ServiceProvider;
            UpdateDatabase(servicesMigrationScope);

            // Hangfire
            GlobalConfiguration.Configuration.UseActivator(new HangfireJobActivator(services));
            GlobalConfiguration.Configuration.UseSqlServerStorage(config.GetDefaultConnection());
            services.AddHangfire(x => x.UseStorage(JobStorage.Current));

            services.AddSwaggerDocument();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            var config = app.ApplicationServices.CreateScope().ServiceProvider.GetConfig();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseMiddleware<ApiExceptionMiddleware>();

            app.UseCors(options => options.WithOrigins(config.Cors.Origins).AllowAnyMethod().AllowAnyHeader());

            app.UseMvc();

            app.UseHangfireServer();
            app.UseHangfireDashboard(
                "/hangfire",
                new DashboardOptions
                {
                    Authorization = new[] { new HangfireDashboardFilter() }
                });


            app.UseSwagger(SwaggerHelper.SwaggerConfigure());
            app.UseSwaggerUi3(SwaggerHelper.SwaggerUi3Configure());
            app.UseReDoc(SwaggerHelper.ReDocConfigure());
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
