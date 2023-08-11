
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.DataAccess.Repositories;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api.Examples
{
    public class UserRegisterExample : IExamplesProvider<UsersModel>
    {
        public UsersModel GetExamples()
        {
            return new UsersModel()
            {
                //User_Name = "Pedro13",
                //Password = "clave123",
                //Status = true,
                //Role = 1,
                //Person = 1,

            };
        }
    }
}
