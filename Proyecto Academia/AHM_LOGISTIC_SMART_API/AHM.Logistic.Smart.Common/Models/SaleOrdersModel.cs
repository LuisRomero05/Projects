using AHM.Logistic.Smart.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class SaleOrdersModel
    {
        public int sor_Id { get; set; }
        public int cus_Id { get; set; }
        public int? cot_Id { get; set; }
        public int? sta_Id { get; set; }
        public int sor_IdUserCreate { get; set; }
        public int? sor_IdUserModified { get; set; }
        public List<OrderDetailsModel> sor_details { get; set; }
    }
}
