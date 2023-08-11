using AHM.Logistic.Smart.BusinessLogic.Services;
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
    public class DashboardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CatalogService _catalogServices;
        public DashboardController(CatalogService catalogServices, IMapper mapper)
        {
            _mapper = mapper;
            _catalogServices = catalogServices;
        }

        // GET: api/Dashboard/Metrics
        /// <summary>
        /// End point para desplegar las metricas de los card del dashboard
        /// </summary>
        /// <remarks>
        /// Colocar el Id del usuario para traer su data correspondiente.
        /// </remarks>
        /// <param name="Id">Id es el parametro mediante el cual se encuentran los registros.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("DashBoardMetrics/{Id}")]
        public IActionResult DashBoardMetrics(int Id)
        {
            var IdUser = Id;
            var result = _catalogServices.DashboardMetrics(IdUser);
            return Ok(result);
        }

        // GET: api/Dashboard/TopMetrics
        /// <summary>
        /// End point para desplegar los mejores 10 productos
        /// </summary>
        /// <remarks>
        /// Colocar el Id del usuario para traer su data correspondiente.
        /// </remarks>
        /// <param name="Id">Id es el parametro mediante el cual se encuentran los registros.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("TopProducts")]
        public IActionResult TopProducts()
        {
            var result = _catalogServices.TopProducts();
            return Ok(result);
        }

        // GET: api/Dashboard/TopCustomers
        /// <summary>
        /// End point para desplegar los 3 clientes con mas ordenes de venta
        /// </summary>
        /// <remarks>
        /// Mandar a llamar para recolectar la informacion
        /// </remarks>
        /// <param name="Id">Id es el parametro mediante el cual se encuentran los registros.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("TopCustomers")]
        public IActionResult TopCustomers()
        {
            var result = _catalogServices.TopCustomers();
            return Ok(result);
        }

        // POST: api/Dashboard/LastCotis
        /// <summary>
        /// End point para desplegar las ultimas 5 cotizaciones en el dashboard
        /// </summary>
        /// <remarks>
        /// Colocar el Id del usuario para traer su data correspondiente.
        /// </remarks>
        /// <param name="Id">Id es el parametro mediante el cual se encuentran los registros.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("LastCotizations/{Id}")]
        public IActionResult LastCotizations(int Id)
        {
            var IdUser = Id;
            var result = _catalogServices.LastCotizations(IdUser);
            return Ok(result);
        }

        // POST: api/Dashboard/LastSales
        /// <summary>
        /// End point para desplegar las ultimas 5 ventas en el dashboard
        /// </summary>
        /// <remarks>
        /// Colocar el Id del usuario para traer su data correspondiente.
        /// </remarks>
        /// <param name="Id">Id es el parametro mediante el cual se encuentran los registros.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("LastSales/{Id}")]
        public IActionResult LastSales(int Id)
        {
            var IdUser = Id;
            var result = _catalogServices.LastSales(IdUser);
            return Ok(result);
        }
    }
}
