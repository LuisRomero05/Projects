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
    public class CotizationsController : Controller
    {

        private readonly SalesService _salesService;

        public CotizationsController(SalesService salesService)
        {
            _salesService = salesService;
        }

        [SessionManager(HelpersUtils.Listado_Cotizaciones)]
        public async Task<IActionResult> Index()
        {
            var model = new List<AHM.Logistic.Smart.Common.Models.CotizationsViewModel>();
            var listado = await _salesService.CotizationsList(model);

            return View(listado.Data);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Cotizaciones)]
        public IActionResult Create()
        {
            var model = new CotizationsModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CotizationsModel model)
        {
            var resultado = await _salesService.InsertCotizations(model);
            return RedirectToAction("Index", "quote");
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Cotizaciones)]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _salesService.CotizationsDetails(id);
            return View(response.Data);
        }

        [HttpPost(Name = "SaveCotization")]
        public async Task<IActionResult> Edit(CotizationsModel model)
        {
            var result = await _salesService.EditCotizations(model);
            return RedirectToAction("Index", "quote");
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Cotizaciones)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _salesService.DeleteCotizations(Id, Mod);
            return Json(result.Success);
        }
    }
}
