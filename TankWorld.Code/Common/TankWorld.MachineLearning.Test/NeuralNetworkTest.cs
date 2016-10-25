using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankWorld.MachineLearning.NeuralNetwork;
using TankWorld.MachineLearning.Classification;
using System.Diagnostics;

namespace TankWorld.MachineLearning.Test
{
    [TestClass]
    public class NeuralNetworkTest
    {
        [TestMethod]
        public void SigmoidTest()
        {
            Debug.Print(MLMath.Sigmoid(5).ToString());
        }

        [TestMethod]
        public void LogTest()
        {
            Debug.Print(Math.Log(5).ToString());
        }


        /* Matlab Code
        myTheta=[1,1,1,1,1,1,1,1,1];
myY=[0;1;1;0];
myX=[0,0;0,1;1,0;1,1];
debug_J  = nnCostFunction(myTheta, 2, ...
                          2, 1, myX, myY, 0);
        */

        [TestMethod]
        public void CostTest()
        {
            Matrix theta1 = new double[,] { { 0.5, 1, 1 }, { 1, 1, 1 } };
            Matrix theta2 = new double[,] { { 1, 1, 0.1 } };
            Matrix[] theta = new Matrix[] { theta1, theta2 };
            Matrix x = new double[,] { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 } };
            Vector y = new double[] { 0, 1, 1, 0 };
            var costf = NeuralNetworkTrainer.CostFunction(theta, x, y, 0);
            double cost = costf.Item1;
            var t = costf.Item2;
            Debug.Print(cost.ToString());
            foreach (var tt in t)
            {
                Debug.Print(tt.ToString());
            }
        }

        [TestMethod]
        public void XOrTest()
        {
            int times = 100000;
            NeuralNetworkTrainer trainer = new NeuralNetworkTrainer();
            trainer.HiddenLayers = new int[] { 2 };
            trainer.lambda = 0;
            trainer.AddSample(new BinaryClassificationSample(new double[] { 0, 0 }, 0))
                .AddSample(new BinaryClassificationSample(new double[] { 0, 1 }, 1))
                .AddSample(new BinaryClassificationSample(new double[] { 1, 0 }, 1))
                .AddSample(new BinaryClassificationSample(new double[] { 1, 1 }, 1));
            trainer.Train(times);
            for (int i = 0; i < 9; i++)
            {
                Debug.Print(trainer.costs[i].ToString());
            }
            for (int i = 0; i < times; i += times / 100)
            {
                Debug.Print(trainer.costs[i].ToString());
            }

            NeuralNetworkClassifier classifier = new NeuralNetworkClassifier(trainer.Theta);
            Assert.AreEqual(0, classifier.Predict(0, 0));
            Assert.AreEqual(1, classifier.Predict(0, 1));
            Assert.AreEqual(1, classifier.Predict(1, 1));
            Assert.AreEqual(1, classifier.Predict(1, 1));
        }
    }
}
