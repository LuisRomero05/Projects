using Microsoft.AspNetCore.Mvc;
using AHM_LOGISTIC_SMART_ADM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHM.Logistic.Smart.Common.Models;
using AHM_Libreria.WebUI.Attribute;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class OccupationController : Controller
    {

        private readonly CatalogService _catalogService;

        public OccupationController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [SessionManager(HelpersUtils.Listado_Puestos)]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            var model = new List<OccupationsViewModel>();
            var list = await _catalogService.OccupationsList(model);

            if (!list.Success)
            {
                TempData["message"] = list.Message;
                var newModel = new List<OccupationsViewModel>();
                var result = new ServiceResult();
                result.Data = newModel;
                return View(newModel);
            }
            return View(list.Data);
        }

        [HttpGet]
        public async Task<IActionResult> OccupationList()
        {
            var model = new List<OccupationsViewModel>();
            var result = await _catalogService.OccupationsList(model);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Puestos)]
        public IActionResult Create()
        {
            var model = new OccupationsModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OccupationsModel model)
        {
            var resultado = await _catalogService.InsertOccupations(model);
            return Json(resultado);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Puestos)]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _catalogService.OccupationsDetails(id);
            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(OccupationsModel model, int id)
        {
            var result = await _catalogService.EditOccupations(model, id);
            return Json(result);
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Puestos)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _catalogService.DeleteOccupation(Id, Mod);
            return Json(result);
        }
    }
}
