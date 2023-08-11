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
    public class RolesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AccessService _accessService;
        public RolesController(AccessService accessService, IMapper mapper)
        {
            _mapper = mapper;
            _accessService = accessService;
        }

        // GET: api/Roles/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Roles".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Roles" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _accessService.ListRoles();
            return Ok(result);
        }

        // GET: api/Components/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Roles".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Roles" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("ComponentsList")]
        public IActionResult ComponentsList()
        {
            var result = _accessService.ListComponents();
            return Ok(result);
        }

        // GET: api/Modules/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Components".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Components" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("ModulesList")]
        public IActionResult ModulesList()
        {
            var result = _accessService.ListModules();
            return Ok(result);
        }

        // GET: api/ModuleItems/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Modules".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Modules" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("ModuleItemsList")]
        public IActionResult ModuleItemsList()
        {
            var result = _accessService.ListModuleItems();
            return Ok(result);
        }

        // GET: api/Roles/Details
        /// <summary>
        /// End point para visualizar los registros de la tabla "ModuleItems".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "ModuleItems" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("Details/{Id}")]
        public IActionResult Details(int Id)
        {
            var IdFind = Id;
            var result = _accessService.FindRoles(x=> x.rol_Id == Id);
            return Ok(result);
        }

        // GET: api/Roles/ModuleItems
        /// <summary>
        /// End point para visualizar los items de los modulos por su rol de la tabla tbRoleModuleItems.
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "tbRoleModuleItems" requieriendo el dato del rol para traer cada item por medio del rol ingresado.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("ModuleItems/{Id}")]
        public IActionResult GetModuleItems(int Id)
        {
            var IdFind = Id;
            var result = _accessService.FindModuleItems(x => x.rol_Id == Id);
            return Ok(result);
        }


        // POST: api/Roles/Insert
        /// <summary>
        /// End point para registrar un rol nuevo.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar al rol.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Insert")]
        public IActionResult Insert(RolModel items)
        {
            var item = new tbRoles()
            { 
                rol_Description = items.rol_Description,
                rol_IdUserCreate = items.rol_IdUserCreate

            };
            var moduleItems = new List<tbRoleModuleItems>();

            foreach (var module in items.roleModuleItems)
            {
                var moduleVal = new tbRoleModuleItems() { mit_Id = module };
                moduleItems.Add(moduleVal);
            }

            var result = _accessService.RegisterRol(item, moduleItems);
            return Ok(result);
        }

        // POST: api/Roles/Update
        /// <summary>
        /// End point para actualizar un rol existente.
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
        public IActionResult Update(int Id, RolModel items)
        {
            var IdUser = Id;
            var item = new tbRoles()
            {
                rol_Id = IdUser,
                rol_Description = items.rol_Description,
                rol_IdUserCreate = items.rol_IdUserCreate,
                rol_IdUserModified = items.rol_IdUserModified

            };
            var moduleItems = new List<tbRoleModuleItems>();

            foreach (var module in items.roleModuleItems)
            {
                var moduleVal = new tbRoleModuleItems() { mit_Id = module };
                moduleItems.Add(moduleVal);
            }
            var result = _accessService.UpdateRol(item, moduleItems);
            return Ok(result);
        }

        // POST: api/Roles/Delete
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
            var result = _accessService.DeleteRol(IdUser, ModUser);
            return Ok(result);
        }

    }
}
