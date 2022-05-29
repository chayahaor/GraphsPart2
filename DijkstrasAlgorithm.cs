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

            /*
            * how to determine if there is a cycle and target node cannot be reached
            */



            //return -1 if no path exists
            double shortestDist = -1.0;

            //vertex->corresponding structure
            Dictionary<Vertex, Dijkstra> vertexStructs =
                new Dictionary<Vertex, Dijkstra>();


            Dijkstra currNode = new Dijkstra(true, 0, source, source);  //intialize currNode to the source node
            Dijkstra targetNode = new Dijkstra(false, int.MaxValue, null, target);  //keep track of targetNode being false


            vertexStructs.Add(source, currNode); //add to dictionary

            while (currNode.vertex != target) //!vertexStructs[target].sdFound
            {
                
                foreach (Vertex v in currNode.vertex.Neighbors)
                {
                    Console.WriteLine("neighbor is " + v.Name);
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
                   
                    
                        Vertex parent = currNode.vertex;

                        while (parent != source)
                        {

                            //Console.WriteLine("parent is" + parent.Name);
                            newDistance += vertexStructs[parent].distanceFromStart;    //   not accessing parent here

                            parent = vertexStructs[parent].parent;

                        }
                        newDistance += graph.GetWeight(currNode.vertex, v);

                    Console.WriteLine(newDistance);
                    
                    if (newDistance < currStruct.distanceFromStart)
                    {
                        //update parent and shortest dist of v
                        currStruct.parent = currNode.vertex;
                        currStruct.distanceFromStart = newDistance;
                        vertexStructs.Remove(v);
                        vertexStructs.Add(v, currStruct);
                        
                        
                    }

                
                }

                

                //find shortest false node and set to currNode and true
                int shortestFalse = int.MaxValue;
                foreach (KeyValuePair<Vertex, Dijkstra> d in vertexStructs)
                {
             
                    if (!d.Value.sdFound && d.Value.distanceFromStart < shortestFalse)
                    {
                        
                        currNode = d.Value;
                        shortestFalse = d.Value.distanceFromStart;
                    }
                }

                if (shortestFalse == int.MaxValue)
                {
                    //all shortest paths have been found
                    throw new Exception("Selected vertices do not have a connection between them");
                }

                Console.WriteLine("shortest false is " + shortestFalse);

                currNode.sdFound = true;
                vertexStructs.Remove(currNode.vertex);
                vertexStructs.Add(currNode.vertex, currNode);
                Console.WriteLine("currNode is " + currNode.vertex.Name);

            }

            shortestDist = currNode.distanceFromStart;

            Console.WriteLine("Shortest distance is" + shortestDist);



            if (shortestDist != -1)
            {
                Vertex parent = currNode.parent;
                Path.Add(parent);
                Console.WriteLine(currNode.vertex.Name);
                Console.WriteLine(parent.Name);

                //create path - add parent vertex of node until reach node with source vertex
                while (parent != source)
                {
                    parent = vertexStructs[parent].parent;
                    Path.Insert(0, parent);
                }
                
                PrintVertexSequence(Path);
            }

            return shortestDist;
        }

        
        private void PrintVertexSequence(List<Vertex> path)
        {
            Console.WriteLine(path.Count);
            for (int i = 0; i < path.Count; i++)
            {
                Console.Write(path[i].Name);
            }
        }

        struct Dijkstra
        {
            internal bool sdFound { get; set; }
            internal int distanceFromStart { get; set; }
            internal Vertex parent { get; set; }
            internal Vertex vertex { get; set; }

            public Dijkstra(bool sdFound, int distanceFromStart, Vertex parent, Vertex vertex)
            {
                this.sdFound = sdFound;
                this.distanceFromStart = distanceFromStart;
                this.parent = parent;
                this.vertex = vertex;
            }

        }
    }
}