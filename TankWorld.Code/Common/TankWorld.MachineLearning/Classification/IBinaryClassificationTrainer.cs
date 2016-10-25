using System.Collections.Generic;

namespace TankWorld.MachineLearning.Classification
{
    public interface IBinaryClassificationTrainer
    {
        /// <summary>
        /// Returen the Trainer per si.
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        IBinaryClassificationTrainer AddSample(BinaryClassificationSample sample);
        IBinaryClassificationTrainer AddSamples(IEnumerable<BinaryClassificationSample> samples);
        void Train(int times);
    }
}
