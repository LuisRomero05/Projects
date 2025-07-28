using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Template.BusinessLogic.Services;
using Template.DataAccess;
using Template.DataAccess.Context;
using Template.DataAccess.Repositories;

namespace Template.BusinessLogic
{
    public static class ServicesConfiguration
    {
        public static void DataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<AreasRepository>();

            TempladeeContext.BuildConnectionString(connectionString);
        }

        public static void BusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<CatalogService>();
  
        }

    }
}
