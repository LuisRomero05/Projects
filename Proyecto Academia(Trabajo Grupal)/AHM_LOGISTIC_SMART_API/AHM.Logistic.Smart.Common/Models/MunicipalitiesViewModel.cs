using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class MunicipalitiesViewModel
    {
        public int mun_Id { get; set; }
        public string mun_Code { get; set; }
        public string mun_Description { get; set; }
        public int dep_Id { get; set; }
        public string dep_Description { get; set; }
        public string Status { get; set; }
        public int? mun_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime? mun_DateCreate { get; set; }
        public int? mun_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime? mun_DateModified { get; set; }
    }
}
