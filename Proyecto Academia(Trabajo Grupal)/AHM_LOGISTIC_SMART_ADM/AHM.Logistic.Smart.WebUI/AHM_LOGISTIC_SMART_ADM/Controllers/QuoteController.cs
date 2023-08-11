using AHM.Logistic.Smart.Common.Models;
using AHM_Libreria.WebUI.Attribute;
using AHM_LOGISTIC_SMART_ADM.Services;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Helpers;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class QuoteController : Controller
    {

        private readonly SalesService _salesService;

        public QuoteController(SalesService salesService)
        {
            _salesService = salesService;
        }

        [SessionManager(HelpersUtils.Listado_Cotizaciones)]
        public async Task<IActionResult> Index()
        {
            var model = new List<CotizationsViewModel>();
            var listado = await _salesService.CotizationsList(model);
            if (listado.Data == null)
            {
                var newModel = new List<CotizationsViewModel>();
                return View(newModel);
            }
            return View(listado.Data);

        }

        public async Task<IActionResult> CotizationsList()
        {
            var model = new List<CotizationsViewModel>();
            var listado = await _salesService.CotizationsList(model);

            return Json(listado);
        }

        public async Task<IActionResult> DetailsList(int id)
        {
            var listado = await _salesService.CotizationsDetails(id);
            return Json(listado);
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var model = new List<CotizationsViewModel>();
            return View(model);
        }


        public async Task<IActionResult> ProductsList()
        {
            var model = new List<CotizationsViewModel>();
            var listado = await _salesService.CotizationsList(model);

            return Json(listado);
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateStock(int Id, ProductStockModel model)
        {
            var resultado = await _salesService.EditStock(Id, model);
            return RedirectToAction("Index", "Quote");
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
            return RedirectToAction("Index", "Quote");
        }

        [HttpPost]
        public async Task<IActionResult> InsertCotizationDetail(CotizationsModel model)
        {
            var resultado = await _salesService.InsertCotizationsDetail(model.cot_details);
            return RedirectToAction("Index", "Quote");
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Cotizaciones)]
        public async Task<IActionResult> Edit()
        {
            var model = new List<CotizationsViewModel>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CotizationsModel model)
        {
            var result = await _salesService.EditCotizations(model);
            return RedirectToAction("Index", "Quote");
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Cotizaciones)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _salesService.DeleteCotizations(Id, Mod);
            return Json(result.Success);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDetailProduct(int Id, int IdCot, int Mod)
        {
            var result = await _salesService.DeleteCotizationsDetails(Id, IdCot, Mod);
            return Json(result.Success);
        }
    }
}
