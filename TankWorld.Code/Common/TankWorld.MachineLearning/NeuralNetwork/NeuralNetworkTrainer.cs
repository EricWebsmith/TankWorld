using System;
using System.Collections.Generic;
using TankWorld.MachineLearning.Classification;
using System.Linq;
using System.Diagnostics;

namespace TankWorld.MachineLearning.NeuralNetwork
{
    public class NeuralNetworkTrainer : BaseClassificationTrainer
    {
        private List<BinaryClassificationSample> samples = new List<BinaryClassificationSample>();
        public int[] HiddenLayers;
        public double lambda = 0.1;
        public double[] costs;
        public Matrix[] Theta;

        public override void Train(int times)
        {
            costs = new double[times];
            List<BinaryClassificationTrainResult> results = new List<BinaryClassificationTrainResult>();
            //double[] costs = new double[times];
            var xy = GetXY();
            var x = xy.Item1;
            var y = xy.Item2;
            Theta = GetThetaList(HiddenLayers, FeatureCount);
            double lowestCost= CostFunction(Theta, x, y, lambda).Item1;
            for (int i = 0; i < times; i++)
            {
                var temp= GetThetaList(HiddenLayers, FeatureCount);
                Tuple<double, Matrix[]> costFunction = CostFunction(temp, x, y, lambda);
                //for (int layer = 0; layer <= HiddenLayers.Length; layer++)
                //{
                //    Theta[layer] +=0.001* (costFunction.Item2[layer]);
                //}

                //costs[i] = lowestCost;
                if (costFunction.Item1 < lowestCost)
                {
                    Theta = temp;
                    lowestCost = costFunction.Item1;
                }
                costs[i] = lowestCost;
            }
        }

        private static Matrix[] GetThetaList(int[] hiddenLayers, int featureCount)
        {
            var thetaList = new Matrix[hiddenLayers.Length + 1];
            thetaList[0] = MLMath.GetRandomMatrix(hiddenLayers[0], featureCount + 1, 0, 1);
            // int previousLayerNeuronCount = FeatureCount;
            for (int layer = 1; layer < hiddenLayers.Length; layer++)
            {
                thetaList[layer] = MLMath.GetRandomMatrix(hiddenLayers[layer], hiddenLayers[layer - 1] + 1, 0, 1);
                // previousLayerNeuronCount = HiddenLayers[layer];
            }
            //last
            thetaList[hiddenLayers.Length] = MLMath.GetRandomMatrix(1, hiddenLayers.Last() + 1, 0, 1);
            return thetaList;
        }

        public static Tuple<double, Matrix[]> CostFunction(Matrix[] theta, Matrix x, Vector y, double lambda)
        {
            //lambda = 0;
            int m = x.Length0;
            int layerCount = theta.Count();
            var a = new Matrix[layerCount + 1];
            var a_with1 = new Matrix[layerCount + 1];
            Matrix[] z = new Matrix[layerCount + 1];
            Matrix[] theta_reg = new Matrix[layerCount];

            a[0] = x;
            a_with1[0] = a[0].AddX0();

            for (int layer = 1; layer <= theta.Length; layer++)
            {
                z[layer] = a_with1[layer - 1] * (~theta[layer - 1]);
                a[layer] = z[layer].EveryItem(MLMath.Sigmoid);
                a_with1[layer] = a[layer].AddX0();
                theta_reg[layer - 1] = theta[layer - 1].ChangeCol(0, c => 0);
            }
            Vector h = a.Last().ExtractColumn();
            //cost Without Regularization
            //J = (1/m)*sum(sum((-Y .* log(H)-(1-Y) .* log(1-H)),2));
            Matrix part1 = h.EveryItem(Math.Log);


            double cost = (1.0 / m) * (-y * h.EveryItem(Math.Log) - (1 - y) * (1 - h).EveryItem(Math.Log)).Sum();

            //cost with Regularization
            cost += lambda / (2 * m) * theta_reg.Sum(c => c.EveryItem(i => Math.Pow(i, 2)).Sum());
            //Calculate Delta
            Matrix[] delta = new Matrix[layerCount + 1];
            Matrix[] Delta = new Matrix[layerCount];
            Matrix[] theta_grad = new Matrix[layerCount];
            delta[layerCount] = ~(h - y);
            //Debug.Print(delta[layerCount].ToString());
            //theta_grad[layerCount - 1] = delta[layerCount - 1] / m + (lambda / m) * theta_reg[layerCount - 1];
            for (int l = layerCount - 1; l > 0; l--)
            {
                var sg = z[l].AddX0().EveryItem(MLMath.SigmoidGradient);
                delta[l] = (delta[l + 1] * theta[l]).EveryItem(sg, (u, v) => u * v);
                delta[l] = delta[l].RemoveColumn(0);
                //theta_grad[l ] =a[l] delta[l ] / m + (lambda / m) * theta_reg[l ];
            }

            //Accumulating the gradient
            for (int l = 0; l < layerCount; l++)
            {
                Delta[l] = ~delta[l + 1] * a_with1[l];
                theta_grad[l] = Delta[l] / m + (lambda / m) * theta_reg[l];
                //Debug.Print("theta_grad" + l.ToString());
                //Debug.Print(theta_grad[l].ToString());
            }
            return new Tuple<double, Matrix[]>(cost, theta_grad);
        }

