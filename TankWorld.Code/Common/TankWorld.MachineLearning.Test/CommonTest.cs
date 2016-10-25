using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TankWorld.MachineLearning.Test
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void LoadMatrixTest()
        {
            Matrix theta1 = Matlab.LoadMatrix("Plugins/nn_theta1.txt");
        }
    }
}
