using Microsoft.AspNetCore.Mvc;
using AHM_LOGISTIC_SMART_ADM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHM.Logistic.Smart.Common.Models;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AHM_Libreria.WebUI.Attribute;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class UserController : Controller
    {
        private readonly AccessService _accessService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOptions<UsersViewModel> _usuariosModel;
        private readonly HelpersInterface _helpersServices;
        private readonly CatalogService _catalogService;
        public readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(AccessService accessService,
            IWebHostEnvironment webHostEnvironment,
            IOptions<UsersViewModel> options,
            HelpersInterface helpersInterface,
            CatalogService catalogService,
            IHttpContextAccessor httpContextAccessor)
        {
            _accessService = accessService;
            _webHostEnvironment = webHostEnvironment;
            _usuariosModel = options;
            _helpersServices = helpersInterface;
            _catalogService = catalogService;
            _httpContextAccessor = httpContextAccessor;
        }


        [SessionManager(HelpersUtils.Listado_Usuarios)]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            var model = new List<UsersViewModel>();
            var list = await _accessService.UsersListado(model);
            if (list.Data == null)
            {
                if (!list.Success)
                    TempData["message"] = list.Message;
                var newModel = new List<UsersViewModel>();
                var result = new ServiceResult();
                result.Data = newModel;
                return View(newModel);
            }
            return View(list.Data);
        }

        [HttpGet]
        public async Task<IActionResult> UsersList()
        {
            var model = new List<UsersViewModel>();
            var result = await _accessService.UsersListado(model);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Usuarios)]
        public async Task<IActionResult> CreateUsers()
        {
            var model = new UsersViewModel();
            ViewBag.usu_Id = 0;
            var result = await _accessService.UsersDetails(model.usu_Id, true);
            model = (UsersViewModel)result.Data;
            model.editarUsuario.usu_Profile_picture = HelpersUtils.ImageDefault;
            return View(nameof(EditUsers), model);
        }

        [HttpPost(Name = "ChangePasswordDash")]
        public async Task<IActionResult> ChangePasswordDash(CambiarContraseña model)
        {
            ViewBag.Usu_Id = model.usu_Id;
            UsersModel usersModel = new UsersModel();
            usersModel.usu_Password = model.usu_Password;
            usersModel.per_Id = model.Per_Id;
            usersModel.usu_Id = model.usu_Id;
            var result = await _accessService.EditUsers(usersModel);
            return RedirectToAction("Dashboard", "DashboardList");
        }

        [HttpPost(Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword(CambiarContraseña model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Usu_Id = model.usu_Id;
                UsersModel usersModel = new UsersModel();
                usersModel.usu_Password = model.NewPassword;
                usersModel.per_Id = model.Per_Id;
                usersModel.usu_Id = model.usu_Id;
                var result = await _accessService.EditUsers(usersModel);
                if (result.Data != null && result.Id == 0 && result.Success && result.Type == ServiceResultType.Success)
                {
                    TempData["Success"] = 1;
                    TempData["Type"] = ServiceResultType.Success;
                    TempData["message"] = "Contraseña actualizada exitosamente";
                }
                string previousUrl = Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }
            else
            {
                var result = await _accessService.UsersDetails(model.usu_Id, false);
                UsersViewModel details = (UsersViewModel)result.Data;
                details.CambiarContraseña.NewPassword = model.NewPassword;
                details.CambiarContraseña.ConfirmContraseña = model.ConfirmContraseña;
                details.CambiarContraseña.Per_Id = model.Per_Id;
                details.CambiarContraseña.usu_UserName = model.usu_UserName;
                details.CambiarContraseña.usu_Id = model.usu_Id;
                details.CambiarContraseña.usu_Password = model.usu_Password;
                var changePass = EditDetails(details, true);
                return View(nameof(EditUsers), changePass);

            }
        }

        [HttpPost("Usuario/Acciones")]
        public async Task<IActionResult> EditUsers(EditarUsuario model)
        {
            string imagepath = _usuariosModel.Value.pathUsuariosImage;
            var result = new ServiceResult();
            if (model.usu_Id == 0)
            {
                if (model.ImageFile != null)
                {
                    var resultImage = _helpersServices.UpdateImagenPerfil(model.ImageFile, model.usu_Id, model.usu_UserName, model.usu_Profile_picture, imagepath);
                    UsersModel usersModel = new UsersModel();
                    usersModel.usu_UserName = model.usu_UserName;
                    usersModel.usu_Password = model.usu_Password;
                    usersModel.usu_IdUserCreate = Convert.ToInt32(model.usu_IdUserCreate);
                    usersModel.per_Id = model.Per_Id;
                    usersModel.rol_Id = model.rol_Id;
                    usersModel.usu_Profile_picture = resultImage;
                    result = await _accessService.InsertUsers(usersModel);
                    return Json(result);
                }
                else
                {
                    var imagen = HelpersUtils.ImageDefault;
                    UsersModel usersModel = new UsersModel();
                    usersModel.usu_UserName = model.usu_UserName;
                    usersModel.usu_Password = model.usu_Password;
                    usersModel.usu_IdUserCreate = Convert.ToInt32(model.usu_IdUserCreate);
                    usersModel.per_Id = model.Per_Id;
                    usersModel.rol_Id = model.rol_Id;
                    usersModel.usu_Profile_picture = imagen;
                    result = await _accessService.InsertUsers(usersModel);
                    return Json(result);

                }
            }
            else
            {
                if (model.ImageFile == null)
                {
                    UsersModel usersModel = new UsersModel();
                    usersModel.usu_Id = model.usu_Id;
                    usersModel.usu_UserName = model.usu_UserName;
                    usersModel.usu_Profile_picture = model.usu_Profile_picture;
                    _httpContextAccessor.HttpContext.Session.SetString("usu_ImagenPerfil", usersModel.usu_Profile_picture);
                    if (model.isEditUnique)
                    {
                        if (model.usu_Id == _httpContextAccessor.HttpContext.Session.GetInt32("usu_Id"))
                        {
                            var det = await _accessService.UsersDetails(model.usu_Id, false);
                            UsersViewModel detUs = (UsersViewModel)det.Data;
                            if (detUs.rol_Id != model.rol_Id)
                                return Json(result.Error("No puedes cambiar tu rol desde tu perfil, comunícate con el encargado."));
                        }
                           
                    }
                    usersModel.rol_Id = model.rol_Id;
                    usersModel.per_Id = model.Per_Id;
                    usersModel.usu_IdUserCreate = model.usu_IdUserCreate;
                    usersModel.usu_IdUserModified = model.usu_IdUserModified;
                    result = await _accessService.EditUsers(usersModel);
                    if (usersModel.usu_Id == _httpContextAccessor.HttpContext.Session.GetInt32("usu_Id"))
                    {
                        string permissions = await GetPermissions(usersModel.usu_UserName);
                        _httpContextAccessor.HttpContext.Session.SetString("userpermissions", permissions);
                    }
                    return Json(result);
                }
                else
                {
                    var resultImage = _helpersServices.UpdateImagenPerfil(model.ImageFile, model.usu_Id, model.usu_UserName, model.usu_Profile_picture, imagepath);
                    UsersModel usersModel = new UsersModel();
                    usersModel.usu_Id = model.usu_Id;
                    usersModel.usu_UserName = model.usu_UserName;
                    usersModel.usu_Profile_picture = resultImage;
                    _httpContextAccessor.HttpContext.Session.SetString("usu_ImagenPerfil", usersModel.usu_Profile_picture);
                    if (model.isEditUnique)
                    {
                        if (model.usu_Id != _httpContextAccessor.HttpContext.Session.GetInt32("usu_Id"))
                        {
                            var det = await _accessService.UsersDetails(model.usu_Id, false);
                            UsersViewModel detUs = (UsersViewModel)det.Data;
                            if (detUs.rol_Id != model.rol_Id)
                                return Json(result.Error("No puedes cambiar tu rol desde tu perfil, comunícate con el encargado."));
                        }                            
                    }
                    usersModel.rol_Id = model.rol_Id;
                    usersModel.per_Id = model.Per_Id;
                    usersModel.usu_IdUserCreate = model.usu_IdUserCreate;
                    usersModel.usu_IdUserModified = model.usu_IdUserModified;
                    result = await _accessService.EditUsers(usersModel);
                    if (usersModel.usu_Id == _httpContextAccessor.HttpContext.Session.GetInt32("usu_Id"))
                    {
                        string permissions = await GetPermissions(usersModel.usu_UserName);
                        _httpContextAccessor.HttpContext.Session.SetString("userpermissions", permissions);
                    }
                    return Json(result);
                }
            }

            //var result = await _accessService.InsertUsers(model);
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Usuarios)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            ServiceResult service = new ServiceResult();
            int? usuId = _httpContextAccessor.HttpContext.Session.GetInt32("usu_Id");
            if (Id == usuId)
                return Json(service.Error("No puedes eliminar este registro, porque es tu usuario."));
            else
            {
                var result = await _accessService.DeleteUsers(Id, Mod);
                return Json(result);
            }
        }

        [HttpGet("Usuario/Edit")]
        [SessionManager(HelpersUtils.Actualizar_Usuarios)]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var result = await _accessService.UsersDetails(id, false);
            UsersViewModel details = (UsersViewModel)result.Data;
            var model = EditDetails(details, false);
            model.editarUsuario.isEditUnique = false;
            ViewBag.isEditUnique = false;
            return View(nameof(EditUsers), model);
        }

        [HttpGet("Usuario/EditUnique")]
        public async Task<IActionResult> GetUsuarioUnique(int id)
        {
            int? usuId = _httpContextAccessor.HttpContext.Session.GetInt32("usu_Id");
            if (id != usuId)
                id = (int)usuId;
            var result = await _accessService.UsersDetails(id, false);
            UsersViewModel details = (UsersViewModel)result.Data;
            var model = EditDetails(details, false);
            model.editarUsuario.isEditUnique = true;
            ViewBag.isEditUnique = true;
            return View(nameof(EditUsers), model);
        }

        public UsersViewModel EditDetails(UsersViewModel users, bool getpass)
        {
            List<EmployeesViewModel> emp = new List<EmployeesViewModel>();
            var model = new UsersViewModel();
            ViewBag.usu_Id = users.usu_Id;
            model.editarUsuario.usu_Id = users.usu_Id;
            if (getpass == true)
            {
                model.CambiarContraseña.usu_Password = users.usu_Password;
                model.CambiarContraseña.NewPassword = users.CambiarContraseña.NewPassword;
                model.CambiarContraseña.ConfirmContraseña = users.CambiarContraseña.ConfirmContraseña;
                model.CambiarContraseña.Per_Id = users.CambiarContraseña.Per_Id;
                model.CambiarContraseña.usu_UserName = users.CambiarContraseña.usu_UserName;
                model.CambiarContraseña.usu_Id = users.CambiarContraseña.usu_Id;
            }
            model.CambiarContraseña.usu_Id = users.usu_Id;
            model.CambiarContraseña.Per_Id = users.Per_Id;
            model.CambiarImagen.usu_Id = users.usu_Id;
            if (users.usu_Profile_picture == null)
            {
                model.editarUsuario.usu_Profile_picture = HelpersUtils.ImageDefault;
            }
            else
            {
                model.editarUsuario.usu_Profile_picture = users.usu_Profile_picture;
            }
            model.editarUsuario.usu_UserName = users.usu_UserName;
            model.editarUsuario.usu_Id = users.usu_Id;
            model.editarUsuario.Per_Id = users.Per_Id;
            model.editarUsuario.rol_Id = users.rol_Id;
            ViewBag.rol_Id = users.rol_Id;
            if (users.usu_Status == true)
            { model.editarUsuario.Status = true; }
            else { model.editarUsuario.Status = false; }
            foreach (var item in users.editarUsuario.employeesModel)
            {
                item.per_Firstname = $"{item.per_Firstname} {item.per_LastNames}";
                if (item.per_Id == users.Per_Id)
                    emp.Add(item);
            }
            model.editarUsuario.employeesModel = emp;
            return model;
        }

        public async Task<IActionResult> ShowImage(string routeImage)
        {
            var path = _webHostEnvironment.WebRootFileProvider.GetFileInfo(routeImage)?.PhysicalPath;
            if (path != null)
            {
                routeImage = path;
            }
            var route = routeImage;
            var memoria = new MemoryStream();
            using (var stream = new FileStream(route, FileMode.Open))
            { await stream.CopyToAsync(memoria); }
            memoria.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            var archivoNombre = Path.GetFileName(route);
            return File(memoria, contentType, archivoNombre);
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost("User/Logged")]
        public async Task<IActionResult> UserLogin(LoginModel model)
        {
            var result = await _accessService.Login(model);
            if (!result.Success)
            {
                return Json(result);
            }
            var currenUser = await _accessService.UsersFindByUser(model.usu_UserName);
            UsersViewModel details = (UsersViewModel)currenUser.Data;
            if (details.usu_Temporal_Password == null)
                details.usu_Temporal_Password = false;
            string permissions = await GetPermissions(model.usu_UserName);
            if (details.usu_Temporal_Password == true)
            {
                permissions = "Actualizar usuarios";
            }
            var datos = new
            {
                usu_Id = details.usu_Id,
                per_Id = details.Per_Id,
                isTemporal = details.usu_Temporal_Password,
            };
            if (permissions == "")
            {
                result.Message = "No posees permisos para ingresar, comunícate con el encargado";
                result.Success = false;
                result.Type = ServiceResultType.Error;
                return Json(result);
            }
            _httpContextAccessor.HttpContext.Session.SetString("userpermissions", permissions);
            var userGet = await _accessService.UsersFind(model.usu_UserName);
            UsersViewModel userLogged = (UsersViewModel)userGet.Data;
            if (userLogged.usu_Id == 0)
            {
                result.Message = "Este usuario no existe, comunícate con el encargado";
                result.Success = false;
                result.Type = ServiceResultType.Error;
                return Json(result);
            }
            _httpContextAccessor.HttpContext.Session.SetInt32("usu_Id", userLogged.usu_Id);
            if (userLogged.usu_Profile_picture == null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("usu_ImagenPerfil", HelpersUtils.ImageDefault);
            }
            else
            {
                _httpContextAccessor.HttpContext.Session.SetString("usu_ImagenPerfil", userLogged.usu_Profile_picture);
            }
            if (details.usu_Temporal_Password == true)
            {
                return Json(datos);
            }
            return Json(result);
        }
        public async Task<IActionResult> PasswordRecovery(string correo)
        {
            var result = await _accessService.RecoveryPassWord(correo);
            return Json(result);
        }


        [HttpGet]
        public bool Logout()
        {
            string userspermission = String.Empty;
            string profilePicture = String.Empty;
            _httpContextAccessor.HttpContext.Session.SetString("userpermissions", userspermission);
            _httpContextAccessor.HttpContext.Session.SetInt32("usu_Id", 0);
            _httpContextAccessor.HttpContext.Session.SetString("usu_ImagenPerfil", profilePicture);
            return true;
        }

        public async Task<string> GetPermissions(string username)
        {
            var permissionResult = await _accessService.GetPermissions(username);
            var currenUser = await _accessService.UsersFindByUser(username);
            UsersViewModel details = (UsersViewModel)currenUser.Data;
            List<PermissionsViewModel> permissionData = (List<PermissionsViewModel>)permissionResult.Data;
            List<string> permissionsList = new List<string>();
            foreach (var item in permissionData)
            {
                var permissionitem = item.mit_Descripction;
                permissionsList.Add(permissionitem);
            }
            string permissions = String.Join(",", permissionsList);
            return permissions;
        }

        [HttpPost("User/UserInfo")]
        public async Task<IActionResult> UserInfo(UserInfoModel result)
        {
            var response = await _accessService.InfoUsers(result);
            return Json(response);
        }
    }
}
