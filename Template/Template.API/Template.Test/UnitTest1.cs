using AutoMapper;
using Consul;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Template.API.Controllers;

namespace Template.Test
{
    [TestClass]
    public class UnitTest1
    {
        private TestController _testController;
        [TestInitialize]
        public void Initialize()
        {
            _testController = new TestController();
        }
        [TestMethod]
        public void TestMethod1()
        {
            var result = _testController.Index();
            Assert.AreEqual(9, result);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var result = _testController.ListaAreasCodigo();
            Assert.AreEqual(200, result);
        }
        [TestMethod]
        public void TestMethod3()
        {
            var result = _testController.ListaAreasCodigo();
            Assert.AreNotEqual(400, result);
            Assert.AreNotEqual(404, result);
        }
        [TestMethod]
        public void TestMethod4()
        {
            var result = _testController.ListaAreas();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var result = _testController.ListaAreasGrande();
            Assert.IsNotNull(result);
        }

        
    }
}
