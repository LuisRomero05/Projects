using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Common.Models
{
    public class AreasViewModel
    {
        public int are_Id { get; set; }
        public string are_Description { get; set; }
        public string Status { get; set; }
        public int? are_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime? are_DateCreate { get; set; }
        public int? are_IdUserModified { get; set; }
        public string usu_UserNameModifies { get; set; }
        public DateTime? are_DateModified { get; set; }
    }
}
