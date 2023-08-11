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
    public class MuniController : Controller
    {
        private readonly CatalogService _catalogService;
        public MuniController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [SessionManager(HelpersUtils.Listado_Municipios)]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            var model = new List<MunicipalitiesViewModel>();
            var listado = await _catalogService.MunicipalitiesList(model);
            if (!listado.Success) { TempData["message"] = listado.Message; }
            if (listado.Data == null)
            {
                if (!listado.Success) 
                 TempData["message"] = listado.Message; 

                var newModel = new List<MunicipalitiesViewModel>();
                var result = new ServiceResult();
                return View(newModel);
            }
            return View(listado.Data);
        }

        [HttpGet]//listado ajax
        public async Task<IActionResult> MuniList()
        {
            var model = new List<MunicipalitiesViewModel>();
            var result = await _catalogService.MunicipalitiesList(model);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Municipios)]
        public IActionResult Create()
        {
            var model = new MunicipalitiesModel();
            return View(model);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Municipios)]
        public async Task<IActionResult> Detail(int Id)
        {
            var result = await _catalogService.MunicipalitiesDetails(Id);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MunicipalitiesModel model)
        {
            var result = await _catalogService.MunicipalitiesInsert(model);
            if (!result.Success) { TempData["message"] = result.Message; }
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int Id, MunicipalitiesModel model)
        {
            var result = await _catalogService.MunicipalitiesEdit(model, Id);
            if (!result.Success) { TempData["message"] = result.Message; }
            return Json(result);
        }


        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Municipios)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _catalogService.MunicipalitiesDelete(Id, Mod);
            return Json(result);
        }
    }
}
