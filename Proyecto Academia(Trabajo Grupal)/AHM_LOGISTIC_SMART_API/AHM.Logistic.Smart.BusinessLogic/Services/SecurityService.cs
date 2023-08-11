using AHM.Logistic.Smart.DataAccess.Repositories;
using AHM.Logistic.Smart.Entities.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.BusinessLogic.Services
{
   public class SecurityService
    {
        private readonly SecurityRepository _securityRepository;
        private readonly UsersRepository _usersRepository;
        public SecurityService(SecurityRepository securityRepository, UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _securityRepository = securityRepository;
        }

        #region ScreenLists
        public ServiceResult ListScreens()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _securityRepository.ListScreens();
                if (listado.Any())
                    return result.Ok(listado);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        public ServiceResult ListGetPermissions(Expression<Func<View_UserPermits_SELECT, bool>> expression = null)
        {
            var result = new ServiceResult();

            List<View_UserPermits_SELECT> resultado = new List<View_UserPermits_SELECT>();
            var errorMessage = "";
            try
            {
                resultado = _securityRepository.ListGetPermissions(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        #endregion

        #region RecuperarContra
        public ServiceResult SendPassword (string correo,Expression<Func<View_tbPersons_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var items = new tbUsers();
            int resultado = 0;
            var errorMessage = "";
            try
            {
                var validacion = _securityRepository.Find2(x => x.per_Email == correo);
                if (validacion == null)
                    return result.Error($"No existe un usuario registrado con ese correo");
     
                var Random = EncryptPass.CreateRandomWordNumberCombination();
                items.usu_PasswordSalt = Guid.NewGuid().ToString();
                items.usu_Password = EncryptPass.GeneratePassword(Random, items.usu_PasswordSalt);
                
                resultado = _securityRepository.IdByEmail(Random,items,expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }
        #endregion
    }
}
