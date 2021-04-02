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
        /// <param name="nodes"></param>
        public void GetAnalyticalSolution(Nodes[] nodes)
        {
            var outFile = new List<string>();
            for (int i=0; i < nodes.Length; i++)
            {
                outFile.Add(nodes[i].Point.X.ToString() + " " + nodes[i].Point.Y.ToString() + " " + (Math.Pow(nodes[i].Point.X, 2) + Math.Pow(nodes[i].Point.Y, 2)).ToString());
            }
            File.WriteAllLines("out.txt", outFile);
        }
    }
}
