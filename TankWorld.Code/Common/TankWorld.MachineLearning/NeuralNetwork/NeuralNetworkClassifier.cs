using TankWorld.MachineLearning.Classification;

namespace TankWorld.MachineLearning.NeuralNetwork
{
    public class NeuralNetworkClassifier : BaseClassifier
    {
        public Matrix[] Theta;

        public NeuralNetworkClassifier(Matrix[] theta)
        {
            this.Theta = theta;
        }

        public override int Predict(params double[] x)
        {
            Vector[] layers = new Vector[Theta.Length];
            layers[0] = x;
            for (int layerNo = 0; layerNo < Theta.Length - 1; layerNo++)
            {
                //layers[layerNo + 1] = new double[Theta[layerNo].Length0-1];
                layers[layerNo + 1] = (Theta[layerNo] * layers[layerNo].Insert(0, 1)).EveryItem(MLMath.Sigmoid);
            }
            Vector outputLayer = (Theta[Theta.Length - 1] * layers[Theta.Length - 1].Insert(0, 1)).EveryItem(MLMath.Sigmoid);
            int result = -1;
            for (int i = 0; i < outputLayer.Length; i++)
            {
                if (outputLayer[i] > 0.5)
                {
                    result = i;
                }
            }
            return result;
        }

        public override double PredictByPercentage(double[] x)
        {
            Vector[] layers = new Vector[Theta.Length];
            layers[0] = x;
            for(int layerNo=0;layerNo< Theta.Length-1;layerNo++)
            {
                //layers[layerNo + 1] = new double[Theta[layerNo].Length0-1];
                layers[layerNo + 1] = (Theta[layerNo]* layers[layerNo].Insert(0, 1) ).EveryItem(MLMath.Sigmoid);
            }
            Vector outputLayer = (Theta[Theta.Length - 1] * layers[Theta.Length - 1].Insert(0, 1)).EveryItem(MLMath.Sigmoid);
            int result = -1;
            for(int i=0;i<outputLayer.Length;i++)
            {
                if(outputLayer[i]>0.5)
                {
                    result = i;
                }
            }
            return result;
        }

        public static double H(double[] x, double[] theta)
        {
            double z = MLMath.ItemMultiplyAndSum(x,theta);
            return MLMath.Sigmoid(z);
        }
    }
}
