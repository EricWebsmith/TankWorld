// ===============================
// AUTHOR     : Weiguang Zhou
// CREATE DATE: 24/11/2015
// PURPOSE    :
// This is a very simple logistic regression class.
// SPECIAL NOTES:
// ===============================
// Change History:
//
//==================================

using System;
using System.Collections.Generic;
using TankWorld.MachineLearning.Classification;

namespace TankWorld.MachineLearning.LogisticRegression
{
    public class LogisticClassifier:IClassifier
    {
        //the sample list.
        List<BinaryClassificationSample> samples = new List<BinaryClassificationSample>();
        int featureCount_plus_1 = 0;
        int sampleCount = 0;

        /// <summary>
        /// 
        /// </summary>
        public double[] Theta { get; set; }

        /// <summary>
        /// The first feature x0==1;
        /// </summary>
        double[,] x;
        double[] y;

        /// <summary>
        /// Regularisatioin parameter.
        /// </summary>
        public double Lambda { get; set; }

        /// <summary>
        /// Controls the step scale of gradient descent.
        /// </summary>
        public double Alpha { get; set; }

        public LogisticClassifier(int featureNumber, double alpha = 0.1, double lambda = 0)
        {
            this.Alpha = alpha;
            this.Lambda = lambda;

            featureCount_plus_1 = featureNumber + 1;
            Theta = MLMath.GetRandomVector(featureCount_plus_1, 0, 1);
        }

        public LogisticClassifier AddSample(double[] x, double y)
        {
            samples.Add(new BinaryClassificationSample(x, y));
            return this;
        }

        public double H(double[] x)
        {
            double z = MLMath.ItemMultiplyAndSum(Theta, x);
            return MLMath.Sigmoid(z);
        }

        public double[] H()
        {
            int m = x.GetLength(0);
            double[] z = new double[m];
            for (int i = 0; i < m; i++)
            {
                double[] x_vector = MLMath.GetVector(x, 0, i);
                z[i] = MLMath.ItemMultiplyAndSum(Theta, x_vector);
            }
            return MLMath.ItemCalculate(z, c => MLMath.Sigmoid(c));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="times"></param>
        /// <returns>The cost of each iteration</returns>
        public List<BinaryClassificationTrainResult> Train(int times)
        {
            List<BinaryClassificationTrainResult> results = new List<BinaryClassificationTrainResult>();
            //double[] costs = new double[times];
            sampleCount = samples.Count;
            x = new double[sampleCount, featureCount_plus_1];
            y = new double[sampleCount];
            Theta = MLMath.GetRandomVector(featureCount_plus_1, 0, 1);
            for (int i = 0; i < sampleCount; i++)
            {
                x[i, 0] = 1;
                for (int j = 0; j < featureCount_plus_1 - 1; j++)
                {
                    x[i, j + 1] = samples[i].X[j];
                }
                y[i] = samples[i].Y;
            }

            for (int i = 0; i < times; i++)
            {
                Tuple<double, double[]> costFunction = CostFunction();
                Theta = MLMath.Add(Theta, costFunction.Item2);
                BinaryClassificationTrainResult result = new BinaryClassificationTrainResult();
                result.Cost = costFunction.Item1;
                result.Theta = Theta;
                results.Add(result);
            }
            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theta"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="lambda"></param>
        /// <returns>Item1 - cost, Item2 - grad</returns>
        public Tuple<double, double[]> CostFunction()
        {
            int m = y.Length;
            int n = Theta.Length;
            double[] one_minus_y = MLMath.ItemCalculate(1, y, (a, b) => a - b);
            double[] h = H();
            double[] log_h = MLMath.ItemCalculate(h, i => Math.Log(i));
            double[] one_minus_h = MLMath.ItemCalculate(1, h, (a, b) => a - b);
            double[] log_1_minus_h = MLMath.ItemCalculate(one_minus_h, i => Math.Log(i));
            double cost = (1.0 / m) * (-MLMath.ItemMultiplyAndSum(y, log_h) - MLMath.ItemMultiplyAndSum(one_minus_y, log_1_minus_h));
            //Regularization
            double[] theta_reg = new double[n];
            theta_reg[0] = 0;
            for (int i = 1; i < n; i++)
            {
                theta_reg[i] = Theta[i];
            }
            double reg = Lambda / (2 * m) * (MLMath.ItemMultiplyAndSum(theta_reg, theta_reg));
            cost += reg;
            //Gradient
            double[] grad = new double[n];
            for (int i = 0; i < n; i++)
            {
                double[] x_i_vector = MLMath.GetVector(x, 1, i);
                grad[i] = -Alpha * ((1.0 / m) * MLMath.ItemMultiplyAndSum(x_i_vector, MLMath.Minus(h, y)) + Lambda / m * theta_reg[i]);
            }
            return new Tuple<double, double[]>(cost, grad);
        }

        public int Predict(double[] x)
        {
            var newX = new double[x.Length + 1];
            newX[0] = 1;
            for (int i = 0; i < x.Length; i++)
            {
                newX[i + 1] = x[i];
            }
            double h = H(newX);
            if (h >= 0.5)
            {
                return 1;
            }
            return 0;
        }

        public double PredictByPercentage(double[] x)
        {
            var newX = new double[x.Length + 1];
            newX[0] = 1;
            for (int i = 0; i < x.Length; i++)
            {
                newX[i + 1] = x[i];
            }
            double h = H(newX);
            //if (h >= 0.5)
            //{
            //    return 1;
            //}
            return h;
        }
    }
}
