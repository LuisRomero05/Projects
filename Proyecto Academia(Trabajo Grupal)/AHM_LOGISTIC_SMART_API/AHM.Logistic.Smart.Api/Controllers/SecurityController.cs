using AHM.Logistic.Smart.BusinessLogic.Services;
using AHM.Logistic.Smart.Common.Models;
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
    public class SecurityController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly SecurityService _securityServices;
        public SecurityController(SecurityService securityServices, IMapper mapper)
        {
            _mapper = mapper;
            _securityServices = securityServices;
        }

        // GET: api/Screens/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Screens".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Screens" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("ListScreens")]
        public IActionResult ListScreens()
        {
            var result = _securityServices.ListScreens();
            return Ok(result);
        }

        // GET: api/ScreensPerRole/Details
        /// <summary>
        /// End point para visualizar el registro de la tabla "ScreensPerRole" segun el Id ingresado.
        /// </summary>
        /// <remarks>
        /// Se mostrara el registro de la tabla "ScreensPerRole" segun el Id ingresado.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("ScreensPerRole")]
        public IActionResult GetPermissions(string User)
        {
          
            var result = _securityServices.ListGetPermissions(x=>x.usu_UserName == User);
            var permissions = result.Data;
            var permissionsView = new List<PermissionsViewModel>();

            foreach (var module in permissions)
            {
                var moduleItem = new PermissionsViewModel() { mit_Descripction = module.mit_Descripction };
                permissionsView.Add(moduleItem);
            }
            result.Data = permissionsView;
            return Ok(result);
        }

        // GET: api/ScreensPerRole/Details
        /// <summary>
        /// End point para visualizar el registro de la tabla "ScreensPerRole" segun el Id ingresado.
        /// </summary>
        /// <remarks>
        /// Se mostrara el registro de la tabla "ScreensPerRole" segun el Id ingresado.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("RecuperarContraseña")]
        public IActionResult SendContraseña(string correo)
        {

            var result = _securityServices.SendPassword(correo, x => x.per_Email == correo);
            return Ok(result);
        }

    }
}
