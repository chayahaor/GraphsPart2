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
            this.Edges = getListOfEdges();
        }

        private List<EdgeStruct> getListOfEdges()
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

        public Vertex[,] KruskalsAlgorithm()
        {
            Vertex[,] ShortestPath = new Vertex[Vertices.Count - 1, 2];
            List<EdgeStruct> OrderedEdges = SortEdges();


            List<List<Vertex>> visited = new List<List<Vertex>>();
            int indexToAddAt = 0;

            while (ShortestPath.Length > Vertices.Count - 1)
            {
                EdgeStruct Shortest = OrderedEdges[0];
                int foundSourceWhere = -1;
                int foundDestinationWhere = -1;

                //check if will create cycle
                foreach (List<Vertex> connectedVertices in visited)
                {
                    foreach (Vertex currVertex in connectedVertices)
                    {
                        if (currVertex == Shortest.source)
                        {
                            foundSourceWhere = visited.IndexOf(connectedVertices);
                        }
                        else if (currVertex == Shortest.destination)
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
                        unconnected.Add(Shortest.source);
                        unconnected.Add(Shortest.destination);
                        visited.Add(unconnected);
                        ShortestPath[indexToAddAt, 0] = Shortest.source;
                        ShortestPath[indexToAddAt, 1] = Shortest.destination;
                        indexToAddAt++;
                    }
                    else
                    {
                        visited[foundDestinationWhere].Add(Shortest.source);
                        ShortestPath[indexToAddAt, 0] = Shortest.source;
                        ShortestPath[indexToAddAt, 1] = Shortest.destination;
                        indexToAddAt++;
                    }
                }
                else
                {
                    if (foundDestinationWhere == -1)
                    {
                        visited[foundSourceWhere].Add(Shortest.source);
                        ShortestPath[indexToAddAt, 0] = Shortest.source;
                        ShortestPath[indexToAddAt, 1] = Shortest.destination;
                        indexToAddAt++;
                    }
                    else if (foundDestinationWhere != foundSourceWhere)
                    {
                        foreach (Vertex movingVertex in visited[foundDestinationWhere])
                        {
                            visited[foundSourceWhere].Add(movingVertex);
                        }
                        visited.RemoveAt(foundDestinationWhere);
                        ShortestPath[indexToAddAt, 0] = Shortest.source;
                        ShortestPath[indexToAddAt, 1] = Shortest.destination;
                        indexToAddAt++;
                    }
                }
                OrderedEdges.RemoveAt(0);
            }
            return ShortestPath;
        }

        private List<EdgeStruct> SortEdges()
        {
            List<EdgeStruct> Sorted = new List<EdgeStruct>();
            foreach (EdgeStruct AddingEdge in Edges)
            {
                foreach (EdgeStruct SortedEdge in Sorted)
                {
                    if (AddingEdge.weight < SortedEdge.weight)
                    {
                        Sorted.Add(AddingEdge);
                    }
                }
                if (!(Sorted[Sorted.Count - 1]).Equals(AddingEdge))
                {
                    Sorted.Add(AddingEdge);
                }
            }
            return Sorted;
        }

        struct EdgeStruct
        {
            // constructor
            public EdgeStruct(Vertex vertexA, int weight, Vertex vertexB)
            {
                this.source = vertexA;
                this.weight = weight;
                this.destination = vertexB;
            }

            internal Vertex source;
            internal int weight { get; set; }
            internal Vertex destination { get; set; }
        }
    }
}

