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
    public class MeetingsControllerShould
    {
        private static IMapper _mapper;
        public MeetingsControllerShould()
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
        MeetingsControllerTest _meetings;
        [TestInitialize]
        public void Initalize()
        {
            _meetings = new MeetingsControllerTest(_mapper);
        }

        [TestMethod]
        public void MeetingsList()
        {
            OkObjectResult result = (OkObjectResult)_meetings.List();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MeetingInsert()
        {
            var model = new MeetingsModel()
            {
                cus_Id = 1,
                met_IdUserCreate = 1
            };
            OkObjectResult result = (OkObjectResult)_meetings.Insert(model);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void MeetingsUpdate()
        {
            int id = 1;
            var model = new MeetingsModel()
            {
                met_Id = 1,
                cus_Id = 1,
                met_IdUserModified = 1
            };
            OkObjectResult result = (OkObjectResult)_meetings.Update(model, id);
            ServiceResult service = (ServiceResult)result.Value;
            Assert.IsTrue(service.Success);
        }

        [TestMethod]
        public void MeetingsDelete()
        {
            int Id = 1;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_meetings.Delete(Id, Mod);
            Assert.AreNotEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void MeetingsDeleteError()
        {
            int id = 1;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_meetings.Delete(id, Mod);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
