using AHM.Logistic.Smart.BusinessLogic.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CallBusinessController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ClientsService _clientsService;
        public CallBusinessController(ClientsService clientsService, IMapper mapper)
        {
            _mapper = mapper;
            _clientsService = clientsService;
        }

        // GET: api/ListCallBusiness/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "ListCallBusiness".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "ListCallBusiness" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _clientsService.ListCallBusiness();
            return Ok(result);
        }
    }
}
