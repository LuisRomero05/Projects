using AHM.Logistic.Smart.Common.Models;
using AHM_LOGISTIC_SMART_ADM.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly CustomersService _customersService;

        public MeetingsController(CustomersService customersService)
        {
            _customersService = customersService;
        }

        [HttpGet]
        public async Task<IActionResult> MeetingsList()
        {
            var result = await _customersService.MeetingsList();
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListCusEmp()
        {
            var result = await _customersService.ListCusEmp();
            return Json(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new MeetingsModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MeetingsModel model)
        {
            model.met_IdUserCreate = HttpContext.Session.GetInt32("usu_Id") ?? 1;
            string mix_Id = string.Empty;//no borrar
            model.met_Title = model.met_Title.Trim();
            model.met_MeetingLink = model.met_MeetingLink.Trim();
            string[] mix_IdPartido;

            foreach (var item in model.met_details)
            {
                mix_Id = item.mix_Id;
                mix_IdPartido = mix_Id.Split(".");
                for (int i = 0; i < mix_IdPartido.Length; i++)
                {
                    if (mix_IdPartido[i] == "0" && i == 1)
                    {
                        item.cus_Id = int.Parse(mix_IdPartido[i - 1]);
                    }
                    else if (mix_IdPartido[i] == "1" && i == 1)
                    {
                        item.emp_Id = int.Parse(mix_IdPartido[i - 1]);
                    }
                    else if (mix_IdPartido[i] == "2" && i == 1)
                    {
                        item.cont_Id = int.Parse(mix_IdPartido[i - 1]);
                    }

                }
            }
            var result = await _customersService.InsertMeeting(model);
            if (result.Success) TempData["CustomerMeetings"] = "Se agendo una nueva reunion";
            else TempData["CustomerMeetings"] = $"{result.Message}";
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDetails(MeetingsDetailUpdateModel model)
        {
            model.mde_IdUserCreate = HttpContext.Session.GetInt32("usu_Id") ?? 1;
            string[] id = model.mix_Id.Split(".");
            if (id[1] == "1")
            {
                model.emp_Id = int.Parse(id[0]);
            }
            else if (id[1] == "0")
            {
                model.cus_Id = int.Parse(id[0]);
            }
            else if (id[1] == "2")
            {
                model.cont_Id = int.Parse(id[0]);
            }
            var result = await _customersService.InsertMeetingDetails(model);
            if (!result.Success) { TempData["message"] = result.Message; }
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _customersService.MeetingsList(id);
            int cus_id = Convert.ToInt32(TempData.Peek("Cus_Id"));
            MeetingsViewModel model = (MeetingsViewModel)response.Data;
            if (response.Success)
            {
                if(model.cus_Id == cus_id) return View(response.Data);
            }
            TempData["CustomerMeetings"] = "La reunion que desea editar no existe";
            return RedirectToAction("Edit", "Customers", new { cus_id });
        }

        [HttpPut]
        public async Task<IActionResult> Edit(MeetingsModel model)
        {
            model.met_Title = model.met_Title.Trim();
            model.met_MeetingLink = model.met_MeetingLink.Trim();
            model.met_IdUserModified = HttpContext.Session.GetInt32("usu_Id") ?? 1;
            var result = await _customersService.EditMeeting(model);
            if (!result.Success) { TempData["message"] = result.Message; }
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _customersService.MeetingsDetails(id);
            if (!result.Success) { TempData["message"] = result.Message; }
            return Json(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            int Mod = HttpContext.Session.GetInt32("usu_Id") ?? 1;
            var resultDetail = await _customersService.MeetingsList(Id);
            ServiceResult result = new ServiceResult();
            result = result.Ok();
            MeetingsViewModel model = (MeetingsViewModel)resultDetail.Data;

            if (resultDetail.Data != null)
            {
                if (model.cus_Id == Convert.ToInt32(TempData.Peek("Cus_Id")))
                {
                    result = await _customersService.DeleteMeetings(Id, Mod);
                    TempData["CustomerMeetings"] = "Eliminado";
                    return Json(result);
                }
            }
            TempData["CustomerMeetings"] = "La reunion que desea eliminar no existe";
            return Json(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDetails(int Id, int Mod)
        {
            var result = await _customersService.DeleteMeetingsDetail(Id, Mod);
            return Json(result);
        }
    }
}
