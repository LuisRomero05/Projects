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
    public class LoginControllerShould
    {
        private static IMapper _mapper;
        public LoginControllerShould()
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
        LoginControllerTest _loginController;
        [TestInitialize]
        public void Initalize()
        {
            _loginController = new LoginControllerTest(_mapper);
        }

        [TestMethod]
        public void Login()
        {
            LoginModel log = new LoginModel() 
            {
                usu_UserName = "Administrator",
                usu_Password = "Administrator"
            };
            OkObjectResult result = (OkObjectResult)_loginController.Login(log);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNull(Value.Data);
            Assert.IsTrue(Value.Code == 200);
        }

    }
}
