using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsClassProject
{
    internal class Kruskal
    {
        private readonly ParentGraph graph;
        private List<Vertex> Vertices;
        private List<EdgeStruct> Edges;


        public Kruskal(ParentGraph graph)
        {
            this.graph = graph;
            this.Vertices = graph.Vertices;
        }

        private List<EdgeStruct> GetListOfEdges()
        {
            List<EdgeStruct> edges = new List<EdgeStruct>();

            foreach (Vertex vertex in Vertices)
            {
                foreach (Vertex neighbor in vertex.Neighbors)
                {
                    edges.Add(new EdgeStruct(vertex, graph.GetWeight(vertex, neighbor), neighbor));
                }
            }

            return edges;
        }

        public Vertex[,] KruskalAlgorithm()
        {
            this.Edges = GetListOfEdges();
            Vertex[,] shortestPath = new Vertex[Vertices.Count - 1, 2];
            List<EdgeStruct> orderedEdges = SortEdges();


            List<List<Vertex>> visited = new List<List<Vertex>>();
            int indexToAddAt = 0;

            int temp = Vertices.Count - 2;
            while (shortestPath[temp, 0] == null)
            {
                EdgeStruct shortest = orderedEdges[0];
                int foundSourceWhere = -1;
                int foundDestinationWhere = -1;

                //check if will create cycle
                foreach (List<Vertex> connectedVertices in visited)
                {
                    foreach (Vertex currVertex in connectedVertices)
                    {
                        if (currVertex == shortest.source)
                        {
                            foundSourceWhere = visited.IndexOf(connectedVertices);
                        }
                        else if (currVertex == shortest.Destination)
                        {
                            foundDestinationWhere = visited.IndexOf(connectedVertices);
                        }
                    }
                }

                if (foundSourceWhere == -1)
                {
                    if (foundDestinationWhere == -1)
                    {
                        List<Vertex> unconnected = new List<Vertex>();
                        unconnected.Add(shortest.source);
                        unconnected.Add(shortest.Destination);
                        visited.Add(unconnected);
                        shortestPath[indexToAddAt, 0] = shortest.source;
                        shortestPath[indexToAddAt, 1] = shortest.Destination;
                        indexToAddAt++;
                    }
                    else
                    {
                        visited[foundDestinationWhere].Add(shortest.source);
                        shortestPath[indexToAddAt, 0] = shortest.source;
                        shortestPath[indexToAddAt, 1] = shortest.Destination;
                        indexToAddAt++;
                    }
                }
                else
                {
                    if (foundDestinationWhere == -1)
                    {
                        visited[foundSourceWhere].Add(shortest.Destination);
                        shortestPath[indexToAddAt, 0] = shortest.source;
                        shortestPath[indexToAddAt, 1] = shortest.Destination;
                        indexToAddAt++;
                    }
                    else if (foundDestinationWhere != foundSourceWhere)
                    {
                        foreach (Vertex movingVertex in visited[foundDestinationWhere])
                        {
                            visited[foundSourceWhere].Add(movingVertex);
                        }

                        visited.RemoveAt(foundDestinationWhere);
                        shortestPath[indexToAddAt, 0] = shortest.source;
                        shortestPath[indexToAddAt, 1] = shortest.Destination;
                        indexToAddAt++;
                    }
                }

                orderedEdges.RemoveAt(0);
            }

            return shortestPath;
        }
        

        private List<EdgeStruct> SortEdges()
        {
            List<EdgeStruct> sorted = Edges;
            sorted.Sort((x, y) => x.Weight - y.Weight);
            return sorted;
        }

        struct EdgeStruct
        {
            // constructor
            public EdgeStruct(Vertex vertexA, int weight, Vertex vertexB)
            {
                this.source = vertexA;
                this.Weight = weight;
                this.Destination = vertexB;
            }

            internal Vertex source;
            internal int Weight { get; set; }
            internal Vertex Destination { get; set; }
        }
    }
}