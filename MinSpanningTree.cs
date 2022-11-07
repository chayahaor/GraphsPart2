using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsClassProject
{
    partial class ParentGraph
    {
        //(if (weighted) )
        //add functionality for unweighted (weight of 1) with basic formula

        private Vertex[,] PrimMinSpanningGraph(Vertex start)
        {
            Vertex[,] edges = new Vertex[Vertices.Count - 1, 2];
            List<EdgeStruct> prims = new List<EdgeStruct>();
            List<Vertex> foundVertices = new List<Vertex>();
            int numEdgesFound = 0;

            foundVertices.Add(start);

            // add prims for all neighbors of start
            foreach (Vertex neighbor in start.Neighbors)
            {
                if (!foundVertices.Contains(neighbor))
                {
                    prims.Add(new EdgeStruct(neighbor,
                        GetWeight(start, neighbor),
                        start));
                }
            }

            while (numEdgesFound < Vertices.Count - 1)
            {

                // get the vertex with the shortest cost
                prims.Sort((x, y) => x.Weight.CompareTo(y.Weight));
                EdgeStruct currentPrim = prims[0];
                prims.RemoveAt(0);

                // add an edge to that prim
                edges[numEdgesFound, 0] = currentPrim.Parent;
                edges[numEdgesFound, 1] = currentPrim.vertex;
                foundVertices.Add(currentPrim.vertex);
                numEdgesFound++;

                foreach (Vertex neighbor in currentPrim.vertex.Neighbors)
                {
                    if (!foundVertices.Contains(neighbor))
                    {
                        EdgeStruct neighborPrim = prims.Find(p => p.vertex.Equals(neighbor));
                        if (neighborPrim.vertex != null)
                        {
                            if (GetWeight(currentPrim.vertex, neighbor) < neighborPrim.Weight)
                            {
                                neighborPrim.Weight = GetWeight(currentPrim.vertex, neighbor);
                                neighborPrim.Parent = currentPrim.vertex;
                            }
                        }
                        else
                        {
                            prims.Add(new EdgeStruct(neighbor,
                            GetWeight(currentPrim.vertex, neighbor),
                            currentPrim.vertex));
                        }
                    }

                }
            }

            return edges;
        }

        //replace with edgestruct that can also be used for kruskal
        struct EdgeStruct
        {
            public EdgeStruct(Vertex vertex, int weight, Vertex parent)
            {
                this.vertex = vertex;
                this.Weight = weight;
                this.Parent = parent;
            }

            internal Vertex vertex;
            internal int Weight { get; set; }
            internal Vertex Parent { get; set; }
        }
    }
}

