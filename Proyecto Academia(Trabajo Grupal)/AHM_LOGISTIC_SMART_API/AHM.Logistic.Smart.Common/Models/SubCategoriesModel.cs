using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    
    public class SubCategoriesModel
    {
        public int scat_Id { get; set; }
        public string scat_Description { get; set; }
        public int cat_Id { get; set; }
        public int? scat_IdUserCreate { get; set; }
        public int? scat_IdUserModified { get; set; }


    }
}
