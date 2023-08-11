using AHM.Logistic.Smart.Api.Extensions;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AHM.Logistic.Smart.Api.TestControllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AHM.Logistic.Smart.BusinessLogic;
using AHM.Logistic.Smart.Common.Models;

namespace AHM.Logistic.Smart.Unit.Test.Controllers
{
    [TestClass]
    public class DepartmentsControllerShould
    {
        private static IMapper _mapper;
        public DepartmentsControllerShould()
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
        DepartmentsControllerTest _departmentsController;
        [TestInitialize]
        public void Initalize()
        {
            _departmentsController = new DepartmentsControllerTest(_mapper);
        }
        [TestMethod]
        public void ShowDepartList()
        {
            OkObjectResult result = (OkObjectResult)_departmentsController.List(); 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DepartInsert()
        {
            DepartmentsModel dep = new DepartmentsModel()
            {
                dep_Id = 0,
                dep_Code ="132",
                dep_Description = "Xff",
                cou_Id = 1,
                dep_IdUserCreate = 1,
            };
            OkObjectResult result = (OkObjectResult)_departmentsController.Insert(dep);
            int Code = (int)result.StatusCode;
            Assert.AreEqual(200, Code);
        }

        [TestMethod]
        public void DepartUpdate()
        {
            int Id = 5;
            DepartmentsModel are = new DepartmentsModel()
            {
                dep_Id = 0,
                dep_Code = "132",
                dep_Description = "XD2",
                cou_Id = 1,
                dep_IdUserModified = 1,
            };
            OkObjectResult result = (OkObjectResult)_departmentsController.Update(Id, are);
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsTrue(srv.Success);
        }
        [TestMethod]
        public void DepartDelete()
        {
            int Id = 132;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_departmentsController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            Assert.AreNotEqual(400, Code);
        }
        //Error.
        [TestMethod]
        public void DepartUpdateError()
        {
            int Id = 5;
            DepartmentsModel are = new DepartmentsModel()
            {
                dep_Id = 0,
                dep_Code = "132",
                dep_Description = "XD2",
                cou_Id = 1,
                dep_IdUserModified = 1,
            };
            OkObjectResult result = (OkObjectResult)_departmentsController.Update(Id, are);
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsFalse(srv.Success);
        }

    }
}
