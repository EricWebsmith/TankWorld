using System;
using System.Collections.Generic;

namespace TankWorld.MachineLearning.Classification
{
    public abstract class BaseClassificationTrainer : IBinaryClassificationTrainer
    {
        protected List<BinaryClassificationSample> Samples = new List<BinaryClassificationSample>();
        protected int SampleCount { get { return Samples.Count; } }
        protected int FeatureCount
        {
            get { return Samples[0].X.Length; }
        }

        public IBinaryClassificationTrainer AddSample(BinaryClassificationSample sample)
        {
            Samples.Add(sample);
            return this;
        }

        public IBinaryClassificationTrainer AddSamples(IEnumerable<BinaryClassificationSample> samples)
        {
            this.Samples.AddRange(samples);
            return this;
        }

        public abstract void Train(int times);

        protected Tuple<Matrix, Vector> GetXY()
        {
            //featureCount = Samples[0].X.GetLength(1);
            double[,] x = new double[SampleCount, FeatureCount];
            double[] y = new double[SampleCount];

            for (int i = 0; i < SampleCount; i++)
            {
                x[i, 0] = 1;
                for (int j = 0; j < FeatureCount; j++)
                {
                    x[i, j ] = Samples[i].X[j];
                }
                y[i] = Samples[i].Y;
            }
            return new Tuple<Matrix, Vector>(x, y);
        }
    }
}
