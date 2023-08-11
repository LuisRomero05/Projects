using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    
    public class DepartmentsModel
    {
        public int dep_Id { get; set; }
        public string dep_Code { get; set; }
        public string dep_Description { get; set; }
        public int cou_Id { get; set; }
        public int? dep_IdUserCreate { get; set; }
        public int? dep_IdUserModified { get; set; }
     

    }
}
