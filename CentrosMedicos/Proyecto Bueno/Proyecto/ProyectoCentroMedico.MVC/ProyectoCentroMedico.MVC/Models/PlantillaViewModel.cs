using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Models
{
    public class PlantillaViewModel
    {
        [Display(Name = "Hospital Id")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? hospi_Id { get; set; }

        [Display(Name = "Sala Id")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? sala_Id { get; set; }

        [Key]
        [Display(Name = "Id")]
        public int planti_EmpleadoId { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string planti_Apellido { get; set; }

        [Display(Name = "Función")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string planti_Funcion { get; set; }

        [Display(Name = "Turno")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string planti_Turno { get; set; }

        [Display(Name = "Salario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int planti_Salario { get; set; }

        public SelectList ListadoSala { get; set; }
        public SelectList ListadoHos { get; set; }

        public void LlenarSala(IEnumerable<tbSala> listado)
        {
            ListadoSala = new SelectList(listado, "sala_Id", "sala_Nombre");
        }

        public void LlenarHospital(IEnumerable<tbHospiltales> listado)
        {
            ListadoHos = new SelectList(listado, "hospi_Id", "hospi_Nombre");
        }
        public string HospitalNombre { get; set; }
        public string SalaNombre { get; set; }
    }
}
