using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
   public class MeetingsModel
    {
        public int met_Id { get; set; }
        public string met_Title { get; set; }
        public string met_MeetingLink { get; set; }
        public int cus_Id { get; set; }
        public DateTime met_Date { get; set; }
        public string met_StartTime { get; set; }
        public string met_EndTime { get; set; }
        public int met_IdUserCreate { get; set; }
        public int met_IdUserModified { get; set; }
        public List<MeetingsViewModel> meetsDetails { get; set; }
        public List<MeetingsDetailUpdateModel> met_details { get; set; }
    }
}
