using Microsoft.Extensions.DependencyInjection;
using Proyecto.BusinessLogic.Services;
using Proyecto.DataAccess;
using Proyecto.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BusinessLogic
{
    public static class ServiceConfiguration
    {
        public static void DataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<EmpleadoHospitalRepository>();
            services.AddScoped<EmpleadoSalaRepository>();
            services.AddScoped<ReporteGeneralRepository>();
            services.AddScoped<PlantillaRepository>();
            services.AddScoped<HospitalRepositories>();
            services.AddScoped<UsuariosRepository>();
            services.AddScoped<RolesRepositories>();
            services.AddScoped<SalasRepository>();
            services.AddScoped<EnfermoRepository>();
            CentrosMedicosContext.BuildConnectionString(connectionString);
        }

        //Resuelve la inyeccion de dependencia de los servicios
        public static void BusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<CatalogService>();
            services.AddScoped<UsuariosService>();
        }
    }
}
