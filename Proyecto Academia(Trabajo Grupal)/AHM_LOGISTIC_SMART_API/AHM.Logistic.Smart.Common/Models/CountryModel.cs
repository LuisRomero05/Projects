using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class CountryModel
    {
        public int cou_Id { get; set; }
        public string cou_Description { get; set; }
        public int? cou_IdUserCreate { get; set; }
        public int? cou_IdUserModified { get; set; }
    }
}
