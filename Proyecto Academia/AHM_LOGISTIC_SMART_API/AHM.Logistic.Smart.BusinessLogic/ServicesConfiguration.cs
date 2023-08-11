using AHM.Logistic.Smart.BusinessLogic.Logger;
using AHM.Logistic.Smart.BusinessLogic.Services;
using AHM.Logistic.Smart.BusinessLogic.TestService;
using AHM.Logistic.Smart.DataAccess;
using AHM.Logistic.Smart.DataAccess.Repositories;
using AHM.Logistic.Smart.DataAccess.TestRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.BusinessLogic
{
    public static class ServicesConfiguration
    {
        public static void DataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<UnitsRepository>();
            services.AddScoped<EmployeesRepository>();
            services.AddScoped<ContactsRepository>();
            services.AddScoped<SubCategoriesRepository>();
            services.AddScoped<CustomersRepository>();
            services.AddScoped<UsersRepository>();
            services.AddScoped<RolesRepository>();
            services.AddScoped<CategoriesRepository>();
            services.AddScoped<ProductsRepository>();
            services.AddScoped<DepartmentsRepository>();
            services.AddScoped<CotizationsRepository>();
            services.AddScoped<MunicipalitiesRepository>();
            services.AddScoped<PersonsRepository>();
            services.AddScoped<AreasRepository>();
            services.AddScoped<OccupationsRepository>();
            services.AddScoped<OrdersRepository>();
            services.AddScoped<CountryRepository>();
            services.AddScoped<SecurityRepository>();
            services.AddScoped<CustomerCallsRepository>();
            services.AddScoped<CustomersNotesRepository>();
            services.AddScoped<CampaignRepository>();
            services.AddScoped<PrioritiesRepository>();
            services.AddScoped<DashboardRepository>();
            services.AddScoped<CallBusinessRepository>();
            services.AddScoped<CallResultRepository>();
            services.AddScoped<CallTypeRepository>();
            services.AddScoped<MeetingsRepository>();
            services.AddScoped<CustomersFileRepository>();
            LogisticSmartContext.BuildConnectionString(connectionString);
            services.AddScoped<AreasRepositoryTest>();
        }

        public static void BusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<EventLogger>();
            services.AddScoped<CatalogService>();
            services.AddScoped<AccessService>();
            services.AddScoped<ClientsService>();
            services.AddScoped<SalesService>();
            services.AddScoped<SecurityService>();
            services.AddScoped<AreaService>();
        }
    }
}
