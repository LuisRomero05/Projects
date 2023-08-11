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
    //class CustomerControllerShould
    [TestClass]
    public class CustomerControllerShould
    {
        private static IMapper _mapper;
        public CustomerControllerShould()
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
        CustomerControllerTest _CustomerController;
        [TestInitialize]
        public void Initalize()
        {
            _CustomerController = new CustomerControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowCustomersList()// Assert.IsNotNull(result);
        {
            OkObjectResult result = (OkObjectResult)_CustomerController.List();
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
        }
      
        [TestMethod]
        public void CustomersInsert()//AreNotEqual(400, Code)
        {
            CustomersModel custom = new CustomersModel()
            {
                cus_Id = 0,
                cus_AssignedUser = 1,
                tyCh_Id = 1,
                cus_Name = "test2511",
                cus_RTN = "12345678901234",
                cus_Address = "holamundo",
                dep_Id = 1,
                mun_Id = 1,
                cus_Email = "holamundo",
                cus_receive_email = true,
                cus_Active = true,
                cus_Phone = "99457860",
                cus_AnotherPhone = "99457860",
                cus_IdUserCreate = 1,
                cus_IdUserModified = 0,

            };
            OkObjectResult result = (OkObjectResult)_CustomerController.Insert(custom);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.AreNotEqual(400, Code);
        }
        
        [TestMethod]
        public void CustomersUpdate()
        {
            int Id = 5;
            CustomersModel are = new CustomersModel()
            {
                cus_Id = 0,
                cus_AssignedUser = 1,
                tyCh_Id = 1,
                cus_Name = "test2022",
                cus_RTN = "12345678901232",
                cus_Address = "holamundo2",
                dep_Id = 1,
                mun_Id = 1,
                cus_Email = "holamundo2",
                cus_receive_email = true,
                cus_Active = true,
                cus_Phone = "99457862",
                cus_AnotherPhone = "99457862",
                cus_IdUserCreate = 1,
                cus_IdUserModified = 0,
            };
            OkObjectResult result = (OkObjectResult)_CustomerController.Update(Id, are);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.AreEqual(200, Code);
        }
        [TestMethod]
        public void CustomersDelete()//Assert.IsFalse(false)
        {
            int Id = 30;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_CustomerController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            //Assert.IsTrue(true);
            //Assert.IsFalse(false);
            Assert.IsTrue(srv.Success);
            //Assert.IsFalse(srv.Success);
            //Assert.AreNotSame(400, Code);
            //Assert.AreSame(400, Code);//mal
            //Assert.AreSame(200, Code);//bien
        }

        [TestMethod]
        public void CustomersUpdateError()//IsNull
        {
            int Id = 5;
            CustomersModel are = new CustomersModel()
            {
                cus_Id = 0,
                cus_AssignedUser = 1,
                tyCh_Id = 1,
                cus_Name = "test0502",
                cus_RTN = "12345678901232",
                cus_Address = "holamundo2",
                dep_Id = 1,
                mun_Id = 1,
                cus_Email = "holamundo2",
                cus_receive_email = true,
                cus_Active = true,
                cus_Phone = "99457862",
                cus_AnotherPhone = "99457862",
                cus_IdUserCreate = 1,
                cus_IdUserModified = 0,
            };
            OkObjectResult result = (OkObjectResult)_CustomerController.Update(Id, are);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNull(result);

        }
    }
}
