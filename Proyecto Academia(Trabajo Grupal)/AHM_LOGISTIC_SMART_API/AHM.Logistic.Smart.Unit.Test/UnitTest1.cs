using AHM.Logistic.Smart.Api.Controllers;
using AHM.Logistic.Smart.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AHM.Logistic.Smart.Unit.Test
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
            Assert.AreEqual(9,result);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var result = _testController.ListaAreasCodigo();
            Assert.AreEqual(200,result);
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

        //private class UpdatedUser
        //{
        //    public int Id { get; set; }
        //    public string Username { get; set; }
        //    public override bool Equals(object obj)
        //    {
        //        return Id == ((UpdatedUser)obj).Id;
        //    }
        //}
        //[TestMethod]
        //public void TestMethod5()
        //{
        //    UpdatedUser expected = new UpdatedUser() { Id = 1, Username = "Tetris" };
        //    UpdatedUser actual = new UpdatedUser() { Id = 2, Username = "Tetris" };
        //    var result = _testController.ListaAreas();
        //    Assert.AreEqual(expected, actual);
        //}
    }
}
