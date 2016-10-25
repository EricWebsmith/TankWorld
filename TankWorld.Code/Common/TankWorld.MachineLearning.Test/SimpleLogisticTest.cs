using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using TankWorld.MachineLearning;
using TankWorld.MachineLearning.LogisticRegression;

namespace TankWorld.MachineLearning.Test
{
    [TestClass]
    public class SimpleLogisticTest
    {
        #region Binary Logistic Regression
        [TestMethod]
        public void GetRandomVectorTest()
        {
            double[] r = MLMath.GetRandomVector(10, 0, 1);
            foreach (double d in r)
            {
                Debug.Print(d.ToString());
            }
            Assert.AreNotEqual(r[0], r[1]);
            Assert.AreNotEqual(r[2], r[3]);
            Assert.AreNotEqual(r[3], r[1]);
            Assert.AreNotEqual(r[5], r[1]);
        }

        /// <summary>
        /// This is to test the LogisticRegression
        /// </summary>
        [TestMethod]
        public void OrTest()
        {
            LogisticClassifier regr = new LogisticClassifier(2);
            regr.AddSample(new double[] { 0, 0 }, 0)
            .AddSample(new double[] { 0, 1 }, 1)
            .AddSample(new double[] { 1, 0 }, 1)
            .AddSample(new double[] { 1, 1 }, 1);

            int times = 1000;
            regr.Alpha = 1;
            regr.Lambda = 0.01;
            List<BinaryClassificationTrainResult> results = regr.Train(times);


            for (int i = 0; i < times; i++)
            {
                if (i % 100 != 0)
                {
                    continue;
                }
                Debug.Print(results[i].Cost.ToString());
                for (int t = 0; t < results[i].Theta.Length; t++)
                {
                    Debug.Print("\t" + results[i].Theta[t].ToString());
                }
            }
            double[] h = new double[4];
            h[0] = regr.H(new double[] { 1, 0, 0 });
            h[1] = regr.H(new double[] { 1, 0, 1 });
            h[2] = regr.H(new double[] { 1, 1, 0 });
            h[3] = regr.H(new double[] { 1, 1, 1 });
            for (int i = 0; i < 4; i++)
            {
                Debug.Print(h[i].ToString());
            }
            Assert.AreEqual(regr.Predict(new double[] { 0, 0 }), 0);
            Assert.AreEqual(regr.Predict(new double[] { 0, 1 }), 1);
            Assert.AreEqual(regr.Predict(new double[] { 1, 0 }), 1);
            Assert.AreEqual(regr.Predict(new double[] { 1, 1 }), 1);
        }

        /// <summary>
        /// This is to test the LogisticRegression
        /// </summary>
        [TestMethod]
        public void AndTest()
        {
            LogisticClassifier regr = new LogisticClassifier(2);
            regr.AddSample(new double[] { 0, 0 }, 0);
            regr.AddSample(new double[] { 0, 1 }, 0);
            regr.AddSample(new double[] { 1, 0 }, 0);
            regr.AddSample(new double[] { 1, 1 }, 1);
            //regr.AddSample(new double[] { 1, 1 }, 1);
            //regr.AddSample(new double[] { 1, 1 }, 1);
            //regr.AddSample(new double[] { 1, 1 }, 1);
            int times = 1000;
            regr.Alpha = 3;
            regr.Lambda = 0.01;
            List<BinaryClassificationTrainResult> results = regr.Train(times);


            for (int i = 0; i < times; i++)
            {
                if (i % 100 != 0)
                {
                    continue;
                }
                Debug.Print(results[i].Cost.ToString());
                for (int t = 0; t < results[i].Theta.Length; t++)
                {
                    Debug.Print("\t" + results[i].Theta[t].ToString());
                }
            }
            double[] h = new double[4];
            h[0] = regr.H(new double[] { 1, 0, 0 });
            h[1] = regr.H(new double[] { 1, 0, 1 });
            h[2] = regr.H(new double[] { 1, 1, 0 });
            h[3] = regr.H(new double[] { 1, 1, 1 });
            for (int i = 0; i < 4; i++)
            {
                Debug.Print(h[i].ToString());
            }
            Assert.AreEqual(regr.Predict(new double[] { 0, 0 }), 0);
            Assert.AreEqual(regr.Predict(new double[] { 0, 1 }), 0);
            Assert.AreEqual(regr.Predict(new double[] { 1, 0 }), 0);
            Assert.AreEqual(regr.Predict(new double[] { 1, 1 }), 1);
        }
        #endregion

        #region MultiClassification

        /// <summary>
        /// In the test, we try to buid a model to find out 
        /// the dissertation type a student will choose.
        /// the first 3 features are student's majors. They are
        /// Computer science, Game and Graphics, KDD
        /// the last 2 features are previous experience in fields. they are:
        /// Game and Graphics Experience, KDD Experience.
        /// 
        /// The the classes are game, kdd and others
        /// 
        /// All features(x) and classes(y) are presented by 0 or 1.
        /// </summary>
        [TestMethod]
        public void MC_ExamResult_Test()
        {
            MultiLogisticClassifier regr = new MultiLogisticClassifier(5, 3);
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 0, 0, 1, 0, 1 }, new double[] { 0, 1, 0 });
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 1, 0, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 1, 0, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 1, 0, 0, 1, 0 }, new double[] { 1, 0, 0 });
            regr.AddSample(new double[] { 1, 0, 0, 1, 0 }, new double[] { 0, 0, 1 });
            regr.AddSample(new double[] { 1, 0, 0, 0, 0 }, new double[] { 0, 0, 1 });
            regr.AddSample(new double[] { 1, 0, 0, 0, 0 }, new double[] { 0, 0, 1 });
            regr.AddSample(new double[] { 1, 0, 0, 0, 0 }, new double[] { 0, 0, 1 });
            regr.AddSample(new double[] { 1, 0, 0, 0, 0 }, new double[] { 0, 0, 1 });
            regr.AddSample(new double[] { 1, 0, 0, 0, 0 }, new double[] { 0, 0, 1 });
            regr.AddSample(new double[] { 1, 0, 0, 0, 0 }, new double[] { 0, 1, 0 });

            int times = 1000;
            regr.Alpha = 3;
            regr.Lambda = 0.01;
            var results = regr.Train(times);


            for (int i = 0; i < times; i++)
            {
                if (i % 100 != 0)
                {
                    continue;
                }
                Debug.Print(results[i].Cost.ToString());
                for (int t = 0; t < results[i].Theta.GetLength(0); t++)
                {
                    string theta_string = string.Empty;
                    for (int s = 0; s < results[i].Theta.GetLength(1);s++)
                    {
                        theta_string += results[i].Theta[t, s].ToString() + ", ";
                    }
                    Debug.Print("\t" + theta_string);
                }
            }

            Assert.IsTrue(MLMath.AreEqual( regr.Predict(new double[] { 0, 1, 0, 1, 0 }), new double[] { 1, 0, 0 }));
            Assert.IsTrue(MLMath.AreEqual( regr.Predict(new double[] { 0, 0, 1, 0, 1 }), new double[] { 0, 1, 0 }));
            Assert.IsTrue(MLMath.AreEqual(regr.Predict(new double[] { 1, 0, 0, 1, 0 }), new double[] { 1, 0, 0 }));
            Assert.IsTrue(MLMath.AreEqual(regr.Predict(new double[] { 1, 0, 0, 0, 0 }), new double[] { 0, 0, 1 }));
        }
        #endregion
    }
}
