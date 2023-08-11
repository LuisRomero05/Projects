using AHM.Logistic.Smart.BusinessLogic;
using AHM.Logistic.Smart.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api.Controllers
{
    public class TestController : Controller
    {
        public int Index()
        {
            return 4 + 5;
        }

        public int ListaAreasCodigo()
        {
           var Listado = new
            {
               code=200,
               are_Id=1,
               are_Description="Administrador"
            };  
            return Listado.code;
        }

        public object ListaAreas()
        {
            object Listado = new
            {
                code = 200,
                are_Id = 1,
                are_Description = "Administrador"
            };
            return Listado;
        }

        public List<AreasViewModel> ListaAreasGrande()
        {
            var Listado = new List<AreasViewModel>() {
                new AreasViewModel(){ are_Id = 1, are_Description="RRHH"},
                new AreasViewModel(){ are_Id = 2, are_Description="Administracion"},
                new AreasViewModel(){ are_Id = 3, are_Description="Ventas"},
                new AreasViewModel(){ are_Id = 4, are_Description="Arre"}
            };

            return Listado;
        }

        public ServiceResult SimulacionAreasList()
        {
            var result = new ServiceResult();
            var Listado = new List<AreasViewModel>() {
                new AreasViewModel(){ are_Id = 1, are_Description="RRHH"},
                new AreasViewModel(){ are_Id = 2, are_Description="Administracion"},
                new AreasViewModel(){ are_Id = 3, are_Description="Ventas"},
                new AreasViewModel(){ are_Id = 4, are_Description="Arre"}
            };
            return result.Ok(Listado);
           
        }
        public ServiceResult SimulacionAreasDetails()
        {
            var result = new ServiceResult();
            object Listado = new
            {
                are_Id = 1,
                are_Description = "Administrador"
            };
            return result.Ok(Listado);

        }
        public ServiceResult SimulacionAreasInsert()
        {
            var result = new ServiceResult();
            object Listado = new
            {
                are_Id = 1,
                are_Description = "null"
            };
            return result.Ok(Listado);

        }
        public ServiceResult SimulacionAreasUpdate()
        {
            var result = new ServiceResult();
            object Listado = new
            {
                are_Id = 0,
                are_Description = "null"
            };
            return result.Ok(Listado);

        }
        public ServiceResult SimulacionAreasDelete()
        {
            var result = new ServiceResult();
            int data = 0;
            return result.Ok(data);

        }
    }
}

