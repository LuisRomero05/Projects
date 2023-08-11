using AHM_LOGISTIC_SMART_ADM.Models;
using AHM_LOGISTIC_SMART_ADM.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHM.Logistic.Smart.Common.Models;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;
using AHM_Libreria.WebUI.Attribute;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class DepartController : Controller
    {   

        private readonly CatalogService _catalogService;

        public DepartController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [SessionManager(HelpersUtils.Listado_Departamento)]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            var model = new List<DepartmentsViewModel>();
            var listado = await _catalogService.ApiDepartmentsList(model);

            if (listado.Data == null)
            {
                if (!listado.Success) 
                TempData["message"] = listado.Message;

                var newModel = new List<DepartmentsViewModel>();
                var result = new ServiceResult();
                result.Data = newModel;
                return View(newModel);
            }
            return View(listado.Data);
        }

        [HttpGet]//listado
        public async Task<IActionResult> DepartList()
        {
            var model = new List<DepartmentsViewModel>();
            var result = await _catalogService.ApiDepartmentsList(model);
            return Json(result);
        }

        [HttpGet]//insertar
        [SessionManager(HelpersUtils.Registro_Departamento)]
        public IActionResult Create()
        {
            var model = new DepartmentsModel();
            return View(model);
        }

        [HttpPost]//crear
        public async Task<IActionResult> Create(DepartmentsModel model)
        {
            var resultado = await _catalogService.InsertDepartments(model);
            return Json(resultado);
        }


        [HttpGet]//actualizar
        [SessionManager(HelpersUtils.Actualizar_Departamento)]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _catalogService.DetailsDepartments(id);
            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(DepartmentsModel model, int id)
        {
            var result = await _catalogService.EditDepartments(model, id);
            return Json(result);
        }


        [HttpDelete]//delete
        [SessionManager(HelpersUtils.Eliminar_Departamento)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _catalogService.DeleteDepartments(Id, Mod);
            return Json(result);
        }
    }
}
