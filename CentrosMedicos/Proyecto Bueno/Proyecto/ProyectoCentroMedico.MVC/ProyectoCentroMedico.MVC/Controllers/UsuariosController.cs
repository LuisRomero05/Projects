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
        private readonly CatalogService _catalogService;
        private readonly RolesRepositories _rolesRepository;
        private readonly IMapper _mapper;
        public readonly IHttpContextAccessor _IHttpContextAccessor;
        public UsuariosController(UsuariosService usuariosService,
            CatalogService catalogService,
            IMapper mapper,
            RolesRepositories rolesRepository,
            IHttpContextAccessor HttpContextAccessor)
        {
            _usuariosService = usuariosService;
            _catalogService = catalogService;
            _mapper = mapper;
            _rolesRepository = rolesRepository;
            _IHttpContextAccessor = HttpContextAccessor;
        }

        //listado
        [HttpGet("Usuarios/Listado")]
        [SessionManager(Helpers.IndexUsuario)]
        public IActionResult Index()
        {
            var listado = _usuariosService.Listado(out string erroMessage);
            var listadoMapeado = _mapper.Map<IEnumerable<UsuariosViewModel>>(listado);
            if (!string.IsNullOrEmpty(erroMessage))
                ModelState.AddModelError("", erroMessage);
            return View(listadoMapeado);

        }

        //crear
        [HttpGet("/Usuarios/Crear")]
        [SessionManager(Helpers.InsertUsuarios)]
        public IActionResult Create()
        {
            var rol = new UsuariosViewModel();
            rol.LlenarCbRol(_catalogService.ListadoRoles(out string errorMessage));
            return View(rol);
        }
        [HttpPost("/Usuarios/Crear")]
        public IActionResult Create(UsuariosViewModel item)
        {
            if (ModelState.IsValid)
            {
                var model = new tbUsuarios()
                {
                    usu_ID = item.usu_ID,
                    rol_ID = item.rol_ID,
                    usu_Nombre = item.usu_Nombre,
                    usu_Apellido = item.usu_Apellido,
                    usu_Email = item.usu_Email,
                    usu_Password = item.usu_Password,
                    usu_PasswordSalt = item.usu_PasswordSalt,
                    usu_NumeroTelefono = item.usu_NumeroTelefono,
                    usu_NumeroCelular = item.usu_NumeroCelular

                };
                string mensaje = _usuariosService.InsertarUsuario(model);
                if (!string.IsNullOrEmpty(mensaje))
                    ModelState.AddModelError("", mensaje);
                else
                    return RedirectToAction(nameof(Index));

            }
            return View(item);

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
            _IHttpContextAccessor.HttpContext.Session.SetString("permisosUsuario", permisos);

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
