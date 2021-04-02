using femsolution.Mesh;
using System;
using System.Collections.Generic;
using System.IO;

namespace femsolution
{
    /// <summary>
    /// Аналитическое решение
    /// </summary>
    public class AnalyticalSolution
    {
        /// <summary>
        /// Получить аналитическре решение
        /// </summary>
        /// <param name="area"></param>
        public void GetAnalyticalSolution(Nodes[] nodes)
        {
            var x = new List<string>();
            var y = new List<string>();
            var z = new List<string>();
            for (int i=0; i < nodes.Length; i++)
            {
                x.Add(nodes[i].Point.X.ToString() + " " + nodes[i].Point.Y.ToString() + " " + (Math.Pow(nodes[i].Point.X, 2) + Math.Pow(nodes[i].Point.Y, 2)).ToString());
                y.Add(nodes[i].Point.Y.ToString());
                z.Add((Math.Pow(nodes[i].Point.X, 2) + Math.Pow(nodes[i].Point.Y, 2)).ToString());
                Console.WriteLine("X: {0} Y: {1} Ez: {2}", nodes[i].Point.X, nodes[i].Point.Y, Math.Pow(nodes[i].Point.X, 2) + Math.Pow(nodes[i].Point.Y, 2));
            }
            File.WriteAllLines("out.txt", x);
           // File.WriteAllLines("outY.txt", y);
            //File.WriteAllLines("outZ.txt", z);



            Console.WriteLine();
        }
    }
}
