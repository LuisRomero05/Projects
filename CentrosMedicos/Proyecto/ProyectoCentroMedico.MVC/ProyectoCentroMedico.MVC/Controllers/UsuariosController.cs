using AutoMapper;
using Proyecto.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoCentroMedico.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoCentroMedico.MVC.Atributes;
using Proyecto.Entities.Entities;
using Proyecto.BusinessLogic;
using Proyecto.DataAccess.Repositories;
using System.Linq.Expressions;

namespace ProyectoCentroMedico.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuariosService _usuariosService;
        //private readonly CatalogService _catalogService;
        private readonly RolesRepository _rolesRepository;
        private readonly IMapper _mapper;
        public readonly IHttpContextAccessor _IHttpContextAccessor;
        public UsuariosController(UsuariosService usuariosService,
            //CatalogService catalogService,
            IMapper mapper,
            RolesRepository rolesRepository,
            IHttpContextAccessor HttpContextAccessor)
        {
            _usuariosService = usuariosService;
            //_catalogService = catalogService;
            _mapper = mapper;
            _rolesRepository = rolesRepository;
            _IHttpContextAccessor = HttpContextAccessor;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(UsuariosViewModel item)
        {
            string resultado = _usuariosService.VerificarLogin(item.usu_Email, item.usu_Password);
            if (!string.IsNullOrEmpty(resultado))
            {
                ModelState.AddModelError("", resultado);
                return View(item);
            }

            string permisos = String.Join(",", _usuariosService.ListadoAccesos(item.usu_Email, out string errorMessage));
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("", resultado);
                return View(item);
            }
            _IHttpContextAccessor.HttpContext.Session.SetString("permisosUsuarios", permisos);

            return RedirectToAction("Index", "Home");
        }

        #region Roles
        //listado 
        public IEnumerable<tbRoles> ListadoRoles(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                return _rolesRepository.List();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Enumerable.Empty<tbRoles>();
            }
        }
        public tbRoles FindRol(int id, out string errorMessage)
        {
            var response = new tbRoles();
            errorMessage = string.Empty;
            try
            {
                response = _rolesRepository.Find(id);

            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }
            return response;

        }
        //busqueda generalizada
        public tbRoles FindRol(out string errorMessage, Expression<Func<tbRoles, bool>> expression = null)
        {
            var response = new tbRoles();
            errorMessage = string.Empty;
            try
            {
                response = _rolesRepository.Find(expression);

            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }
            return response;
        }
        #endregion


    }
}
