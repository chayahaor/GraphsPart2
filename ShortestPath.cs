using System;
using System.Collections.Generic;

namespace GraphsClassProject
{
    partial class Graph
    {
        private const int MAX_VAL = int.MaxValue;

        private void DijskstrasShortestPath(Vertex source, Vertex target)
        {
            double ShortestDist;

            //ClearPath();

            if (source.Equals(target))
            {
                throw new Exception("Source and target are the same. Shortest distance: 0.0");
            }

            Dictionary<Vertex, Dijkstra> VertexStructs =
                new Dictionary<Vertex, Dijkstra>();
            Dijkstra CurrNode = new Dijkstra(true, 0, source, source);
            
            VertexStructs.Add(source, CurrNode);

            while (CurrNode.Vertex != target)
            {
                foreach (Vertex V in CurrNode.Vertex.Neighbors)
                {
                    CurrNode = UpdateStructs(VertexStructs, CurrNode, out Dijkstra CurrStruct, out double NewDistance, V);

                }

                CurrNode = GetNewCurrNode(VertexStructs, CurrNode);

            }

            CreatePath(source, VertexStructs, CurrNode);

            ShortestDist = CurrNode.DistanceFromStart;
        }

        private static Dijkstra GetNewCurrNode(Dictionary<Vertex, Dijkstra> vertexStructs, Dijkstra currNode)
        {
            //find shortest false node and set to currNode and true
            double ShortestFalse = MAX_VAL;
            foreach (KeyValuePair<Vertex, Dijkstra> D in vertexStructs)
            {

                if (!D.Value.SdFound && D.Value.DistanceFromStart < ShortestFalse)
                {
                    currNode = D.Value;
                    ShortestFalse = D.Value.DistanceFromStart;
                }
            }

            if (ShortestFalse == MAX_VAL)
            {
                //all shortest paths have been found
                throw new Exception("No path exists");
            }


            currNode.SdFound = true;
            vertexStructs.Remove(currNode.Vertex);
            vertexStructs.Add(currNode.Vertex, currNode);
            return currNode;
        }

        private Dijkstra UpdateStructs(Dictionary<Vertex, Dijkstra> vertexStructs, Dijkstra currNode, out Dijkstra currStruct, out double newDistance, Vertex neighbor)
        {
            if (!vertexStructs.ContainsKey(neighbor))
            {
                Dijkstra NewNode = new Dijkstra(false, MAX_VAL, null, neighbor);
                vertexStructs.Add(neighbor, NewNode);
            }

            currStruct = vertexStructs[neighbor];
            newDistance = vertexStructs[currNode.Vertex].DistanceFromStart + GetEdgeWeight(currNode.Vertex, neighbor);

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
            List<Vertex> Path = new List<Vertex>();

            Vertex Parent = currNode.Parent;
            Path.Add(Parent);

            while (Parent != source)
            {
                Parent = vertexStructs[Parent].Parent;
                Path.Insert(0, Parent);
            }

            Path.Add(currNode.Vertex);
            
        }

        //private void ClearPath()
        //{
        //    Path.Clear();
        //}

        struct Dijkstra
        {
            internal bool SdFound { get; set; }
            internal double DistanceFromStart { get; set; }
            internal Vertex Parent { get; set; }
            internal Vertex Vertex { get; set; }

            public Dijkstra(bool sdFound, double distanceFromStart, Vertex parent, Vertex vertex)
            {
                this.SdFound = sdFound;
                this.DistanceFromStart = distanceFromStart;
                this.Parent = parent;
                this.Vertex = vertex;
            }
        }
    }
}