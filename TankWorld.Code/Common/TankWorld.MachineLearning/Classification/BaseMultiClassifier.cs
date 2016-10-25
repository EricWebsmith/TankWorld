// ===============================
// AUTHOR     : Weiguang Zhou
// CREATE DATE: 24/11/2015
// PURPOSE    :
// This uses several BinaryClassification and one-vs-all method to classify
// Multiple classes.
// SPECIAL NOTES:
// ===============================
// Change History:
//
//==================================


namespace TankWorld.MachineLearning.Classification
{
    public class BaseMultiClassifier
    {
        public int FeatureCount { get; private set; }
        public int ClassCount { get; private set; }

        public BaseClassifier[] Classifiers { get; private set; }
        
        public BaseMultiClassifier(BaseClassifier[] classifiers)
        {
            this.Classifiers = classifiers;
        }

        public double[] Predict(double[] x)
        {
            double[] p = new double[ClassCount];
            for (int c = 0; c < ClassCount; c++)
            {
                p[c] = Classifiers[c].Predict(x);

            }
            return p;
        }

        public double[] PredictByPercentage(double[] x)
        {
            double[] p = new double[ClassCount];
            for (int c = 0; c < ClassCount; c++)
            {
                p[c] = Classifiers[c].PredictByPercentage(x);
            }
            return p;
        }
    }
}
