using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWorld.MachineLearning
{
    public static class Matlab
    {
        public static Matrix LoadMatrix(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                int row = 0;
                int col = 0;
                string line = sr.ReadLine();

                while (line != null)
                {
                    if (line.StartsWith("# rows: "))
                    {
                        row = int.Parse(line.Replace("# rows: ", string.Empty));
                    }
                    if (line.StartsWith("# columns: "))
                    {
                        col = int.Parse(line.Replace("# columns: ", string.Empty));
                        break;
                    }

                    line = sr.ReadLine();
                }

                double[,] mat = new double[row, col];
                int rowIndex = 0;
                while (line != null)
                {
                    if (!line.StartsWith("#"))
                    {
                        var intArr = line.Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries).Select(s => double.Parse(s)).ToArray();
                        for (int j = 0; j < intArr.Length; j++)
                        {
                            mat[rowIndex, j] = intArr[j];
                        }
                        rowIndex++;
                    }
                    line = sr.ReadLine();
                }
                return mat;
            }
        }
    }
}
