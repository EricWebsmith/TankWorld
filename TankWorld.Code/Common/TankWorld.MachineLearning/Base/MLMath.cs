using System;

namespace TankWorld.MachineLearning
{
    public static class MLMath
    {

        public static double ItemMultiplyAndSum(double[] a, double[] b)
        {
            if(a.Length!=b.Length)
            {
                throw new ArgumentException("a and b have different size!");
            }

            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] * b[i];
            }
            return sum;
        }

        public static double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }

        public static double SigmoidGradient(double z)
        {
            return Sigmoid(z) * (1 - Sigmoid(z));
        }

        public static double[] Minus(double a, double[] b)
        {
            double[] c = new double[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                c[i] = a - b[i];
            }
            return c;
        }

        public static double[] Add(double[] a, double[] b)
        {
            return ItemCalculate(a, b, (x, y)=>x + y);
        }

        public static double[] Minus(double[] a, double[] b)
        {
            return ItemCalculate(a, b, (x, y) => x - y);
        }

        public static double[] Multiply(double[] a, double[] b)
        {
            return ItemCalculate(a, b, (x, y) => x * y);
        }

        public static double[] ItemCalculate(double[] a, double[] b, Func<double, double, double> calculateFunction)
        {
            double[] c = new double[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                c[i] = calculateFunction(a[i], b[i]);
            }
            return c;
        }

        public static double[] ItemCalculate(double a, double[] b, Func<double, double, double> calculateFunction)
        {
            double[] c = new double[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                c[i] = calculateFunction(a, b[i]);
            }
            return c;
        }

        public static double[] ItemCalculate(double[] a, Func<double, double> calculateFunction)
        {
            double[] c = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                c[i] = calculateFunction(a[i]);
            }
            return c;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">n*m</param>
        /// <param name="dimension"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static double[] GetVector(double[,] x, int dimension, int index)
        {
            int n = x.GetLength(0);
            int m = x.GetLength(1);
            if (dimension == 0)
            {

                double[] v = new double[m];
                for (int i = 0; i < m; i++)
                {
                    v[i] = x[index, i];
                }
                return v;
            }
            else
            {
                double[] v = new double[n];
                for (int i = 0; i < n; i++)
                {
                    v[i] = x[i, index];
                }
                return v;
            }
        }

        public static double[] GetRandomVector(int n, double min, double max)
        {
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = GetRandomNumber(min, max);
            }
            return result;
        }

        public static double[,] GetRandomMatrix(int m,int n, double min, double max)
        {
            double[,] result = new double[m,n];
            for (int i = 0; i < m; i++)
            {
                for(int j=0;j< n;j++)
                {
                    result[i,j] = GetRandomNumber(min, max);
                }
            }
            return result;
        }

        static Random random = new Random();
        public static double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static bool AreEqual(double[] a, double[] b)
        {
            if(a.Length!=b.Length)
            {
                return false;
            }

            for(int i=0;i<a.Length;i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }


    }
}
