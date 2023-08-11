using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Entities.Entities
{
    public partial class EmpleadoHospital
    {
        public int hospi_Id { get; set; }
        public string hospi_Nombre { get; set; }
        public int planti_EmpleadoId { get; set; }
        public string planti_Apellido { get; set; }
        public string planti_Funcion { get; set; }
        public string planti_Turno { get; set; }
        public int planti_Salario { get; set; }
    }
}
