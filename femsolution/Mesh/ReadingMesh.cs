using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace femsolution.Mesh
{
    public class ReadingMesh
    {
        public void ReadingFromFile()
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

            var numEntityBlocks = (int)storage[0][0];
            var numNodes = (int)storage[0][1];
            var minNodeTag = storage[0][2];
            var maxNodeTag = storage[0][3];
            storage.RemoveAt(0);

            var N_nodes = 0;

            var nodesList = new List<Nodes>();

            for (int i = 0; i < numNodes; i++)
            {
                var item = new Nodes();
                item.NodeTag = 0;
                item.Point.X = 0.0;
                item.Point.Y = 0.0;
                item.Point.Z = 0.0;

                nodesList.Add(item);
            }

            var nodes = nodesList.ToArray();

            var k = 0;
            var unorganized_node_index = 0;
            while (k < storage.Count)
            {
                var EntityDim = (int)storage[k][0];
                var EntityTag = (int)storage[k][1];
                var Parametric = (int)storage[k][2];
                var NumNodesInBlock = (int)storage[k][3];

                N_nodes += NumNodesInBlock;

                for (int node = 0; node < NumNodesInBlock; node++)
                {
                    var index = (int)storage[k + node + 1][0];
                    nodes[index - 1].NodeTag = index;
                }

                for (int node = 0; node < NumNodesInBlock; node++)
                {
                    if (NumNodesInBlock > 1)
                    {
                        nodes[unorganized_node_index].Point.X = storage[k + node + NumNodesInBlock + 1][0];
                        nodes[unorganized_node_index].Point.Y = storage[k + node + NumNodesInBlock + 1][1];
                        nodes[unorganized_node_index].Point.Z = storage[k + node + NumNodesInBlock + 1][2];
                    }
                    else
                    {
                        nodes[unorganized_node_index].Point.X = storage[k + node + 2][0];
                        nodes[unorganized_node_index].Point.Y = storage[k + node + 2][1];
                        nodes[unorganized_node_index].Point.Z = storage[k + node + 2][2];
                    }
                    
                    unorganized_node_index++;
                }

                k = k + NumNodesInBlock + NumNodesInBlock + 1;
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
