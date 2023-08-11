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
    public class ContactsController : Controller
    {
        private readonly CustomersService _customersService;
        private readonly CatalogService _catalogService;

        public ContactsController(CustomersService trepService, CatalogService catalogService)
        {

            _catalogService = catalogService;
            _customersService = trepService;

        }

        [SessionManager(HelpersUtils.Listado_Contacto)]
        public async Task<IActionResult> Index()
        {
            var model = new List<AHM.Logistic.Smart.Common.Models.ContactsViewModel>();
            var listado = await _customersService.ApiContactsList(model);
            if (listado.Data == null)
            {
                if (!listado.Success)
                    TempData["message"] = listado.Message;
                var newModel = new List<ContactsViewModel>();
                var result = new ServiceResult();
                result.Data = newModel;
                return View(newModel);
            }
            return View(listado.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int Id)
        {
            var result = await _customersService.ContactsDetails(Id);
            return Json(result);
        }

        #region Lists
        [HttpGet]
        public async Task<IActionResult> ContactsList()
        {
            var model = new List<AHM.Logistic.Smart.Common.Models.ContactsViewModel>();
            var result = await _customersService.ApiContactsList(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> OccupationList()
        {
            var model = new List<AHM.Logistic.Smart.Common.Models.OccupationsViewModel>();
            var result = await _catalogService.OccupationsList(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> CustomersList()
        {
            var model = new List<AHM.Logistic.Smart.Common.Models.CustomerViewModel>();
            var result = await _customersService.CustomersList(model);
            return Json(result);
        }
        #endregion

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Contacto)]
        public IActionResult Create()
        {
            var model = new ContactsModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactsModel model)
        {
            var result = await _customersService.InsertContacts(model);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Contacto)]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _customersService.ContactsDetails(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactsViewModel model)
        {
            var result = await _customersService.EditContacts(model);
            return Json(result);
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Contacto)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _customersService.DeleteContacts(Id, Mod);
            if (!result.Success && result.Type == ServiceResultType.Error)
                TempData["message"] = "Se ha perdido la conexión con el servidor";
            return Json(result);
            //var result = await _customersService.DeleteContacts(Id, Mod);
            //return Json(result);
        }
    }
}
