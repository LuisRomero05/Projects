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
    public class EmployeesControllerShould
    {
        private static IMapper _mapper;
        public EmployeesControllerShould()
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
        EmployeesControllerTest _employeesController;
        [TestInitialize]
        public void Initalize()
        {
            _employeesController = new EmployeesControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowEmployeesList()
        {
            OkObjectResult result = (OkObjectResult)_employeesController.List();
            int Code = (int)result.StatusCode;
            Assert.AreEqual(200, Code);
        }

        [TestMethod]
        public void EmployeesInsert()
        {
            EmployeesModel emp = new EmployeesModel()
            {
                emp_Id = 0,
                per_Id = 1,
                are_Id = 1,
                occ_Id = 1,
                emp_IdUserCreate = 1,
                emp_IdUserModified = 0,
            };
            OkObjectResult result = (OkObjectResult)_employeesController.Insert(emp);
            int Code = (int)result.StatusCode;
            Assert.AreEqual(200, Code);
        }

        [TestMethod]
        public void EmployeesUpdate()
        {
            int Id = 41;
            EmployeesModel emp = new EmployeesModel()
            {
                emp_Id = 0,
                per_Id = 2,
                are_Id = 2,
                occ_Id = 2,
                emp_IdUserCreate = 0,
                emp_IdUserModified = 1,
            };
            OkObjectResult result = (OkObjectResult)_employeesController.Update(Id, emp);
            int Code = (int)result.StatusCode;
            Assert.AreNotSame(400, Code);
        }

        [TestMethod]
        public void EmployeesUpdateError()
        {
            int Id = 5;
            EmployeesModel emp = new EmployeesModel()
            {
                emp_Id = 0,
                per_Id = 2,
                are_Id = 2,
                occ_Id = 2,
                emp_IdUserCreate = 0,
                emp_IdUserModified = 1,
            };
            OkObjectResult result = (OkObjectResult)_employeesController.Update(Id, emp);
            int Code = (int)result.StatusCode;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void EmployeesDelete()
        {
            int Id = 5;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_employeesController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            Assert.AreNotEqual(400, Code);
        }
    }
}
