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
    public class DashboardController : Controller
    {
        private readonly CatalogService _catalogService;

        public DashboardController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }


        public IActionResult Index()
        {
            return View();
        }
        [SessionManager(HelpersUtils.Inicio)]
        public IActionResult DashboardList()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DashBoardGetMetrics(int UserId)
        {
            var result = await _catalogService.Dashboard(UserId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> LastCotizations(int UserId)
        {
            var model = new List<CotizationsViewModel>();
            var result = await _catalogService.LastCotizations(model, UserId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> LastSales(int UserId)
        {
            var model = new List<SalesOrderViewModel>();
            var result = await _catalogService.LastSales(model, UserId);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _catalogService.TopProducts();
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _catalogService.TopCustomers();
            return Json(result);
        }


    }
}
