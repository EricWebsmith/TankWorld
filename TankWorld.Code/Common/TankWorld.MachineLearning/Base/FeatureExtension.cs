using System.Collections.Generic;
using System.IO;

namespace TankWorld.MachineLearning
{
    public static class FeatureExtension
    {

        public static int ToInt(this bool b)
        {
            if (b) { return 1; }
            else { return 0; }
        }

        public static string ToLine(this double[] x)
        {
            string s = string.Empty;
            for (int i = 0; i < x.Length - 1; i++)
            {
                s += x[i].ToString() + ",";
            }
            s += x[x.Length - 1].ToString();
            return s;
        }

        public static string ToLines(this double[,] x)
        {
            string s = string.Empty;
            for (int line = 0; line < x.GetLength(0); line++)
            {
                for (int i = 0; i < x.GetLength(1) - 1; i++)
                {
                    s += x[line, i].ToString() + ",";
                }
                s += x[line, x.GetLength(1) - 1].ToString() + "\r\n";
            }

            return s;
        }

        public static string ToLine(this double[,] x, int lineNumber)
        {
            string s = string.Empty;
            for (int i = 0; i < x.Length - 1; i++)
            {
                s += x[lineNumber, i].ToString() + ",";
            }
            s += x[lineNumber, x.GetLength(2) - 1].ToString();
            return s;
        }



        public static double[] ToArray(this string s)
        {
            var arr = s.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            double[] x = new double[arr.Length];
            for (int i = 0; i < x.Length - 1; i++)
            {
                x[i] = double.Parse(arr[i]);
            }
            return x;
        }

        public static void SaveMatrix(double[,] x, string path)
        {
            StreamWriter sw_y = new StreamWriter(path);
            sw_y.Write(x.ToLines());
            sw_y.Flush();
            sw_y.Close();
        }

        public static void SaveMatrix(IEnumerable<double[]> x, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (double[] arr in x)
                {
                    sw.WriteLine(arr.ToLine());
                }
                sw.Flush();
                sw.Close();
            }
        }

        public static List<double[]> LoadMatrix(string path)
        {
            List<double[]> result = new List<double[]>();
            string content = string.Empty;

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.", path);
            }

            using (StreamReader sr = new StreamReader(path))
            {
                content = sr.ReadToEnd();
                sr.Close();
            }
            string[] lines = content.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                result.Add(line.ToArray());
            }
            return result;
        }
    }
}
