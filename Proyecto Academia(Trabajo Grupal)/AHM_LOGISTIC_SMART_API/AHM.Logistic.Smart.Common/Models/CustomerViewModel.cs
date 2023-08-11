using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class CustomerViewModel
    {
        public int cus_Id { get; set; }
        public string cus_Name { get; set; }
        public int cus_AssignedUser { get; set; }
        public int tyCh_Id { get; set; }
        public string tyCh_Description { get; set; }
        public string cus_RTN { get; set; }
        public string cus_Address { get; set; }
        public int dep_Id { get; set; }
        public string dep_Description { get; set; }
        public int mun_Id { get; set; }
        public string mun_Description { get; set; }
        public string cus_Email { get; set; }
        public bool? cus_receive_email { get; set; }
        public string cus_Phone { get; set; }
        public string cus_AnotherPhone { get; set; }        
        public string Status { get; set; }
        public string Estado { get; set; }
        public int cus_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime cus_DateCreate { get; set; }
        public int? cus_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime? cus_DateModified { get; set; }

        public CustomerNotesModel customerNotes { get; set; }
        public CustomerCallsModel customerCalls { get; set; }
        public MeetingsModel meetings { get; set; }
        public CustomerFilesModel customerFiles { get; set; }
    }
}
