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
                    currNode = UpdateStructs(vertexStructs, currNode, out Dijkstra currStruct, out int newDistance, v);

                }

                currNode = GetNewCurrNode(vertexStructs, currNode);

            }

            CreatePath(source, vertexStructs, currNode);

            return currNode.DistanceFromStart;
        }

        private static Dijkstra GetNewCurrNode(Dictionary<Vertex, Dijkstra> vertexStructs, Dijkstra currNode)
        {
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
                throw new Exception("No path exists");
            }


            currNode.SdFound = true;
            vertexStructs.Remove(currNode.Vertex);
            vertexStructs.Add(currNode.Vertex, currNode);
            return currNode;
        }

        private Dijkstra UpdateStructs(Dictionary<Vertex, Dijkstra> vertexStructs, Dijkstra currNode, out Dijkstra currStruct, out int newDistance, Vertex neighbor)
        {
            if (!vertexStructs.ContainsKey(neighbor))
            {
                Dijkstra newNode = new Dijkstra(false, MaxVal, null, neighbor);
                vertexStructs.Add(neighbor, newNode);
            }

            currStruct = vertexStructs[neighbor];
            newDistance = vertexStructs[currNode.Vertex].DistanceFromStart + graph.GetWeight(currNode.Vertex, neighbor);

            if (newDistance < currStruct.DistanceFromStart)
            {
                //update parent and shortest dist of v
                currStruct.Parent = currNode.Vertex;
                currStruct.DistanceFromStart = newDistance;
                vertexStructs.Remove(neighbor);
                vertexStructs.Add(neighbor, currStruct);


            }

            return currNode;
        }


        private void CreatePath(Vertex source, Dictionary<Vertex, Dijkstra> vertexStructs, Dijkstra currNode)
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