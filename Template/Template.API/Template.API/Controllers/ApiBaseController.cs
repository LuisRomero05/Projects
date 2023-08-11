using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.BusinessLogic;

namespace Template.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult ApiServiceResult(ServiceResult result)
        {
            switch (result.Type)
            {
                case ServiceResultType.info:
                case ServiceResultType.Success:
                case ServiceResultType.Warning:
                    return Ok(result);

                case ServiceResultType.BadRecuest:
                    return BadRequest(result);

                case ServiceResultType.Unauthorize:
                    return Unauthorized(result);

                case ServiceResultType.Forbidden:
                    return StatusCode(403, result);

                case ServiceResultType.NotFound:
                    return NotFound(result);

                case ServiceResultType.NotAcceptable:
                    return StatusCode(406, result);

                case ServiceResultType.Conflict:
                    return Conflict(result);

                case ServiceResultType.Disabled:
                    return StatusCode(410, result);


                default:
                case ServiceResultType.Error:
                    return StatusCode(500, result);
            }
        }
    }
}
