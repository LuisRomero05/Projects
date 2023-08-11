using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Models
{
    public class EmpleadoHospitalViewModel
    {
        [Display(Name = "Hospital")]
        public int hospi_Id { get; set; }

        [Display(Name = "Nombre")]
        public string hospi_Nombre { get; set; }

        [Display(Name = "Empleado Id")]
        public int planti_EmpleadoId { get; set; }

        [Display(Name = "Apellido")]
        public string planti_Apellido { get; set; }

        [Display(Name = "Cargo")]
        public string planti_Funcion { get; set; }

        [Display(Name = "Jornada")]
        public string planti_Turno { get; set; }

        [Display(Name = "Salario")]
        public int planti_Salario { get; set; }

        public SelectList ListadoHospitales { get; set; }

        public void LlenarHosp(IEnumerable<tbHospiltales> listado)
        {
            ListadoHospitales = new SelectList(listado, "hospi_Id", "hospi_Nombre");
        }
    }
}
