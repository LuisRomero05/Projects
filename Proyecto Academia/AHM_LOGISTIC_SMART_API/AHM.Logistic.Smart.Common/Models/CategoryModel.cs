using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{

    public class CategoryModel
    {

        public int cat_Id { get; set; }
        public string cat_Description { get; set; }
        public int? cat_IdUserCreate { get; set; }
        public int? cat_IdUserModified { get; set; }

     
    }
}
