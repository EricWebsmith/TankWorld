using System.Collections.Generic;
using TankWorld.MachineLearning.Classification;
using TankWorld.MachineLearning.DecisionTree;

namespace TankWorld.MachineLearning.Ensemble
{
    public class AdaBoostClassifier:IClassifier
    {
        protected List<BinarySplitter> splitters;

        public AdaBoostClassifier(List<BinarySplitter> splitters)
        {
            this.splitters = splitters;
        }

        public int Predict(double[] x)
        {
            double percentage = PredictByPercentage(x);
            if (percentage > 0.5) { return 1; }
            else { return 0; }
        }

        public double PredictByPercentage(double[] x)
        {
            double trueValue = 0;
            double falseValue = 0;
            foreach(BinarySplitter bs in splitters)
            {
                if(x[bs.SplitedXIndex]==1 && bs.YGiven1==1)
                {
                    trueValue += bs.Alpha;
                }
                else
                {
                    falseValue += bs.Alpha;
                }
            }
            return trueValue / (trueValue + falseValue);
        }
    }
}
