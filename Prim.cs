using System;
using System.Collections.Generic;

namespace GraphsClassProject
{
    internal class Prim
    {
        //only relevant to weighted graphs
        private readonly ParentGraph graph;

        public Prim(ParentGraph graph)
        {
            this.graph = graph;
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
                PrimStruct currentPrim = prims[0];
                prims.RemoveAt(0);

                edges[numEdgesFound, 0] = currentPrim.Parent;
                edges[numEdgesFound, 1] = currentPrim.vertex;
                numEdgesFound++;

                Vertex currentVertex = currentPrim.vertex;

                foreach (Vertex node in currentVertex.Neighbors)
                {
                    PrimStruct neighborPrim = prims.Find(p => p.vertex == node);
                    if (!neighborPrim.Equals(null))
                    {
                        if (graph.GetWeight(currentVertex, node) < neighborPrim.Cost)
                        {
                            neighborPrim.Cost = graph.GetWeight(currentVertex, node);
                            neighborPrim.Parent = currentVertex;
                        }
                    }
                    else
                    {
                        neighborPrim = new PrimStruct(node, graph.GetWeight(currentVertex, node), currentVertex);
                        prims.Add(neighborPrim);
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
