using System;
using System.Collections.Generic;

namespace GraphsClassProject
{
    public class ParentGraph
    {
        public int MaxWeight { get; set; } 
        internal List<Vertex> Vertices { get; set; }
        public String GraphName { get; set; }

        public GraphType Type { get; set; }
        public ParentGraph(String graphName)
        {
            this.GraphName = graphName;

            Vertices = new List<Vertex>();

            MaxWeight = 1;
        }
        internal int GetWeight(Vertex initial, Vertex terminal)
        {
            int vertexIndex = Vertices.IndexOf(initial);
            int neighborIndex = Vertices[vertexIndex].Neighbors.IndexOf(terminal);
            int weight = Vertices[vertexIndex].Weights[neighborIndex];
            return weight;
        }
    }
}