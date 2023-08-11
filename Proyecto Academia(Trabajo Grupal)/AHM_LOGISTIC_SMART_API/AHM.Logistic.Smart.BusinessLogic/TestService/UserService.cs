using AHM.Logistic.Smart.BusinessLogic.Logger;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.DataAccess.TestRepositories;
using AHM.Logistic.Smart.Entities.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.BusinessLogic.TestService
{
    public class UserService : IService<tbUsers, View_tbUsers_List>
    {
        UserRepositoryTest _usersRepository = new UserRepositoryTest();
        public ServiceResult List()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _usersRepository.List();
                if (listado.Any())
                {
                    return result.Ok(listado);
                }
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbUsers_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbUsers_List();
            var errorMessage = "";
            try
            {
                resultado = _usersRepository.Search(expression);
                var model = new UsersViewModel()
                {
                    usu_Id = resultado.usu_Id,
                    usu_UserName = resultado.usu_UserName,
                    usu_Password = resultado.usu_Password,
                    rol_Id = resultado.rol_Id,
                    rol_Description = resultado.rol_Description,
                    Per_Id = resultado.Per_Id,
                    per_PerName = resultado.per_PerName,
                    Status = resultado.Status,
                    usu_Status = resultado.usu_Status,
                    usu_IdUserCreate = resultado.usu_IdUserCreate,
                    usu_UserNameCreate = resultado.usu_UserNameCreate,
                    usu_DateCreate = resultado.usu_DateCreate,
                    usu_IdUserModified = resultado.usu_IdUserModified,
                    usu_UserNameModified = resultado.usu_UserNameModified,
                    usu_DateModified = resultado.usu_DateModified,
                    usu_Profile_picture = resultado.usu_Profile_picture,
                };
                return result.Ok(model);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult FindUsersByUser(Expression<Func<View_tbUsers_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbUsers_List();
            var errorMessage = "";
            try
            {
                resultado = _usersRepository.Search(expression);
                return result.Ok(resultado);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult Insert(tbUsers item)
        {
            var result = new ServiceResult();
            var entidad = new UsersModel();
            try
            {
                item.usu_PasswordSalt = Guid.NewGuid().ToString();
                item.usu_Password = EncryptPass.GeneratePassword(item.usu_Password, item.usu_PasswordSalt);
                var repeated = _usersRepository.Find(x => x.usu_UserName.ToLower() == item.usu_UserName.ToLower());
                if (repeated != null && repeated.usu_Status == true)
                    return result.Conflict($"Un usuario con el nombre '{item.usu_UserName}' ya existe");
                var query = _usersRepository.Insert(item);
                EventLogger.UserId = item.usu_IdUserCreate;
                EventLogger.Create($"Creado Usuario '{item.usu_UserName}'.", item);
                if (query > 0)
                {
                    entidad.usu_Id = query;
                    return result.Ok(entidad);
                }
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Update(tbUsers item, int Id)
        {
            var result = new ServiceResult();
            var confirmedItems = new tbUsers();
            string DefString = "string";
            try
            {
                if (item.usu_Password != null && item.usu_Password != DefString)
                {
                    item.usu_PasswordSalt = Guid.NewGuid().ToString();
                    item.usu_Password = EncryptPass.GeneratePassword(item.usu_Password, item.usu_PasswordSalt);
                }
                var IdUser = Id;
                var usuario = _usersRepository.Find(x => x.usu_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                else
                {
                    confirmedItems = GetDetailsUser(usuario, item);
                }
                var repeated = _usersRepository.Find(x => x.usu_UserName.ToLower() == item.usu_UserName.ToLower());
                if (repeated != null && repeated.usu_Status == true && repeated.usu_Id != Id)
                    return result.Conflict($"Un usuario con el nombre '{repeated.usu_UserName}' ya existe");
                var query = _usersRepository.Update(IdUser, confirmedItems);
                EventLogger.UserId = item.usu_IdUserModified;
                EventLogger.Update($"Actualizado Usuario '{item.usu_UserName}'.", usuario, item);
                return result.Ok(query);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Delete(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var modUser = Mod;
                var IdUser = Id;
                var usuario = _usersRepository.Find(x => x.usu_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _usersRepository.Delete(IdUser, modUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminado Usuario '{usuario.usu_UserName}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        public tbUsers GetDetailsUser(tbUsers details, tbUsers confirmItems)
        {
            string DefString = "string";
            if (confirmItems.usu_UserName == "" || confirmItems.usu_UserName == DefString || confirmItems.usu_UserName == null)
            {
                confirmItems.usu_UserName = details.usu_UserName;
            }
            if (confirmItems.usu_Password == "" || confirmItems.usu_Password == DefString || confirmItems.usu_Password == null)
            {
                confirmItems.usu_Password = details.usu_Password;
            }
            if (confirmItems.usu_PasswordSalt == "" || confirmItems.usu_PasswordSalt == DefString || confirmItems.usu_PasswordSalt == null)
            {
                confirmItems.usu_PasswordSalt = details.usu_PasswordSalt;
            }
            if (confirmItems.usu_Profile_picture == "" || confirmItems.usu_Profile_picture == DefString || confirmItems.usu_Profile_picture == null)
            {
                confirmItems.usu_Profile_picture = details.usu_Profile_picture;
            }
            if (confirmItems.rol_Id == 0 || confirmItems.rol_Id == null)
            {
                confirmItems.rol_Id = details.rol_Id;
            }
            if (confirmItems.Per_Id == 0)
            {
                confirmItems.Per_Id = details.Per_Id;
            }
            if (confirmItems.usu_IdUserCreate == 0)
            {
                confirmItems.usu_IdUserCreate = details.usu_IdUserCreate;
            }
            if (confirmItems.usu_IdUserModified == 0 || confirmItems.usu_IdUserModified == null)
            {
                confirmItems.usu_IdUserModified = details.usu_IdUserModified;
            }
            return confirmItems;
        }
    }
}
