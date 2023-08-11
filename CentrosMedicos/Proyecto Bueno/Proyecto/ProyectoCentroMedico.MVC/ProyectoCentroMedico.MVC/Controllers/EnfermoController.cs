using AspNetCore.Reporting;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.BusinessLogic.Services;
using Proyecto.DataAccess.Repositories;
using Proyecto.Entities.Entities;
using ProyectoCentroMedico.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Controllers
{
    public class EnfermoController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly EnfermoRepository _enfermoRepository;
        private readonly CatalogService _catalogService;
        private readonly IMapper _mapper;
        public readonly IHttpContextAccessor _IHttpContextAccessor;

        public EnfermoController(IWebHostEnvironment webHostEnvironment,
            EnfermoRepository enfermoRepository, 
            CatalogService catalogService, 
            IHttpContextAccessor HttpContextAccessor)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._enfermoRepository = enfermoRepository;
            _IHttpContextAccessor = HttpContextAccessor;
            _catalogService = catalogService;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/Enfermos/Crear")]
        //[SessionManager(Helpers.UsuarioC)]
        public IActionResult Create()
        {
            var rol = new EnfermosViewModel();
            rol.LlenarEmpleado(_catalogService.ListadoPlantilla(out string errorMessage));

            return View(rol);
            // return View();
        }
        [HttpPost("/Enfermos/Crear")]
        public IActionResult Create(EnfermosViewModel item)
        {
            if (ModelState.IsValid)
            {
                var model = new tbEnfermo()
                {
                    enfer_Inscripcioon = item.enfer_Inscripcioon,
                    enfer_Apellido = item.enfer_Apellido,
                    planti_EmpleadoId = item.planti_EmpleadoId,
                    enfer_Direccion = item.enfer_Direccion,
                    enfer_FechaNac = item.enfer_FechaNac,
                    enfer_NSS = item.enfer_NSS


                };
                string mensaje = _catalogService.InsertarEnfermo(model);
                if (!string.IsNullOrEmpty(mensaje))
                    ModelState.AddModelError("", mensaje);
                else
                    return RedirectToAction(nameof(Create));

            }
            return View(item);
        }

        public IActionResult Print()
        {
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReporteEnfermo.rdlc";
            var tabla = _catalogService.EnfermoReporte();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", tabla);
            var result = localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }

        public IActionResult PrintUltId()
        {
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReporteEnfermo.rdlc";
            var tabla = _catalogService.EnfermoUltimoId();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", tabla);
            var result = localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }

    }
}
