using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
   public class DepartmentsViewModel
    {
        public int dep_Id { get; set; }
        public string dep_Code { get; set; }
        public string dep_Description { get; set; }
        public int cou_Id { get; set; }
        public string cou_Description { get; set; }
        public string Status { get; set; }
        public int? dep_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime? dep_DateCreate { get; set; }
        public int? dep_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime? dep_DateModified { get; set; }
    }
}
