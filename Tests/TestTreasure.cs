using GameLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests
{
    [TestClass]
    public class TestTreasure
    {
        [TestMethod]
        public void TestConstructorThrows()
        {
            var ps = new Mock<IProbabilityService>();
            var list = new List<BoxInfo>();
            list.Add(new BoxInfo());
            Assert.ThrowsException<ArgumentNullException>(() => new Treasure(null, list));
            Assert.ThrowsException<ArgumentNullException>(() => new Treasure(ps.Object, null));
            Assert.ThrowsException<ArgumentException>(() => new Treasure(ps.Object, new List<BoxInfo>()));
            ps.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void TestConstructorDuplicateBoxesThrows()
        {
            var ps = new Mock<IProbabilityService>();
            var list = new List<BoxInfo>
            {
               
                new BoxInfo()
                {
                    Name = "RewardTC2",
                    Probabilities = new[]
                {
                    new ProbabilityInfo { Name = "RewardTC3", Weight = 2 },
                    new ProbabilityInfo { Name = "Gem2", Weight = 4 },
                }
                },
                new BoxInfo()
                {
                    Name = "RewardTC2",
                    Probabilities = new[]
                {
                    new ProbabilityInfo { Name = "Gem3", Weight = 1 },
                }
                }
            };

            Assert.ThrowsException<Exception>(() => new Treasure(ps.Object, list));
            ps.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void TestGetItemThrows()
        {
            var ps = new Mock<IProbabilityService>();
            var list = new List<BoxInfo>
            {
                new BoxInfo()
                {
                    Name = "RewardTC3",
                    Probabilities = new[]
                {
                    new ProbabilityInfo { Name = "Gem3", Weight = 1 },
                }
                }
            };

            var t = new Treasure(ps.Object, list);
            Assert.ThrowsException<Exception>(() => t.GetItemFromBox("fff"));
            ps.VerifyNoOtherCalls();
        }

        //RewardTC1 RewardTC2	6	RewardTC3	4	Gem1	2
        //RewardTC2 RewardTC3	2	Gem2	4
        //RewardTC3 Gem3	1
        [TestMethod]
        public void TestGetItemSampleCase()
        {
            var ps = new Mock<IProbabilityService>();
            var list = new List<BoxInfo>
            {
                new BoxInfo()
                {
                    Name = "RewardTC1",
                    Probabilities = new[]
                {
                    new ProbabilityInfo { Name = "RewardTC2", Weight = 6 },
                    new ProbabilityInfo { Name = "RewardTC3", Weight = 4 },
                    new ProbabilityInfo { Name = "Gem1", Weight = 2 }
                }
                },
                new BoxInfo()
                {
                    Name = "RewardTC2",
                    Probabilities = new[]
                {
                    new ProbabilityInfo { Name = "RewardTC3", Weight = 2 },
                    new ProbabilityInfo { Name = "Gem2", Weight = 4 },
                }
                },
                new BoxInfo()
                {
                    Name = "RewardTC3",
                    Probabilities = new[]
                {
                    new ProbabilityInfo { Name = "Gem3", Weight = 1 },
                }
                }
            };

            var t = new Treasure(ps.Object, list);
            ps.Setup(_ => _.GetProbability(12)).Returns(7);
            ps.Setup(_ => _.GetProbability(1)).Returns(1);
            var i = t.GetItemFromBox("RewardTC1");
            Assert.AreEqual("Gem3", i.Name);
            ps.Verify(_ => _.GetProbability(12), Times.Once());
            ps.Verify(_ => _.GetProbability(1), Times.Once());
            ps.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void TestHasBox()
        {
            var ps = new Mock<IProbabilityService>();
            var list = new List<BoxInfo>
            {
                new BoxInfo()
                {
                    Name = "RewardTC3",
                    Probabilities = new[]
                {
                    new ProbabilityInfo { Name = "Gem3", Weight = 1 },
                }
                }
            };

            var t = new Treasure(ps.Object, list);
            Assert.IsTrue(t.HasBox("RewardTC3"));
            ps.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void TestBoxNames()
        {
            var ps = new Mock<IProbabilityService>();
            var list = new List<BoxInfo>
            {
                new BoxInfo()
                {
                    Name = "RewardTC1",
                    Probabilities = new[]
                {
                    new ProbabilityInfo { Name = "RewardTC2", Weight = 6 },
                    new ProbabilityInfo { Name = "RewardTC3", Weight = 4 },
                    new ProbabilityInfo { Name = "Gem1", Weight = 2 }
                }
                },
                new BoxInfo()
                {
                    Name = "RewardTC2",
                    Probabilities = new[]
                {
                    new ProbabilityInfo { Name = "RewardTC3", Weight = 2 },
                    new ProbabilityInfo { Name = "Gem2", Weight = 4 },
                }
                },
                new BoxInfo()
                {
                    Name = "RewardTC3",
                    Probabilities = new[]
                {
                    new ProbabilityInfo { Name = "Gem3", Weight = 1 },
                }
                }
            };

            var t = new Treasure(ps.Object, list);
            var bn = t.BoxNames;
            Assert.AreEqual(3, bn.Count());
            Assert.AreEqual("RewardTC1", bn.ElementAt(0));
            Assert.AreEqual("RewardTC2", bn.ElementAt(1));
            Assert.AreEqual("RewardTC3", bn.ElementAt(2));
            ps.VerifyNoOtherCalls();
        }
    }
}