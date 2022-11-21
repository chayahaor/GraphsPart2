using System.Collections.Generic;

namespace GraphsClassProject
{
    partial class Graph
    {

        public Vertex[,] GetKruskalsMST()
        {
            Vertex[,] MSTEdges = new Vertex[Vertices.Count - 1, 2];
            List<EdgeStruct> OrderedEdges = GetSortedListOfEdges();

            List<List<Vertex>> Visited = new List<List<Vertex>>();
            int IndexToAddAt = 0;

            while (MSTEdges[Vertices.Count - 2, 0] == null)
            {
                EdgeStruct Shortest = OrderedEdges[0];
                int FoundSourceWhere = -1;
                int FoundDestinationWhere = -1;

                //check if will create cycle
                foreach (List<Vertex> ConnectedVertices in Visited)
                {
                    foreach (Vertex CurrVertex in ConnectedVertices)
                    {
                        if (CurrVertex == Shortest.Source)
                        {
                            FoundSourceWhere = Visited.IndexOf(ConnectedVertices);
                        }
                        else if (CurrVertex == Shortest.Destination)
                        {
                            FoundDestinationWhere = Visited.IndexOf(ConnectedVertices);
                        }
                    }
                }

                if (FoundSourceWhere == -1)
                {
                    if (FoundDestinationWhere == -1)
                    {
                        List<Vertex> Unconnected = new List<Vertex>
                        {
                            Shortest.Source,
                            Shortest.Destination
                        };
                        Visited.Add(Unconnected);
                        MSTEdges[IndexToAddAt, 0] = Shortest.Source;
                        MSTEdges[IndexToAddAt, 1] = Shortest.Destination;
                        IndexToAddAt++;
                    }
                    else
                    {
                        Visited[FoundDestinationWhere].Add(Shortest.Source);
                        MSTEdges[IndexToAddAt, 0] = Shortest.Source;
                        MSTEdges[IndexToAddAt, 1] = Shortest.Destination;
                        IndexToAddAt++;
                    }
                }
                else
                {
                    if (FoundDestinationWhere == -1)
                    {
                        Visited[FoundSourceWhere].Add(Shortest.Destination);
                        MSTEdges[IndexToAddAt, 0] = Shortest.Source;
                        MSTEdges[IndexToAddAt, 1] = Shortest.Destination;
                        IndexToAddAt++;
                    }
                    else if (FoundDestinationWhere != FoundSourceWhere)
                    {
                        foreach (Vertex MovingVertex in Visited[FoundDestinationWhere])
                        {
                            Visited[FoundSourceWhere].Add(MovingVertex);
                        }

                        Visited.RemoveAt(FoundDestinationWhere);
                        MSTEdges[IndexToAddAt, 0] = Shortest.Source;
                        MSTEdges[IndexToAddAt, 1] = Shortest.Destination;
                        IndexToAddAt++;
                    }
                }

                OrderedEdges.RemoveAt(0);
            }

            return MSTEdges;
        }

        public Vertex[,] GetPrimsMST(Vertex start)
        {
            Vertex[,] MSTEdges = new Vertex[Vertices.Count - 1, 2];
            List<EdgeStruct> Prims = new List<EdgeStruct>();
            List<Vertex> FoundVertices = new List<Vertex>();
            int NumEdgesFound = 0;

            FoundVertices.Add(start);

            // add prims for all neighbors of start
            foreach (Vertex Neighbor in start.Neighbors)
            {
                if (!FoundVertices.Contains(Neighbor))
                {
                    Prims.Add(new EdgeStruct(start,
                        GetEdgeWeight(start, Neighbor),
                        Neighbor));
                }
            }

            while (NumEdgesFound < Vertices.Count - 1)
            {

                // get the vertex with the shortest cost
                Prims.Sort((x, y) => x.Weight.CompareTo(y.Weight));
                EdgeStruct CurrentPrim = Prims[0];
                Prims.RemoveAt(0);

                // add an edge to that prim
                MSTEdges[NumEdgesFound, 0] = CurrentPrim.Source;
                MSTEdges[NumEdgesFound, 1] = CurrentPrim.Destination;
                FoundVertices.Add(CurrentPrim.Destination);
                NumEdgesFound++;

                foreach (Vertex Neighbor in CurrentPrim.Destination.Neighbors)
                {
                    if (!FoundVertices.Contains(Neighbor))
                    {
                        EdgeStruct NeighborPrim = Prims.Find(p => p.Destination.Equals(Neighbor));
                        if (NeighborPrim.Destination != null)
                        {
                            if (GetEdgeWeight(CurrentPrim.Destination, Neighbor) < NeighborPrim.Weight)
                            {
                                NeighborPrim.Weight = GetEdgeWeight(CurrentPrim.Destination, Neighbor);
                                NeighborPrim.Source = CurrentPrim.Destination;
                            }
                        }
                        else
                        {
                            Prims.Add(new EdgeStruct(CurrentPrim.Destination,
                            GetEdgeWeight(CurrentPrim.Destination, Neighbor),
                            Neighbor));
                        }
                    }

                }
            }

            return MSTEdges;
        }

        struct EdgeStruct
        {
            public EdgeStruct(Vertex vertexA, double weight, Vertex vertexB)
            {
                Source = vertexA;
                Weight = weight;
                Destination = vertexB;
            }

            internal Vertex Source { get; set; }
            internal double Weight { get; set; }
            internal Vertex Destination { get; set; }
        }

        private List<EdgeStruct> GetSortedListOfEdges()
        {
            List<EdgeStruct> Edges = new List<EdgeStruct>();

            foreach (Vertex Vertex in Vertices)
            {
                foreach (Vertex Neighbor in Vertex.Neighbors)
                {
                    Edges.Add(new EdgeStruct(Vertex, GetEdgeWeight(Vertex, Neighbor), Neighbor));
                }
            }

            Edges.Sort((x, y) => (int)(x.Weight - y.Weight));
            return Edges;
        }
    }
}