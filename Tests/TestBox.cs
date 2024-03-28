using GameLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests
{
    [TestClass]
    public class TestBox
    {
        [TestMethod]
        public void TestConstructorThrows()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Box("ti", null));
        }

        [TestMethod]
        public void TestConstructorSuccess()
        {
            var ps = new Mock<IProbabilityService>();
            var box = new Box("b", ps.Object);
            Assert.AreEqual("b", box.Name);
            ps.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void TestOpenEmptyBoxThrows()
        {
            var ps = new Mock<IProbabilityService>();
            var box = new Box("b", ps.Object);
            Assert.ThrowsException<Exception>(() => box.Open());
            ps.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void TestAddOpenSimple()
        {
            var ps = new Mock<IProbabilityService>();
            var box = new Box("b", ps.Object);
            box.AddItem(2, new Item("i1"));
            box.AddItem(3, new Item("i2"));
            ps.Setup(_ => _.GetProbability(5)).Returns(5);
            Assert.AreEqual("i2", box.Open().Name);
            ps.Setup(_ => _.GetProbability(5)).Returns(2);
            Assert.AreEqual("i1", box.Open().Name);
            ps.Verify(_ => _.GetProbability(5), Times.Exactly(2));
            ps.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void TestAddOpenRecursive()
        {
            var ps = new Mock<IProbabilityService>();

            var boxInner = new Box("bi", ps.Object);
            boxInner.AddItem(4, new Item("ib"));

            var box = new Box("b", ps.Object);
            box.AddItem(2, new Item("i1"));
            box.AddItem(3, new Item("i2"));
            box.AddItem(2, boxInner);
            
            ps.Setup(_ => _.GetProbability(7)).Returns(6);
            ps.Setup(_ => _.GetProbability(4)).Returns(4);
            Assert.AreEqual("ib", box.Open().Name);
            ps.Verify(_ => _.GetProbability(7), Times.Once);
            ps.Verify(_ => _.GetProbability(4), Times.Once);
            ps.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void TestOpenWithWrongService()
        {
            var ps = new Mock<IProbabilityService>();
            var box = new Box("b", ps.Object);
            box.AddItem(2, new Item("i1"));
            ps.Setup(_ => _.GetProbability(2)).Returns(10);
            Assert.ThrowsException<Exception>(() => box.Open());
            ps.Verify(_ => _.GetProbability(2), Times.Once);
            ps.VerifyNoOtherCalls();
        }

    }
}