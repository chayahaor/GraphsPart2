using System;
using System.Collections.Generic;

namespace GraphsClassProject
{
    partial class Graph
    {
        public Vertex[] GetTopologicalSort()
        {
            Vertex[] Sorted = new Vertex[Vertices.Count];
            
            // make adjacency list (use each vertex's neighbors list) - dictionary
            Dictionary<Vertex, List<Vertex>> AdjacencyList = new Dictionary<Vertex, List<Vertex>>();

            // make indegree list (use each vertex's indegree) - dictionary
            Dictionary<Vertex, int> IndegreeList = new Dictionary<Vertex, int>();
            
            foreach (Vertex Vertex in Vertices)
            {
                AdjacencyList.Add(Vertex, Vertex.Neighbors);
                IndegreeList.Add(Vertex, Vertex.Indegree);
            }

            // make 0s queue
            Queue<Vertex> Zeroes = new Queue<Vertex>();

            // enqueue all 0 indegrees
            foreach (KeyValuePair<Vertex, int> Entry in IndegreeList)
            {
                if (Entry.Value == 0)
                {
                    Zeroes.Enqueue(Entry.Key);
                }
            }

            if (Zeroes.Count == 0)
            {
                throw new Exception("Graph contains cycle");
            }

            int NumVerticesAdded = 0;

            while (Zeroes.Count > 0)
            {
                // dequeue Vertex v, add to sorted
                Vertex V = Zeroes.Dequeue();
                Sorted[NumVerticesAdded] = V;
                NumVerticesAdded++;

                // for all vertices in v's adjacency list, indegree--. if new indegree == 0, enqueue
                List<Vertex> VerticesToSubtract = AdjacencyList[V];
                foreach (Vertex Neighbor in VerticesToSubtract)
                {
                    IndegreeList[Neighbor]--;
                    if (IndegreeList[Neighbor] == 0)
                    {
                        Zeroes.Enqueue(Neighbor);
                    }
                }
                
                if (NumVerticesAdded > Vertices.Count)
                {
                    throw new Exception("Graph contains cycle");
                }
            }

            return Sorted;
        }
    }
}
