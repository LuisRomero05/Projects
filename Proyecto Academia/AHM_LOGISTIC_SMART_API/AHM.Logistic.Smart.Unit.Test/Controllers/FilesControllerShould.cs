using AHM.Logistic.Smart.Api.Extensions;
using AHM.Logistic.Smart.Api.TestControllers;
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
    public class FilesControllerShould
    {
        private static IMapper _mapper;
        public FilesControllerShould()
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
        FilesControllerTest _files;
        [TestInitialize]
        public void Initalize()
        {
            _files = new FilesControllerTest(_mapper);
        }

        [TestMethod]
        public void FilesList()
        {
            OkObjectResult result = (OkObjectResult)_files.List();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void FilesInsert()
        {
            var model = new CustomerFilesModel()
            {
                cus_Id = 1,
                cfi_IdUserCreate = 1
            };
            OkObjectResult result = (OkObjectResult)_files.Insert(model);
            Assert.AreEqual(200, result.StatusCode);
        }


        [TestMethod]
        public void FilesDelete()
        {
            int Id = 1;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_files.Delete(Id, Mod);
            Assert.AreNotEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void FilesDeleteError()
        {
            int id = 1;
            int Mod = 1;
            OkObjectResult result = (OkObjectResult)_files.Delete(id, Mod);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
