using AutoMapper;
using Proyecto.Entities.Entities;
using ProyectoCentroMedico.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Extensions
{
    public class MappingProfileExntensions : Profile
    {
        public MappingProfileExntensions()
        {
            CreateMap<EmpleadoSala, EmpleadoSalaViewModel>().ReverseMap();
            CreateMap<ReporteGeneral, ReporteGeneralViewModel>().ReverseMap();
            CreateMap<tbUsuarios, UsuariosViewModel>().ReverseMap();
            CreateMap<tbPlantilla, PlantillaViewModel>().ReverseMap();
            CreateMap<tbSala, SalaViewModel>().ReverseMap();
            CreateMap<tbHospiltales, HospitalesViewModel>().ReverseMap();
            CreateMap<tbEnfermo, EnfermosViewModel>().ReverseMap();
        }
    }
}
