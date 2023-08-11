using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class UnitsViewModel
    {
        public int uni_Id { get; set; }
        public string uni_Description { get; set; }
        public string uni_Abrevitation { get; set; }
        public string Status { get; set; }
        public int uni_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime uni_DateCreate { get; set; }
        public int uni_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime uni_DateModified { get; set; }
    }
}
