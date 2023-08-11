using AHM.Logistic.Smart.Common.Models;
using AHM_Libreria.WebUI.Attribute;
using AHM_LOGISTIC_SMART_ADM.Models;
using AHM_LOGISTIC_SMART_ADM.Services;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class PersonsController : Controller
    {
        private readonly CatalogService _catalogService;

        public PersonsController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [SessionManager(HelpersUtils.Listado_Personas)]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            var model = new List<PersonsViewModel>();
            var list = await _catalogService.PersonsList(model);
            if (list.Data == null)
            {
                if (!list.Success)
                    TempData["message"] = list.Message;
                var newModel = new List<PersonsViewModel>();
                return View(newModel);
            }
            return View(list.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int Id)
        {
            var result = await _catalogService.PersonsDetails(Id);
            return Json(result);
        }

        #region Lists
        [HttpGet]
        public async Task<IActionResult> PersonsList()
        {
            var model = new List<AHM.Logistic.Smart.Common.Models.PersonsViewModel>();
            var result = await _catalogService.PersonsList(model);
            return Json(result);
        }
        #endregion

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Personas)]
        public IActionResult Create()
        {
            var model = new PersonsModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonsModel model)
        {
            var result = await _catalogService.InsertPersons(model);
            if (!result.Success && result.Type == ServiceResultType.Error)
                return Json(result);
            return Json(result);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Actualizar_Personas)]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _catalogService.PersonsDetails(id);
            if (!response.Success)
            {
                TempData["message"] = response.Message;
                return RedirectToAction("Index", "Persons");
            }
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonsViewModel model)
        {
            var result = await _catalogService.EditPersons(model);
            if (!result.Success && result.Type == ServiceResultType.Error)
                return Json(result);
            return Json(result);
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Personas)]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _catalogService.DeletePersons(Id, Mod);
            if (!result.Success && result.Type == ServiceResultType.Error)
                TempData["message"] = "Se ha perdido la conexión con el servidor";
            return Json(result);
        }
    }
}
