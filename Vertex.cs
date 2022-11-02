﻿using System;
using System.Collections.Generic;

namespace GraphsClassProject
{
    public class Vertex
    {
        public String Name { get; set; }
        public List<Vertex> Neighbors = new List<Vertex>();
        public List<int> Weights {get; set;}
        public int Indegree { get; set; }

        public Vertex(String nm)
        {
            Name = nm;
            Indegree = 0;
            Weights = new List<int>();
        }

        public void AddEdge(Vertex target, int weight)
        {
            Neighbors.Add(target);
            Weights.Add(weight);
            target.Indegree++;
        }
    }
}
