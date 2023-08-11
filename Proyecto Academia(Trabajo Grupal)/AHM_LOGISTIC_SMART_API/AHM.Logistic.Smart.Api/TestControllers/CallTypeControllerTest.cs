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
    //[Authorize] <---descomentar para habilitar la autorizacion por token
    public class CallTypeControllerTest : ControllerBase
    {
        public CallTypeServices _callTypeServices = new CallTypeServices();
        private readonly IMapper _mapper;

        public CallTypeControllerTest(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: api/CallType/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "CallType".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "CallType" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _callTypeServices.List();
            return Ok(result);
        }
    }
}
