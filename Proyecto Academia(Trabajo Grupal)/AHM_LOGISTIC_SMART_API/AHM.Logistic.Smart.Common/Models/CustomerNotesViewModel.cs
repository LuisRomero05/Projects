using AHM.Logistic.Smart.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    class CustomerNotesViewModel
    {
        public int cun_Id { get; set; }
        public string cun_Descripcion { get; set; }
        public DateTime cun_ExpirationDate { get; set; }
        public int pry_Id { get; set; }
        public string cun_FileRoute { get; set; }
        public int cus_Id { get; set; }
        public bool cun_Status { get; set; }
        public int cun_IdUserCreate { get; set; }
        public DateTime cun_DateCreate { get; set; }
        public int? cun_IdUserModified { get; set; }
        public DateTime? cun_DateModified { get; set; }
    }
}
