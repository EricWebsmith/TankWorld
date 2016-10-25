using System;

namespace TankWorld.MachineLearning.DecisionTree
{
    internal static class Entropy
    {
        public static double GetEntropy(int a, int b)
        {
            if (a == 0 || b == 0) return 0;
            double p1 = a * 1.0 / (a + b);
            double p2 = b * 1.0 / (a + b);
            return p1 * Math.Log(1 / p1, 2) + p2 * Math.Log(1 / p2, 2);
        }

        public static double InformationGain(int left1, int left2, int right1, int right2)
        {
            int left = left1 + left2;
            int right = right1 + right2;
            int total = left + right;
            double entropyAll = GetEntropy(left1 + right1, left2 + right2);
            double entropyLeft = GetEntropy(left1, left2);
            double entropyRight = GetEntropy(right1, right2);
            return left * 1.0 / total * (entropyAll - entropyLeft) + right * 1.0 / total * (entropyAll - entropyRight);
        }
    }
}
