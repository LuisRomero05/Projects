using AHM.Logistic.Smart.Common.Models;
using AHM_Libreria.WebUI.Attribute;
using AHM_LOGISTIC_SMART_ADM.Services;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class CustomersCallsController : Controller
    {
        private readonly CustomersService _customersService;
        public CustomersCallsController(CustomersService customersService)
        {
            _customersService = customersService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Llamadas_Clientes)]
        public IActionResult Create()
        {
            var model = new CustomerCallsModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCallsModel model)
        {
            int id = model.cus_Id;
            model.cca_IdUserCreate = HttpContext.Session.GetInt32("usu_Id") ?? 1;
            var resultado = await _customersService.InsertCustomerCalls(model);
            if (resultado.Success) TempData["CustomerCalls"] = "0";
            else TempData["CustomerCalls"] = $"{resultado.Message}";
            return RedirectToAction("Edit", "Customers", new { id });

        }

        [SessionManager(HelpersUtils.Listado_Llamadas_Clientes)]
        public async Task<IActionResult> CustomerCallsList()
        {
            var list = await _customersService.CustomerCallsList();
            return Json(list);
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Llamadas_Clientes)]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _customersService.CustomerCallsDetails(Id);
            var model = (CustomerCallsModel)response.Data;
            ServiceResult result = new ServiceResult();
            result = result.Ok();
            if (response.Success && model != null)
            {
                if (model.cus_Id == Convert.ToInt32(TempData.Peek("Cus_Id")))
                {
                    int Mod = HttpContext.Session.GetInt32("usu_Id") ?? 1;
                    result = await _customersService.DeleteCustomerCalls(Id, Mod);
                    TempData["CustomerCalls"] = "1";
                    return Json(result);
                }
            }
            TempData["CustomerCalls"] = "La Lamada que desea eliminar no existe";
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Llamadas_Clientes)]
        public async Task<IActionResult> Edit(int id)
        {
            var res = await _customersService.CustomerCallsDetails(id);
            ServiceResult result = new ServiceResult();
            result = result.Ok();
            CustomerCallsModel model = (CustomerCallsModel)res.Data;
            if (res.Data != null)
            {
                if (model.cus_Id == Convert.ToInt32(TempData.Peek("Cus_Id")))
                {
                    return Json(res);
                }
            }
            TempData["CustomerCalls"] = "La llamada que desea editar no existe";
            return Json(res);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(CustomerCallsModel model)
        {
            model.cca_IdUserModified = HttpContext.Session.GetInt32("usu_Id") ?? 1;
            int id = model.cus_Id;
            var result = await _customersService.EditCustomerCalls(model);
            if (!result.Success)
            {
                TempData["CustomerCalls"] = "Un error ocurrio al actualizar el registro";
                return Json(result);
            }
            TempData["CustomerCalls"] = "2";
            return Json(result);
        }
    }
}
