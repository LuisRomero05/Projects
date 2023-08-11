using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class PersonsModel
    {
        public int per_Id { get; set; }
        public string per_Identidad{ get; set; }
        public string per_Firstname { get; set; }
        public string per_Secondname { get; set; }
        public string per_LastNames { get; set; }
        public DateTime per_BirthDate { get; set; }
        public string per_Sex { get; set; }
        public string per_Email { get; set; }
        public string per_Phone { get; set; }
        public string per_Direccion { get; set; }
        public int dep_Id { get; set; }
        public int mun_Id { get; set; }
        public string per_Esciv { get; set; }
        public int? per_IdUserCreate { get; set; }
        public int? per_IdUserModified{ get; set; }
    }
}
