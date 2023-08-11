using AHM.Logistic.Smart.BusinessLogic.Logger;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.DataAccess.Repositories;
using AHM.Logistic.Smart.Entities.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AHM.Logistic.Smart.BusinessLogic.Services
{
    public class AccessService
    {

        private readonly UsersRepository _usersRepository;
        private readonly RolesRepository _rolesRepository;

        public AccessService(RolesRepository rolesRepository, UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
        }

        #region tbUsers

        public ServiceResult ListUser()
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

        public ServiceResult FindUsers(Expression<Func<View_tbUsers_List, bool>> expression = null)
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

        public ServiceResult RegisterUser(tbUsers item)
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

        public ServiceResult UpdateUser(int Id, tbUsers item)
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

        public ServiceResult DeleteUser(int Id, int Mod)
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
        #endregion

        #region tbRoles

        public ServiceResult ListRoles()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _rolesRepository.List();
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

        public ServiceResult ListComponents()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _rolesRepository.ComponentsList();
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

        public ServiceResult ListModules()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _rolesRepository.ModulesList();
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

        public ServiceResult ListModuleItems()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _rolesRepository.ModuleItemsList();
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

        public ServiceResult FindRoles(Expression<Func<View_tbRoles_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbRoles_List();
            var errorMessage = "";
            try
            {
                resultado = _rolesRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult FindModuleItems(Expression<Func<tbRoleModuleItems, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new List<tbRoleModuleItems>();
            try
            {
                resultado = _rolesRepository.RolModuleItemsDetails(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterRol(tbRoles item, List<tbRoleModuleItems> rolModule)
        {
            var result = new ServiceResult();
            RolModel model = new RolModel();
            try
            {
                var repeated = _rolesRepository.Find(x => x.rol_Description.ToLower() == item.rol_Description.ToLower());
                if (repeated != null && repeated.rol_Status == true)
                    return result.Conflict($"Un rol con el nombre '{repeated.rol_Description}' ya existe");
                var query = _rolesRepository.Insert(item, rolModule);
                EventLogger.UserId = item.rol_IdUserCreate;
                EventLogger.Create($"Creado Rol '{item.rol_Description}'.", item);
                if (query > 0)
                {
                    model.rol_Id = query;
                    return result.Ok(model);
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

        public ServiceResult UpdateRol(tbRoles item, List<tbRoleModuleItems> rolModule)
        {
            var result = new ServiceResult();
            RolModel model = new RolModel();
            try
            {
                var usuario = _rolesRepository.Find(x => x.rol_Id == item.rol_Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var repeated = _rolesRepository.Find(x => x.rol_Description.ToLower() == item.rol_Description.ToLower());
                if (repeated != null && repeated.rol_Status == true && repeated.rol_Id != item.rol_Id)
                    return result.Conflict($"Un rol con el nombre '{repeated.rol_Description}' ya existe");
                var query = _rolesRepository.Update(item, rolModule);
                EventLogger.UserId = item.rol_IdUserModified;
                EventLogger.Update($"Actualizado Rol '{item.rol_Description}'.", usuario, item);
                if (query > 0)
                {
                    model.rol_Id = query;
                    return result.Ok(model);
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

        public ServiceResult DeleteRol(int Id, int Mod)
        {
            var result = new ServiceResult();
            RolModel model = new RolModel();
            try
            {
                var ModUser = Mod;
                var IdUser = Id;
                var usuario = _rolesRepository.Find(x => x.rol_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _rolesRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminado Rol '{usuario.rol_Description}'.", usuario);
                if (query > 0)
                {
                    model.rol_Id = query;
                    return result.Ok(model);
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

        #endregion
    }
}
