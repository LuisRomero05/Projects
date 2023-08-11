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
    public class CustomerNotesController : ControllerBase
        {
            private readonly IMapper _mapper;
            private readonly ClientsService _clientsServices;
            public CustomerNotesController(ClientsService clientsServices, IMapper mapper)
            {
                _mapper = mapper;
                _clientsServices = clientsServices;
            }


            // GET: api/CustomerNotes/List
            /// <summary>
            /// End point para visualizar los registros de la tabla "CustomerNotes".
            /// </summary>
            /// <remarks>
            /// Se mostraran todos los registros de la tabla "CustomerNotes" sin requerir de ningun dato a ingresar.        
            /// </remarks>
            /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
            /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
            /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
            [HttpGet("List")]
            public IActionResult List()
            {
                var result = _clientsServices.ListCustomersNotes();
                return Ok(result);
            }


            // GET: api/CustomerNotes/Details
            /// <summary>
            /// End point para visualizar el registro de la tabla "CustomerNotes" segun el Id ingresado.
            /// </summary>
            /// <remarks>
            /// Se mostrara el registro de la tabla "CustomerNotes" segun el Id ingresado.
            /// </remarks>
            /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
            /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
            /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
            [HttpGet("Details/{Id}")]
            public IActionResult Details(int Id)
            {
                var result = _clientsServices.FindCustomersNotes(x => x.cun_Id == Id);
                return Ok(result);
            }


            // POST: api/CustomerNotes/Insert
            /// <summary>
            /// End point para registrar un Customer Notes nuevo.
            /// </summary>
            /// <remarks>
            /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar a la Customer Notes.
            /// </remarks>
            /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
            /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
            /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
            /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
            [HttpPost("Insert")]
            public IActionResult Insert(CustomerNotesModel items)
            {
                var item = _mapper.Map<tbCustomerNotes>(items);
                var result = _clientsServices.RegisterCustomersNotes(item);
                return Ok(result);
            }

            // POST: api/CustomerNotes/Update
            /// <summary>
            /// End point para actualizar una Customer Notes existente.
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
            public IActionResult Update(int Id, CustomerNotesModel items)
            {
                var item = _mapper.Map<tbCustomerNotes>(items);
                var IdUser = Id;
                var result = _clientsServices.UpdateCustomersNotes(IdUser, item);
                return Ok(result);
            }

            // POST: api/CustomerNotes/Delete
            /// <summary>
            /// End point para eliminar una CustomerNotes existente.
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
                var result = _clientsServices.DeleteCustomersNotes(IdUser, ModUser);
                return Ok(result);
            }
        }
    
}
