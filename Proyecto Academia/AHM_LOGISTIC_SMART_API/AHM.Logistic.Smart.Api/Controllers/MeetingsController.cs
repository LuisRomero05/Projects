using AHM.Logistic.Smart.BusinessLogic.Services;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.Entities.Entities;
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
    public class MeetingsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ClientsService _clientsServices;
        public MeetingsController(ClientsService clientsServices, IMapper mapper)
        {
            _mapper = mapper;
            _clientsServices = clientsServices;
        }

        // GET: api/Cotizations/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Meetings".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Cotizations" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _clientsServices.ListMeeting();
            return Ok(result);
        }


        // GET: api/Cotizations/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Meetings".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Cotizations" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("ListMeetingsDetails")]
        public IActionResult ListMeetingsDetails()
        {
            var result = _clientsServices.ListMeetingDetails();
            return Ok(result);
        }

        // GET: api/Cotizations/ListInv
        /// <summary>
        /// End point para visualizar los registros de la tabla "Customers y Employees".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Customers y Employees" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("ListCusEmp")]
        public IActionResult ListCusEmp()
        {
            var result = _clientsServices.ListInvEmp();
            return Ok(result);
        }

        // GET: api/Cotizations/Details
        /// <summary>
        /// End point para visualizar un registro de la tabla "Cotizations".
        /// </summary>
        /// <remarks>
        /// Se mostrara un registro de la tabla "Cotizations" requiriendo el id del encabezado de dicha cotizacion.        
        /// </remarks>
        /// <param name="Id">Id es el identificador del encabezado de la cotizacion.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("MeetingsDetails/{Id}")]
        public IActionResult MeetingsDetails(int Id)
        {
            var result = _clientsServices.DetailsMeetings(x => x.met_Id == Id && x.mde_Status==true);
            return Ok(result);
        }

        // POST: api/Cotizations/Insert
        /// <summary>
        /// End point para crear una cotizacion.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar a la cotizacion.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Insert")]
        public IActionResult Insert(MeetingsModel items)
        {
            var item = _mapper.Map<tbMeetings>(items);
            var data = _mapper.Map<List<tbMeetingsDetails>>(items.met_details);
            var result = _clientsServices.RegisterMeetings(item, data);
            return Ok(result);
        }

        // POST: api/Cotizations/Insert
        /// <summary>
        /// End point para crear una cotizacion.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar a la cotizacion.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("InsertDetails")]
        public IActionResult InsertDetails(MeetingsDetailUpdateModel items)
        {

            var data = _mapper.Map<tbMeetingsDetails>(items);
            var result = _clientsServices.RegisterMeetingsDetails(data);
            return Ok(result);
        }

        // POST: api/Cotizations/Update
        /// <summary>
        /// End point para actualizar una cotizacion.
        /// </summary>
        /// <remarks>
        /// Actualizar y llenar los campos correspondientes.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// /// <param name="Id">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPut("Update/{Id}")]
        public IActionResult Update(MeetingsModel items, int Id)
        {
            var item = _mapper.Map<tbMeetings>(items);
            var data = _mapper.Map<List<tbMeetingsDetails>>(items.met_details);

            var result = _clientsServices.UpdateMeetings(item, data, Id);
            return Ok(result);
        }

        // POST: api/Cotizations/Delete
        /// <summary>
        /// End point para eliminar una Cotizacion existente.
        /// </summary>
        /// <remarks>
        /// Colocar los campos solicitados para proceder a su eliminacion.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id, int Mod)
        {
            var result = _clientsServices.DeleteMeetings(Id, Mod);
            return Ok(result);
        }

        // POST: api/CotizationsDetail/Delete
        /// <summary>
        /// End point para eliminar una Cotizacion existente.
        /// </summary>
        /// <remarks>
        /// Colocar los campos solicitados para proceder a su eliminacion.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpDelete("DeleteDetail/{Id}")]
        public IActionResult DeleteDetail(int Id, int Mod)
        {
            var result = _clientsServices.DeleteDetail(Id, Mod);
            return Ok(result);
        }

    }
}
