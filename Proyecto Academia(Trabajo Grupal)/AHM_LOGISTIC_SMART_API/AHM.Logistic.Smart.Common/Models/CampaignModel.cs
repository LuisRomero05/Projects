using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class CampaignModel
    {
        public int cam_Id { get; set; }
        public string cam_Nombre { get; set; }
        public string cam_Descripcion { get; set; }
        public string cam_Html { get; set; }
        public int cam_IdUserCreate { get; set; }

    }
}
