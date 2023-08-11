using AHM.Logistic.Smart.Api.Examples;
using AHM.Logistic.Smart.BusinessLogic;
using AHM.Logistic.Smart.BusinessLogic.Services;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.Entities.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AccessService _accessServices;
        public LoginController(AccessService accessServices, IMapper mapper)
        {
            _mapper = mapper;
            _accessServices = accessServices;
        }

        // POST: api/User/Login
        /// <summary>
        /// End point para logearse.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes para logearse.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Login")]
        [SwaggerRequestExample(typeof(LoginModel), typeof(LoginExample))]
        public IActionResult Login(LoginModel items)
        {
            var item = _mapper.Map<tbUsers>(items);
            var result = _accessServices.Login(item);
            if(result.Code == 500)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
