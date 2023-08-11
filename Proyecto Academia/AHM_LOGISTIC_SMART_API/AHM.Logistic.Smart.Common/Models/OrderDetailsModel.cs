using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class OrderDetailsModel
    {
        public int ode_Id { get; set; }
        public int ode_Amount { get; set; }
        public int? pro_Id { get; set; }
        public decimal ode_TotalPrice { get; set; }
        [JsonIgnore]
        public int? sor_Id { get; set; }
        [JsonIgnore]
        public int ode_IdUserCreate { get; set; }
        [JsonIgnore]
        public int? ode_IdUserModified { get; set; }
    }
}
