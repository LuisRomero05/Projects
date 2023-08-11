using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class SalesOrderViewModel
    {
        public int sor_Id { get; set; }
        public int cus_Id { get; set; }
        public string cus_Name { get; set; }
        public string cus_Email { get; set; }
        public string cus_Phone { get; set; }
        public string cus_RTN { get; set; }
        public int mun_Id { get; set; }
        public string mun_Description { get; set; }
        public int? cot_Id { get; set; }
        public string Coti_Status { get; set; }
        public int sta_Id { get; set; }
        public string sta_Description { get; set; }
        public string Status { get; set; }
        public int? sor_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime? sor_DateCreate { get; set; }
        public int? sor_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime? sor_DateModified { get; set; }
    }
}
