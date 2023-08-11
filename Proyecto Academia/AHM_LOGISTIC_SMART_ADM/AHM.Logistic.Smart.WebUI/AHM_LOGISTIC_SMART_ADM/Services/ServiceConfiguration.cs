using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Services
{
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Agrega las dependencias de la capa logica de negocios al contenedor DI
        /// </summary>
        /// <param name="services">El DI container de .NET Core</param>
        public static void AddBusinessLogicWebUI(this IServiceCollection services)
        {
            services.AddScoped<CatalogService>();
            services.AddScoped<SalesService>();
            services.AddScoped<AccessService>();
            services.AddScoped<CustomersService>();
        }
    }
}
