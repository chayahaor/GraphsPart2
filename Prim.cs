using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            Vertex[,] edges = new Vertex[graph.Vertices.Count - 1, 2];
            List<PrimStruct> prims = new List<PrimStruct>();
            List<Vertex> foundVertices = new List<Vertex>();
            int numEdgesFound = 0;

            foundVertices.Add(start);

            // add prims for all neighbors of start
            foreach (Vertex neighbor in start.Neighbors)
            {
                if (!foundVertices.Contains(neighbor))
                {
                    prims.Add(new PrimStruct(neighbor,
                        graph.GetWeight(start, neighbor),
                        start));
                }
            }

            while (numEdgesFound < graph.Vertices.Count - 1)
            {

                // get the vertex with the shortest cost
                prims.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                PrimStruct currentPrim = prims[0];
                prims.RemoveAt(0);

                // add an edge to that prim
                edges[numEdgesFound, 0] = currentPrim.Parent;
                edges[numEdgesFound, 1] = currentPrim.vertex;
                foundVertices.Add(currentPrim.vertex);
                numEdgesFound++;

                foreach (Vertex neighbor in currentPrim.vertex.Neighbors)
                {
                    if (!foundVertices.Contains(neighbor))
                    {
                        PrimStruct neighborPrim = prims.Find(p => p.vertex.Equals(neighbor));
                        if (neighborPrim.vertex != null)
                        {
                            if (graph.GetWeight(currentPrim.vertex, neighbor) < neighborPrim.Cost)
                            {
                                neighborPrim.Cost = graph.GetWeight(currentPrim.vertex, neighbor);
                                neighborPrim.Parent = currentPrim.vertex;
                            }
                        }
                        else
                        {
                            prims.Add(new PrimStruct(neighbor,
                            graph.GetWeight(currentPrim.vertex, neighbor),
                            currentPrim.vertex));
                        }
                    }

                }
            }

            return edges;
        }

        struct PrimStruct
        {
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
