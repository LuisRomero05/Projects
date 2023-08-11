using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class ModulesViewModel
    {
        public int mod_Id { get; set; }
        public string mod_Description { get; set; }
        public int com_Id { get; set; }
        public string com_Description { get; set; }
        public string Status { get; set; }
    }
}
