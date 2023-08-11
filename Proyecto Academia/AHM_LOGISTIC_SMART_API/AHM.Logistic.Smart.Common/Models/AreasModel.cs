using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class AreasModel
    {
        public int are_Id { get; set; }
        public string are_Description { get; set; }

        public int? are_IdUserCreate { get; set; }

        public int? are_IdUserModified { get; set; }
    }
}
