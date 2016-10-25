using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWorld.MachineLearning
{
    public static class MatrixHelper
    {
        public static double Sum(this Matrix a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length0; i++)
            {
                for (int j = 0; j < a.Length1; j++)
                {
                    sum += a[i, j];
                }
            }
            return sum;
        }

        public static Matrix EveryItem(this Matrix a, Matrix b, Func<double, double,double> f)
        {
            double[,] bb = new double[a.Length0, a.Length1];
            for (int i = 0; i < a.Length0; i++)
            {
                for (int j = 0; j < a.Length1; j++)
                {
                    bb[i, j] = f(a[i, j],b[i,j]);
                }
            }
            return new Matrix(bb);
        }

        public static Matrix EveryItem(this Matrix a, Func<double, double> f)
        {
            double[,] bb = new double[a.Length0, a.Length1];
            for (int i = 0; i < a.Length0; i++)
            {
                for(int j=0;j<a.Length1;j++)
                {
                    bb[i, j] = f(a[i, j]);
                }
            }
            return new Matrix(bb);
        }

        /// <summary>
        /// Add x0 to every x, x0=1.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Matrix AddX0(this Matrix a)
        {
            //number of samples
            int dim0 = a.Length0;
            //number of features
            int dim1 = a.Length1;
            double[,] x_with1 = new double[dim0, dim1+1];
            string s = string.Empty;
            for (int i = 0; i < dim0; i++)
            {
                x_with1[i, 0] = 1;
                for (int j = 0; j < dim1; j++)
                {
                    x_with1[i, j + 1] = a[i, j];
                }
            }
            return new Matrix(x_with1);
        }

        public static double[,] Transpose(this double[,] m)
        {
            int dim0 = m.GetLength(0);
            int dim1 = m.GetLength(1);
            double[,] m_transpose = new double[dim1, dim0];
            for (int i = 0; i < dim0; i++)
            {
                for (int j = 0; j < dim1; j++)
                {
                    m_transpose[j, i] = m[i, j];
                }
            }
            return m_transpose;
        }

        public static double[,] Multiply(this double[,] matrix1, double[,] matrix2)
        {
            int m1dim0 = matrix1.GetLength(0);
            int m1dim1 = matrix1.GetLength(1);
            int m2dim0 = matrix2.GetLength(0);
            int m2dim1 = matrix2.GetLength(1);
            throw new Exception();
        }
    }
}
