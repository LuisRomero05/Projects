using AHM.Logistic.Smart.Api.Extensions;
using AHM.Logistic.Smart.Api.TestControllers;
using AHM.Logistic.Smart.BusinessLogic;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.Entities.Entities;
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
    public class CategoriesControllerShould
    {
        private static IMapper _mapper;
        public CategoriesControllerShould()
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
        CategoriesControllerTest _categoriesController;

        [TestInitialize]
        public void Initalize()
        {
            _categoriesController = new CategoriesControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowCategoriesList()
        {
            OkObjectResult result = (OkObjectResult)_categoriesController.List();
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CategoriesInsert()
        {
            CategoryModel con = new CategoryModel()
            {
                cat_Id = 0,
                cat_Description = "Xd",
                cat_IdUserCreate = 1,
                cat_IdUserModified = 0,
            };
            OkObjectResult result = (OkObjectResult)_categoriesController.Insert(con);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }
        [TestMethod]
        public void CategoriesUpdate()
        {
            int Id = 5;
            CategoryModel con = new CategoryModel()
            {
                cat_Id = 0,
                cat_Description = "Xd",
                cat_IdUserCreate = 0,
                cat_IdUserModified = 1,
            };
            OkObjectResult result = (OkObjectResult)_categoriesController.Update(Id,con);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }
        [TestMethod]
        public void CategoriesDelete()
        {
            int Id = 5;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_categoriesController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }
    }
}
