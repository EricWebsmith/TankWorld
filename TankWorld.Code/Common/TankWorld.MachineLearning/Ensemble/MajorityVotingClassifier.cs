using TankWorld.MachineLearning.Classification;

namespace TankWorld.MachineLearning.Ensemble
{
    public class MajorityVotingClassifier:BaseClassifier
    {
        protected IClassifier[] classifiers;

        protected double[] weights;

        public MajorityVotingClassifier(IClassifier[] classifiers, double[] weights)
        {
            this.classifiers = classifiers;
            this.weights = weights;
        }

        public override double PredictByPercentage(double[] x)
        {
            double trueValue = 0;
            double falseValue = 0;
            for (int i = 0; i < classifiers.Length;i++)
            {
                trueValue += weights[i]* classifiers[i].Predict(x);
                falseValue += weights[i] * (1-classifiers[i].Predict(x));
            }
            return trueValue / (trueValue + falseValue);
        }
    }
}
