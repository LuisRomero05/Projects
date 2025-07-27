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
            CreateMap<tbUsuarios, UsuariosViewModel>().ReverseMap();
        }
    }
}
