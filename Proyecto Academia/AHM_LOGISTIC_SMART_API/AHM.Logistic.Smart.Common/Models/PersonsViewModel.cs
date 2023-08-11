using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class PersonsViewModel
    {
        public int per_Id { get; set; }
        public string per_Identidad { get; set; }
        public string per_Firstname { get; set; }
        public string per_Secondname { get; set; }
        public string per_LastNames { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime per_BirthDate { get; set; }
        public string per_Sex { get; set; }
        public string per_Email { get; set; }
        public string per_Phone { get; set; }
        public string per_Direccion { get; set; }
        public int dep_Id { get; set; }
        public string dep_Description { get; set; }
        public int mun_Id { get; set; }
        public string mun_Description { get; set; }
        public string per_Esciv { get; set; }
        public string Status { get; set; }
        public int? per_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime? per_DateCreate { get; set; }
        public int? per_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime? per_DateModified { get; set; }
    }
}
