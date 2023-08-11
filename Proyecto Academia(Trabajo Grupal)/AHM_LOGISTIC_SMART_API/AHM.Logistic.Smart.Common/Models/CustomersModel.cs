using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{

    public class CustomersModel
    {

        public int cus_Id { get; set; }
        public int cus_AssignedUser { get; set; }
        public int tyCh_Id { get; set; }
        public string cus_Name { get; set; }
        public string cus_RTN { get; set; }
        public string cus_Address { get; set; }
        public int dep_Id { get; set; }
        public int mun_Id { get; set; }
        public string cus_Email { get; set; }
        public bool cus_receive_email { get; set; }
        public bool cus_Active { get; set; }
        public string cus_Phone { get; set; }
        public string cus_AnotherPhone { get; set; }
        public int cus_IdUserCreate { get; set; }
        public int? cus_IdUserModified { get; set; }

   
    }
}
