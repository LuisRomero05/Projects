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
    public class CampaignControllerShould
    {
        private static IMapper _mapper;
        public CampaignControllerShould()
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
        CampaignControllerTest _campaignController;
        [TestInitialize]
        public void Initalize()
        {
            _campaignController = new CampaignControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowCampaignList()
        {
            OkObjectResult result = (OkObjectResult)_campaignController.List();
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
            Assert.IsTrue(Value.Data.Count != 0);
        }

        [TestMethod]
        public void DetailsCampaign()
        {
            int id = 2;
            OkObjectResult result = (OkObjectResult)_campaignController.DetailsCampaigns(id);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
        }

        [TestMethod]
        public void InsertCampaigns()
        {
            CampaignModel camp = new CampaignModel() 
            { 
                cam_Nombre = "New Campaign",
                cam_Descripcion = "New Campaign",
                cam_Html = "<h1>New Campaign</h1>",
                cam_IdUserCreate = 1
            };
            OkObjectResult result = (OkObjectResult)_campaignController.Insert(camp);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
        }

        [TestMethod]
        public void DeleteCampaign()
        {
            int id = 6;
            OkObjectResult result = (OkObjectResult)_campaignController.Delete(id);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
        }
    }
}
