using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsClassProject
{
    partial class GraphNew
    {
<<<<<<< HEAD:Kruskal.cs
        private readonly GraphNew GraphNew;
        private List<Vertex> Vertices;
        private List<EdgeStruct> Edges;

        public Kruskal(GraphNew graphNew)
        {
            this.GraphNew = graphNew;
            this.Vertices = graphNew.Vertices;
        }

        private List<EdgeStruct> GetListOfEdges()
=======
        
       // private List<EdgeStruct> Edges;

        private List<EdgeStruct> GetListOfEdgesForKruskal()
>>>>>>> e821afd8a1b49f7121c129b6bb950751f1e633cc:MinSpanningTrees.cs
        {
            List<EdgeStruct> Edges = new List<EdgeStruct>();

            foreach (Vertex Vertex in Vertices)
            {
                foreach (Vertex Neighbor in Vertex.Neighbors)
                {
<<<<<<< HEAD:Kruskal.cs
                    Edges.Add(new EdgeStruct(Vertex, GraphNew.GetEdgeWeight(Vertex, Neighbor), Neighbor));
=======
                    Edges.Add(new EdgeStruct(Vertex, GetEdgeWeight(Vertex, Neighbor), Neighbor));
>>>>>>> e821afd8a1b49f7121c129b6bb950751f1e633cc:MinSpanningTrees.cs
                }
            }

            return Edges;
        }

        public Vertex[,] GetKruskalAlgorithm()
        {
            Vertex[,] ShortestPath = new Vertex[Vertices.Count - 1, 2];
            List<EdgeStruct> OrderedEdges = SortEdges();


            List<List<Vertex>> Visited = new List<List<Vertex>>();
            int IndexToAddAt = 0;

            int Temp = Vertices.Count - 2;
            while (ShortestPath[Temp, 0] == null)
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
                        List<Vertex> Unconnected = new List<Vertex>();
                        Unconnected.Add(Shortest.Source);
                        Unconnected.Add(Shortest.Destination);
                        Visited.Add(Unconnected);
                        ShortestPath[IndexToAddAt, 0] = Shortest.Source;
                        ShortestPath[IndexToAddAt, 1] = Shortest.Destination;
                        IndexToAddAt++;
                    }
                    else
                    {
                        Visited[FoundDestinationWhere].Add(Shortest.Source);
                        ShortestPath[IndexToAddAt, 0] = Shortest.Source;
                        ShortestPath[IndexToAddAt, 1] = Shortest.Destination;
                        IndexToAddAt++;
                    }
                }
                else
                {
                    if (FoundDestinationWhere == -1)
                    {
                        Visited[FoundSourceWhere].Add(Shortest.Destination);
                        ShortestPath[IndexToAddAt, 0] = Shortest.Source;
                        ShortestPath[IndexToAddAt, 1] = Shortest.Destination;
                        IndexToAddAt++;
                    }
                    else if (FoundDestinationWhere != FoundSourceWhere)
                    {
                        foreach (Vertex MovingVertex in Visited[FoundDestinationWhere])
                        {
                            Visited[FoundSourceWhere].Add(MovingVertex);
                        }

                        Visited.RemoveAt(FoundDestinationWhere);
                        ShortestPath[IndexToAddAt, 0] = Shortest.Source;
                        ShortestPath[IndexToAddAt, 1] = Shortest.Destination;
                        IndexToAddAt++;
                    }
                }

                OrderedEdges.RemoveAt(0);
            }

            return ShortestPath;
        }


        private List<EdgeStruct> SortEdges()
        {
            List<EdgeStruct> Sorted = GetListOfEdgesForKruskal();
            Sorted.Sort((x, y) => (int)(x.Weight - y.Weight)); //TODO: Confirm that casting does not mess it up
            return Sorted;
        }

        struct EdgeStruct
        {
            // constructor
            public EdgeStruct(Vertex vertexA, double weight, Vertex vertexB)
            {
                Source = vertexA;
                Weight = weight;
                Destination = vertexB;
            }

            internal Vertex Source;
            internal double Weight { get; set; }
            internal Vertex Destination { get; set; }
        }



        ////////////////////////////////////////////////////////////////////////////////////////

        public Vertex[,] PrimMinSpanningGraph(Vertex start)
        {
            Vertex[,] Edges = new Vertex[Vertices.Count - 1, 2];
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
                        GetEdgeWeight(start, Neighbor),
                        start));
                }
            }

            while (NumEdgesFound < Vertices.Count - 1)
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
                            if (GetEdgeWeight(CurrentPrim.Vertex, Neighbor) < NeighborPrim.Cost)
                            {
                                NeighborPrim.Cost = GetEdgeWeight(CurrentPrim.Vertex, Neighbor);
                                NeighborPrim.Parent = CurrentPrim.Vertex;
                            }
                        }
                        else
                        {
                            Prims.Add(new PrimStruct(Neighbor,
                            GetEdgeWeight(CurrentPrim.Vertex, Neighbor),
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
