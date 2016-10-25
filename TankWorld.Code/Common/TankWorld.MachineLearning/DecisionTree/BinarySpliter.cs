namespace TankWorld.MachineLearning.DecisionTree
{

    public class BinarySplitter
    {
        public int YGiven1 { get; set; }
        public int SplitedXIndex { get; set; }
        public double Alpha { get; set; }

        //first - SplitedXIndex
        //second - YGiven1
        public double[] Vector
        {
            get { return new double[] { SplitedXIndex, YGiven1, Alpha }; }
            set { SplitedXIndex = (int)value[0]; YGiven1 = (int)value[1]; Alpha = value[3]; }
        }
    }
}
