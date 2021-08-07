using BingoBongoAPI.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FluentMigrator.Runner;
using Microsoft.Data;
using BingoBongoAPI.Repositories;
using BingoBongoAPI.Repositories.Contracts;
using BingoBongoAPI.Services.Contracts;
using BingoBongoAPI.Services;
using BingoBongoAPI.Repositories.Migrations;
using System;

namespace BingoBongoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            AddRepositories(services);
            AddServices(services);
            AddDbContext(services);
            AddCorsPolicy(services);
            AddFluentMigrator(services, Configuration);
            //AddHttpClient(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region Private Methods

        private void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ISlackService, SlackService>();
        }

        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<BingoBongoContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("BingoBongo");
                options.UseSqlServer(connectionString);
            });
        }

        private void AddCorsPolicy(IServiceCollection services)
        {
            // For simplicity no CORS policy was implemented
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        //private void AddHttpClient(IServiceCollection services)
        //{
        //    services.AddHttpClient("slackClient", c =>
        //    {
        //        c.BaseAddress = new Uri("https://slack.com/api");
        //    });
        //}

        private void AddFluentMigrator(IServiceCollection services, IConfiguration config)
        {
            // Fluent migrator
            services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer2014()     
                    .WithGlobalConnectionString(config.GetDefaultConnection())
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(M20210708_FirstMigration).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        #endregion
    }
}
