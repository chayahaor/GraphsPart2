using System.Collections.Generic;

namespace GraphsClassProject
{
    internal class Prim
    {
        //only relevant to weighted graphs
        private readonly GraphNew Graph;
        

        public Prim(GraphNew graph)
        {
            this.Graph = graph;
        }
        
        public Vertex[,] PrimMinSpanningGraph(Vertex start)
        {
            Vertex[,] Edges = new Vertex[Graph.Vertices.Count - 1, 2];
            List<PrimStruct> Prims = new List<PrimStruct>();
            List<Vertex> FoundVertices = new List<Vertex>();
            int NumEdgesFound = 0;

            FoundVertices.Add(start);

            // add prims for all neighbors of start
            foreach (Vertex Neighbor in start.Neighbors)
            {
                if (!FoundVertices.Contains(Neighbor))
                {
                    Prims.Add(new PrimStruct(Neighbor,
                        Graph.GetEdgeWeight(start, Neighbor),
                        start));
                }
            }

            while (NumEdgesFound < Graph.Vertices.Count - 1)
            {

                // get the vertex with the shortest cost
                Prims.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                PrimStruct CurrentPrim = Prims[0];
                Prims.RemoveAt(0);

                // add an edge to that prim
                Edges[NumEdgesFound, 0] = CurrentPrim.Parent;
                Edges[NumEdgesFound, 1] = CurrentPrim.Vertex;
                FoundVertices.Add(CurrentPrim.Vertex);
                NumEdgesFound++;

                foreach (Vertex Neighbor in CurrentPrim.Vertex.Neighbors)
                {
                    if (!FoundVertices.Contains(Neighbor))
                    {
                        PrimStruct NeighborPrim = Prims.Find(p => p.Vertex.Equals(Neighbor));
                        if (NeighborPrim.Vertex != null)
                        {
                            if (Graph.GetEdgeWeight(CurrentPrim.Vertex, Neighbor) < NeighborPrim.Cost)
                            {
                                NeighborPrim.Cost = Graph.GetEdgeWeight(CurrentPrim.Vertex, Neighbor);
                                NeighborPrim.Parent = CurrentPrim.Vertex;
                            }
                        }
                        else
                        {
                            Prims.Add(new PrimStruct(Neighbor,
                            Graph.GetEdgeWeight(CurrentPrim.Vertex, Neighbor),
                            CurrentPrim.Vertex));
                        }
                    }

                }
            }

            return Edges;
        }

        struct PrimStruct
        {
            public PrimStruct(Vertex vertex, double cost, Vertex parent)
            {
                this.Vertex = vertex;
                this.Cost = cost;
                this.Parent = parent;
            }

            internal Vertex Vertex;
            internal double Cost { get; set; }
            internal Vertex Parent { get; set; }
        }
    }
}
