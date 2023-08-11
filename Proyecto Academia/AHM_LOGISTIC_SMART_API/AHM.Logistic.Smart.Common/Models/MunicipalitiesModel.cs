using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class MunicipalitiesModel
    {
        public int mun_Id { get; set; }
        public string mun_Code { get; set; }
        public string mun_Description { get; set; }
        public int dep_Id { get; set; }
       public int? mun_IdUserCreate { get; set; }

        public int? mun_IdUserModified { get; set; }

    }
}
