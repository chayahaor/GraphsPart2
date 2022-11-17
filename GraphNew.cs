using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GraphsClassProject
{
    public class GraphNew
    {
        internal List<Vertex> Vertices { get; set; }
        public String GraphName { get; set; }
        
        private String Server;
        private String Database;
        public GraphNew(String graphName, String server, String database)
        {
            this.GraphName = graphName;
            this.Server = server;
            this.Database = database;
            Vertices = new List<Vertex>();
            LoadGraph(graphName);
        }

        private bool LoadGraph(String name)
        {
            bool RetVal = true;
            
            try
            {
                String StrConnect = $"Server={Server};Database={Database};Trusted_Connection=True;";
                var SqlCon = new SqlConnection(StrConnect);
                SqlCon.Open();

                SqlCommand GetEdgesForGraph = new SqlCommand("spGetEdges", SqlCon);

                SqlParameter SqlParameter = new SqlParameter();
                SqlParameter.ParameterName = "@GraphName";
                SqlParameter.Value = name;
                GetEdgesForGraph.Parameters.Add(SqlParameter);

                GetEdgesForGraph.CommandType = CommandType.StoredProcedure;
                GetEdgesForGraph.ExecuteNonQuery();
                SqlDataAdapter Da2 = new SqlDataAdapter(GetEdgesForGraph);
                DataSet DataSet = new DataSet();
                Da2.Fill(DataSet, "Edges");
                // edge table: initialNode, terminalNode, weight (should be 1)

                var NrEdges = DataSet.Tables["Edges"].Rows.Count;
                for (int Row = 0; Row < NrEdges; ++Row)
                {
                    // check initial node
                    String InitialNode = (String)DataSet.Tables["Edges"].Rows[Row].ItemArray[0];
                    String TerminalNode = (String)DataSet.Tables["Edges"].Rows[Row].ItemArray[1];
                    double initXCoord = .5; //TODO: get from SP
                    double initYCoord = .5; //TODO: get from SP
                    double termXCoord = .5; //TODO: get from SP
                    double termYCoord = .5; //TODO: get from SP
                    int InitialIndex = Vertices.FindIndex(item => InitialNode.Equals(item.Name));
                    int TerminalIndex = Vertices.FindIndex(item => TerminalNode.Equals(item.Name));

                    Vertex Initial = InitialIndex < 0 ? new Vertex(InitialNode, initXCoord, initYCoord)
                                                    : Vertices[InitialIndex];
                    Vertex Terminal = TerminalIndex < 0 ? new Vertex(TerminalNode, termXCoord, termYCoord)
                                                    : Vertices[TerminalIndex];

                    if (InitialIndex < 0 && TerminalIndex < 0)
                    {
                        // neither exist - create both, add edge between them with weight = 1
                        Vertices.Add(Initial);
                        Vertices.Add(Terminal);
                    }
                    else if (InitialIndex < 0 && TerminalIndex > -1)
                    {
                        // initial doesn't exist, create and add edge between it and terminal with weight = 1
                        Vertices.Add(Initial);
                    }
                    else if (InitialIndex > -1 && TerminalIndex < 0)
                    {
                        // terminal doesn't exist, create and add edge between initial and it with weight = 1
                        Vertices.Add(Terminal);
                    }
                    // if they both already exist, no need to add anything
                    Initial.AddEdge(Terminal, 1);
                    Terminal.AddEdge(Initial, 1);
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.GetBaseException());
                Console.WriteLine(E.StackTrace);
                RetVal = false;
            }

            return RetVal;
        }

        
        internal double GetEdgeWeight(Vertex initial, Vertex terminal)
        {
            int VertexIndex = Vertices.IndexOf(initial);
            int NeighborIndex = Vertices[VertexIndex].Neighbors.IndexOf(terminal);
            double Weight = Vertices[VertexIndex].Weights[NeighborIndex];
            return Weight;
        }
        
        //TODO: add partial classes with all the algorithms 
        //TODO: Add implementation
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