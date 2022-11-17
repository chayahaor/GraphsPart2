using System;
using System.Collections.Generic;

namespace GraphsClassProject
{
    public class Vertex
    {
        public String Name { get; }
        public List<Vertex> Neighbors = new List<Vertex>();
        public List<int> Weights {get; }
        public int Indegree { get; private set; }
        public double XCoord { get; }
        public double YCoord { get; }
        public Vertex(String nm, double xCoord, double yCoord)
        {
            Name = nm;
            Indegree = 0;
            Weights = new List<int>();
            XCoord = xCoord;
            YCoord = yCoord;

        }

        public void AddEdge(Vertex target, int weight)
        {
            Neighbors.Add(target);
            Weights.Add(weight);
            target.Indegree++;
        }
    }
}
