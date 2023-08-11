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
    public class SubCategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SalesService _salesServices;
        public SubCategoriesController(SalesService SalesService, IMapper mapper)
        {
            _mapper = mapper;
            _salesServices = SalesService;
        }


        // POST: api/SubCategories/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "SubCategories".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "SubCategories" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _salesServices.ListSubCategories();
            return Ok(result);
        }

        // GET: api/SubCategories/Details
        /// <summary>
        /// End point para visualizar los registros de la tabla "SubCategories".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "SubCategories" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("Details/{Id}")]
        public IActionResult Details(int Id)
        {
            var IdFind = Id;
            var result = _salesServices.FindSubCategories(x => x.scat_Id == Id);
            return Ok(result);
        }

        // POST: api/SubCategories/Insert
        /// <summary>
        /// End point para registrar una sub-categoria nueva.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar al sub-categories.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Insert")]
        //[SwaggerRequestExample(typeof(CategoryModel), typeof(CategoryExample))]
        public IActionResult Insert(SubCategoriesModel items)
        {
            tbSubCategories sub = new tbSubCategories();
            var item = _mapper.Map<tbSubCategories>(items);
            if (item == null)
            {

                sub.scat_Id = items.scat_Id;
                sub.scat_Description = items.scat_Description;
                sub.cat_Id = items.cat_Id;
                sub.scat_IdUserCreate = (int)items.scat_IdUserCreate;
                sub.scat_IdUserModified = items.scat_IdUserModified;
            }
            var result = _salesServices.RegisterSubCategories(item);
            return Ok(result);
        }

        // POST: api/SubCategories/Update
        /// <summary>
        /// End point para actualizar una sub-categoria existente.
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
        public IActionResult Update(int Id, SubCategoriesModel items)
        {
            tbSubCategories sub = new tbSubCategories();
            var item = _mapper.Map<tbSubCategories>(items);
            if (item == null)
            {

                sub.scat_Id = items.scat_Id;
                sub.scat_Description = items.scat_Description;
                sub.cat_Id = items.cat_Id;
                sub.scat_IdUserCreate = (int)items.scat_IdUserCreate;
                sub.scat_IdUserModified = items.scat_IdUserModified;
            }
            var IdUser = Id;
            var result = _salesServices.UpdateSubCategories(IdUser, item);
            return Ok(result);
        }

        // POST: api/SubCategories/Delete
        /// <summary>
        /// End point para eliminar un sub-categoria existente.
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
            var result = _salesServices.DeleteSubCategories(IdUser, ModUser);
            return Ok(result);
        }
    }
}
