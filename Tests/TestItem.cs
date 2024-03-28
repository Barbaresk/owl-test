using GameLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests
{
    [TestClass]
    public class TestItem
    {
        [TestMethod]
        public void TestConstructor()
        {
            var item = new Item("ti");
            Assert.AreEqual("ti", item.Name);
        }

        [TestMethod]
        public void TestOpen()
        {
            var item = new Item("ti");
            Assert.AreEqual(item, item.Open());
        }
    }
}