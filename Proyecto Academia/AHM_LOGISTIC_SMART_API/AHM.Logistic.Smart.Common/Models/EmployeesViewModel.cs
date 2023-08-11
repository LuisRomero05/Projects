using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class EmployeesViewModel
    {
        public int emp_Id { get; set; }
        public int per_Id { get; set; }
        public string per_Firstname { get; set; }
        public string per_LastNames { get; set; }
        public int are_Id { get; set; }
        public string are_Description { get; set; }
        public int occ_Id { get; set; }
        public string occ_Description { get; set; }
        public string Status { get; set; }
        public int? emp_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime? emp_DateCreate { get; set; }
        public int? emp_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime? emp_DateModified { get; set; }
    }
}
