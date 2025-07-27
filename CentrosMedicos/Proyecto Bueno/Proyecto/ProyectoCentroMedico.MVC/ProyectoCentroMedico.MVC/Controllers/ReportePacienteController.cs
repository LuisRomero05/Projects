using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.BusinessLogic.Services;
using ProyectoCentroMedico.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Controllers
{
    public class ReportePacienteController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CatalogService _catalogService;
        public readonly IHttpContextAccessor _IHttpContextAccessor;
        public ReportePacienteController(CatalogService catalogService,
            IHttpContextAccessor HttpContextAccessor,
            IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
            _catalogService = catalogService;
            _IHttpContextAccessor = HttpContextAccessor;
        }

        [HttpGet("/ReportePaciente/Crear")]
        //[SessionManager(Helpers.UsuarioC)]
        public IActionResult Create()
        {
            var rol = new ReporteGeneralViewModel();
            rol.LlenarEnfer(_catalogService.ListadoEnfermo(out string errorMessage));
            return View(rol);
        }

        [HttpPost("/ReportePaciente/Crear")]
        //[SessionManager(Helpers.UsuarioC)]
        public IActionResult Create(ReporteGeneralViewModel item)
        {
            int id = item.enfer_Inscripcioon;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportePrincipal.rdlc";
            var tabla = _catalogService.ReporteGeneralId(id);
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", tabla);
            var result = localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }

        [HttpGet("EmpleadoSala/ListaHospitalSala/{hospi_Id}")]
        public IActionResult MuniDepaCreate(int hospi_Id)
        {
            var editorial = new EmpleadoSalaViewModel();
            editorial.LlenarSala(_catalogService.ListadoSala(out string mensajeError).Where(x => x.hospi_Id == hospi_Id));
            return Ok(editorial);
        }

        [HttpGet("/EmpleadoSala/Crear")]
        //[SessionManager(Helpers.UsuarioC)]
        public IActionResult EmpleadoSalaId()
        {
            var rol = new EmpleadoSalaViewModel();
            rol.LlenarSala(_catalogService.ListadoSala(out string errorMessage));
            rol.LlenarHospital(_catalogService.ListadoHospital(out string errorMessager));
            return View(rol);
        }

        [HttpPost("/EmpleadoSala/Crear")]
        //[SessionManager(Helpers.UsuarioC)]
        public IActionResult EmpleadoSalaId(EmpleadoSalaViewModel item)
        {
            int id = item.sala_Id;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReporteEmpleadosPorSala.rdlc";
            var tabla = _catalogService.EmpleadoSalaId(id);
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", tabla);
            var result = localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }

        [HttpGet("/EmpleadoHospital/Crear")]
        //[SessionManager(Helpers.UsuarioC)]
        public IActionResult EmpleadoHospitalId()
        {
            var rol = new EmpleadoHospitalViewModel();
            rol.LlenarHosp(_catalogService.ListadoHospital(out string errorMessage));
            return View(rol);
        }

        [HttpPost("/EmpleadoHospital/Crear")]
        //[SessionManager(Helpers.UsuarioC)]
        public IActionResult EmpleadoHospitalId(EmpleadoHospitalViewModel item)
        {
            int id = item.hospi_Id;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReporteEmpleadosPorHospital.rdlc";
            var tabla = _catalogService.EmpleadoHospitalId(id);
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", tabla);
            var result = localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }
    }
}
