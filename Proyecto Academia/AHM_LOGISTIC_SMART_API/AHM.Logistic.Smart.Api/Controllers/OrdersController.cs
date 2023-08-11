using AHM.Logistic.Smart.BusinessLogic.Services;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.Entities.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SalesService _salesServices;
        public OrdersController(SalesService salesServices, IMapper mapper)
        {
            _mapper = mapper;
            _salesServices = salesServices;
        }

        // GET: api/SaleOrders/Details
        /// <summary>
        /// End point para visualizar los registros de la tabla "SaleOrders".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "SaleOrders" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _salesServices.ListSaleOrder();
            return Ok(result);
        }

        // GET: api/SaleOrders/Details
        /// <summary>
        /// End point para visualizar un registro de la tabla "SaleOrders y OrderDetails".
        /// </summary>
        /// <remarks>
        /// Se mostrara un registro de la tabla "SaleOrders y sus detalles" requiriendo el id del encabezado de dicha orden.        
        /// </remarks>
        /// <param name="Id">Id es el identificador del encabezado de la orden.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("SaleOrdersDetails/{Id}")]
        public IActionResult SaleOrdersDetails(int Id)
        {

            var result = _salesServices.DetailsSalesOrder(x => x.sor_Id == Id);
            return Ok(result);
        }

        // POST: api/SaleOrders/Insert
        /// <summary>
        /// End point para crear una orden de venta
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar a la orden de venta.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Insert")]
        public IActionResult Insert(SaleOrdersModel items)
        {
            var item = _mapper.Map<tbSaleOrders>(items);
            var data = _mapper.Map<List<tbOrderDetails>>(items.sor_details);
            var obj = new tbSaleOrders()
            {
                sor_Id = items.sor_Id,
                cus_Id = items.cus_Id,
                cot_Id = items.cot_Id,
                sta_Id = items.sta_Id,
                sor_IdUserCreate = items.sor_IdUserCreate,
                sor_IdUserModified = items.sor_IdUserModified
            };
            foreach (var item2 in data)
            {
                var datas = new tbOrderDetails()
                {
                    ode_Id = item2.ode_Id,
                    ode_Amount = item2.ode_Amount,
                    pro_Id = item2.pro_Id,
                    ode_TotalPrice = item2.ode_TotalPrice,
                    sor_Id = item2.sor_Id,
                    ode_IdUserCreate = item2.ode_IdUserCreate,
                    ode_IdUserModified = item2.ode_IdUserModified
                };
            }
            var result = _salesServices.RegisterSaleOrder(item, data);
            return Ok(result);
        }

        // POST: api/SalesOrder/Insert
        /// <summary>
        /// End point para crear una Orden de Ventas.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar a la cotizacion.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("InsertDetails")]
        public IActionResult InsertDetails(List<OrderDetailsUpdateModel> items)
        {

            var data = _mapper.Map<List<tbOrderDetails>>(items);
            var result = _salesServices.RegisterOrdersDetails(data);
            return Ok(result);
        }

        // POST: api/SaleOrders/Update
        /// <summary>
        /// End point para actualizar una orden de venta
        /// </summary>
        /// <remarks>
        /// Actualizar y llenar los campos correspondientes.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Update")]
        public IActionResult Update(SaleOrdersModel items)
        {
            var item = _mapper.Map<tbSaleOrders>(items);
            var data = _mapper.Map<List<tbOrderDetails>>(items.sor_details);
            var result = _salesServices.UpdateSaleOrder(item, data);
            return Ok(result);
        }

        // POST: api/SaleOrders/Delete
        /// <summary>
        /// End point para eliminar una orden de venta existente.
        /// </summary>
        /// <remarks>
        /// Colocar los campos solicitados para proceder a su eliminacion.
        /// </remarks>
        /// <param name="Id">Id es el id del registro que sera eliminado.</param>
        /// <param name="Mod">Mod es el id del usuario que esta modificando el registro.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id, int Mod)
        {
            var result = _salesServices.DeleteSaleOrder(Id, Mod);
            return Ok(result);
        }

        // POST: api/OrdersDetail/Delete
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
        public IActionResult DeleteDetail(int Id, int proId, int Mod)
        {
            var result = _salesServices.DeleteOrdersDetail(Id, proId, Mod);
            return Ok(result);
        }
    }
}
