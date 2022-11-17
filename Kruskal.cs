using System.Collections.Generic;

namespace GraphsClassProject
{
    internal class Kruskal
    {
        private readonly GraphNew GraphNew;
        private List<Vertex> Vertices;
        private List<EdgeStruct> Edges;

        public Kruskal(GraphNew graphNew)
        {
            this.GraphNew = graphNew;
            this.Vertices = graphNew.Vertices;
        }

        private List<EdgeStruct> GetListOfEdges()
        {
            List<EdgeStruct> Edges = new List<EdgeStruct>();

            foreach (Vertex Vertex in Vertices)
            {
                foreach (Vertex Neighbor in Vertex.Neighbors)
                {
                    Edges.Add(new EdgeStruct(Vertex, GraphNew.GetEdgeWeight(Vertex, Neighbor), Neighbor));
                }
            }

            return Edges;
        }

        public Vertex[,] KruskalAlgorithm()
        {
            this.Edges = GetListOfEdges();
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
            List<EdgeStruct> Sorted = Edges;
            Sorted.Sort((x, y) => (int)(x.Weight - y.Weight)); //TODO: Confirm that casting does not mess it up
            return Sorted;
        }

        struct EdgeStruct
        {
            // constructor
            public EdgeStruct(Vertex vertexA, double weight, Vertex vertexB)
            {
                this.Source = vertexA;
                this.Weight = weight;
                this.Destination = vertexB;
            }

            internal Vertex Source;
            internal double Weight { get; set; }
            internal Vertex Destination { get; set; }
        }
    }
}