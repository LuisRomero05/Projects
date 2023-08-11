using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
   
    public class ProductsModel
    {
        public int pro_Id { get; set; }
        public string pro_Description { get; set; }
        public decimal pro_PurchasePrice { get; set; }
        public decimal pro_SalesPrice { get; set; }
        public int pro_Stock { get; set; }
        public decimal pro_ISV { get; set; }
        public int uni_Id { get; set; }
        public int scat_Id { get; set; }
        public int? pro_IdUserCreate { get; set; }
        public int? pro_IdUserModified { get; set; }
    }
}
