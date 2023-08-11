using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
   public class CategoryViewModel
    {
        public int cat_Id { get; set; }
        public string cat_Description { get; set; }
        public string Status { get; set; }
        public int? cat_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime? cat_DateCreate { get; set; }
        public int? cat_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime? cat_DateModified { get; set; }
    }
}
