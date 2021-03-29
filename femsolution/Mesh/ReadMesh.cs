using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace femsolution.Mesh
{
    public class ReadMesh
    {
        public void ReadMeshFile()
        {
            string path = @"guad3.msh";

            //Считываем узлы
            var nodesFile = File.ReadLines(path)
            .SkipWhile(line => !line.Contains("$Nodes"))
            .Skip(1) // optional
            .TakeWhile(line => !line.Contains("$EndNodes"))
            .ToArray();

            List<double[]> storage = new List<double[]>();

            foreach(var item in nodesFile)
            {
                var t = item.Split(' ').
                Select(x => double.Parse(x)).ToArray();

                storage.Add(t);
            }

            var numEntityBlocks = storage[0][0];
            var numNodes = (int)storage[0][1];
            var minNodeTag = storage[0][2];
            var maxNodeTag = storage[0][3];
            storage.RemoveAt(0);

            var nodes = new List<Nodes>();

            for (int i = 0 ; i < storage.Count; i += 3)
            {
                var item = new Nodes();
                item.EntityDim = (int)storage[i][0];
                item.EntityTag = (int)storage[i][1];
                item.Parametric = (int)storage[i][2];
                item.NumNodesInBlock = (int)storage[i][3];

                item.NodeTag = (int)storage[i + 1][0];

                item.X = storage[i + 2][0];
                item.Y = storage[i + 2][1];
                item.Z = storage[i + 2][2];

                nodes.Add(item);
            }

            storage.Clear();

            //Считываем элементы
            var elementsFile = File.ReadLines(path)
            .SkipWhile(line => !line.Contains("$Elements"))
            .Skip(1) // optional
            .TakeWhile(line => !line.Contains("$EndElements"))
            .ToArray();

            foreach (var item in elementsFile)
            {
                var t = item.Trim().Split(' ').
                Select(x => double.Parse(x)).ToArray();

                storage.Add(t);
            }

            storage.RemoveAt(0);
            storage.RemoveAt(0);

            var elements = new List<Elements>();

            for (int i = 0; i < storage.Count; i++)
            {
                var item = new Elements();
                item.Nodes = new int[8];
                item.ElementTag = (int)storage[i][0];
                for (int j = 0; j < 8; j++)
                {
                    item.Nodes[j] = (int)storage[i][j+1];
                }

                elements.Add(item);
            }
        }
    }
}
