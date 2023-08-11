using AHM.Logistic.Smart.BusinessLogic.TestService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesControllerTest : ControllerBase
    {
        public PrioritiesService _prioritiesService = new PrioritiesService();
        private readonly IMapper _mapper;
        public PrioritiesControllerTest(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _prioritiesService.List();
            return Ok(result);
        }
    }
}
