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
        private ParentGraph graph;

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
            Dijkstra targetNode = new Dijkstra(false, -1, null, target);  //keep track of targetNode being false


            List<Dijkstra> dijkstras = new List<Dijkstra>();

            dijkstras.Add(currNode);
            vertexStructs.Add(source, currNode); //add to dictionary
            int stopper = 0;
            while (currNode.vertex != target && !targetNode.sdFound && stopper<30)
            {
                stopper++;
                foreach (Vertex v in currNode.vertex.Neighbors)
                {

                    //if newNode from this vertex doesn't exist
                    if (!vertexStructs.ContainsKey(v))
                    {
                        Dijkstra newNode = new Dijkstra(false, int.MaxValue, null, v);
                        dijkstras.Add(newNode);
                        vertexStructs.Add(v, newNode);
                    }

                    Dijkstra currStruct = vertexStructs[v];

                    int newDistance = currNode.distanceFromStart + graph.GetWeight(currNode.vertex, v);
                    if (newDistance < currStruct.distanceFromStart)
                    {
                        //update parent and shortest dist of v
                        currStruct.parent = currNode.vertex;
                        currNode.distanceFromStart = newDistance;
                    }

                }

                //find shortest false node and set to currNode and true
                int shortestFalse = int.MaxValue;
                foreach (Dijkstra d in dijkstras)
                {
                    if (!d.sdFound && d.distanceFromStart < shortestFalse)
                    {
                        currNode = d;
                        shortestFalse = d.distanceFromStart;
                    }

                    if (shortestFalse == int.MaxValue)
                    {
                        //all shortest paths have been found
                    }
                }

                currNode.sdFound = true;
            }


            shortestDist = targetNode.distanceFromStart;


            if (shortestDist != -1)
            {
                Vertex parent = targetNode.parent;
                Path.Add(parent);

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
            internal bool sdFound;
            internal int distanceFromStart;
            internal Vertex parent;
            internal Vertex vertex;

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