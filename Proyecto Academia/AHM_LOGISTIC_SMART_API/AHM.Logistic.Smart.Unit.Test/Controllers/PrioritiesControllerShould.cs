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
    public class PrioritiesControllerShould
    {
        private static IMapper _mapper;
        public PrioritiesControllerShould()
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
        PrioritiesControllerTest _priorities;

        [TestInitialize]
        public void Initalize()
        {
            _priorities = new PrioritiesControllerTest(_mapper);
        }

        [TestMethod]
        public void PrioritiesList()
        {
            OkObjectResult result = (OkObjectResult)_priorities.List();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PrioritiesListError()
        {
            OkObjectResult result = (OkObjectResult)_priorities.List();
            ServiceResult service = (ServiceResult)result.Value;
            Assert.IsFalse(service.Success);
        }
    }
}
