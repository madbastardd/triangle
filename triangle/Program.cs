using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace triangle {
    class Program {
        static void Main(string[] args) {
            List<int[]> triangle = new List<int[]>();
            int count = 0;

            using (StreamReader sr = new StreamReader("triangle.dat")) {
                while (!sr.EndOfStream) {
                    int[] line = new int[++count];
                    int i = 0;
                    foreach (var item in sr.ReadLine().Split(' '))
                        line[i++] = int.Parse(item);
                    triangle.Add(line);
                }
            }

            List<int[]> path = new List<int[]>();
            for (int i = 0; i < count; ++i)
                path.Add(new int[i + 1]); 

            path[0][0] = triangle[0][0];
            path[1][0] = triangle[0][0] + triangle[1][0];
            path[1][1] = triangle[0][0] + triangle[1][1];

            for (int i = 2; i < count; ++i) {
                path[i][0] = new int[] { path[i - 1][0], path[i - 1][1] }.Max() + triangle[i][0];
                for (int j = 1; j < i - 1; ++j)
                    path[i][j] = new int[] { path[i - 1][j - 1], path[i - 1][j], path[i - 1][j + 1] }.Max() + triangle[i][j];

                path[i][i - 1] = new int[] { path[i - 1][i - 2], path[i - 1][i - 1] }.Max() + triangle[i][i - 1];
                path[i][i] = path[i - 1][i - 1] + triangle[i][i];
            }

            Console.WriteLine(path[count - 1].Max());
        }
    }
}
