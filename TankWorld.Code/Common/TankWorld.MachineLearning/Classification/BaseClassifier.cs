using System;

namespace TankWorld.MachineLearning.Classification
{
    public abstract class BaseClassifier : IClassifier
    {

        public virtual int Predict(params  double[] x)
        {
            double percentage = PredictByPercentage(x);
            if (percentage > 0.5) { return 1; }
            else { return 0; }
        }

        public abstract double PredictByPercentage(double[] x);
    }
}
