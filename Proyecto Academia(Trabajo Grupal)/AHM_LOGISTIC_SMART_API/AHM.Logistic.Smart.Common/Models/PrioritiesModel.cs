using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class PrioritiesModel
    {
        public int pry_Id { get; set; }
        public string pry_Descripcion { get; set; }
        public bool pry_Status { get; set; }
        public int pry_IdUserCreate { get; set; }
        public int? pry_IdUserModified { get; set; }
    }
}
