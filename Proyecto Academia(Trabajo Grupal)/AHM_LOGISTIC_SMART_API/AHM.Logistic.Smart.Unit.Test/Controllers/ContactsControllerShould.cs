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
    public class ContactsControllerShould
    {
        private static IMapper _mapper;
        public ContactsControllerShould()
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
        ContactsControllerTest _contactsController;

        [TestInitialize]
        public void Initalize()
        {
            _contactsController = new ContactsControllerTest(_mapper);
        }

        [TestMethod]
        public void ShowContactsList()
        {
            OkObjectResult result = (OkObjectResult)_contactsController.List();
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void ContactsInsert()
        {
            ContactsModel con = new ContactsModel()
            {
                cont_Id = 0,
                cont_Name = "XD",
                cont_LastName = "XD2",
                cont_Email = "XD",
                cont_Phone = "1234",
                occ_Id = 1,
                cus_Id = 1,
                cont_IdUserCreate = 1,
                cont_IdUserModified = 0,
        };
            OkObjectResult result = (OkObjectResult)_contactsController.Insert(con);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }
        [TestMethod]
        public void ContactsUpdate()
        {
            int Id = 5;
            tbContacts con = new tbContacts()
            {
                cont_Id = 0,
                cont_Name = "XD",
                cont_LastName = "XD2",
                cont_Email = "XD",
                cont_Phone = "1234",
                occ_Id = 1,
                cus_Id = 1,
                cont_IdUserCreate = 1,
                cont_IdUserModified = 0,
            };
            OkObjectResult result = (OkObjectResult)_contactsController.Update(con, Id);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }
        [TestMethod]
        public void ContactsDelete()
        {
            int Id = 5;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_contactsController.Delete(Id, Mod);
            int Code = (int)result.StatusCode;
            ServiceResult srv = (ServiceResult)result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, Code);
            Assert.AreNotEqual(400, Code);
        }

    }
}
