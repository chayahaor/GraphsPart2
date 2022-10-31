using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GraphsClassProject
{
    public class GraphNew
    {
        //public int MaxWeight { get; set; }
        internal List<Vertex> Vertices { get; set; }
        public String GraphName { get; set; }

        public GraphType Type { get; set; }
        private String server;
        private String database;
        public GraphNew(String graphName, String server, String database)
        {
            this.GraphName = graphName;
            this.server = server;
            this.database = database;
            Vertices = new List<Vertex>();
            //MaxWeight = 1;
            LoadGraph(graphName);
        }
        
        public bool LoadGraph(String name)
        {
            bool retVal = true;


            try
            {
                String strConnect = $"Server={server};Database={database};Trusted_Connection=True;";
                var sqlCon = new SqlConnection(strConnect);
                sqlCon.Open();

                SqlCommand getEdgesForGraph = new SqlCommand("spGetEdges", sqlCon);

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@GraphName";
                sqlParameter.Value = name;
                getEdgesForGraph.Parameters.Add(sqlParameter);

                getEdgesForGraph.CommandType = CommandType.StoredProcedure;
                getEdgesForGraph.ExecuteNonQuery();
                SqlDataAdapter da2 = new SqlDataAdapter(getEdgesForGraph);
                DataSet dataSet = new DataSet();
                da2.Fill(dataSet, "Edges");
                // edge table: initialNode, terminalNode, weight (should be 1)

                var nrEdges = dataSet.Tables["Edges"].Rows.Count;
                for (int row = 0; row < nrEdges; ++row)
                {
                    // check initial node
                    String initialNode = (String)dataSet.Tables["Edges"].Rows[row].ItemArray[0];
                    String terminalNode = (String)dataSet.Tables["Edges"].Rows[row].ItemArray[1];

                    int initialIndex = Vertices.FindIndex(item => initialNode.Equals(item.Name));
                    int terminalIndex = Vertices.FindIndex(item => terminalNode.Equals(item.Name));

                    Vertex initial = initialIndex < 0 ? new Vertex(initialNode)
                                                    : Vertices[initialIndex];
                    Vertex terminal = terminalIndex < 0 ? new Vertex(terminalNode)
                                                    : Vertices[terminalIndex];

                    if (initialIndex < 0 && terminalIndex < 0)
                    {
                        // neither exist - create both, add edge between them with weight = 1
                        Vertices.Add(initial);
                        Vertices.Add(terminal);
                    }
                    else if (initialIndex < 0 && terminalIndex > -1)
                    {
                        // initial doesn't exist, create and add edge between it and terminal with weight = 1
                        Vertices.Add(initial);
                    }
                    else if (initialIndex > -1 && terminalIndex < 0)
                    {
                        // terminal doesn't exist, create and add edge between initial and it with weight = 1
                        Vertices.Add(terminal);
                    }
                    // if they both already exist, no need to add anything
                    initial.AddEdge(terminal, 1);
                    terminal.AddEdge(initial, 1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException());
                Console.WriteLine(e.StackTrace);
                retVal = false;
            }

            return retVal;
        }

        
        internal double GetEdgeWeight(Vertex initial, Vertex terminal)
        {
            int vertexIndex = Vertices.IndexOf(initial);
            int neighborIndex = Vertices[vertexIndex].Neighbors.IndexOf(terminal);
            double weight = Vertices[vertexIndex].Weights[neighborIndex];
            return weight;
        }
        
        //TODO: add partial classes with all the algorithms 


        public Vertex[,] KruskalAlgorithm()
        {
            throw new NotImplementedException();
        }

        public void DoTopological()
        {
            throw new NotImplementedException();
        }

        public Vertex[,] PrimAlgorithm(Vertex selectedVertexA)
        {
            throw new NotImplementedException();
        }

        public void DijkstraAlgorithm()
        {
            throw new NotImplementedException();
        }
    }
}