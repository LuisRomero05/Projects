using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class CustomerNotesModel
    {
        public int cun_Id { get; set; }
        public string cun_Descripcion { get; set; }
        public DateTime cun_ExpirationDate { get; set; }
        public int pry_Id { get; set; }
        public string pry_Descripcion { get; set; }
        public string cun_FileRoute { get; set; }
        public int cus_Id { get; set; }
        public int cun_IdUserCreate { get; set; }
        public int? cun_IdUserModified { get; set; }
        public List<CustomerNotesModel> notes { get; set; }
        public List<PrioritiesModel> listPriorities { get; set; }
    }
}
