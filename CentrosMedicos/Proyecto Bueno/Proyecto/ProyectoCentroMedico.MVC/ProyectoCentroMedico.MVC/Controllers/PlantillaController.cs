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
    public class PlantillaController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PlantillaRepository _plantillaRepository;
        private readonly CatalogService _catalogService;
        private readonly IMapper _mapper;
        public readonly IHttpContextAccessor _IHttpContextAccessor;

        public PlantillaController(IWebHostEnvironment webHostEnvironment, PlantillaRepository plantillaRepository, CatalogService catalogService, IHttpContextAccessor HttpContextAccessor)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._plantillaRepository = plantillaRepository;
            _IHttpContextAccessor = HttpContextAccessor;
            _catalogService = catalogService;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        [HttpGet("Plantilla/ListaPlantilla/{hospi_Id}")]
        public IActionResult MuniDepaCreate(int hospi_Id)
        {
            var editorial = new PlantillaViewModel();
            editorial.LlenarSala(_catalogService.ListadoSala(out string mensajeError).Where(x => x.hospi_Id == hospi_Id));
            return Ok(editorial);
        }

        //crear
        [HttpGet("/Plantilla/Crear")]
        [SessionManager(Helpers.InsertPersonal)]
        public IActionResult Create()
        {
            var rol = new PlantillaViewModel();
            rol.LlenarSala(_catalogService.ListadoSala(out string errorMessage));
            rol.LlenarHospital(_catalogService.ListadoHospital(out string errorMessager));

            return View(rol);
            // return View();
        }
        [HttpPost("/Plantilla/Crear")]
        public IActionResult Create(PlantillaViewModel item)
        {
            if (ModelState.IsValid)
            {
                var model = new tbPlantilla()
                {
                    hospi_Id = item.hospi_Id,
                    sala_Id = item.sala_Id,
                    planti_EmpleadoId = item.planti_EmpleadoId,

                    planti_Apellido = item.planti_Apellido,
                    planti_Funcion = item.planti_Funcion,
                    planti_Turno = item.planti_Turno,
                    planti_Salario = item.planti_Salario


                };
                string mensaje = _catalogService.InsertarPlantilla(model);
                if (!string.IsNullOrEmpty(mensaje))
                    ModelState.AddModelError("", mensaje);
                else
                    return RedirectToAction(nameof(Create));

            }
            return View(item);

        }

        public IActionResult Print()
        {
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportePlantilla.rdlc";
            var tabla = _catalogService.PlantillaReporte();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", tabla);
            var result = localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }

        public IActionResult PrintUltId()
        {
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportePlantilla.rdlc";
            var tabla = _catalogService.PlantillaUltimoId();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", tabla);
            var result = localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }
    }
}
