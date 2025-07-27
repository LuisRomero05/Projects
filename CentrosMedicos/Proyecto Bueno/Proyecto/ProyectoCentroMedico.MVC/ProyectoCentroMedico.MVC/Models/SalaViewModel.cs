using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Models
{
    public class SalaViewModel
    {
        [Display(Name = "Hospital")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? hospi_Id { get; set; }

        [Key]
        [Display(Name = "Id")]
        public int sala_Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string sala_Nombre { get; set; }

        [Display(Name = "Número de camas")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? sala_NumCamas { get; set; }

        public SelectList ListadoHosp { get; set; }

        public void LlenarHosp(IEnumerable<tbHospiltales> listado)
        {
            ListadoHosp = new SelectList(listado, "hospi_Id", "hospi_Nombre");
        }

        public string HospitalsNombre { get; set; }
    }
}
