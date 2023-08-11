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
    public class CotizationsControllerShould
    {
        private static IMapper _mapper;
        public CotizationsControllerShould()
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
        CotizationsControllerTest _cotizationsController;
        [TestInitialize]
        public void Initalize()
        {
            _cotizationsController = new CotizationsControllerTest(_mapper);
        }

        [TestMethod]

        public void CotizationsList()
        {
            OkObjectResult result = (OkObjectResult)_cotizationsController.List();
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.AreEqual(200, Code);
        }

        [TestMethod]

        public void DetailsCotizations()
        {
            int id = 2;
            OkObjectResult result = (OkObjectResult)_cotizationsController.CotizationsDetails(id);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(Value.Data);
        }

        [TestMethod]
        public void InsertCotizations()
        {
            CotizationsDetailsModel CC = new CotizationsDetailsModel()
            {
                code_Id = 001,
                code_Cantidad = 5,
                pro_Id = 1,
                code_TotalPrice = 500
            };

            List<CotizationsDetailsModel> cod = new List<CotizationsDetailsModel>();
            cod.Add(CC);
            
           

            CotizationsModel cot = new CotizationsModel()
            {
                cot_Id = 0,
                cus_Id = 1,
                cot_DateValidUntil = DateTime.Now,
                sta_Id = 1, 
                cot_IdUserCreate = 1,
                cot_IdUserModified = 0,
                cot_details = cod
            };
            OkObjectResult result = (OkObjectResult)_cotizationsController.Insert(cot);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;           
            Assert.AreEqual(200, Code);
            
        }

        [TestMethod]
        public void UpdateCotizations()
        {
            
            CotizationsModel cot = new CotizationsModel()
            {
                cot_Id=2,
                cus_Id = 2,
                cot_DateValidUntil = DateTime.Now,
                sta_Id = 2,              
                cot_IdUserModified = 2,
            };
            OkObjectResult result = (OkObjectResult)_cotizationsController.Update(cot);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            
            Assert.AreEqual(200, Code);
            
            
        }

        [TestMethod]
        public void DeleteCotizations()
        {
            int id = 6;
            int mod = 1;
            OkObjectResult result = (OkObjectResult)_cotizationsController.Delete(id,mod);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;           
            Assert.AreEqual(200, Code);
        }

        [TestMethod]

        public void CotizationsFail()
        {
            OkObjectResult result = (OkObjectResult)_cotizationsController.List();
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.AreEqual(400, Code);
        }

    }
}
