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
using static AHM.Logistic.Smart.Api.TestControllers.CountriesControllerTest;

namespace AHM.Logistic.Smart.Unit.Test.Controllers
{
    [TestClass]
    public class CountriesControllerShould
    {
        private static IMapper _mapper;
        public CountriesControllerShould()
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
        CountriesControllerTest _countriesController;
        [TestInitialize]
        public void Initalize()
        {
            _countriesController = new CountriesControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowCountriesList()
        {
            OkObjectResult result = (OkObjectResult)_countriesController.List();
            int Code = (int)result.StatusCode;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CountriesInsert()
        {
            CountryModel cou = new CountryModel()
            {
                cou_Id = 0,
                cou_Description = "Guatemala5",
                cou_IdUserCreate = 1,
                cou_IdUserModified = 0,
            };
            OkObjectResult result = (OkObjectResult)_countriesController.Insert(cou);
            int Code = (int)result.StatusCode;
            Assert.AreEqual(200, Code);
        }
        [TestMethod]
        public void CountriesUpdate()
        {
            int Id = 36;
            CountryModel cou = new CountryModel()
            {
                cou_Id = 0,
                cou_Description = "Honduras5",
                cou_IdUserCreate = 0,
                cou_IdUserModified = 1,
            };
            OkObjectResult result = (OkObjectResult)_countriesController.Update(Id, cou);
            int Code = (int)result.StatusCode;
            Assert.AreNotSame(400, Code);
        }

        [TestMethod]
        public void CountriesDelete()
        {
            int Id = 41;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_countriesController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            Assert.AreNotEqual(400, Code);
        }

        [TestMethod]
        public void CountriesDeleteError()
        {
            int Id = 5;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_countriesController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            Assert.IsNull(result);
        }
    }
}
