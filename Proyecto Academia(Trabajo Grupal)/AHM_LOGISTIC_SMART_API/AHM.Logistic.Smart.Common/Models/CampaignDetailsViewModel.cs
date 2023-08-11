using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class CampaignDetailsViewModel
    {
        public int cde_Id { get; set; }
        public int cus_Id { get; set; }
        public int cam_Id { get; set; }
        public string cam_Nombre { get; set; }
        public string cam_Descripcion { get; set; }
        public string cam_Html { get; set; }
        public int cam_Status { get; set; }
        public int cam_IdUserCreate { get; set; }
        public string cus_Name { get; set; }
        public string cus_Email { get; set; }
        public string cus_Phone { get; set; }
        public int cus_IdUserCreate { get; set; }
    }
}
