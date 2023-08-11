using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
   public  class CotizationsDetailUpdateModel
    {
        public int code_Cantidad { get; set; }
        public int pro_Id { get; set; }
        public decimal code_TotalPrice { get; set; }
        public int cot_Id { get; set; }
        public int code_IdUserCreate { get; set; }
        public int code_IdUserModified { get; set; }
    }
}
