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
    public class ProductsController : Controller
    {

        private readonly SalesService _salesService;

        public ProductsController(SalesService salesService)
        {
            _salesService = salesService;
        }

        [SessionManager(HelpersUtils.Listado_Productos)]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            var model = new List<ProductsViewModel>();
            var list = await _salesService.ProductsList(model);
            if (list.Data == null)
            {
                if (!list.Success)
                    TempData["message"] = list.Message;
                var newModel = new List<ProductsViewModel>();
                return View(newModel);
            }
            return View(list.Data);
        }

        public async Task<IActionResult> ProductsList()
        {
            var model = new List<ProductsViewModel>();
            var result = await _salesService.ProductsList(model);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Productos)]
        public IActionResult Create()
        {
            var model = new ProductsModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductsModel model)
        {
            var result = await _salesService.InsertProducts(model);
            if (!result.Success && result.Type == ServiceResultType.Error)
                return Json(result);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Productos)]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _salesService.ProductDetails(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductsViewModel model)
        {
            var result = await _salesService.EditProducts(model);
            if (!result.Success && result.Type == ServiceResultType.Error)
                return Json(result);
            return Json(result);
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Productos)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _salesService.DeleteProducts(Id, Mod);
            if (!result.Success && result.Type == ServiceResultType.Error)
                TempData["message"] = "Se ha perdido la conexión con el servidor";
            return Json(result);
        }
    }
}
