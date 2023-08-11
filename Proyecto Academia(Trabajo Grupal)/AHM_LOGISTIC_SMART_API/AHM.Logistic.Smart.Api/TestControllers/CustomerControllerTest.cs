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
    //public class CustomerControllerTest : Controller
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] <---descomentar para habilitar la autorizacion por token
    public class CustomerControllerTest : ControllerBase
    {
        public CustomerService _customrerService = new CustomerService();
        private readonly IMapper _mapper;
        public CustomerControllerTest(IMapper mapper)
        {
            _mapper = mapper;
        }
        // GET: api/Areas/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Clientes".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Areas" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _customrerService.List();
            return Ok(result);
        }


        // GET: api/Areas/Details
        /// <summary>
        /// End point para visualizar el registro de la tabla "Clientes" segun el Id ingresado.
        /// </summary>
        /// <remarks>
        /// Se mostrara el registro de la tabla "Areas" segun el Id ingresado.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("Details/{Id}")]
        public IActionResult Details(int Id)
        {
            var IdFind = Id;
            var result = _customrerService.Find(x => x.cus_Id == Id);
            return Ok(result);
        }


        // POST: api/Areas/Insert
        /// <summary>
        /// End point para registrar una Clientes nueva.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar a la Area.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Insert")]
        public IActionResult Insert(CustomersModel items)
        {
            //tbCustomers areas = new tbCustomers();
            //var item = _mapper.Map<tbCustomers>(items);
            //if (item == null)
            //{

            //    areas.cus_Id = items.cus_Id;
            //    areas.cus_AssignedUser = items.cus_AssignedUser;
            //    areas.tyCh_Id = items.tyCh_Id;
            //    areas.cus_Name = items.cus_Name;
            //    areas.cus_RTN = items.cus_RTN;
            //    areas.cus_Address = items.cus_Address;
            //    areas.dep_Id = items.dep_Id;
            //    areas.mun_Id = items.mun_Id;
            //    areas.cus_Email = items.cus_Email;
            //    areas.cus_Phone = items.cus_Phone;
            //    areas.cus_AnotherPhone = items.cus_AnotherPhone;
            //    areas.cus_IdUserCreate = items.cus_IdUserCreate;
            //    areas.cus_IdUserModified = items.cus_IdUserModified;

            //}
            //var result = _customrerService.Insert(areas);
            //return Ok(result);
            var item = _mapper.Map<tbCustomers>(items);
            var result = _customrerService.Insert(item);
            if (!result.Success)
                return Conflict(result);
            return Ok(result);
        }

        // POST: api/Areas/Update
        /// <summary>
        /// End point para actualizar un Clientes existente.
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
        public IActionResult Update(int Id, CustomersModel items)
        {
            //tbCustomers areas = new tbCustomers();
            //var item = _mapper.Map<tbCustomers>(items);
            //var IdUser = Id;
            var item = _mapper.Map<tbCustomers>(items);
            var IdUser = Id;
            //var result = _customrerService.Update(areas, IdUser);
            var result = _customrerService.Update(item, IdUser);
            if (!result.Success)
                return Conflict(result);
            return Ok(result);
        }

        // POST: api/Areas/Delete
        /// <summary>
        /// End point para eliminar un Clientes existente.
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
            var result = _customrerService.Delete(IdUser, ModUser);
            return Ok(result);
        }
    }
}
