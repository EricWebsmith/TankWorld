using System.Collections.Generic;
using System.Linq;
using TankWorld.MachineLearning.Classification;

namespace TankWorld.MachineLearning.DecisionTree
{
    public static class Stump
    {
        public static BinarySplitter GetBestSpliter(List<BinaryClassificationSample> samples)
        {
            double informationGain = 0;
            int featureCount = samples[0].X.Length;
            int y0 = samples.Where(c => c.Y == 0).Count();
            int y1 = samples.Where(c => c.Y == 1).Count();
            double entropy = Entropy.GetEntropy(y0, y1);
            BinarySplitter bs = new BinarySplitter();
            for (int i = 0; i < featureCount; i++)
            {
                //when x(i)=0
                int y1x0 = samples.Where(c => c.X[i] == 0 && c.Y == 1).Count();
                int y0x0 = samples.Where(c => c.X[i] == 0 && c.Y == 0).Count();
                //when x(i)=1
                int y1x1 = samples.Where(c => c.X[i] == 1 && c.Y == 1).Count();
                int y0x1 = samples.Where(c => c.X[i] == 1 && c.Y == 0).Count();
                double tempInfoGain = Entropy.InformationGain(y1x0, y0x0, y1x1, y0x1);
                if (tempInfoGain > informationGain)
                {
                    informationGain = tempInfoGain;
                    double py1x0 = y1x0 * 1.0 / (y1x0 + y0x0);
                    double py1x1 = y1x1 * 1.0 / (y1x1 + y0x1);
                    //when x=1, and there is more y=1,
                    //x=1 predicts y=1
                    //otherwise, 0.
                    int yGiven1 = (py1x1 > py1x0).ToInt();
                    bs = new BinarySplitter();
                    bs.YGiven1 = yGiven1;
                    
                    //What is alpha???

                    //All points have been classified successfully.
                    if(informationGain==entropy)
                    {
                        return bs;
                    }
                }
            }

            return bs;
        }
    }
}
