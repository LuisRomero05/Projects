using AHM.Logistic.Smart.Common.Models;
using AHM_Libreria.WebUI.Attribute;
using AHM_LOGISTIC_SMART_ADM.Services;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class AreasController : Controller
    {
        private readonly CatalogService _catalogService;

        public AreasController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [SessionManager(HelpersUtils.Listado_Areas)]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            var model = new List<AreasViewModel>();
            var list = await _catalogService.AreasList(model);

            if (list.Data == null)
            {
                if (!list.Success)
                    TempData["message"] = list.Message;
                var newModel = new List<AreasViewModel>();
                var result = new ServiceResult();
                result.Data = newModel;
                return View(newModel);
            }
            return View(list.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AreasList()
        {
            var model = new List<AreasViewModel>();
            var result = await _catalogService.AreasList(model);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Areas)]
        public IActionResult Create()
        {
            var model = new AreasModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AreasModel model)
        {
            var result = await _catalogService.InsertAreas(model);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Areas)]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _catalogService.AreasDetails(id);
            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(AreasModel model, int id)
        {
            var result = await _catalogService.EditAreas(model, id);
            return Json(result);
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Areas)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _catalogService.DeleteAreas(Id, Mod);
            return Json(result);
        }
    }
}
