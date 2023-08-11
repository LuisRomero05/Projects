using AHM_LOGISTIC_SMART_ADM.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class CallBusinessController : Controller
    {
        private readonly CustomersService _customersService;
        public CallBusinessController(CustomersService customersService)
        {
            _customersService = customersService;
        }

        [HttpGet]
        public async Task<IActionResult> CallBusinessList()
        {
            var result = await _customersService.CallBusinessList();
            return Json(result);
        }
    }
}
