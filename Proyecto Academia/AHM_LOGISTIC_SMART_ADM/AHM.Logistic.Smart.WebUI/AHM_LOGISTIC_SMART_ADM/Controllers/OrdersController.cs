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
    public class OrdersController : Controller
    {
        private readonly SalesService _salesService;

        public OrdersController(SalesService salesService)
        {
            _salesService = salesService;
        }

        [SessionManager(HelpersUtils.Listado_Ordenes)]
        public async Task<IActionResult> Index()
        {
            var model = new List<SalesOrderViewModel>();
            var listado = await _salesService.OrderList(model);
            if (listado.Data == null)
            {
                var newModel = new List<SalesOrderViewModel>();
                return View(newModel);
            }
            return View(listado.Data);
        }

        public async Task<IActionResult> OrdersList()
        {
            var model = new List<SalesOrderViewModel>();
            var listado = await _salesService.OrderList(model);

            return Json(listado);
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var model = new List<SaleOrdersModel>();
            return View(model);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Ordenes)]
        public IActionResult Create()
        {
            var model = new SaleOrdersModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleOrdersModel model)
        {
            var resultado = await _salesService.InsertOrders(model);
            return RedirectToAction("Index", "Quote");
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Ordenes)]
        public async Task<IActionResult> Edit()
        {
            var model = new List<SaleOrdersModel>();
            return View(model);
        }

        public async Task<IActionResult> OrderDetailsList(int id)
        {
            var listado = await _salesService.OrdersDetails(id);
            return Json(listado);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaleOrdersModel model)
        {
            var result = await _salesService.EditOrders(model);
            return RedirectToAction("Index", "Quote");
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Ordenes)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _salesService.DeleteOrders(Id, Mod);
            return Json(result.Success);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDetailProduct(int Id, int proId, int Mod)
        {
            var result = await _salesService.DeleteOrdersDetails(Id, proId, Mod);
            return Json(result.Success);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrderDetail(SaleOrdersModel model)
        {
            var resultado = await _salesService.InsertOrdersDetail(model.sor_details);
            return RedirectToAction("Index", "Quote");
        }
    }
}
