using System;

namespace TankWorld.MachineLearning
{
    public class Matrix
    {
        double[,] x;

        public int Length0 { get { return x.GetLength(0); } }
        public int Length1 { get { return x.GetLength(1); } }

        public Matrix(double[,] x)
        {
            this.x = x;
        }

        public double this[int i, int j]
        {
            get { return x[i, j]; }
            set { x[i, j] = value; }
        }

        public static implicit operator Matrix(double[,] a)
        {
            return new Matrix(a);
        }

        /// <summary>
        /// transpose
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Matrix operator ~(Matrix m)
        {
            int dim0 = m.Length0;
            int dim1 = m.Length1;
            double[,] m_transpose = new double[dim1, dim0];
            for (int i = 0; i < dim0; i++)
            {
                for (int j = 0; j < dim1; j++)
                {
                    m_transpose[j, i] = m[i, j];
                }
            }
            return new Matrix(m_transpose);
        }

        public static Matrix operator -(double a, Matrix b)
        {
            return MatrixHelper.EveryItem(b, (bb) => a - bb);
        }

        public static Matrix operator -(Matrix a, double b)
        {
            return MatrixHelper.EveryItem(a, (aa) => aa - b);
        }

        public static Matrix operator -(Matrix a)
        {
            return MatrixHelper.EveryItem(a, (aa) => -aa);
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            return MatrixHelper.EveryItem(a, b, (aa, bb) => aa - bb);
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            return MatrixHelper.EveryItem(a, b, (aa, bb) => aa + bb);
        }

        public static Matrix operator /(Matrix a, double b)
        {
            return MatrixHelper.EveryItem(a, (aa) => aa / b);
        }

        public static Matrix operator *(Matrix a, double b)
        {
            return MatrixHelper.EveryItem(a, (aa) => aa * b);
        }

        public static Matrix operator *(double a, Matrix b)
        {
            return MatrixHelper.EveryItem(b, (bb) => a * bb);
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {

            if (a.Length1 != b.Length0)
            {
                throw new Exception("dimension mis-match.");
            }
            double[,] cc = new double[a.Length0, b.Length1];
            for (int i = 0; i < a.Length0; i++)
            {
                for (int j = 0; j < a.Length1; j++)
                {
                    for (int k = 0; k < b.Length1; k++)
                    {
                        cc[i, k] += a[i, j] * b[j, k];
                    }
                }
            }

            Matrix c = new Matrix(cc);
            return c;
        }

        public static Vector operator *(Matrix a, Vector b)
        {

            if (a.Length1 != b.Length)
            {
                throw new Exception("dimension mis-match.");
            }
            double[] cc = new double[a.Length0];
            for (int i = 0; i < a.Length0; i++)
            {
                for (int j = 0; j < a.Length1; j++)
                {
                    cc[i] += a[i, j] * b[j];
                }
            }
            return cc;
        }

        public Vector ExtractColumn(int col = 0)
        {
            double[] v = new double[this.Length0];
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = this[i, col];
            }
            return new Vector(v);
        }



        public Vector ExtractRow(int row = 0)
        {
            double[] v = new double[this.Length1];
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = this[row, i];
            }
            return new Vector(v);
        }

        public Matrix RemoveColumn(int col)
        {

            double[,] newMatrix = new double[this.Length0, this.Length1 - 1];
            for (int i = 0; i < this.Length0; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    newMatrix[i, j] = this[i, j];
                }

                for (int j = col + 1; j < Length1; j++)
                {
                    newMatrix[i, j - 1] = this[i, j];
                }
            }
            return newMatrix;
        }

        public Matrix ChangeCol(int col = 0, Func<double, double> f = null)
        {
            double[,] m = new double[this.Length0, this.Length1];

            for (int i = 0; i < this.Length0; i++)
            {
                for (int j = 0; j < this.Length1; j++)
                {
                    if (j == col)
                    {
                        m[i, j] = f(this[i, j]);
                    }
                    else
                    {
                        m[i, j] = this[i, j];
                    }
                }
            }
            return new Matrix(m);
        }

        public override string ToString()
        {
            string s = string.Empty;
            for (int i = 0; i < this.Length0; i++)
            {
                for (int j = 0; j < this.Length1; j++)
                {
                    s += this[i, j].ToString() + ", ";
                }
                s += "; ";
            }
            return s;
        }
    }
}
