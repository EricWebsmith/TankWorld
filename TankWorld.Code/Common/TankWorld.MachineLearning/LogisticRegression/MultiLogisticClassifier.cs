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

namespace TankWorld.MachineLearning.LogisticRegression
{
    public class MultiLogisticClassifier
    {
        public int FeatureCount { get; private set; }
        public int ClassCount { get; private set; }
        public int Alpha { get; set; }
        public double Lambda { get; set; }
        public LogisticClassifier[] Classifiers { get; private set; }

        public List<double[]> Theta {
            get
            {
                List<double[]> t = new List<double[]>();
                for(int i=0;i<Classifiers.Length;i++)
                {
                      t[i]= Classifiers[i].Theta;
                }
                return t;
            }
            set {
                for (int i = 0; i < Classifiers.Length; i++)
                {
                     Classifiers[i].Theta=value[i];
                }
            }
        }
        
        public MultiLogisticClassifier(int featureCount, int classCount, double alpha = 0.1, double lambda = 0)
        {
            this.FeatureCount = featureCount;
            this.ClassCount = classCount;
            Classifiers = new LogisticClassifier[ClassCount];
            for (int i = 0; i < ClassCount; i++)
            {
                Classifiers[i] = new LogisticClassifier(featureCount, alpha, lambda);
            }
        }

        public void AddSample(double[] x, double[] y)
        {
            for (int i = 0; i < ClassCount; i++)
            {
                Classifiers[i].AddSample(x, y[i]);
            }
        }

        public MultiClassificationTrainResult[] Train(int times)
        {
            MultiClassificationTrainResult[] m_results = new MultiClassificationTrainResult[times];
            var bResults_list = new List<BinaryClassificationTrainResult>[ClassCount];
            for(int t = 0; t < times; t++)
            {
                m_results[t] = new MultiClassificationTrainResult();
                m_results[t].Theta = new double[ClassCount, FeatureCount + 1];
            }

            for (int c = 0; c < ClassCount; c++)
            {
                bResults_list[c] = Classifiers[c].Train(times);
            }


            for (int c = 0; c < ClassCount; c++)
            {
                for (int i = 0; i < times; i++)
                {
                    m_results[i].Cost += bResults_list[c][i].Cost/times/FeatureCount;
                    for(int f=0;f<FeatureCount;f++)
                    {
                        m_results[i].Theta[c,f] = bResults_list[c][i].Theta[f];
                    }
                    
                }
            }
            return m_results;
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
