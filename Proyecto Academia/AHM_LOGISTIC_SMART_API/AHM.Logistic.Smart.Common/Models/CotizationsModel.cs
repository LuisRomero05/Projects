using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class CotizationsModel
    {
        public int cot_Id { get; set; }

        public int cus_Id { get; set; }

        public DateTime cot_DateValidUntil { get; set; }

        public int sta_Id { get; set; }

        public int cot_IdUserCreate { get; set; }

        public int? cot_IdUserModified { get; set; }

        public List<CotizationsDetailsModel> cot_details { get; set; }
    }
}
