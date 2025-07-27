using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Models
{
    public class UserViewModel
    {
        public UsuariosViewModel UsuariosViewModel { get; set; }
        public UserViewModel()
        {
            UsuariosViewModel = new UsuariosViewModel();
        }
    }
}
