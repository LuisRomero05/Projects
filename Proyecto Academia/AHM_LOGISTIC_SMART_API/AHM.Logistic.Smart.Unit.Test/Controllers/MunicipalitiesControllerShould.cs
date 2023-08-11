using AHM.Logistic.Smart.Api.Extensions;
using AHM.Logistic.Smart.Api.TestControllers;
using AHM.Logistic.Smart.BusinessLogic;
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
    public class MunicipalitiesControllerShould
    {
        private static IMapper _mapper;
        public MunicipalitiesControllerShould()
        {
            if (_mapper == null)
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
        MunicipalitiesControllerTest _municipalitiesControllerTest;
        [TestInitialize]
        public void Initalize()
        {
            _municipalitiesControllerTest = new MunicipalitiesControllerTest(_mapper);
        }
        [TestMethod]
        public void ShowMuniList()
        {
            OkObjectResult result = (OkObjectResult)_municipalitiesControllerTest.List();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DepartInsert()
        {
            MunicipalitiesModel mun = new MunicipalitiesModel()
            {
                mun_Id = 0,
                mun_Code = "132",
                mun_Description = "Xff",
                dep_Id = 1,
                mun_IdUserCreate = 1,
            };
            OkObjectResult result = (OkObjectResult)_municipalitiesControllerTest.Insert(mun);
            int Code = (int)result.StatusCode;
            Assert.AreEqual(200, Code);
        }
        [TestMethod]
        public void DepartUpdate()
        {
            int Id = 5;
            MunicipalitiesModel are = new MunicipalitiesModel()
            {
                mun_Id = 0,
                mun_Code = "132",
                mun_Description = "XD",
                dep_Id = 1,
                mun_IdUserCreate = 1,
            };
            OkObjectResult result = (OkObjectResult)_municipalitiesControllerTest.Update(Id, are);
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsTrue(srv.Success);
        }
        [TestMethod]
        public void DepartDelete()
        {
            int Id = 132;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_municipalitiesControllerTest.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            Assert.AreNotEqual(400, Code);
        }
        [TestMethod]
        public void DepartUpdateError()
        {
            int Id = 5;
            MunicipalitiesModel are = new MunicipalitiesModel()
            {
                mun_Id = 0,
                mun_Code = "132",
                mun_Description = "XD",
                dep_Id = 1,
                mun_IdUserCreate = 1,
            };
            OkObjectResult result = (OkObjectResult)_municipalitiesControllerTest.Update(Id, are);
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsFalse(srv.Success);
        }
    }
}
