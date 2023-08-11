using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class OccupationsModel
    {
        public int occ_Id { get; set; }
        public string occ_Description { get; set; }
        public int? occ_IdUserCreate { get; set; }
        public int? occ_IdUserModified { get; set; }
    }
}
