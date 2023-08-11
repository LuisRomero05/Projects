using AHM.Logistic.Smart.Api.Extensions;
using AHM.Logistic.Smart.Api.TestControllers;
using AHM.Logistic.Smart.BusinessLogic;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.Entities.Entities;
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
    public class RolesControllerShould
    {
        private static IMapper _mapper;
        public RolesControllerShould()
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
        RolesControllerTest _rolesController;
        [TestInitialize]
        public void Initalize()
        {
            _rolesController = new RolesControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowRolesList()
        {
            OkObjectResult result = (OkObjectResult)_rolesController.List();
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RolesInsert()
        {
            RolModel rol = new RolModel()
            {
                
                rol_Id = 0,
                rol_Description = "XD",
                rol_Status = true,
                rol_IdUserCreate = 1,
                rol_IdUserModified = 0     
            };
            OkObjectResult result = (OkObjectResult)_rolesController.Insert(rol);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }

        [TestMethod]
        public void RolesUpdate()
        {
            int Id = 1;
            RolModel are = new RolModel()
            {
                rol_Id = 0,
                rol_Description = "XD2",
                rol_Status = true,
                rol_IdUserCreate =  0,
                rol_IdUserModified = 1
            };
            OkObjectResult result = (OkObjectResult)_rolesController.Update(Id, are);
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
            OkObjectResult result = (OkObjectResult)_rolesController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }
    }
}
