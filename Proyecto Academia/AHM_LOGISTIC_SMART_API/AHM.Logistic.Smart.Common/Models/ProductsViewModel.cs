using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class ProductsViewModel
    {
        public int pro_Id { get; set; }
        public string pro_Description { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal pro_PurchasePrice { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal pro_SalesPrice { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int pro_Stock { get; set; }
        public decimal pro_ISV { get; set; }
        public int uni_Id { get; set; }
        public string uni_Description { get; set; }
        public string scat_Description { get; set; }
        public int scat_Id { get; set; }
        public string Status { get; set; }
        public int? pro_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime? pro_DateCreate { get; set; }
        public int? pro_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime? pro_DateModified { get; set; }
    }
}
