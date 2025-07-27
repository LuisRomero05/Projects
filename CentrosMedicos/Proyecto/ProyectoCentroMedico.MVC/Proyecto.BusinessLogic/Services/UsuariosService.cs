using Proyecto.DataAccess.Repositories;
using Proyecto.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BusinessLogic.Services
{
    public class UsuariosService
    {
        private readonly UsuariosRepository _usuarioRepository;
        public UsuariosService(UsuariosRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IEnumerable<tbUsuarios> Listado(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                return _usuarioRepository.List();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Enumerable.Empty<tbUsuarios>();
            }
        }

        public tbUsuarios FindUsuario(int id, out string errorMessage)
        {
            var response = new tbUsuarios();
            errorMessage = string.Empty;
            try
            {
                response = _usuarioRepository.Find(id);

            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }
            return response;
        }

        public tbUsuarios FindUsuario(out string errorMessage, Expression<Func<tbUsuarios, bool>> expression = null)
        {
            var response = new tbUsuarios();
            errorMessage = string.Empty;
            try
            {
                response = _usuarioRepository.Find(expression);

            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }
            return response;
        }

        public string VerificarLogin(string email, string contraseña)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return "El email no puede estar vacío";

                if (string.IsNullOrEmpty(contraseña))
                    return "La contraseña no puede estar vacía";

                var usuario = _usuarioRepository.Find(x => x.usu_Email.ToLower() == email.ToLower());
                if (usuario == null)
                    return $"No existe un usuario con el email {email}";

                if (!Security.VerifyPassword(contraseña, usuario.usu_Password, usuario.usu_PasswordSalt))
                    return $"Correo electrónico o contraseña incorrecto";

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IEnumerable<string>ListadoAccesos(string correoElectronico, out string mensajeError)
        {
            mensajeError = string.Empty;
            try
            {
                return _usuarioRepository.UserAccess(correoElectronico);
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                return Enumerable.Empty<string>();
            }
        }
    }
}
