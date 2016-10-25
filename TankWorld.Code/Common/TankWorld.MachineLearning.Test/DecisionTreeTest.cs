using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankWorld.MachineLearning.DecisionTree;

namespace TankWorld.Test
{
    [TestClass]
    public class DecisionTreeTest
    {
        [TestMethod]
        public void EntropyTest()
        {
            Assert.AreEqual(Entropy.GetEntropy(20, 0), 0);
            Assert.AreEqual(Entropy.GetEntropy(20, 20), 1);
            Assert.AreEqual(0, Entropy.InformationGain(10,10,0,0));
        }
    }
}
