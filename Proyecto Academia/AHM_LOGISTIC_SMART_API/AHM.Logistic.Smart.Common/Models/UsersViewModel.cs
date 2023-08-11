using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class UsersViewModel
    {
        public EditarUsuario editarUsuario { get; set; }
        public CambiarContraseña CambiarContraseña { get; set; }
        public CambiarImagen CambiarImagen { get; set; }        
        public string pathUsuariosImage { get; set; }

        public UsersViewModel()
        {
            editarUsuario = new EditarUsuario();
            CambiarContraseña = new CambiarContraseña();
            CambiarImagen = new CambiarImagen();
            
        }
        public int usu_Id { get; set; }
        public string usu_UserName { get; set; }
        public string usu_Password { get; set; }
        public int? rol_Id { get; set; }
        public string rol_Description { get; set; }
        public int Per_Id { get; set; }
        public bool? usu_Temporal_Password { get; set; }
        public string per_PerName { get; set; }
        public string Status { get; set; }
        public bool usu_Status { get; set; }
        public int? usu_IdUserCreate { get; set; }
        public string usu_UserNameCreate { get; set; }
        public DateTime? usu_DateCreate { get; set; }
        public int? usu_IdUserModified { get; set; }
        public string usu_UserNameModified { get; set; }
        public DateTime? usu_DateModified { get; set; }
        public string usu_Profile_picture { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class EditarUsuario
    {
        public List<EmployeesViewModel> employeesModel { get; set; }
        public EditarUsuario()
        {
            employeesModel = new List<EmployeesViewModel>();
        }
        

        [Display(Name = "ID")]
        public int usu_Id { get; set; }

        [Display(Name = "Nombre de usuario")]
        public string usu_UserName { get; set; }

        [Display(Name = "Contraseña")]
        [StringLength(50, ErrorMessage = "La contraseña debe tener un maximo de {1} caracteres. ")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "La constraseña debe tener un minimo de 8 caracteres, 1 letra mayúscula, 1 letra minúscula y 1 numero")]
        [Required(ErrorMessage = "El campo contraseña es requerido")]
        public string usu_Password { get; set; }

        [Required(ErrorMessage = "El campo rol es requerido")]
        //[Remote(action: "VerifyRol", controller: "Usuarios", AdditionalFields = nameof(Es_Admin))]
        public int? rol_Id { get; set; }

        
        public string per_Identidad { get; set; }

        [Display(Name = "Nombre")]
        public string per_PerName { get; set; }

        [Display(Name = "Identidad")]
        [Required(ErrorMessage = "El campo identidad es requerido")]
        public int Per_Id { get; set; }
        public bool Status { get; set; }
        public bool isEditUnique { get; set; } //Esta propiedad es para cuando se quiera acceder al perfil

        //[Required(ErrorMessage = "El campo {0} es requerido")]
        //public SelectList Roleslist { get; set; }

        //[Display(Name = "Pantalla")]
        //[Required(ErrorMessage = "La {0} es requerido, seleccione una pantalla.")]
        //public virtual tbRoles Rol { get; set; }

        public string usu_Profile_picture { get; set; }
        public string rol_Descripcion { get; set; }

        [Display(Name = "Usuario creación")]
        public int? usu_IdUserCreate { get; set; }
        [Display(Name = "Usuario creación")]
        public string usu_UserNameCreate { get; set; }
        [Display(Name = "Fecha creación")]
        public DateTime? usu_DateCreate { get; set; }
        [Display(Name = "Usuario modificación")]
        public int? usu_IdUserModified { get; set; }

        [Display(Name = "Usuario modificación")]
        public string UsuarioModificacion { get; set; }

        [Display(Name = "Usuario modificación")]
        public string usu_UserNameModified { get; set; }

        [Display(Name = "Fecha modificación")]
        public DateTime? usu_DateModified { get; set; }

        public IFormFile ImageFile { get; set; }

    }

    public class CambiarContraseña
    {
        [Display(Name = "Nombre de Usuario")]
        public string usu_UserName { get; set; }

        [Required]
        public int usu_Id { get; set; }
        public int Per_Id { get; set; }
        public int rol_Id { get; set; }

        [Display(Name = "Contraseña")]
        public string usu_Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        [Required(ErrorMessage = "Ingrese su nueva contraseña")]
        [StringLength(50, ErrorMessage = "La contraseña debe tener un maximo de {1} caracteres. ")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "La constraseña debe tener un minimo de 8 caracteres, 1 letra mayuscula, 1 letra minuscula y 1 numero")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nueva Contraseña")]
        [Required(ErrorMessage = "Escriba nuevamente su contraseña")]
        [StringLength(50, ErrorMessage = "La contraseña debe tener un maximo de {1} caracteres. ")]
        [Compare(nameof(NewPassword), ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmContraseña { get; set; }
    }
    public class CambiarImagen
    {
        [Display(Name = "ID")]
        public int usu_Id { get; set; }

        public string usu_Profile_picture { get; set; }

        public string usu_NombreUsuario { get; set; }

        [Display]
        [Required(ErrorMessage = "Seleccione una imagen ")]
        public IFormFile ImageFile { get; set; }
    }


}
