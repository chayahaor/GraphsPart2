using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsClassProject
{
    class Vertex
    {
        public String Name { get; set; }
        public List<Vertex> Neighbors = new List<Vertex>();
        public List<int> Weights = new List<int>();
        public int Indegree { get; set; }

        public Vertex(String nm)
        {
            Name = nm;
            Indegree = 0;
        }

        public void AddEdge(Vertex target, int weight)
        {
            Neighbors.Add(target);
            Weights.Add(weight);
            target.Indegree++;
        }
    }
}
