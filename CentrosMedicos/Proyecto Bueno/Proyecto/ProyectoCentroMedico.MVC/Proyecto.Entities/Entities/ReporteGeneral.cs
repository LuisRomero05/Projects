using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Entities.Entities
{
    public partial class ReporteGeneral
    {
        public string hospi_Nombre { get; set; }
        public string hospi_Telefono { get; set; }
        public string planti_Apellido { get; set; }
        public string planti_Funcion { get; set; }
        public string sala_Nombre { get; set; }
        public string enfer_Apellido { get; set; }
        public string enfer_Direccion { get; set; }
        public DateTime? enfer_FechaNac { get; set; }
        public int enfer_NSS { get; set; }
        public int enfer_Inscripcioon { get; set; }
        public int sala_Id { get; set; }
    }
}
