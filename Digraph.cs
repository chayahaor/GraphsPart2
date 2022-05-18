using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Graphs
{
    class Digraph
    {
        public List<Vertex> Vertices = new List<Vertex>();

        public void AddVertex(Vertex v)
        {
            Vertices.Add(v);
        }

        public bool LoadVertices(String FileName)
        {
            bool RetVal = true;
            try
            {
                using (TextReader reader = new StreamReader(FileName))
                {
                    String line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        String[] vertices = line.Split(' ');
                        if (vertices.Length > 0)
                        {
                            // TODO add check that the vertex names are not repeated  
                            Vertex v = new Vertex(vertices[0]);
                            for (int eix = 1; eix < vertices.Length; ++eix)
                            {
                                Vertex nbr = new Vertex(vertices[eix]);
                                v.AddEdge(nbr, 1);
                            }
                            Vertices.Add(v);
                        }
                    }
                }
            }
            catch
            {
                RetVal = false;
            }
            return RetVal;
        }

        
    }
}
