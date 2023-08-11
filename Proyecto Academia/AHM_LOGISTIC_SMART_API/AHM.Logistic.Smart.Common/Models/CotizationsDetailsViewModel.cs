using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
   public class CotizationsDetailsViewModel
    {
        public int cot_Id { get; set; }
        public int cus_Id { get; set; }
        public DateTime cot_DateValidUntil { get; set; }
        public int sta_Id { get; set; }
        public bool cot_Status { get; set; }
        public int cot_IdUserCreate { get; set; }
        public DateTime cot_DateCreate { get; set; }
        public int? cot_IdUserModified { get; set; }
        public DateTime? cot_DateModified { get; set; }
        public int code_Id { get; set; }
        public int code_Cantidad { get; set; }
        public int pro_Id { get; set; }
        public decimal code_TotalPrice { get; set; }
        public bool code_Status { get; set; }
        public int code_IdUserCreate { get; set; }
        public DateTime code_DateCreate { get; set; }
        public int? code_IdUserModified { get; set; }
        public DateTime? code_DateModified { get; set; }
    }
}
