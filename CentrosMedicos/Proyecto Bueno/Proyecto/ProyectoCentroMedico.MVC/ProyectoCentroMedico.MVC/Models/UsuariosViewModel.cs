using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Models
{
    public class UsuariosViewModel
    {
        public int usu_ID { get; set; }

        [Range(1, 200, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int rol_ID { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string usu_Nombre { get; set; }
        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string usu_Apellido { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string usu_Email { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string usu_Password { get; set; }
        public string usu_PasswordSalt { get; set; }
        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string usu_NumeroTelefono { get; set; }
        [Display(Name = "Celular")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string usu_NumeroCelular { get; set; }

        public SelectList ListadoRoles { get; set; }
        public SelectList ListRol { get; set; }

        public void LlenarCbRol(IEnumerable<tbRoles> listado)
        {
            ListRol = new SelectList(listado, "rol_Id", "rol_Nombre");
        }

        public string RolNombre { get; set; }
    }
}
