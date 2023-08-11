using AHM.Logistic.Smart.Common.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api.Examples
{
    public class CategoryExample : IExamplesProvider<CategoryModel>
    {
        public CategoryModel GetExamples()
        {
            return new CategoryModel()
            {
                cat_Description = "Categoria",
            };
        }
    }
}
