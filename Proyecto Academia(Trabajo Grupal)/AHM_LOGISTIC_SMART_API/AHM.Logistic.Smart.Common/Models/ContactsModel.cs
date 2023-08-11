using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class ContactsModel
    {
        public int cont_Id { get; set; }
        public string cont_Name { get; set; }
        public string cont_LastName { get; set; }
        public string cont_Email { get; set; }
        public string cont_Phone { get; set; }
        public int occ_Id { get; set; }
        public int cus_Id { get; set; }
        public int? cont_IdUserCreate { get; set; }
        public int? cont_IdUserModified { get; set; }
    }
}
