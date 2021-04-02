using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace femsolution.Mesh
{
    public class ReadingMesh
    {
        private readonly string _fileName;
            
        public ReadingMesh(string fileName)
        {
            _fileName = fileName;
        }

        public Nodes[] GetNodes()
        {
            List<double[]> storage = new List<double[]>();

            //Считываем узлы
            var nodesFile = File.ReadLines(_fileName)
            .SkipWhile(line => !line.Contains("$Nodes"))
            .Skip(1) // optional
            .TakeWhile(line => !line.Contains("$EndNodes"))
            .ToArray();

            foreach (var item in nodesFile)
            {
                var t = item.Split(' ').
                Select(x => double.Parse(x)).ToArray();

                storage.Add(t);
            }

            var numEntityBlocks = (int)storage[0][0];
            var numNodes = (int)storage[0][1];
            var minNodeTag = (int)storage[0][2];
            var maxNodeTag = (int)storage[0][3];
            storage.RemoveAt(0);

            var N_nodes = 0;

            var nodes = new Nodes[numNodes];

            for (int i = 0; i < numNodes; i++)
            {
                nodes[i] = new Nodes
                {
                    NodeTag = 0,
                    Point = new Point
                    {
                        X = 0.0,
                        Y = 0.0,
                        Z = 0.0
                    }
                };
            }

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
                        nodes[unorganized_node_index].Point = new Point
                        {
                            X = storage[k + node + NumNodesInBlock + 1][0],
                            Y = storage[k + node + NumNodesInBlock + 1][1],
                            Z = storage[k + node + NumNodesInBlock + 1][2]
                        };
                    }
                    else
                    {
                        nodes[unorganized_node_index].Point = new Point
                        {
                            X = storage[k + node + 2][0],
                            Y = storage[k + node + 2][1],
                            Z = storage[k + node + 2][2]
                        };
                    }

                    unorganized_node_index++;
                }

                k = k + NumNodesInBlock + NumNodesInBlock + 1;
            }

            var kx = 8.0 / 3.0;
            var ky = 8.0 / 3.0;
            var kz = 8.0 / 3.0;

            var node0 = new Point
            {
                X = 0.0,
                Y = 0.0,
                Z = 0.0
            };

            var hx = (1 - node0.X) / (4 * kx - kx);
            var hy = (1 - node0.Y) / (4 * ky - ky);
            var hz = (1 - node0.Z) / (4 * kz - kz);

            var mx = (int)(4 * kx - (kx - 1));
            var my = (int)(4 * ky - (ky - 1));
            var mz = (int)(4 * kz - (kz - 1));

            var SetPoints = new Nodes[mx * my * mz];

            int i1 = 0;

            for (int w = 0; w < mz; w++)
            {
                for (int j = 0; j < my; j++)
                {
                    for (int i = 0; i < mx; i++)
                    {
                        //SetPoints[i1] = new Nodes
                        //{
                        //    NodeTag = i1 + 1,
                        //    Point = new Point
                        //    {
                        //        X = node0.X + hx * i,
                        //        Y = node0.Y + hy * j,
                        //        Z = node0.Z + hz * w
                        //    }
                        //};
                        for (int l = 0; l < nodes.Length; l++)
                        {
                            if (((node0.X + hx * i) == nodes[l].Point.X) && ((node0.Y + hy * j) == nodes[l].Point.Y) && ((node0.Z + hz * w) == nodes[l].Point.Z))
                            {
                                SetPoints[i1] = nodes[l];
                            }
                        }

                        i1++;
                    }
                }
            }

            return SetPoints;
        }

        public void ReadingFromFile(Nodes[] nodes) 
        {
            string path = @"guad4.msh";

            List<double[]> storage = new List<double[]>();

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
                item.Nodes = new Nodes[8];
                item.ElementTag = (int)storage[i][0];
                for (int j = 0; j < 8; j++)
                {
                    item.Nodes[j] = new Nodes
                    {
                        NodeTag = (int)storage[i][j + 1],
                        Point = nodes[((int)storage[i][j + 1]) - 1].Point
                    };  
                }

                elements.Add(item);
            }

            var numx = nodes.Where(x => x.Point.Y == 0).Count() - 9;

            var edges = new Edges[numx];

            for (int i = 0; i < edges.Length; i++)
            {
                edges[i] = new Edges
                {
                    EdgeTag = 0,
                };
            }
        }
    }
}
