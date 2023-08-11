using AHM_LOGISTIC_SMART_ADM.Models;
using AHM_LOGISTIC_SMART_ADM.WebApi;
using AHM.Logistic.Smart.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Services
{

    public class AccessService
    {

        private readonly Api _api;

        public AccessService(Api api)
        {
            _api = api;
        }

        #region tbUsuarios
        public async Task<ServiceResult> UsersListado(List<UsersViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<UsersViewModel>>(req =>
                {
                    req.Path = $"/api/Users/List";
                    req.Content = model;
                });
                var persresult = await _api.Get<List<PersonsViewModel>>(req =>
                {
                    req.Path = $"/api/Persons/List";
                });
                List<PersonsViewModel> persons = persresult.Data;

                if (!response.Success)
                    return result.FromApi(response);

                if(response.Success && response.Data.Count() > 0)
                {
                    foreach (var item in response.Data)
                    {
                        PersonsViewModel per = persons.Find(fnd => fnd.per_Id == item.Per_Id);
                        item.per_PerName = $"{per.per_Firstname} {per.per_Secondname} {per.per_LastNames}";
                    }
                }
                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }



        public async Task<ServiceResult> UsersDetails(int id, bool getEmployees)
        {
            var result = new ServiceResult();
            var model = new UsersViewModel();
            try
            {
                var empresult = await _api.Get<List<EmployeesViewModel>>(req =>
                {
                    req.Path = $"/api/Employees/List";
                });
                var usresult = await _api.Get<List<UsersViewModel>>(req =>
                {
                    req.Path = $"/api/Users/List";
                });
                var persresult = await _api.Get<List<PersonsViewModel>>(req =>
                {
                    req.Path = $"/api/Persons/List";
                });
                List<EmployeesViewModel> employees = empresult.Data;
                List<UsersViewModel> users = usresult.Data;
                List<PersonsViewModel> persons = persresult.Data;
                List<int> listperId = new List<int>();

                if (id == 0 && getEmployees)
                {
                    UsersViewModel usersView = new UsersViewModel();
                    foreach (var item in users)
                    {
                        listperId.Add(item.Per_Id);
                    }
                    usersView.editarUsuario.employeesModel = employees
                        .Where(x => !(listperId.Contains(x.per_Id)))
                        .ToList();

                    usersView.editarUsuario.employeesModel = usersView.editarUsuario.employeesModel
                        .GroupBy(x => x.per_Id)
                        .Select(grp => grp.First())
                        .ToList();

                    foreach (var item in usersView.editarUsuario.employeesModel)
                    {
                        PersonsViewModel per = persons.Find(fnd => fnd.per_Id == item.per_Id);
                        item.per_Firstname = $"{per.per_Firstname} {per.per_Secondname} {per.per_LastNames}";
                    }
                    return result.Ok(usersView);
                }
                var exist = await _api.Get<List<UsersViewModel>>(req =>
                {
                    req.Path = $"/api/Users/List";
                });
                if (exist.Data.Where(x => x.usu_Id == id).Count() != 0)
                {
                    var response = await _api.Get<UsersViewModel>(req =>
                    {
                        req.Path = $"/api/Users/Details/{id}";
                        req.Content = model;
                    });
                    UsersViewModel usersView = response.Data;
                    usersView.editarUsuario.employeesModel = employees
                        .GroupBy(x => x.per_Id)
                        .Select(grp => grp.First())
                        .ToList();


                    if (!response.Success)
                        return result.FromApi(response);
                    return result.Ok(response.Data);
                }
                else
                    return result.Error(ServiceProblemMessage.NotFound);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> UsersFindByUser(string username)
        {
            var result = new ServiceResult();
            var model = new UsersViewModel();
            try
            {
                var response = await _api.Get<UsersViewModel>(req =>
                {
                    req.Path = $"/api/Users/FindByUser?username={username}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);
                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> UsersFind(string username)
        {
            var result = new ServiceResult();
            var model = new UsersViewModel();
            try
            {
                var response = await _api.Get<UsersViewModel>(req =>
                {
                    req.Path = $"/api/Users/Find?username={username}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);
                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InsertUsers(UsersModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<UsersModel>(req =>
                {
                    req.Path = $"/api/Users/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok("Registro creado exitosamente.", response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> EditUsers(UsersModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Put<UsersModel>(req =>
                {
                    req.Path = $"/api/Users/Update/{model.usu_Id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok("Registro actualizado exitosamente.", response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> DeleteUsers(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var getDetails = await UsersDetails(Id, false);
                if (!getDetails.Success && getDetails.Data == null)
                    return result.Error(getDetails.Message);
                var response = await _api.Delete<UsersModel>(req =>
                {
                    //req.Path = $"/api/Employees/Delete" + Id + "?Mod=" + Mod;
                    req.Path = $"/api/Users/Delete/" + Id + "?Mod=" + Mod;
                    //req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> Login(LoginModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<LoginModel>(req =>
                {
                    req.Path = $"/api/Login/Login";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> RecoveryPassWord(string correo)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<int>(req =>
                {
                    req.Path = $"api/Security/RecuperarContraseña?correo=" + correo;
                    //req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InfoUsers(UserInfoModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<UserInfoModel>(req =>
                {
                    req.Path = $"/api/Users/UserInfo";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        #endregion

        #region tbRoles
        //Listado
        public async Task<ServiceResult> RolesList(List<RolesViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<RolesViewModel>>(req =>
                {
                    req.Path = $"/api/Roles/List";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> ComponentsList(List<ComponentsModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<ComponentsModel>>(req =>
                {
                    req.Path = $"/api/Roles/ComponentsList";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> ModuleList(List<ModuleModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<ModuleModel>>(req =>
                {
                    req.Path = $"/api/Roles/ModulesList";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> ModuleItemsList(List<ModuleItemsModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<ModuleItemsModel>>(req =>
                {
                    req.Path = $"/api/Roles/ModuleItemsList";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> GetModuleItemsList(int rol_Id)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<RoleModuleItemViewModel>>(req =>
                {
                    req.Path = $"/api/Roles/ModuleItems/{rol_Id}";
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        //insertar
        public async Task<ServiceResult> InsertRoles(RolModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<RolModel>(req =>
                {
                    req.Path = $"/api/Roles/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro creado exitosamente.", response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        //details
        public async Task<ServiceResult> DetailsRoles(int Id)
        {
            var result = new ServiceResult();
            try
            {
                var exist = await _api.Get<List<RolesViewModel>>(req =>
                {
                    req.Path = $"/api/Roles/List";
                });
                if (exist.Data.Where(x => x.rol_Id == Id).Count() != 0)
                {
                    var response = await _api.Get<RolesModel>(req =>
                    {
                        req.Path = $"/api/Roles/Details/{Id}";
                    });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok(response.Data);
                }
                else
                    return result.Error(ServiceProblemMessage.NotFound);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        //edit
        public async Task<ServiceResult> EditRoles(RolModel model, int id)
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Put<RolModel>(req =>
                {
                    req.Path = $"/api/Roles/Update/{id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro actualizado exitosamente.", response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        //Delete
        public async Task<ServiceResult> DeleteRol(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Delete<RolesModel>(req =>
                {
                    //req.Path = $"/api/Employees/Delete" + Id + "?Mod=" + Mod;
                    req.Path = $"/api/Roles/Delete/" + Id + "?Mod=" + Mod;
                    //req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> GetPermissions(string email)
        {
            var result = new ServiceResult();
            var model = new PermissionsViewModel();
            try
            {
                var response = await _api.Get<List<PermissionsViewModel>>(req =>
                {
                    req.Path = $"/api/Security/ScreensPerRole?User={email}";
                });

                if (!response.Success)
                    return result.FromApi(response);
                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        #endregion

    }
}
