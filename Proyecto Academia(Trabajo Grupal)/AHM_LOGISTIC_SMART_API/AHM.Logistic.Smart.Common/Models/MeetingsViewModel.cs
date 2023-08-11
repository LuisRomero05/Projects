using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class MeetingsViewModel
    {
        public int met_Id { get; set; }
        public string met_Title { get; set; }
        public string met_MeetingLink { get; set; }
        public int cus_Id { get; set; }
        public string cus_Name { get; set; }
        public DateTime met_Date { get; set; }
        public string met_StartTime { get; set; }
        public string met_EndTime { get; set; }
        public string Status { get; set; }
        public int met_IdUserCreate { get; set; }
        public DateTime met_DateCreate { get; set; }
        public List<MeetingsDetailUpdateModel> met_details { get; set; }
        public List<CustomersEmployeesViewModel> ListCusEmpCon { get; set; }
    }
}
