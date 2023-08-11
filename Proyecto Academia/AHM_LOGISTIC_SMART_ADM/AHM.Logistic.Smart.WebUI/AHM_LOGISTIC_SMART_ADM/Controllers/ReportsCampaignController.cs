using AHM_Libreria.WebUI.Attribute;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class ReportsCampaignController : Controller
    {
        [SessionManager(HelpersUtils.Reportes_Ordenes)]
        public IActionResult Index()
        {
            return View();
        }

        [SessionManager(HelpersUtils.Reportes_Campañas)]
        public IActionResult CampaignReports()
        {
            return View();
        }

        [SessionManager(HelpersUtils.Reportes_Clientes)]
        public IActionResult CustomersReports()
        {
            return View();
        }

        [SessionManager(HelpersUtils.Reportes_Cotizaciones)]
        public IActionResult QuoteReports()
        {
            return View();
        }
    }
}
