namespace TankWorld.MachineLearning.Classification
{
    public interface IClassifier
    {
        int Predict(double[] x);
        double PredictByPercentage(params double[] x);
    }
}
