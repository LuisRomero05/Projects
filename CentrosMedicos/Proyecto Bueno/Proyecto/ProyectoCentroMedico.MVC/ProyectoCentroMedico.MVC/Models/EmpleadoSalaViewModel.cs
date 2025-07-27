using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Models
{
    public class EmpleadoSalaViewModel
    {
        [Display(Name = "Sala")]
        public int sala_Id { get; set; }

        [Display(Name = "Empleado Id")]
        public int planti_EmpleadoId { get; set; }

        [Display(Name = "Apellido")]
        public string planti_Apellido { get; set; }

        [Display(Name = "Función")]
        public string planti_Funcion { get; set; }

        [Display(Name = "Turno")]
        public string planti_Turno { get; set; }

        [Display(Name = "Salario")]
        public int planti_Salario { get; set; }

        [Display(Name = "Hospital Id")]
        public int hospi_Id { get; set; }

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
    }
}
