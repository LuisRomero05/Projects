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
    public class ContactsControllerTest : Controller
    {
        public ContactsService _contactsService = new ContactsService();
        private readonly IMapper _mapper;
        public ContactsControllerTest(IMapper mapper)
        {
            _mapper = mapper;
        }

        // POST: api/Contacts/List
        /// <summary>
        /// End point para visualizar los registros de la tabla "Contacts".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Contacts" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _contactsService.List();
            return Ok(result);
        }
        // GET: api/Contacts/Details
        /// <summary>
        /// End point para visualizar los registros de la tabla "Contacts".
        /// </summary>
        /// <remarks>
        /// Se mostraran todos los registros de la tabla "Contacts" sin requerir de ningun dato a ingresar.
        /// </remarks>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpGet("Details/{Id}")]
        public IActionResult Details(int Id)
        {
            var IdFind = Id;
            var result = _contactsService.Find(x => x.cont_Id == Id);
            return Ok(result);
        }

        // POST: api/Contacts/Insert
        /// <summary>
        /// End point para registrar un contacto nuevo.
        /// </summary>
        /// <remarks>
        /// Se deben ingresar los datos correspondientes con el tipo solicitado. Colocar Id's existentes y que correspondan a lo que se le quiere asignar al contacto.
        /// </remarks>
        /// <param name="items">Items es la entidad que contiene los datos que se solicitan.</param>
        /// <response code="200">Success. Los valores son correctos y se han agregado exitosamente.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JTW de acceso.</response>
        /// <response code="400">Bad Request. La informacion ingresada no es la correcta o el proceso no existe.</response>
        [HttpPost("Insert")]
        //[SwaggerRequestExample(typeof(CategoryModel), typeof(CategoryExample))]
        public IActionResult Insert(ContactsModel items)
        {
            tbContacts contacts = new tbContacts();
            var item = _mapper.Map<tbContacts>(items);
            if (item == null)
            {
                contacts.cont_Id = item.cont_Id;
                contacts.cont_Name = item.cont_Name;
                contacts.cont_LastName = item.cont_LastName;
                contacts.cont_Email = item.cont_Email;
                contacts.cont_Phone = item.cont_Phone;
                contacts.occ_Id = item.occ_Id;
                contacts.cus_Id = item.cus_Id;
                contacts.cont_Status = item.cont_Status;
                contacts.cont_IdUserCreate = item.cont_IdUserCreate;
                contacts.cont_IdUserModified = item.cont_IdUserModified;
                contacts.cont_DateModified = item.cont_DateModified;
            }
            var result = _contactsService.Insert(contacts);
            return Ok(result);
        }


        // POST: api/Contacts/Update
        /// <summary>
        /// End point para actualizar un contacto existente.
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
        public IActionResult Update(tbContacts items,int Id )
        {
            tbContacts contacts = new tbContacts();
            var item = _mapper.Map<tbContacts>(items);
            var IdUser = Id;
            if (item == null)
            {
                contacts.cont_Id = item.cont_Id;
                contacts.cont_Name = item.cont_Name;
                contacts.cont_LastName = item.cont_LastName;
                contacts.cont_Email = item.cont_Email;
                contacts.cont_Phone = item.cont_Phone;
                contacts.occ_Id = item.occ_Id;
                contacts.cus_Id = item.cus_Id;
                contacts.cont_Status = item.cont_Status;
                contacts.cont_IdUserCreate = item.cont_IdUserCreate;
                contacts.cont_IdUserModified = item.cont_IdUserModified;
                contacts.cont_DateModified = item.cont_DateModified;
            }
            var result = _contactsService.Update(contacts, IdUser);
            return Ok(result);
        }

        // POST: api/Contacts/Delete
        /// <summary>
        /// End point para eliminar un contacto existente.
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
            var result = _contactsService.Delete(IdUser, ModUser);
            return Ok(result);
        }
    }
}
