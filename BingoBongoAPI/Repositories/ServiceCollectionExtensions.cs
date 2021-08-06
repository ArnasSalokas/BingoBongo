 using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories
{
    public static class ServiceCollectionExtensions
    {
        public static string GetDefaultConnection(this IConfiguration config) =>
            config.GetConnectionString("DefaultConnection");
    }
}
