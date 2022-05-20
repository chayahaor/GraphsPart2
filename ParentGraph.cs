using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsClassProject
{
    public class ParentGraph
    {
        internal List<Vertex> Vertices { get; set; }

        public String GraphName { get; set; }

        public ParentGraph(String graphName)
        {
            this.GraphName = graphName;

            Vertices = new List<Vertex>();

        }

        internal void AddNode(Vertex node)
        {
            Vertices.Add(node);
        }

    }
}
