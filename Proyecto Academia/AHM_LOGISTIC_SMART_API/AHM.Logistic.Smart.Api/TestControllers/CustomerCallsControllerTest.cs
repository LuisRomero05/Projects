using AHM.Logistic.Smart.BusinessLogic.TestService;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.Entities.Entities;
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
    public class CustomerCallsControllerTest : ControllerBase
    {
        public CustomerCallsServices _customerCallsServices = new CustomerCallsServices();
        private readonly IMapper _mapper;

        public CustomerCallsControllerTest(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: api/CustomerCalls/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "CustomerCalls".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "CustomerCalls" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _customerCallsServices.List();
            return Ok(result);
        }
      


        // POST: api/CustomerCalls/Insert
        /// <summary>
        /// End point para registrar un Customer Call nuevo.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar a la Customer Call.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Insert")]
        public IActionResult Insert(CustomerCallsModel items)
        {
            var item = _mapper.Map<tbCustomerCalls>(items);
            var result = _customerCallsServices.Insert(item);
            if (!result.Success)
                return Conflict(result);
            return Ok(result);
        }

        // POST: api/CustomerCalls/Update
        /// <summary>
        /// End point para actualizar una Customer Call existente.
        /// </summary>
        /// <remarks>
        /// Colocar el Id del registro a actualizar y llenar los campos correspondientes.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <param name="id">Id es el parametro mediante el cual se encuentra el registro a actualizar.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPut("Update/{Id}")]
        public IActionResult Update(CustomerCallsModel items, int id)
        {
            var item = _mapper.Map<tbCustomerCalls>(items);
            var result = _customerCallsServices.Update(item, id);
            if (!result.Success)
                return Conflict(result);
            return Ok(result);
        }

        // POST: api/CustomerCalls/Delete
        /// <summary>
        /// End point para eliminar una CustomerCall existente.
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
            var result = _customerCallsServices.Delete(IdUser, ModUser);
            return Ok(result);
        }
    }
}
