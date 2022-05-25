using System;
using System.Collections.Generic;

namespace GraphsClassProject
{
    internal class TopologicalSort
    {
        // only for digraphs
        private readonly ParentGraph graph;

        public TopologicalSort(ParentGraph graph)
        {
            this.graph = graph;
        }

        public Vertex[] GetTopologicalSort()
        {
            Vertex[] sorted = new Vertex[graph.Vertices.Count];

            // make adjacency list (use each vertex's neighbors list) - dictionary
            Dictionary<Vertex, List<Vertex>> adjacencyList = new Dictionary<Vertex, List<Vertex>>();
            foreach (Vertex vertex in graph.Vertices)
            {
                adjacencyList.Add(vertex, vertex.Neighbors);
            }

            // make indegree list (use each vertex's indegree) - dictionary
            Dictionary<Vertex, int> indegreeList = new Dictionary<Vertex, int>();
            foreach (Vertex vertex in graph.Vertices)
            {
                indegreeList.Add(vertex, vertex.Indegree);
            }

            // make 0s queue
            Queue<Vertex> zeroes = new Queue<Vertex>();

            // enqueue all 0 indegrees
            foreach (KeyValuePair<Vertex, int> entry in indegreeList)
            {
                if (entry.Value == 0)
                {
                    zeroes.Enqueue(entry.Key);
                }
            }

            int numVerticesAdded = 0;
            while (zeroes.Count > 0)
            {
                // dequeue Vertex v, add to sorted
                Vertex v = zeroes.Dequeue();
                sorted[numVerticesAdded] = v;
                numVerticesAdded++;

                // for all vertices in v's adjacency list, indegree--. if new indegree == 0, enqueue
                List<Vertex> verticesToSubtract = adjacencyList[v];
                foreach (Vertex neighbor in verticesToSubtract)
                {
                    indegreeList[neighbor] = indegreeList[neighbor]--;
                    if (indegreeList[neighbor] == 0)
                    {
                        zeroes.Enqueue(neighbor);
                    }
                }

            }

            // todo: if sorted is shorter than adjacency list, throw error - contains cycle
            if (numVerticesAdded < graph.Vertices.Count)
            {
                throw new Exception("Graph contains cycle");
            }

            return sorted;
        }
    }
}
