using AHM_LOGISTIC_SMART_ADM.Models;
using AHM_LOGISTIC_SMART_ADM.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHM.Logistic.Smart.Common.Models;
using AHM_Libreria.WebUI.Attribute;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class EmployersController : Controller
    {
        private readonly CatalogService _catalogService;

        public EmployersController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [SessionManager(HelpersUtils.Listado_Empleados)]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            var model = new List<EmployeesViewModel>();
            var list = await _catalogService.EmployeesList(model);
            if (!list.Success)
            {
                if (!list.Success)
                    TempData["message"] = list.Message;

                var newModel = new List<EmployeesViewModel>();
                var result = new ServiceResult();
                result.Data = newModel;
                return View(newModel);
            }
            return View(list.Data);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeesList()
        {
            var model = new List<EmployeesViewModel>();
            var result = await _catalogService.EmployeesList(model);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Empleados)]
        public IActionResult Create()
        {
            var model = new EmployeesModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeesModel model)
        {
            var result = await _catalogService.InsertEmployees(model);
            if (!result.Success && result.Type == ServiceResultType.Error)
                return Json(result);
            return Json(result);
        }


        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Empleados)]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _catalogService.DetailsEmployees(id);
            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EmployeesModel model, int id)
        {
            var result = await _catalogService.EditEmployees(model, id);
            if (!result.Success && result.Type == ServiceResultType.Error)
                return Json(result);
            return Json(result);
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Empleados)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _catalogService.DeleteEmployees(Id, Mod);
            return Json(result);
        }
    }
}
