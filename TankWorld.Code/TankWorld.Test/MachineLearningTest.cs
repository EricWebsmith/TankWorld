using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TankWorld.MachineLearning;
using TankWorld.MachineLearning.LogisticRegression;
using TankWorld.MachineLearning.Trainers;
using TankWorld.MachineLearningPlayers;

namespace TankWorld.Test
{
    [TestClass]
    public class MachineLearningTest
    {
        /// <summary>
        /// This is not a test!!
        /// </summary>
        [TestMethod]
        public void PrintFeatures()
        {
            for (int i = 1; i < 41; i++)
            {
                Debug.Print("        [Ezfx.Csv.SystemCsvColumn(Name = \"X{0}\", Ordinal = {1})]", i, i - 1);
                Debug.Print("        public double X" + i.ToString() + " { get; set; }");
            }
        }

        /// <summary>
        /// Smock Test
        /// </summary>
        [TestMethod]
        public void CollectFeatures()
        {
            SampleCollector collector = new SampleCollector();
            collector.Collect(20);
            //Save x
            int l = collector.samples.Count;
            StreamWriter sw = new StreamWriter("f:\\x.txt");
            for (int i = 0; i < l; i++)
            {

                sw.WriteLine(collector.samples[i].X.ToLine());
            }
            sw.Flush();
            sw.Close();
            //save y
            StreamWriter sw_y = new StreamWriter("f:\\y.txt");
            for (int i = 0; i < l; i++)
            {
                sw_y.WriteLine(collector.samples[i].Y.ToLine());
            }
            sw_y.Flush();
            sw_y.Close();
        }

        [TestMethod]
        public void TrainTest()
        {
            List<double[]> x = FeatureExtension.LoadMatrix("F:\\x.txt");
            List<double[]> y = FeatureExtension.LoadMatrix("F:\\y.txt");
            MultiLogisticClassifier regr = new MultiLogisticClassifier(FeatureGenerator.FeatureCount, 6, 3, 0);
            int m = 1000; //x.Count();
            for (int i = 0; i < m; i++)
            {
                regr.AddSample(x[i], y[i]);
            }
            int times = 2000;
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
                    for (int s = 0; s < results[i].Theta.GetLength(1); s++)
                    {
                        theta_string += results[i].Theta[t, s].ToString() + ", ";
                    }
                    Debug.Print("\t" + theta_string);
                }
            }

            List<double[]> x_test = FeatureExtension.LoadMatrix("F:\\x_test.txt");
            List<double[]> y_test = FeatureExtension.LoadMatrix("F:\\y_test.txt");
            int m_test = 100; x_test.Count();
            //List<double[]> x_test = x;
            //List<double[]> y_test = y;
            //int m_test = 100; //x_test.Count();
            int correct_test = 0;
            int error_compare = 0;
            for (int i = 0; i < m_test; i++)
            {
                if (error_compare < 20)
                {
                    double[] p1 = regr.Predict(x_test[i]);
                    if (MLMath.AreEqual(p1, y[i]))
                    {
                        correct_test += 1;
                    }
                    else
                    {
                        double[] p2 = regr.PredictByPercentage(x_test[i]);
                        Debug.Print(y[i].ToLine());
                        Debug.Print(p1.ToLine());
                        Debug.Print(p2.ToLine());
                        error_compare++;
                    }

                }
            }
            double precise = correct_test * 1.0 / m_test;
            Debug.Print(precise.ToString());

            var theta = results.Last().Theta;
            FeatureExtension.SaveMatrix(theta, "F:\\theta.txt");

        }

    }
}
