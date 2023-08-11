using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class MeetingsDetailsViewModel
    {
        public int mde_Id { get; set; }
        public int met_Id { get; set; }
        public int? cus_Id { get; set; }
        public int? emp_Id { get; set; }
        public int? cont_Id { get; set; }
        public bool mde_Status { get; set; }
        public int mde_IdUserCreate { get; set; }
        public DateTime mde_DateCreate { get; set; }
        public int? mde_IdUserModified { get; set; }
        public DateTime? mde_DateModified { get; set; }
    }
}
