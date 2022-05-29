using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsClassProject
{

    internal class DijkstrasAlgorithm
    {

        private readonly ParentGraph graph;
        public List<Vertex> Path { get; set; }
        private const int MaxVal = int.MaxValue;


        public DijkstrasAlgorithm(ParentGraph graph)
        {
            this.graph = graph;
            Path = new List<Vertex>();
        }



        public double DijskstrasShortestPath(Vertex source, Vertex target)  //TODO C and R - return this value in a message box
        {
            ClearPath(); 

            Dictionary<Vertex, Dijkstra> vertexStructs =
                new Dictionary<Vertex, Dijkstra>();

            Dijkstra currNode = new Dijkstra(true, 0, source, source);  
            vertexStructs.Add(source, currNode);

            while (currNode.Vertex != target)
            {

                foreach (Vertex v in currNode.Vertex.Neighbors)
                {
                    if (!vertexStructs.ContainsKey(v))
                    {
                        Dijkstra newNode = new Dijkstra(false, MaxVal, null, v);
                        vertexStructs.Add(v, newNode);
                    }

                    Dijkstra currStruct = vertexStructs[v];
                    int newDistance = vertexStructs[currNode.Vertex].DistanceFromStart + graph.GetWeight(currNode.Vertex, v);

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
                int shortestFalse = MaxVal;
                foreach (KeyValuePair<Vertex, Dijkstra> d in vertexStructs)
                {

                    if (!d.Value.SdFound && d.Value.DistanceFromStart < shortestFalse)
                    {
                        currNode = d.Value;
                        shortestFalse = d.Value.DistanceFromStart;
                    }
                }

                if (shortestFalse == MaxVal)
                {
                    //all shortest paths have been found
                    throw new Exception("Selected vertices do not have a connection between them");
                }


                currNode.SdFound = true;
                vertexStructs.Remove(currNode.Vertex);
                vertexStructs.Add(currNode.Vertex, currNode);

            }

            double shortestDist = currNode.DistanceFromStart;


            if (shortestDist != MaxVal)
            {
                Vertex parent = currNode.Parent;
                Path.Add(parent);

                while (parent != source)
                {
                    parent = vertexStructs[parent].Parent;
                    Path.Insert(0, parent);
                }

                Path.Add(currNode.Vertex);

            }
            else
            {
                throw new Exception("No path exists");
            }

            return shortestDist;
        }


        public void ClearPath()
        {
            Path.Clear();
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