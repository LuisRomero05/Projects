using AHM.Logistic.Smart.Api.Examples;
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
    public class ProductsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly SalesService _salesServices;
        public ProductsController(SalesService SalesService, IMapper mapper)
        {
            _mapper = mapper;
            _salesServices = SalesService;
        }

        // POST: api/Products/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Products".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Products" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _salesServices.ListProducts();
            return Ok(result);
        }

        // GET: api/Products/Details
        /// <summary>
        /// End point para visualizar el registro de la tabla "Products" segun el Id ingresado.
        /// </summary>
        /// <remarks>
        /// Se mostrara el registro de la tabla "Products" segun el Id ingresado.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("Details/{Id}")]
        public IActionResult Details(int Id)
        {
            var IdFind = Id;
            var result = _salesServices.FindProducts(x => x.pro_Id == Id);
            return Ok(result);
        }

        // POST: api/Products/Insert
        /// <summary>
        /// End point para registrar un producto nuevo.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar al producto.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Insert")]
     //   [SwaggerRequestExample(typeof(ProductsModel), typeof(RolRegisterExample))]
        public IActionResult Insert(ProductsModel items)
        {
            var item = _mapper.Map<tbProducts>(items);
            var result = _salesServices.RegisterProducts(item);
            return Ok(result);
        }

        // POST: api/Products/Update
        /// <summary>
        /// End point para actualizar un producto existente.
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
        public IActionResult Update(int Id, ProductsModel items)
        {
            var item = _mapper.Map<tbProducts>(items);
            var IdUser = Id;
            var result = _salesServices.UpdateProducts(IdUser, item);
            return Ok(result);
        }

        // POST: api/Products/StockUpdate
        /// <summary>
        /// End point para actualizar el stock de un producto existente.
        /// </summary>
        /// <remarks>
        /// Colocar el Id del registro a actualizar y llenar los campos correspondientes.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <param name="Id">Id es el parametro mediante el cual se encuentra el registro a actualizar.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("StockUpdate/{Id}")]
        public IActionResult StockUpdate(int Id, ProductStockModel items)
        {
            var item = _mapper.Map<tbProducts>(items);
            var IdUser = Id;
            var result = _salesServices.UpdateStockProducts(IdUser, item);
            return Ok(result);
        }


        // POST: api/Products/Delete
        /// <summary>
        /// End point para eliminar un roles existente.
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
            var result = _salesServices.DeleteProducts(IdUser, ModUser);
            return Ok(result);
        }

    }
}
