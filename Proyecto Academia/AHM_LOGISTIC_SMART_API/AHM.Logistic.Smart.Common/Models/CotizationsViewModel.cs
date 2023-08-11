using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
   public class CotizationsViewModel
    {
        public int cot_Id { get; set; }
        public int cus_Id { get; set; }
        public string cus_Name { get; set; }
        public string cus_Email { get; set; }
        public string cus_Phone { get; set; }
        public string cus_RTN { get; set; }
        public int mun_Id { get; set; }
        public string mun_Description { get; set; }
        public DateTime cot_DateValidUntil { get; set; }
        public int sta_Id { get; set; }
        public string sta_Description { get; set; }
        public string Status { get; set; }
        public int? cot_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime? cot_DateCreate { get; set; }
        public int? cot_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime? cot_DateModified { get; set; }
    }
}
