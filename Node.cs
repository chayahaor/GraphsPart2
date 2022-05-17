using System;
using System.Collections.Generic;

namespace GraphsClassProject  
{
    class Node
    { 
        public string Name { get; set;}
        public List<Node> Neighbors = new List<Node>();
        public int Indegree { get; set; }
        
        public Node(string name)
        {
            Name = name;
            Indegree = 0;
        }

        public Node(Node other)
        {
            Name = other.Name;
            Indegree = other.Indegree;
        }

        public void addEdge(Node target)
        {
            Neighbors.Add(target);
            Indegree++;
        }
    }
}
