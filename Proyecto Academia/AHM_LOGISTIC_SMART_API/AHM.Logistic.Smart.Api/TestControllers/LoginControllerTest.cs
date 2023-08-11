using AHM.Logistic.Smart.Api.Examples;
using AHM.Logistic.Smart.BusinessLogic.TestService;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.Entities.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginControllerTest : ControllerBase
    {
        public LoginService _loginService = new LoginService();
        private readonly IMapper _mapper;
        public LoginControllerTest(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpPost("Login")]
        [SwaggerRequestExample(typeof(LoginModel), typeof(LoginExample))]
        public IActionResult Login(LoginModel items)
        {
            var item = _mapper.Map<tbUsers>(items);
            var result = _loginService.Login(item);
            if (result.Code == 500)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
