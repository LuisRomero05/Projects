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
    public class SubCategoriesControllerShould
    {
        private static IMapper _mapper;
        public SubCategoriesControllerShould()
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
        SubCategoriesControllerTest _subcategoriesController;

        [TestInitialize]
        public void Initalize()
        {
            _subcategoriesController = new SubCategoriesControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowSubCategoriesList()
        {
            OkObjectResult result = (OkObjectResult)_subcategoriesController.List();
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SubCategoriesInsert()
        {
            SubCategoriesModel con = new SubCategoriesModel()
            {
                scat_Id = 0,            
                scat_Description = "Xd",
                cat_Id = 1,
                scat_IdUserCreate = 1,
                scat_IdUserModified = 0,
            };
            OkObjectResult result = (OkObjectResult)_subcategoriesController.Insert(con);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }
        [TestMethod]
        public void SubCategoriesUpdate()
        {
            int Id = 5;
            SubCategoriesModel con = new SubCategoriesModel()
            {
                scat_Id = 0,
                scat_Description = "Xd2",
                cat_Id = 1,
                scat_IdUserCreate = 0,
                scat_IdUserModified = 1,
            };
            OkObjectResult result = (OkObjectResult)_subcategoriesController.Update(Id, con);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }
        [TestMethod]
        public void SubCategoriesDelete()
        {
            int Id = 5;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_subcategoriesController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }
    }
}
