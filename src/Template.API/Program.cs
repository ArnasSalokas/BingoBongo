using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Template.API
{
    /// <summary>
    /// Main entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Property that holds DateTime application launched
        /// </summary>
        public static DateTime Launched { get; private set; }

        /// <summary>
        /// Entry point method
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Launched = DateTime.UtcNow;
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Web builder method
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
