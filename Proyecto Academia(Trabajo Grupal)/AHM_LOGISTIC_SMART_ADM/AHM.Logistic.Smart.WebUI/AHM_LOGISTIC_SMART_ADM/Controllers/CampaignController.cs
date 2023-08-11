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
    public class CampaignController : Controller
    {
        private readonly SalesService _salesService;

        public CampaignController(SalesService salesService)
        {
            _salesService = salesService;
        }
        [SessionManager(HelpersUtils.Listado_Campañas)]
        public async Task<IActionResult> Index() 
        {
            TempData.Clear();
            var model = new List<CampaignViewModel>();  
            var list = await _salesService.CampaignList(model); 
            if(list.Data == null)
            {
                if (!list.Success)
                    TempData["message"] = list.Message;
                var newModel = new List<CampaignViewModel>();
                return View(newModel);
            }
            return View(list.Data); 
        }

        public IActionResult CampaignList()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _salesService.CampaignDetails(id);
            if (!response.Success) { 
                TempData["message"] = response.Message;
                return RedirectToAction("Index", "Campaign");
            }
            CampaignViewModel campaign = (CampaignViewModel)response.Data;
            ViewBag.Status = campaign.Status;
            return View(campaign);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Campañas)]
        public IActionResult Create()
        {
            var model = new CampaignModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CampaignModel model)
        {
            var result = await _salesService.InsertCampaigns(model);
            if (!result.Success) { TempData["message"] = result.Message; }
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Send(int id)
        {
            var model = await _salesService.CampaignDetails(id);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendCampaign(CampaignDetailsModel model)
        {
            var result = await _salesService.SendCampaign(model);               
            return Json(result);
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Campañas)]
        public async Task<IActionResult> Delete(int Id, int mod)
        {
            var result = await _salesService.DeleteCampaign(Id, mod);
            return Json(result);
        }
    }
}
