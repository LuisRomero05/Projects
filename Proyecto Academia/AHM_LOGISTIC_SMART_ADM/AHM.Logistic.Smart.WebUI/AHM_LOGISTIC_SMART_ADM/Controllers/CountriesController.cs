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
    public class CountriesController : Controller
    {
        private readonly CatalogService _catalogService;

        public CountriesController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [SessionManager(HelpersUtils.Listado_Paises)]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            var model = new List<CountryViewModel>();
            var list = await _catalogService.CountriesList(model);

            if (!list.Success)
            {
                TempData["message"] = list.Message;

                var newModel = new List<CountryViewModel>();
                var result = new ServiceResult();
                result.Data = newModel;
                return View(newModel);
            }
            return View(list.Data);
        }

        [HttpGet]
        public async Task<IActionResult> CountriesList()
        {
            var model = new List<CountryViewModel>();
            var result = await _catalogService.CountriesList(model);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Paises)]
        public IActionResult Create()
        {
            var model = new CountryModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryModel model)
        {
            var resultado = await _catalogService.InsertCountries(model);
            return Json(resultado);
        }


        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Paises)]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _catalogService.DetailsCountries(id);

            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(CountryModel model, int id)
        {
            var result = await _catalogService.EditCountries(model, id);
            return Json(result);
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Paises)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _catalogService.DeleteCountries(Id, Mod);
            return Json(result);
        }
    }
}
