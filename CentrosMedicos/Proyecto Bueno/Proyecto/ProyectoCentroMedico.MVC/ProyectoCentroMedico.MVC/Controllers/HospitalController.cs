using AspNetCore.Reporting;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.BusinessLogic;
using Proyecto.BusinessLogic.Services;
using Proyecto.DataAccess.Repositories;
using Proyecto.Entities.Entities;
using ProyectoCentroMedico.MVC.Atributes;
using ProyectoCentroMedico.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Controllers
{
    public class HospitalController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly CatalogService _catalogService;
        private readonly HospitalRepositories _hospitalesRepository;
        private readonly IMapper _mapper;
        public readonly IHttpContextAccessor _IHttpContextAccessor;
        public HospitalController(CatalogService catalogService,
            IMapper mapper,
            HospitalRepositories hospitalRepository,
            IHttpContextAccessor HttpContextAccessor,
              IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
            _catalogService = catalogService;
            _mapper = mapper;
            _hospitalesRepository = hospitalRepository;
            _IHttpContextAccessor = HttpContextAccessor;
        }
        [HttpGet("Hospitales/Listado")]
        //[SessionManager(Helpers.ListadoUsuario)]
        public IActionResult Index()
        {
            var listado = _catalogService.ListadoHospital(out string erroMessage);
            var listadoMapeado = _mapper.Map<IEnumerable<HospitalesViewModel>>(listado);
            if (!string.IsNullOrEmpty(erroMessage))
                ModelState.AddModelError("", erroMessage);
            return View(listadoMapeado);

        }

        //crear
        [HttpGet("/Hospitales/Crear")]
        [SessionManager(Helpers.InsertHospital)]
        public IActionResult Create()
        {
            //var rol = new UsuariosViewModel();
            //rol.LlenarCbRol(_catalogService.ListadoRoles(out string errorMessage));
            //return View(rol);
            return View();
        }
        [HttpPost("/Hospitales/Crear")]
        public IActionResult Create(HospitalesViewModel item)
        {
            if (ModelState.IsValid)
            {
                var model = new tbHospiltales()
                {
                    hospi_Id = item.hospi_Id,
                    hospi_Nombre = item.hospi_Nombre,
                    hospi_Telefono = item.hospi_Telefono


                };
                string mensaje = _catalogService.InsertarHospita(model);
                if (!string.IsNullOrEmpty(mensaje))
                    ModelState.AddModelError("", mensaje);
                else
                    return RedirectToAction(nameof(Create));

            }
            return View(item);
        }

        public IActionResult Print()
        {
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReporteHospital.rdlc";
            var tabla = _catalogService.HospitalReporte();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", tabla);
            var result = localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }

        public IActionResult PrintUltId()
        {
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReporteHospital.rdlc";
            var tabla = _catalogService.HospitalUltimoId();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", tabla);
            var result = localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }

    }
}
