using System.Collections.Generic;
using System.Linq;
using TankWorld.MachineLearning.Classification;
using TankWorld.MachineLearning.DecisionTree;

namespace TankWorld.MachineLearning.Ensemble
{
    public class AdaBoostTrainer
    {
        public List<BinarySplitter> Train(List<BinaryClassificationSample> samples)
        {
            List<BinarySplitter> result = new List<BinarySplitter>();
            var resamples = samples;
            while(resamples.Count>0)
            {
                BinarySplitter bs= Stump.GetBestSpliter(resamples);
                result.Add(bs);
                //Resample out wrong points
                resamples = resamples.Where(c => (c.X[bs.SplitedXIndex] == 0 && c.Y == bs.YGiven1) 
                || (c.X[bs.SplitedXIndex] == 1 && c.Y != bs.YGiven1)).ToList();
            }
            return result;
        }
    }
}
