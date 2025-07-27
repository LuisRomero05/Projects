using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<HospitalRepositories>();
            services.AddScoped<UsuariosRepository>();
            services.AddScoped<RolesRepository>();
            CentrosMedicosContext.BuildConnectionString(connectionString);
        }

        //Resuelve la inyeccion de dependencia de los servicios
        public static void BusinessLogic(this IServiceCollection services)
        {
<<<<<<< Updated upstream
            //services.AddScoped<CatalogsService>();
=======
            services.AddScoped<UsuariosService>();
>>>>>>> Stashed changes
        }
    }
}
