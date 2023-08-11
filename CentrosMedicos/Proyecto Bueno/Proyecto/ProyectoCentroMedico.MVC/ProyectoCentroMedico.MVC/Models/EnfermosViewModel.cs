using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Models
{
    public class EnfermosViewModel
    {
        [Key]
        [Display(Name = "Inscripción")]
        public int enfer_Inscripcioon { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string enfer_Apellido { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string enfer_Direccion { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? enfer_FechaNac { get; set; }

        [Display(Name = "NSS")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int enfer_NSS { get; set; }

        [Display(Name = "Empleado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? planti_EmpleadoId { get; set; }

        public SelectList ListadoEmp { get; set; }

        public void LlenarEmpleado(IEnumerable<tbPlantilla> listado)
        {
            ListadoEmp = new SelectList(listado, "planti_EmpleadoId", "planti_Apellido");
        }

        public string EmpNombre { get; set; }
    }
}
