using System;
using System.Collections.Generic;

namespace GraphsClassProject
{
    public class ParentGraph
    {
        public int maxWeight { get; set; }
        internal List<Vertex> Vertices { get; set; }

        public String GraphName { get; set; }

        public ParentGraph(String graphName)
        {
            this.GraphName = graphName;

            Vertices = new List<Vertex>();

            maxWeight = 1;
        }
    }
}