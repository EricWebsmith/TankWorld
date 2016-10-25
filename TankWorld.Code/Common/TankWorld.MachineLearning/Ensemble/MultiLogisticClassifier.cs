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

using System.Collections.Generic;
using TankWorld.MachineLearning.Classification;

namespace TankWorld.MachineLearning.Ensemble
{
    public class EnsembleClassifier
    {
        public int FeatureCount { get; private set; }
        public int ClassCount { get; private set; }
        public int Alpha { get; set; }
        public double Lambda { get; set; }
        public IClassifier[] Classifiers { get; private set; }

        public EnsembleClassifier(IClassifier[] classifiers)
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

        public double[] Predict2(double[] x)
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
