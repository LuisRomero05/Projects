using AHM.Logistic.Smart.Common.Models;
using AHM_Libreria.WebUI.Attribute;
using AHM_LOGISTIC_SMART_ADM.Models;
using AHM_LOGISTIC_SMART_ADM.Services;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomersService _customersService;

        public CustomersController(CustomersService customersService)
        {
            _customersService = customersService;
        }

        [SessionManager(HelpersUtils.Listado_Cliente)]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            var model = new List<CustomerViewModel>();
            var listado = await _customersService.CustomersList(model);
            //if (listado.Data == null)
            //{
            if (!listado.Success)
            {
                TempData["message"] = listado.Message;
                var newModel = new List<CustomerViewModel>();
                var result = new ServiceResult();
                result.Data = newModel;
                return View(newModel);
            }
            return View(listado.Data);
        }

        [HttpGet]
        public async Task<IActionResult> CustomersList()
        {

            var model = new List<CustomerViewModel>();
            var list = await _customersService.CustomersList(model);
            if (!list.Success) { TempData["message"] = list.Message; }
            return Json(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CustomersModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomersModel model)
        {
            var result = await _customersService.InsertCustomers(model);
            if (!result.Success && result.Type == ServiceResultType.Error)
                return Json(result);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _customersService.CustomersDetails(id);
            var customerN = (CustomerViewModel)response.Data;
            var customerC = (CustomerViewModel)response.Data;
            var customerF = (CustomerViewModel)response.Data;
            var customerM = (CustomerViewModel)response.Data;

            customerN.customerNotes = new CustomerNotesModel();
            customerC.customerCalls = new CustomerCallsModel();
            customerF.customerFiles = new CustomerFilesModel();
            customerM.meetings = new MeetingsModel();

            customerN.customerNotes.cus_Id = id;
            customerC.customerCalls.cus_Id = id;
            customerF.customerFiles.cus_Id = id;
            customerM.meetings.cus_Id = id;

            var listFiles = await _customersService.CustomerFileList();
            if(listFiles.Success)customerF.customerFiles.listFile = (List<CustomerFilesModel>)listFiles.Data;

            var listNotes = await _customersService.CustomerNotesList();
            var listPri = await _customersService.PrioritiesList();
            if(listNotes.Success)customerN.customerNotes.notes = (List<CustomerNotesModel>)listNotes.Data;
            if(listPri.Success)customerN.customerNotes.listPriorities = (List<PrioritiesModel>)listPri.Data;

            var listCalls = await _customersService.CustomerCallsList();
            var callTypes = await _customersService.CallTypeList();
            var callBuss = await _customersService.CallBusinessList();
            var callResults = await _customersService.CallResultList();
            if(listCalls.Success)customerC.customerCalls.calls = (List<CustomerCallsModel>)listCalls.Data;
            if(callTypes.Success)customerC.customerCalls.callType = (List<CallTypeModel>)callTypes.Data;
            if(callBuss.Success)customerC.customerCalls.callBusiness = (List<CallBusinessModel>)callBuss.Data;
            if(callResults.Success)customerC.customerCalls.callResult = (List<CallResultModel>)callResults.Data;

            var meetings = await _customersService.MeetingsList();
            if(meetings.Success)customerM.meetings.meetsDetails = (List<MeetingsViewModel>)meetings.Data;

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerViewModel model)
        {
            var result = await _customersService.EditCustomers(model);
            return Json(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id, int Mod)
        {
            var result = await _customersService.DeleteCustomers(Id, Mod);
            if (!result.Success && result.Type == ServiceResultType.Error)
                TempData["message"] = "Se ha perdido la conexión con el servidor";
            return Json(result);
        }


    }
}
