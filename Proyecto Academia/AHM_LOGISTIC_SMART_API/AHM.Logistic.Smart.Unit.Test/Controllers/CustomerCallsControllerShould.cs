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
    public class CustomerCallsControllerShould
    {
        private static IMapper _mapper;
        public CustomerCallsControllerShould()
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
        CustomerCallsControllerTest _customerCallsController;
        [TestInitialize]

        public void Initalize()
        {
            _customerCallsController = new CustomerCallsControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowAreasList()
        {
            OkObjectResult result = (OkObjectResult)_customerCallsController.List();
            int Code = (int)result.StatusCode;
            //ServiceResult srv = (ServiceResult)result.Value;
            //ObjectResultController obr = new ObjectResultController() 
            //{ 
            //    ContentTypes = result.ContentTypes,
            //};
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CustomerCallsInsert()
        {
            CustomerCallsModel cca = new CustomerCallsModel()
            {
                cca_Id = 0,
                cca_CallType=1,  
                cca_Business=1,
                cca_Result=1,
                cca_Date= DateTime.Now,
                cca_StartTime ="10:22",
                cca_EndTime ="11:10",
                cus_Id =1,
                cca_Status= true,
                cca_IdUserCreate=1, 
                cca_IdUserModified=0, 
              
             };
            OkObjectResult result = (OkObjectResult)_customerCallsController.Insert(cca);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;           
            Assert.AreNotEqual(400, Code);
        }
        [TestMethod]
        public void CustomerCallsUpdate()
        {
            int id = 4;
            CustomerCallsModel are = new CustomerCallsModel()
            {
                cca_Id = 3,
                cca_CallType = 2,
                cca_Business = 2,
                cca_Result = 2,
                cca_Date = DateTime.Now,
                cca_StartTime = "10:00:00",
                cca_EndTime = "11:00:00",
                cus_Id = 1,
                cca_Status = true,
                cca_IdUserCreate = 0,
                cca_IdUserModified = 1,
            };
            OkObjectResult result = (OkObjectResult)_customerCallsController.Update(are,id);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.AreEqual(200, Code);
        }
        [TestMethod]
        public void CustomerCallsDelete()
        {
            int Id = 4;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_customerCallsController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            //ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CustomerCallsFail()
        {
            CustomerCallsModel cca = new CustomerCallsModel()
            {
                cca_Id = 25,
                cca_CallType = 1,
                cca_Business = 1,
                cca_Result = 1,
                cca_Date = DateTime.Now,
                cca_StartTime = "19:00",
                cca_EndTime = "20:00",
                cus_Id = 1,
                cca_Status = true,
                cca_IdUserCreate = 1,
                cca_IdUserModified = 0,

            };
            OkObjectResult result = (OkObjectResult)_customerCallsController.Insert(cca);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.AreNotEqual(200, Code);
        }
    }
}
