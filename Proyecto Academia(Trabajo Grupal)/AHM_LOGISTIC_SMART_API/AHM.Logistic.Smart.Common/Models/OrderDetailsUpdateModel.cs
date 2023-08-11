using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class OrderDetailsUpdateModel
    {
        public int ode_Amount { get; set; }
        public int pro_Id { get; set; }
        public int sor_Id { get; set; }
        public decimal ode_TotalPrice { get; set; }
        public int ode_IdUserCreate { get; set; }
        public int ode_IdUserModified { get; set; }
    }
}
