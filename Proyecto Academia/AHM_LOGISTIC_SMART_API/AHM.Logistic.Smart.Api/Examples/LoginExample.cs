using AHM.Logistic.Smart.Common.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api.Examples
{
    public class LoginExample : IExamplesProvider<LoginModel>
    {
        public LoginModel GetExamples()
        {
            return new LoginModel()
            {
                usu_UserName = "Pedro13",
                usu_Password = "clave123"
            };
        }
    }
}
