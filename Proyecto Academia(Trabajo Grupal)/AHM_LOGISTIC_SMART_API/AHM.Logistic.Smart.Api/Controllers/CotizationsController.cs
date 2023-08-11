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
    public class CotizationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SalesService _salesServices;
        public CotizationsController(SalesService salesServices, IMapper mapper)
        {
            _mapper = mapper;
            _salesServices = salesServices;
        }

        // GET: api/Cotizations/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Cotizations".
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
            var result = _salesServices.ListCotization();
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
        [HttpGet("CotizationsDetails/{Id}")]
        public IActionResult CotizationsDetails(int Id)
        {
            var result = _salesServices.DetailsCotizations(x => x.cot_Id == Id);
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
        public IActionResult Insert(CotizationsModel items)
        {
            var item = _mapper.Map<tbCotizations>(items);
            var data = _mapper.Map<List<tbCotizationsDetail>>(items.cot_details);

            var obj = new tbCotizations()
            {
                cot_Id = items.cot_Id,
                cus_Id = items.cus_Id,
                sta_Id = items.sta_Id,
                cot_IdUserCreate = items.cot_IdUserCreate,
                cot_IdUserModified = items.cot_IdUserModified,
            };

            foreach (var item2 in data)
            {
                var datas = new tbCotizationsDetail()
                {
                    code_Id = item2.code_Id,
                    code_Cantidad = item2.code_Cantidad,
                    pro_Id = item2.pro_Id,
                    code_TotalPrice = item2.code_TotalPrice,
                    cot_Id = item2.cot_Id,
                    code_IdUserCreate = item2.code_IdUserCreate,
                    code_IdUserModified = item2.code_IdUserModified
                };
            }

            var result = _salesServices.RegisterCotization(item, data);
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
        public IActionResult InsertDetails(List<CotizationsDetailUpdateModel> items)
        {
      
            var data = _mapper.Map<List<tbCotizationsDetail>>(items);
            var result = _salesServices.RegisterCotizationDetails(data);
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
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Update")]
        public IActionResult Update(CotizationsModel items)
        {
            var item = _mapper.Map<tbCotizations>(items);
            var data = _mapper.Map<List<tbCotizationsDetail>>(items.cot_details);
            
            var result = _salesServices.UpdateCotizations(item, data);
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
            var result = _salesServices.DeleteCotizations(Id, Mod);
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
        public IActionResult DeleteDetail(int proId, int Id, int Mod)
        {
            var result = _salesServices.DeleteDetail(proId, Id, Mod);
            return Ok(result);
        }
    }
}
