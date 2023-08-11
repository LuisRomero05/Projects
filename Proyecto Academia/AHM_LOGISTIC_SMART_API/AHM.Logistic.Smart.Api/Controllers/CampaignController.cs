using AHM.Logistic.Smart.BusinessLogic;
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
    public class CampaignController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly SalesService _salesServices;
        public CampaignController(SalesService salesServices, IMapper mapper)
        {
            _mapper = mapper;
            _salesServices = salesServices;
        }

        // GET: api/Campaign/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Campaign".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Campaign" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _salesServices.ListCampaigns();
            return Ok(result);
        }

        // GET: api/CampaignDetails/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "CampaignDetail;s".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "CampaignDetails" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("ListCampaignDetails")]
        public IActionResult ListCampaignDetails()
        {
            var result = _salesServices.ListCampaignsDetails();
            return Ok(result);
        }

        // GET: api/CampaignFilter/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "CampaignFilter".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "CampaignFilter" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("DetailsCampaign/{Id}")]
        public IActionResult DetailsCampaigns(int Id)
        {
            var result = _salesServices.FindCampaigns(x => x.cam_Id == Id);
            return Ok(result);
        }

        // GET: api/CampaignDetails/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "CampaignDetails".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "CampaignDetails" sin requerir de ningun dato a ingresar.        
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("SendedCampaigns/{Id}")]
        public IActionResult Details(int Id)
        {
            var result = _salesServices.ListCampaignsDetails(x => x.cam_Id == Id);
            return Ok(result);
        }
        // POST: api/Campaign/Insert
        /// <summary>
        /// End point para registrar una Campaign nueva.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar a la Campaign.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Insert")]
        public IActionResult Insert(CampaignModel items)
        {
            var item = _mapper.Map<tbCampaign>(items);
            var result = _salesServices.RegisterCampaigns(item);
            return Ok(result);
        }

        // POST: api/SendCampaign/Insert
        /// <summary>
        /// End point para madnar una Campaign nueva.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar a la Campaign.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("SendCampaign")]
        public IActionResult Insert(CampaignDetailsModel items)
        {
            var item = _mapper.Map<tbCampaignDetails>(items);
            var result = new ServiceResult();
           
            Task.Factory.StartNew(() =>
            {
                result = _salesServices.SendCampaigns(item);

            });
            return Ok(result);
        }

        // POST: api/Campaign/Delete
        /// <summary>
        /// End point para eliminar un Area existente.
        /// </summary>
        /// <remarks>
        /// Colocar el Id del registro a eliminar y el Id del Usuario que modifico el registro.
        /// </remarks>
        /// <param name="Id">Id es el parametro mediante el cual se encuentra el registro a eliminar.</param>
        /// <param name="Mod">Mod es el Id del Usuario que modifica el registro</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpDelete("DeleteCampaign/{Id}")]
        public IActionResult Delete(int Id)
        {

            var IdUser = Id;
            var result = _salesServices.DeleteCampaign(IdUser);
            return Ok(result);
        }


    }
}
