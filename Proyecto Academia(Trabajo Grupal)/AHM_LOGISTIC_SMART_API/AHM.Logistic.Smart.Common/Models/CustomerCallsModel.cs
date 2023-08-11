using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class CustomerCallsModel
    {
        public int cca_Id { get; set; }
        public int cca_CallType { get; set; }
        public string cati_Description { get; set; }
        public int cca_Business { get; set; }
        public DateTime cca_Date { get; set; }
        public string cabu_Description { get; set; }
        public string cca_StartTime { get; set; }
        public string cca_EndTime { get; set; }
        public int cca_Result { get; set; }
        public string caru_Description { get; set; }
        public int cus_Id { get; set; }
        public bool cca_Status { get; set; }
        public int cca_IdUserCreate { get; set; }
        public int cca_IdUserModified { get; set; }
        public List<CustomerCallsModel> calls { get; set; }
        public List<CallTypeModel> callType  { get; set; }
        public List<CallBusinessModel> callBusiness { get; set; }
        public List<CallResultModel> callResult { get; set; }
    }
}
