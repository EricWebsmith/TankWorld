using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWorld.MachineLearning
{
    public class Vector
    {
        double[] x;
        public int Length { get { return x.Length; } }
        public Vector(double[] x)
        {
            this.x = x;
        }

        public double this[int index]
        {
            get { return x[index]; }
            set { x[index] = value; }
        }

        public double Sum()
        {
            return x.Sum();
        }

        public static implicit operator Vector(double[] a)
        {
            return new Vector(a);
        }

        public static implicit operator Matrix(Vector a)
        {
            double[,] x = new double[1, a.Length];
            for(int i=0;i<a.Length;i++)
            {
                x[0, i] = a[i];
            }
            return new Matrix(x);
        }

        public Vector Insert(int index, double value)
        {
            double[] newVector = new double[this.Length + 1];
            for(int i=0;i<index;i++)
            {
                newVector[i] = this[i];
            }
            newVector[index] = value;
            for(int i=index+1;i<this.Length;i++)
            {
                newVector[i+1] = this[i];
            }
            return newVector;
        }

        public static Matrix operator ~(Vector a)
        {
            double[,] x = new double[a.Length, 1];
            for(int i=0;i<a.Length;i++)
            {
                x[i, 0] = a[i];
            }
            return x;
        }


        public static Vector operator -(Vector a)
        {
            return a.EveryItem((x) => -x);
        }

        public static Vector operator -(double a, Vector b)
        {
            return b.EveryItem(x => a - x);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return a.EveryItem(b, (x, y) => x - y);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return a.EveryItem(b, (x, y) => x + y);
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return a.EveryItem(b, (x, y) => x * y);
        }

        public static Vector operator *(double a, Vector b)
        {
            return b.EveryItem(x => x * a);
        }
    }
}
