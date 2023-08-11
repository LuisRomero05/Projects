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
    public class CustomersNotesController : Controller
    {
        private readonly CustomersService _customersService;
        public CustomersNotesController(CustomersService customersService)
        {
            _customersService = customersService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Notas_Clientes)]
        public IActionResult Create()
        {
            var model = new CustomerNotesModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerNotesModel model)
        {
            int id = model.cus_Id;
            model.cun_Descripcion = model.cun_Descripcion.Trim();
            ServiceResult result = new ServiceResult();
            result = result.Ok();
            string date = model.cun_ExpirationDate.ToString("yyyy-MM-dd HH:mm:ss");
            if(model.pry_Id == 0 || date == "0001-01-01 00:00:00")
            {
                result.Message = "Seleccione una prioridad valida";
                TempData["CustomerNotes"] = $"{result.Message}";
                return RedirectToAction("Edit", "Customers", new { id });
            }
            model.cun_IdUserCreate = HttpContext.Session.GetInt32("usu_Id") ?? 1;
            var resultado = await _customersService.InsertCustomerNotes(model);
            if (resultado.Success) TempData["CustomerNotes"] = "Se agrego una nueva nota";
            else TempData["CustomerNotes"] = $"{resultado.Message}";
            return RedirectToAction("Edit", "Customers", new { id });
        }

        [SessionManager(HelpersUtils.Listado_Notas_Clientes)]
        public async Task<IActionResult> CustomerNotesList()
        {
            var list = await _customersService.CustomerNotesList();
            return Json(list);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _customersService.CustomerNotesDetails(Id);
            ServiceResult result = new ServiceResult();
            result = result.Ok();
            CustomerNotesModel model = (CustomerNotesModel)response.Data;
            if (response.Data != null)
            {
                if (model.cus_Id == Convert.ToInt32(TempData.Peek("Cus_Id")))
                {
                    int Mod = HttpContext.Session.GetInt32("usu_Id") ?? 1;
                    result = await _customersService.DeleteCustomerNotes(Id, Mod);
                    TempData["CustomerNotes"] = "Eliminado";
                    return Json(result);
                }

            }
            TempData["CustomerNotes"] = "La nota que desea eliminar no existe";
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Notas_Clientes)]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _customersService.CustomerNotesDetails(id);
            ServiceResult result = new ServiceResult();
            result = result.Ok();
            CustomerNotesModel model = (CustomerNotesModel)response.Data;
            if (response.Data != null)
            {
                if (model.cus_Id == Convert.ToInt32(TempData.Peek("Cus_Id")))
                {
                    return Json(response);
                }
            }
            TempData["CustomerNotes"] = "La nota que desea editar no existe";
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(CustomerNotesModel model)
        {
            model.cun_Descripcion = model.cun_Descripcion.Trim();
            ServiceResult result = new ServiceResult();
            result = result.Ok();
            string date = model.cun_ExpirationDate.ToString("yyyy-MM-dd HH:mm:ss");
            if (model.pry_Id == 0 || date == "0001-01-01 00:00:00")
            {
                result.Message = "Seleccione una prioridad valida";
                TempData["CustomerNotes"] = $"{result.Message}";
                return Json(result);
            }
            model.cun_IdUserModified = HttpContext.Session.GetInt32("usu_Id") ?? 1;
            var response = await _customersService.EditCustomerNotes(model);
            TempData["CustomerNotes"] = "Editado";
            return Json(response);
        }
    }
}
