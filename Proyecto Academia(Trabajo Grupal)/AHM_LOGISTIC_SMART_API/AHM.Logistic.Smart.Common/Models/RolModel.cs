using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class RolModel
    {
        public int? rol_Id { get; set; }
        public string rol_Description { get; set; }
        public bool rol_Status { get; set; }
        public int? rol_IdUserCreate { get; set; }
        public int? rol_IdUserModified { get; set; }
        public IEnumerable<int> roleModuleItems { get; set; }
    }
}
