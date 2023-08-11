using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class EmployeesModel
    {
       public int emp_Id { get; set; }
        public int per_Id { get; set; }
        public int are_Id { get; set; }
        public int occ_Id { get; set; }
        public int? emp_IdUserCreate { get; set; }
        public int? emp_IdUserModified { get; set; }
    }
}
