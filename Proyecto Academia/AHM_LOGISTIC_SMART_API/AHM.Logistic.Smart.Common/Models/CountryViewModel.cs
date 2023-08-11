using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class CountryViewModel
    {
        public int cou_Id { get; set; }
        public string cou_Description { get; set; }
        public string Status { get; set; }
        public int? cou_IdUserCreate { get; set; }

        public string cou_UserNameCreate { get; set; }

        public DateTime? cou_DateCreate { get; set; }

        public int? cou_IdUserModified { get; set; }

        public string cou_UserNameModified { get; set; }

        public DateTime? cou_DateModified { get; set; }

    }
}
