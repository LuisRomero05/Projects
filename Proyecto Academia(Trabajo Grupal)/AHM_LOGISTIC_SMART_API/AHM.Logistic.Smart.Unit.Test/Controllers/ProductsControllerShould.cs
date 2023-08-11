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
    public class ProductsControllerShould
    {
        private static IMapper _mapper;
        public ProductsControllerShould()
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
        ProductsControllerTest _productsController;
        [TestInitialize]
        public void Initalize()
        {
            _productsController = new ProductsControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowProductsList()
        {
            OkObjectResult result = (OkObjectResult)_productsController.List();
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
            Assert.IsTrue(Value.Data.Count != 0);
        }

        [TestMethod]
        public void ProductsInsert()
        {
            ProductsModel pro = new ProductsModel()
            {
                pro_Id = 0,
                pro_Description = "NuevoProduct",
                pro_PurchasePrice = 100,
                pro_SalesPrice = 120,
                pro_Stock = 30,
                pro_ISV = 15,
                uni_Id = 1,
                scat_Id = 2,
                pro_IdUserCreate = 1,
                pro_IdUserModified = 0,
            };
            OkObjectResult result = (OkObjectResult)_productsController.Insert(pro);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }
        [TestMethod]
        public void ProductsUpdate()
        {
            int Id = 46;
            ProductsModel pro = new ProductsModel()
            {
                pro_Id = 0,
                pro_Description = "NuevoProductUpdate",
                pro_PurchasePrice = 100,
                pro_SalesPrice = 120,
                pro_Stock = 30,
                pro_ISV = 15,
                uni_Id = 1,
                scat_Id = 2,
                pro_IdUserCreate = 1,
                pro_IdUserModified = 0,
            };
            OkObjectResult result = (OkObjectResult)_productsController.Update(Id, pro);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }

        [TestMethod]
        public void ProductsDelete()
        {
            int Id = 45;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_productsController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }

        [TestMethod]
        public void ProductsDeleteError()
        {
            int Id = 46;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_productsController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.IsNotNull(Value.Data);
            Assert.IsTrue(Value.Data != null);
        }
    }
}
