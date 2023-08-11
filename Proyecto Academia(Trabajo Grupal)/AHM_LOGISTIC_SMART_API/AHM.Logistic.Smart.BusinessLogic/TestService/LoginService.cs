using AHM.Logistic.Smart.DataAccess.TestRepositories;
using AHM.Logistic.Smart.Entities.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.BusinessLogic.TestService
{
    public class LoginService 
    {
        public UserRepositoryTest _usersRepository = new UserRepositoryTest();
        public ServiceResult Login(tbUsers items)
        {
            var result = new ServiceResult();
            try
            {
                if (string.IsNullOrEmpty(items.usu_UserName))
                    return result.Error("El usuario no puede estar vacío");

                if (string.IsNullOrEmpty(items.usu_Password))
                    return result.Error("La contraseña no puede estar vacía");

                var usuario = _usersRepository.Find(x => x.usu_UserName.ToLower() == items.usu_UserName.ToLower());
                if (usuario == null)
                    return result.Error($"No existe un usuario con el nombre {items.usu_UserName}");

                if (!EncryptPass.VerifyPassword(items.usu_Password, usuario.usu_Password, usuario.usu_PasswordSalt))
                    return result.Error($"Usuario o contraseña incorrecto");

                return result.Ok();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
    }
}
