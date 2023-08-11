using AHM.Logistic.Smart.Api.Controllers;
using AHM.Logistic.Smart.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace AHM.Logistic.Smart.TEST2
{
    public class UnitTest1
    {
        public readonly AreasController _controller;
        public readonly CatalogService _catalogServices;

        public UnitTest1(AreasController controller, 
            CatalogService catalogServices)
        {
            _controller = controller;
            _catalogServices = catalogServices;
        }

        //listado, pero no trae correctamente
        //tendria que evaluar el cod 200
        [Fact]
        public void Get_Ok()
        {
            //int id = 1;
            var result = _controller.List();
            Assert.IsType<OkObjectResult>(result);
        }
        //[Fact]
        //public void Get_Ok()
        //{
        //    //var result = (OkObjectResult)_controller.List();
        //    //var beers = Assert.IsType<List<AreasController>>(result.Value);
        //    //Assert.True(beers.Count > 0);

        //}
    }
}
