using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Models
{
    public class ReporteGeneralViewModel
    {
        [Display(Name = "Nombre Hospital")]
        public string hospi_Nombre { get; set; }

        [Display(Name = "Hospital Telefono")]
        public string hospi_Telefono { get; set; }

        [Display(Name = "Apellido Encargado")]
        public string planti_Apellido { get; set; }

        [Display(Name = "Cargo")]
        public string planti_Funcion { get; set; }

        [Display(Name = "Nombre de la Sala")]
        public string sala_Nombre { get; set; }

        [Display(Name = "Apellido Paciente")]
        public string enfer_Apellido { get; set; }

        [Display(Name = "Direccion Paciente")]
        public string enfer_Direccion { get; set; }

        [Display(Name = "Fecha de Nacimiento Paciente")]
        public DateTime? enfer_FechaNac { get; set; }

        [Display(Name = "NSS")]
        public int enfer_NSS { get; set; }

        [Display(Name = "Inscripción")]
        public int enfer_Inscripcioon { get; set; }

        [Display(Name = "Id Sala")]
        public int sala_Id { get; set; }

        public SelectList ListadoPaciente { get; set; }

        public void LlenarEnfer(IEnumerable<tbEnfermo> listado)
        {
            ListadoPaciente = new SelectList(listado, "enfer_Inscripcioon", "enfer_Apellido");
        }
    }
}
