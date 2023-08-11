using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.API.Models;
using Template.Entities.Entities;

namespace Template.API.Extensions
{
    public class MappingProfileExtension : Profile
    {
        public MappingProfileExtension()
        {
            CreateMap<tbAreas, AreasModel>().ReverseMap();
        }

    }
}
