using GameLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests
{
    [TestClass]
    public class TestItemProbability
    {
        [TestMethod]
        public void TestConstructorSuccess()
        {
            var itemProbability = new ItemProbability(4, new Item("ti"));
            Assert.AreEqual("ti", itemProbability.Item.Name);
            Assert.AreEqual(4u, itemProbability.Weight);
        }

        [TestMethod]
        public void TestConstructorThrows()
        {
            Assert.ThrowsException<ArgumentException>(() => new ItemProbability(0, new Item("i")));
            Assert.ThrowsException<ArgumentNullException>(() => new ItemProbability(1, null));
        }
    }
}