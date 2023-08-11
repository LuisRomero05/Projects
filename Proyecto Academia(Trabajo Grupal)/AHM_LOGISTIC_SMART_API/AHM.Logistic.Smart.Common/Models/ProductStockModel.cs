using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class ProductStockModel
    {
        public int pro_Id { get; set; }
        public int pro_Stock { get; set; }
        public int? pro_IdUserModified { get; set; }
    }
}
