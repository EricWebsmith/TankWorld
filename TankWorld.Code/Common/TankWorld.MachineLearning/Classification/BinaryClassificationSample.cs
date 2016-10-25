namespace TankWorld.MachineLearning.Classification
{
    public class BinaryClassificationSample
    {
        public double[] X { get; set; }
        public double Y { get; set; }

        public BinaryClassificationSample(double[] x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
