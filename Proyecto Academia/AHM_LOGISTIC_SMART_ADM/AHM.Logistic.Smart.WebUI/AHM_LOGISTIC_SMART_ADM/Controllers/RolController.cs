using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using AHM.Logistic.Smart.Common.Models;
using System.Threading.Tasks;
using AHM_LOGISTIC_SMART_ADM.Services;
using AHM_Libreria.WebUI.Attribute;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class RolController : Controller
    {
        private readonly AccessService _accessService;

        public RolController(AccessService accessService)
        {
            _accessService = accessService;
        }

        [SessionManager(HelpersUtils.Listado_Roles)]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            var model = new List<RolesViewModel>();
            var listado = await _accessService.RolesList(model);
            if (listado.Data == null)
            {
                if (!listado.Success)
                    TempData["message"] = listado.Message;
                var newModel = new List<RolesViewModel>();
                var result = new ServiceResult();
                result.Data = newModel;
                return View(newModel);
            }
            return View(listado.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DepartList()
        {
            var model = new List<AHM.Logistic.Smart.Common.Models.RolesViewModel>();
            var result = await _accessService.RolesList(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> RolList()
        {
            var model = new List<RolesViewModel>();
            var result = await _accessService.RolesList(model);
            return Json(result);
        }

        public async Task<IActionResult> RolDetail(int id)
        {
            var model = new List<AHM.Logistic.Smart.Common.Models.RolesViewModel>();
            var result = await _accessService.DetailsRoles(id);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Roles)]
        public async Task<IActionResult> Create(string rol)
        {
            var model = new RolesModel();
            var com = new List<ComponentsModel>();
            var mod = new List<ModuleModel>();
            var ite = new List<ModuleItemsModel>();
            var resC = await _accessService.ComponentsList(com);
            var resM = await _accessService.ModuleList(mod);
            var resI = await _accessService.ModuleItemsList(ite);
            var resCList = resC.Data;
            var resMList = resM.Data;
            var resIList = resI.Data;
            model.rol_Description = rol;
            model.LoadTreeViewData((IEnumerable<ComponentsModel>)resCList, (IEnumerable<ModuleModel>)resMList, (IEnumerable<ModuleItemsModel>)resIList
             );
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolesModel rolesModel)
        {
            if (String.IsNullOrEmpty(rolesModel.ModuleItemsInput))
            {
                ServiceResult service = new ServiceResult();
                return Json(service.Error("Debe seleccionar al menos un permiso"));
            }
            var model = new RolModel();
            rolesModel.ParseTreeViewData();
            model.roleModuleItems = rolesModel.ModuleIdList;
            model.rol_Description = rolesModel.rol_Description;
            model.rol_IdUserCreate = rolesModel.rol_IdUserCreate;
            model.rol_Description = rolesModel.rol_Description.Trim();
            var result = await _accessService.InsertRoles(model);
            return Json(result);
        }


        [HttpGet]//actualizar
        [SessionManager(HelpersUtils.Actualizar_Roles)]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.rol_Id = id;
            var model = new RolesModel();
            var com = new List<ComponentsModel>();
            var mod = new List<ModuleModel>();
            var ite = new List<ModuleItemsModel>();
            var resC = await _accessService.ComponentsList(com);
            var resM = await _accessService.ModuleList(mod);
            var resI = await _accessService.ModuleItemsList(ite);
            var resCList = resC.Data;
            var resMList = resM.Data;
            var resIList = resI.Data;
            var response = await _accessService.DetailsRoles(id);
            model = (RolesModel)response.Data;
            model.LoadTreeViewData((IEnumerable<ComponentsModel>)resCList, (IEnumerable<ModuleModel>)resMList, (IEnumerable<ModuleItemsModel>)resIList
            );
            var moduItem = await _accessService.GetModuleItemsList(id);
            List<RoleModuleItemViewModel> moduItemData = (List<RoleModuleItemViewModel>)moduItem.Data;

            model.LoadList(moduItemData.Select(x => x.mit_Id));
            model.ParseTreeViewData();
            return View(model);
        }

        [HttpPost("Edit/roles")]
        public async Task<IActionResult> EditRole(RolesModel rolesModel)
        {
            if (String.IsNullOrEmpty(rolesModel.ModuleItemsInput))
            {
                ServiceResult service = new ServiceResult();
                return Json(service.Error("Debe seleccionar al menos un permiso"));
            }
            rolesModel.ParseTreeViewData();
            var model = new RolModel()
            {
                rol_Id = rolesModel.rol_Id,
                rol_Description = rolesModel.rol_Description,
                rol_IdUserCreate = rolesModel.rol_IdUserCreate,
                rol_IdUserModified = rolesModel.rol_IdUserModified,
                roleModuleItems = rolesModel.ModuleIdList
            };
            var result = await _accessService.EditRoles(model, (int)model.rol_Id);
            return Json(result);
        }

        [HttpDelete]//delete
        [SessionManager(HelpersUtils.Eliminar_Roles)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _accessService.DeleteRol(Id, Mod);
            return Json(result);
        }
    }
}
