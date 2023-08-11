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
    public class SalaController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CatalogService _catalogService;
        private readonly SalasRepository _salasRepository;
        private readonly IMapper _mapper;
        public readonly IHttpContextAccessor _IHttpContextAccessor;
        public SalaController(CatalogService catalogService,
            IMapper mapper,
            SalasRepository salasRepository,
            IHttpContextAccessor HttpContextAccessor,
            IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
            _catalogService = catalogService;
            _mapper = mapper;
            _salasRepository = salasRepository;
            _IHttpContextAccessor = HttpContextAccessor;
        }
        [HttpGet("Sala/Listado")]
        //[SessionManager(Helpers.ListadoUsuario)]
        public IActionResult Index()
        {
            var listado = _catalogService.ListadoSala(out string erroMessage);
            var listadoMapeado = _mapper.Map<IEnumerable<SalaViewModel>>(listado);
            if (!string.IsNullOrEmpty(erroMessage))
                ModelState.AddModelError("", erroMessage);
            return View(listadoMapeado);

        }

        [HttpGet("/Sala/Crear")]
        [SessionManager(Helpers.InsertSalas)]
        public IActionResult Create()
        {
            var rol = new SalaViewModel();
            rol.LlenarHosp(_catalogService.ListadoHospital(out string errorMessage));
            return View(rol);
        }

        [HttpPost("/Sala/Crear")]
        public IActionResult Create(SalaViewModel item)
        {
            if (ModelState.IsValid)
            {
                var model = new tbSala()
                {
                    hospi_Id = item.hospi_Id,
                    sala_Id = item.sala_Id,
                    sala_Nombre = item.sala_Nombre,
                    sala_NumCamas = item.sala_NumCamas
                };
                string mensaje = _catalogService.InsertarSala(model);
                if (!string.IsNullOrEmpty(mensaje))
                    ModelState.AddModelError("", mensaje);
                else
                    return RedirectToAction(nameof(Create));

            }
            return View(item);
        }
        public IActionResult Print()
        {
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReporteSala.rdlc";
            var tabla = _catalogService.SalaReporte();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", tabla);
            var result = localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }

        public IActionResult PrintUltId()
        {
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReporteSala.rdlc";
            var tabla = _catalogService.SalaUltimoId();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", tabla);
            var result = localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }
    }
}
