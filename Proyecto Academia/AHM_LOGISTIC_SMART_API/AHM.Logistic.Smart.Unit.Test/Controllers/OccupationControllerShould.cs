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
    public class OccupationControllerShould
    {
        private static IMapper _mapper;

        public OccupationControllerShould()
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
        OccupationControllerTest _occupationController;
        [TestInitialize]
        public void Initalize()
        {
            _occupationController = new OccupationControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowOccupationList()
        {
            OkObjectResult result = (OkObjectResult)_occupationController.List();
            int Code = (int)result.StatusCode;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowOccupationListError()
        {
            OkObjectResult result = (OkObjectResult)_occupationController.List();
            int Code = (int)result.StatusCode;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void OccupationInsert()
        {
            OccupationsModel occu = new OccupationsModel()
            {
                occ_Id = 0,
                occ_Description = "Test",
                occ_IdUserCreate = 1,
                occ_IdUserModified = 0,
            };
            OkObjectResult result = (OkObjectResult)_occupationController.Insert(occu);
            int Code = (int)result.StatusCode;
            Assert.AreEqual(200, Code);
        }

        [TestMethod]
        public void OcupationUpdate()
        {
            int Id = 48;
            OccupationsModel occu = new OccupationsModel()
            {
                occ_Id = 0,
                occ_Description = "AdministraciónTEST",
                occ_IdUserCreate = 0,
                occ_IdUserModified = 1,
            };
            OkObjectResult result = (OkObjectResult)_occupationController.Update(Id, occu);
            int Code = (int)result.StatusCode;
            Assert.AreNotSame(400, Code);
        }

        [TestMethod]
        public void OccupationDelete()
        {
            int Id = 49;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_occupationController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            Assert.AreNotEqual(400, Code);
        }
    }
}
