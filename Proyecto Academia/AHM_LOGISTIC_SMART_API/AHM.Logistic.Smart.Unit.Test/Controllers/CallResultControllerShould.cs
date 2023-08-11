using AHM.Logistic.Smart.Api.Extensions;
using AHM.Logistic.Smart.Api.TestControllers;
using AHM.Logistic.Smart.BusinessLogic;
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
    public class CallResultControllerShould
    {
        private static IMapper _mapper;

        public CallResultControllerShould()
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
        CallResultControllerTest callResultControllerTest;

        [TestInitialize]
        public void Initalize()
        {
            callResultControllerTest = new CallResultControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowCallResultList()
        {
            OkObjectResult result = (OkObjectResult)callResultControllerTest.List();
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            //ObjectResultController obr = new ObjectResultController() 
            //{ 
            //    ContentTypes = result.ContentTypes,
            //};
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowCallResultListFail()
        {
            OkObjectResult result = (OkObjectResult)callResultControllerTest.List();
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            //ObjectResultController obr = new ObjectResultController() 
            //{ 
            //    ContentTypes = result.ContentTypes,
            //};
            Assert.AreEqual(400, Code);
        }
    }
    
}
