using AHM.Logistic.Smart.Api.Examples;
using AHM.Logistic.Smart.BusinessLogic.Logger;
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
    public class UserControllerTest : ControllerBase
    {
        public UserService _userService = new UserService();
        public EventLogger _eventLogger = new EventLogger();
        private readonly IMapper _mapper;
        public UserControllerTest(IMapper mapper)
        {
            _mapper = mapper;
        }
        // POST: api/User/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Users".
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar al usuario.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _userService.List();
            return Ok(result);
        }

        // GET: api/User/Find
        /// <summary>
        /// End point para visualizar los registros de la tabla "Users".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Users" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("Find")]
        public IActionResult Find(string username)
        {
            var result = _userService.Find(x => x.usu_UserName == username);
            return Ok(result);
        }

        // GET: api/User/Find
        /// <summary>
        /// End point para visualizar los registros de la tabla "Users".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Users" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("FindByUser")]
        public IActionResult FindByUser(string username)
        {
            var result = _userService.FindUsersByUser(x => x.usu_UserName == username);
            return Ok(result);
        }

        // GET: api/User/Details
        /// <summary>
        /// End point para visualizar los registros de la tabla "Users".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Users" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("Details/{Id}")]
        public IActionResult Details(int Id)
        {
            var IdFind = Id;
            var result = _userService.Find(x => x.usu_Id == Id);
            return Ok(result);
        }

        // POST: api/User/Insert
        /// <summary>
        /// End point para registrar un usuario nuevo.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar al usuario.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Insert")]
        [SwaggerRequestExample(typeof(UsersModel), typeof(UserRegisterExample))]
        public IActionResult Insert(UsersModel items)
        {
            var item = _mapper.Map<tbUsers>(items);
            var result = _userService.Insert(item);
            if (!result.Success)
                return Conflict(result);
            return Ok(result);
        }

        // POST: api/User/Update
        /// <summary>
        /// End point para actualizar un usuario existente.
        /// </summary>
        /// <remarks>
        /// Colocar el Id del registro a actualizar y llenar los campos correspondientes.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <param name="Id">Id es el parametro mediante el cual se encuentra el registro a actualizar.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPut("Update/{Id}")]
        public IActionResult Update(int Id, UsersModel items)
        {
            var item = _mapper.Map<tbUsers>(items);
            var IdUser = Id;
            var result = _userService.Update(item, IdUser);
            if (!result.Success)
                return Conflict(result);
            return Ok(result);
        }

        // POST: api/User/Delete
        /// <summary>
        /// End point para eliminar un usuario existente.
        /// </summary>
        /// <remarks>
        /// Colocar el Id del registro a eliminar y el Id del Usuario que modifico el registro.
        /// </remarks>
        /// <param name="Id">Id es el parametro mediante el cual se encuentra el registro a eliminar.</param>
        /// <param name="Mod">Mod es el Id del Usuario que modifica el registro</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id, int Mod)
        {
            var ModUser = Mod;
            var IdUser = Id;
            var result = _userService.Delete(IdUser, ModUser);
            if (!result.Success)
                return Conflict(result);
            return Ok(result);
        }

        [HttpPost("UserInfo")]
        public IActionResult UserInfo(UserInfoModel result)
        {
            var ip = result.info_Ip.ToString();
            var useragent = result.info_UserAgent.ToString();
            _eventLogger.InfoUser(ip, useragent);
            return Ok(result);
        }
    }
}
