using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsClassProject
{
    internal class DijkstrasAlgorithm
    {
        //only relevant to weighted graphs
        private readonly ParentGraph graph;
        public List<Vertex> Path { get; set; }


        public DijkstrasAlgorithm(ParentGraph graph)
        {
            this.graph = graph;

            //path of nodes
            Path = new List<Vertex>();
        }


        public double DijskstrasShortestPath(Vertex source, Vertex target)
        {
            //return -1 if no path exists
            double shortestDist = -1.0;

            //vertex->corresponding structure
            Dictionary<Vertex, Dijkstra> vertexStructs =
                new Dictionary<Vertex, Dijkstra>();


            Dijkstra currNode = new Dijkstra(true, 0, source, source); //initialize currNode to the source node
            Dijkstra targetNode =
                new Dijkstra(false, int.MaxValue, null, target); //keep track of targetNode being false


            vertexStructs.Add(source, currNode); //add to dictionary

            while (currNode.Vertex != target) //!vertexStructs[target].sdFound
            {
                foreach (Vertex v in currNode.Vertex.Neighbors)
                {
                    //if newNode from this vertex doesn't exist
                    if (!vertexStructs.ContainsKey(v))
                    {
                        if (v == target)
                        {
                            vertexStructs.Add(v, targetNode);
                        }
                        else
                        {
                            Dijkstra newNode = new Dijkstra(false, int.MaxValue, null, v);
                            vertexStructs.Add(v, newNode);
                        }
                    }

                    Dijkstra currStruct = vertexStructs[v];

                    int newDistance = 0;

                    Vertex parent = currNode.Vertex;

                    while (parent != source)
                    {
                        newDistance += vertexStructs[parent].DistanceFromStart; //   not accessing parent here

                        parent = vertexStructs[parent].Parent;
                    }

                    newDistance += graph.GetWeight(currNode.Vertex, v);
                    
                    if (newDistance < currStruct.DistanceFromStart)
                    {
                        //update parent and shortest dist of v
                        currStruct.Parent = currNode.Vertex;
                        currStruct.DistanceFromStart = newDistance;
                        vertexStructs.Remove(v);
                        vertexStructs.Add(v, currStruct);
                    }
                }


                //find shortest false node and set to currNode and true
                int shortestFalse = int.MaxValue;
                foreach (KeyValuePair<Vertex, Dijkstra> d in vertexStructs)
                {
                    if (!d.Value.SdFound && d.Value.DistanceFromStart < shortestFalse)
                    {
                        currNode = d.Value;
                        shortestFalse = d.Value.DistanceFromStart;
                    }
                }

                if (shortestFalse == int.MaxValue)
                {
                    //all shortest paths have been found
                    throw new Exception("Selected vertices do not have a connection between them");
                }
                
                currNode.SdFound = true;
                vertexStructs.Remove(currNode.Vertex);
                vertexStructs.Add(currNode.Vertex, currNode);
            }

            shortestDist = currNode.DistanceFromStart;
            

            if (shortestDist != -1) //TODO: Why are we comparing a double to an int?
            {
                Vertex parent = currNode.Parent;
                Path.Add(parent);
               
                //create path - add parent vertex of node until reach node with source vertex
                while (parent != source)
                {
                    parent = vertexStructs[parent].Parent;
                    Path.Insert(0, parent);
                }
            }

            return shortestDist;
        }


        struct Dijkstra
        {
            internal bool SdFound { get; set; }
            internal int DistanceFromStart { get; set; }
            internal Vertex Parent { get; set; }
            internal Vertex Vertex { get; set; }

            public Dijkstra(bool sdFound, int distanceFromStart, Vertex parent, Vertex vertex)
            {
                this.SdFound = sdFound;
                this.DistanceFromStart = distanceFromStart;
                this.Parent = parent;
                this.Vertex = vertex;
            }
        }
    }
}