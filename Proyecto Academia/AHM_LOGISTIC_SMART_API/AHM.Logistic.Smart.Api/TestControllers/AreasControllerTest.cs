using AHM.Logistic.Smart.BusinessLogic.Services;
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
    public class AreasControllerTest : ControllerBase
    {
        public AreaService _areaService = new AreaService();
        private readonly IMapper _mapper;
        public AreasControllerTest(IMapper mapper)
        {
            _mapper = mapper;
        }
        // GET: api/Areas/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Areas".
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
            var result = _areaService.List();
            return Ok(result);
        }


        // GET: api/Areas/Details
        /// <summary>
        /// End point para visualizar el registro de la tabla "Areas" segun el Id ingresado.
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
            var result = _areaService.Find(x => x.are_Id == Id);
            return Ok(result);
        }


        // POST: api/Areas/Insert
        /// <summary>
        /// End point para registrar una Area nueva.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar a la Area.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Insert")]
        public IActionResult Insert(AreasModel items)
        {
            tbAreas areas = new tbAreas();
            var item = _mapper.Map<tbAreas>(items);
            if(item == null)
            {

                areas.are_Id = items.are_Id;
                areas.are_Description = items.are_Description;
                areas.are_IdUserCreate = items.are_IdUserCreate;
                areas.are_IdUserModified = items.are_IdUserModified;
            }
            var result = _areaService.Insert(areas);
            return Ok(result);
        }

        // POST: api/Areas/Update
        /// <summary>
        /// End point para actualizar un Area existente.
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
        public IActionResult Update(int Id, AreasModel items)
        {
            tbAreas areas = new tbAreas();
            var item = _mapper.Map<tbAreas>(items);
            var IdUser = Id;
            if (item == null)
            {

                areas.are_Id = items.are_Id;
                areas.are_Description = items.are_Description;
                areas.are_IdUserCreate = items.are_IdUserCreate;
                areas.are_IdUserModified = items.are_IdUserModified;
            }           
            var result = _areaService.Update(areas, IdUser);
            return Ok(result);
        }

        // POST: api/Areas/Delete
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
        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id, int Mod)
        {
            var ModUser = Mod;
            var IdUser = Id;
            var result = _areaService.Delete(IdUser, ModUser);
            return Ok(result);
        }
    }
}

