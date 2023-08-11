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
    public class UserControllerShould
    {
        private static IMapper _mapper;
        public UserControllerShould()
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
        UserControllerTest _userController;
        [TestInitialize]
        public void Initalize()
        {
            _userController = new UserControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowUserList()
        {
            OkObjectResult result = (OkObjectResult)_userController.List();
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
            Assert.IsTrue(Value.Data.Count != 0);
        }

        [TestMethod]
        public void UsersInsert()
        {
            UsersModel pro = new UsersModel()
            {
               usu_UserName = "NewUser",
               usu_Password = "NewUser1234",
               usu_Profile_picture = null,
               usu_IdUserCreate = 1,               
            };
            OkObjectResult result = (OkObjectResult)_userController.Insert(pro);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }
        [TestMethod]
        public void UsersUpdate()
        {
            int Id = 9;
            UsersModel pro = new UsersModel()
            {
                usu_UserName = "NewUser",
                usu_Password = "NewUser12345",
                usu_Profile_picture = null,
                usu_IdUserCreate = 1,
                usu_IdUserModified = 1
            };
            OkObjectResult result = (OkObjectResult)_userController.Update(Id, pro);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }

        [TestMethod]
        public void UsersDelete()
        {
            int Id = 5;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_userController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }

        [TestMethod]
        public void UsersDeleteError()
        {
            int Id = 10;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_userController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }
    }
}
