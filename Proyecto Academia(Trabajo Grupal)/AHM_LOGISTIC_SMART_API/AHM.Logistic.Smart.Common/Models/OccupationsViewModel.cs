using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class OccupationsViewModel
    {
        public int occ_Id { get; set; }
        public string occ_Description { get; set; }
        public string Status { get; set; }
        public int? occ_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime? occ_DateCreate { get; set; }
        public int? occ_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime? occ_DateModified { get; set; }
    }
}
