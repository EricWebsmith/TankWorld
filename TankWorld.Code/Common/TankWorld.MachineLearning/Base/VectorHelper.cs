using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWorld.MachineLearning
{
   public static class VectorHelper
    {
        public static Vector EveryItem(this Vector a, Vector b, Func<double, double, double> f)
        {
            double[] bb = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                
                    bb[i] = f(a[i], b[i]);
              
            }
            return new Vector(bb);
        }

        public static Vector EveryItem(this Vector a, Func<double, double> f)
        {
            double[] bb = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                    bb[i] = f(a[i]);
            }
            return new Vector(bb);
        }
    }
}
