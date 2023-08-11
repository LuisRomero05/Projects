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
    public class OrdersControllerShould
    {
        private static IMapper _mapper;
        public OrdersControllerShould()
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
        OrdersControllerTest _ordersControllerTest;
        [TestInitialize]
        public void Initalize()
        {
            _ordersControllerTest = new OrdersControllerTest(_mapper);
        }

        [TestMethod]

        public void OrdersList()
        {
            OkObjectResult result = (OkObjectResult)_ordersControllerTest.List();
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.AreEqual(200, Code);
        }

        [TestMethod]

        public void DetailsOrders()
        {
            int id = 2;
            OkObjectResult result = (OkObjectResult)_ordersControllerTest.SaleOrdersDetails(id);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.IsNotNull(Value.Data);
        }

        [TestMethod]
        public void InsertOrders()
        {
            OrderDetailsModel OS = new OrderDetailsModel()
            {
                ode_Id = 0,
                ode_Amount = 2,
                pro_Id=2,
                ode_TotalPrice=500,
                sor_IdUserCreate=1,
                ode_IdUserModified=0,             
            };

            List<OrderDetailsModel> od = new List<OrderDetailsModel>();
                od.Add(OS);

            SaleOrdersModel ord = new SaleOrdersModel()
            {
                sor_Id=0,
                cus_Id=1,
                cot_Id=1,
                sta_Id=1,
                sor_IdUserCreate=1,
                sor_details= od,
            };
             
            OkObjectResult result = (OkObjectResult)_ordersControllerTest.Insert(ord);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.AreEqual(200, Code);

        }

        [TestMethod]
        public void UpdateOrders()
        {

            SaleOrdersModel cot = new SaleOrdersModel()
            {
                sor_Id = 0,
                cus_Id = 1,
                cot_Id = 1,
                sta_Id = 1,
                sor_IdUserModified = 1,
            };
            OkObjectResult result = (OkObjectResult)_ordersControllerTest.Update(cot);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;

            Assert.AreEqual(200, Code);


        }

        [TestMethod]
        public void DeleteOrders()
        {
            int id = 6;
            int mod = 1;
            OkObjectResult result = (OkObjectResult)_ordersControllerTest.Delete(id, mod);
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.AreEqual(200, Code);
        }

        [TestMethod]

        public void OrdersFail()
        {
            OkObjectResult result = (OkObjectResult)_ordersControllerTest.List();
            int Code = (int)result.StatusCode;
            ServiceResult Value = (ServiceResult)result.Value;
            Assert.AreEqual(400, Code);
        }
    }
}
