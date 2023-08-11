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
    public class CustomerNotesControllerShould
    {
        private static IMapper _mapper;
        public CustomerNotesControllerShould()
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
        CustomerNotesControllerTest _customerNotes;
        [TestInitialize]
        public void Initalize()
        {
            _customerNotes = new CustomerNotesControllerTest(_mapper);
        }

        [TestMethod]
        public void CustomerNotesList()
        {
            OkObjectResult result = (OkObjectResult)_customerNotes.List();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CustomerNotesInsert()
        {
            var model = new CustomerNotesModel()
            {
                cun_Descripcion = "TestNotes",
                cun_ExpirationDate = DateTime.Now,
                pry_Id = 1,
                cus_Id = 1,
                cun_IdUserCreate = 1
            };
            OkObjectResult result = (OkObjectResult)_customerNotes.Insert(model);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void CustomerNotesUpdate()
        {
            int Id = 1;
            var model = new CustomerNotesModel()
            {
                cun_Descripcion = "UpdateTest",
                cun_ExpirationDate = DateTime.Now,
                pry_Id = 1,
                cus_Id = 1
            };
            OkObjectResult result = (OkObjectResult)_customerNotes.Update(Id, model);
            ServiceResult service = (ServiceResult)result.Value;
            Assert.IsTrue(service.Success);
        }

        [TestMethod]
        public void CustomerNotesDelete()
        {
            int Id = 1;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_customerNotes.Delete(Id, Mod);
            Assert.AreNotEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void CustomerNotesDeleteError()
        {
            int id = 1;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_customerNotes.Delete(id, Mod);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
