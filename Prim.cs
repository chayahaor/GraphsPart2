using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GraphsClassProject
{
    internal class Prim
    {
        //only relevant to weighted graphs
        private readonly ParentGraph graph;

        private List<String> edgesNames = new List<String>();

        //private Dictionary<Vertex, Vertex> alreadyTraversed;

        public Prim(ParentGraph graph)
        {
            this.graph = graph;
            //alreadyTraversed = new Dictionary<Vertex, Vertex>();
        }

        public Vertex[,] PrimMinSpanningGraph(Vertex start)
        {
            Vertex[,] edges = new Vertex[graph.Vertices.Count, 2];

            List<PrimStruct> prims = new List<PrimStruct>();
            int numEdgesFound = 0;

            foreach (Vertex neighbor in start.Neighbors)
            {
                prims.Add(new PrimStruct(neighbor, graph.GetWeight(start, neighbor), start));
            }

            while (numEdgesFound < graph.Vertices.Count - 1)
            {
                prims.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                System.Threading.Thread.Sleep(100);
                PrimStruct currentPrim = prims[0];
                //prims.RemoveAt(0);
                prims.Clear();

                edges[numEdgesFound, 0] = currentPrim.Parent;
                edges[numEdgesFound, 1] = currentPrim.vertex;
                numEdgesFound++;

                Vertex currentVertex = currentPrim.vertex;
                /*if (!alreadyTraversed.ContainsKey(currentVertex))
                {
                    alreadyTraversed.Add(currentPrim.Parent, currentPrim.vertex);
                }*/

                foreach (Vertex node in currentVertex.Neighbors)
                {
                    bool edgeExists = false;
                    for (int i = 0; i < numEdgesFound; i++)
                    {
                        if (edges[i, 0].Equals(currentPrim.Parent) && edges[i, 1].Equals(node))
                        {
                            edgeExists = true;
                            break;
                        }
                    }
                    if (!edgeExists)
                    {
                        prims.Add(new PrimStruct(node, graph.GetWeight(currentVertex, node), currentVertex));
                    }
                }
            }

            return edges;
        }

        struct PrimStruct
        {
            // constructor
            public PrimStruct(Vertex vertex, int cost, Vertex parent)
            {
                this.vertex = vertex;
                this.Cost = cost;
                this.Parent = parent;
            }

            internal Vertex vertex;
            internal int Cost { get; set; }
            internal Vertex Parent { get; set; }
        }
    }
}
