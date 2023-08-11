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
    public class PersonsControllerShould
    {
        private static IMapper _mapper;
        public PersonsControllerShould()
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
        PersonsControllerTest _personsController;
        [TestInitialize]
        public void Initalize()
        {
            _personsController = new PersonsControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowPersonsList()
        {
            OkObjectResult result = (OkObjectResult)_personsController.List();
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
            Assert.IsTrue(Value.Data.Count != 0);
        }


        [TestMethod]
        public void PersonsInsert()
        {
            PersonsModel pro = new PersonsModel()
            {
                per_Firstname = "Person",
                per_Secondname = "New",
                per_LastNames = "Insert",
                per_Identidad = "1601200300789",
                per_BirthDate = DateTime.Now,
                per_Sex = "F",
                per_Direccion = "AddressTest",
                per_Email = "NewPerson@gmail.com",
                per_Esciv = "S",
                per_Phone = "+504 96781718",
                per_IdUserCreate = 1
            };
            OkObjectResult result = (OkObjectResult)_personsController.Insert(pro);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }
        [TestMethod]
        public void PersonsUpdate()
        {
            int Id = 9;
            PersonsModel pro = new PersonsModel()
            {
                per_Firstname = "Person",
                per_Secondname = "New",
                per_LastNames = "Insert",
                per_Identidad = "1601200300789",
                per_BirthDate =  DateTime.Now,
                per_Sex = "F",
                per_Direccion = "AddressTest",
                per_Email = "NewPerson@gmail.com",
                per_Esciv = "S",
                per_Phone = "+504 96781718",
                per_IdUserCreate = 1,
                per_IdUserModified = Id
            };
            OkObjectResult result = (OkObjectResult)_personsController.Update(Id, pro);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }

        [TestMethod]
        public void PersonsDelete()
        {
            int Id = 9;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_personsController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }

        [TestMethod]
        public void PersonsDeleteError()
        {
            int Id = 10;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_personsController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }
    }
}
