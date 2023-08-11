using AHM.Logistic.Smart.Api.Controllers;
using AHM.Logistic.Smart.Api.Extensions;
using AHM.Logistic.Smart.Api.TestControllers;
using AHM.Logistic.Smart.BusinessLogic;
using AHM.Logistic.Smart.BusinessLogic.Services;
using AHM.Logistic.Smart.Common.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Unit.Test.Controllers
{
    [TestClass]
    public class AreasControllerShould
    {
        private static IMapper _mapper;
        public AreasControllerShould()
        {
            if(_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc => 
                {
                    mc.AddProfile(new MappingProfileExtensions());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
        protected Mock<IMapper> map = new Mock<IMapper>();
        AreasControllerTest _areasController;
        [TestInitialize]
        public void Initalize()
        {
            _areasController = new AreasControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowAreasList()
        {
            OkObjectResult result = (OkObjectResult)_areasController.List();
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
        }
        //private TestController _testController;

        //[TestInitialize]
        //public void Initialize()
        //{
        //    _testController = new TestController();
        //}

        //[TestMethod]
        //public void AreasIndex()
        //{
        //    var result = _testController.SimulacionAreasList();
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(200, result.Code);
        //    Assert.AreNotEqual(400, result.Code);
        //}
        //[TestMethod]
        //public void AreasDetails()
        //{
        //    var result = _testController.SimulacionAreasDetails();
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(200, result.Code);
        //    Assert.AreNotEqual(400, result.Code);
        //}
        [TestMethod]
        public void AreasInsert()
        {            
            AreasModel are = new AreasModel() 
            {
                are_Id = 0,
                are_Description = "XD",
                are_IdUserCreate = 1,
                are_IdUserModified = 0,
            };
            OkObjectResult result = (OkObjectResult)_areasController.Insert(are);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code); 
        }
        [TestMethod]
        public void AreasUpdate()
        {
            int Id = 5;
            AreasModel are = new AreasModel()
            {
                are_Id = 0,
                are_Description = "XD2",
                are_IdUserCreate = 0,
                are_IdUserModified = 1,
            };
            OkObjectResult result = (OkObjectResult)_areasController.Update(Id, are);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }
        [TestMethod]
        public void AreasDelete()
        {
            int Id = 5;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_areasController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }
    }
}
