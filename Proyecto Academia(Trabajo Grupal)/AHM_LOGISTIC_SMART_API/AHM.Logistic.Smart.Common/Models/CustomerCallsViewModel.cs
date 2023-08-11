using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class CustomerCallsViewModel
    {
        public int cca_Id { get; set; }
        public int cca_CallType { get; set; }
        public string cati_Description { get; set; }
        public int cca_Bussines { get; set; }
        public DateTime cca_Date { get; set; }
        public string cabu_Description { get; set; }
        public string cca_StartTime { get; set; }
        public string cca_EndTime { get; set; }
        public int cca_Result { get; set; }
        public string caru_Description { get; set; }
        public int cus_Id { get; set; }
        public string cus_Name { get; set; }
        public string Status { get; set; }
        public int cca_IdUserCreate { get; set; }
        public string cca_UserNameCreate { get; set; }
        public DateTime cca_DateCreate { get; set; }
        public int cca_IdUserModified { get; set; }
        public string cca_UserNameModified { get; set; }
        public DateTime cca_DateModified { get; set; }
    }
}