        public static Tuple<double, Matrix[]> CostFunction(Matrix[] theta, Matrix x, Matrix y, double lambda)
        {
            lambda = 0;
            int m = x.Length0;
            int layerCount = theta.Count();
            var a = new Matrix[layerCount + 1];
            var a_with1 = new Matrix[layerCount + 1];
            Matrix[] z = new Matrix[layerCount + 1];
            Matrix[] theta_reg = new Matrix[layerCount];

            a[0] = x;
            a_with1[0] = a[0].AddX0();

            for (int layer = 1; layer <= theta.Length; layer++)
            {
                z[layer] = a_with1[layer - 1] * (~theta[layer - 1]);
                a[layer] = z[layer].EveryItem(MLMath.Sigmoid);
                a_with1[layer] = a[layer].AddX0();
                //z[layer+1 ] = a_with1[layer ] * (theta[layer ]);
                theta_reg[layer - 1] = theta[layer - 1].ChangeCol(0, c => 0);
            }
            Vector h = a.Last().ExtractColumn();
            //cost Without Regularization
            //J = (1/m)*sum(sum((-Y .* log(H)-(1-Y) .* log(1-H)),2));
            double cost = (1.0 / m) * (-y * h.EveryItem(Math.Log) - (1 - y) * (1 - h).EveryItem(Math.Log)).Sum();
            //cost with Regularization
            cost += lambda / (2 * m) * theta_reg.Sum(c => c.EveryItem(i => Math.Pow(i, 2)).Sum());
            //Calculate Delta
            Matrix[] delta = new Matrix[layerCount + 1];
            Matrix[] Delta = new Matrix[layerCount];
            Matrix[] theta_grad = new Matrix[layerCount];
            delta[layerCount] = ~(h - y);
            //theta_grad[layerCount - 1] = delta[layerCount - 1] / m + (lambda / m) * theta_reg[layerCount - 1];
            for (int l = layerCount - 1; l > 0; l--)
            {
                var sg = z[l].AddX0().EveryItem(MLMath.SigmoidGradient);
                delta[l] = (delta[l + 1] * theta[l]).EveryItem(sg, (u, v) => u * v);
                delta[l] = delta[l].RemoveColumn(0);
                //theta_grad[l ] =a[l] delta[l ] / m + (lambda / m) * theta_reg[l ];
            }

            //Accumulating the gradient
            for (int l = 0; l < layerCount; l++)
            {
                Delta[l] = ~delta[l + 1] * a_with1[l];
                theta_grad[l] = Delta[l] / m + (lambda / m) * theta_reg[l];
            }
            return new Tuple<double, Matrix[]>(cost, theta_grad);
        }

    }

}
