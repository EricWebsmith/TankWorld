using System.Collections.Generic;
using System.Linq;
using TankWorld.MachineLearning.Classification;

namespace TankWorld.MachineLearning.Ensemble
{
    public class BaggingTrainer : IBinaryClassificationTrainer
    {
        private List<BinaryClassificationSample> samples = new List<BinaryClassificationSample>();
        private List<IBinaryClassificationTrainer> trainers = new List<IBinaryClassificationTrainer>();

        public IBinaryClassificationTrainer AddSample(BinaryClassificationSample sample)
        {
            samples.Add(sample);
            return this;
        }

        public IBinaryClassificationTrainer AddSamples(IEnumerable<BinaryClassificationSample> samples)
        {
            this.samples.AddRange(samples);
            return this;
        }

        public BaggingTrainer AddBinaryClassificationTrainers(IEnumerable<IBinaryClassificationTrainer> trainers)
        {
            this.trainers.AddRange(trainers);
            return this;
        }

        public void Train(int times)
        {
            for (int i = 0; i < samples.Count; i++)
            {
                //resample
                trainers[i % trainers.Count].AddSample(samples[i]);
            }
            //Parallel Training
            trainers.AsParallel().ForAll(trainer => trainer.Train(times));
        }
    }
}
