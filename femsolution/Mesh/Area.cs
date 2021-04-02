using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace femsolution.Mesh
{
    public class Area
    {
        //Считываем область
        public Point[] GetArea(string fileName)
        {
            List<double[]> storage = new List<double[]>();

            //Считываем область
            var areaFile = File.ReadLines(fileName)
            .SkipWhile(line => !line.Contains("$Entities"))
            .Skip(1) // optional
            .TakeWhile(line => !line.Contains("$EndEntities"))
            .ToArray();

            foreach (var item in areaFile)
            {
                var t = item.Trim().Split(' ').
                Select(x => double.Parse(x)).ToArray();

                storage.Add(t);
            }

            var numPoints = (int)storage[0][0];

            var area = new Point[numPoints];

            for (int i = 1; i < area.Length + 1; i++)
            {
                area[i - 1] = new Point
                {
                    X = storage[i][1],
                    Y = storage[i][2],
                    Z = storage[i][3]
                };
            }

            return area;
        }
    }
}
