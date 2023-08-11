using AHM.Logistic.Smart.Common.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api.Examples
{
    public class RolRegisterExample : IExamplesProvider<RolesModel>
    {
        public RolesModel GetExamples()
        {
            return new RolesModel()
            {
                rol_Description = "Administrador",
            };
        }
    }
}
