using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GraphsClassProject
{
    class WeightedDigraph : ParentGraph
    {
        public WeightedDigraph(String graphName) : base(graphName)
        {
            GraphName = graphName;
            Vertices = new List<Vertex>();
        }

        public bool LoadGraph(String name, String server, String database)
        {
            bool retVal = true;

            SqlConnection sqlCon;

            try
            {
                String strConnect = $"Server={server};Database={database};Trusted_Connection=True;";
                sqlCon = new SqlConnection(strConnect);
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
                    int weight = (int)dataSet.Tables["Edges"].Rows[row].ItemArray[2];

                    if (weight > maxWeight)
                    {
                        maxWeight = weight;
                    }

                    int initialIndex = Vertices.FindIndex(item => initialNode.Equals(item.Name));
                    int terminalIndex = Vertices.FindIndex(item => terminalNode.Equals(item.Name));

                    Vertex initial = initialIndex < 0
                        ? new Vertex(initialNode)
                        : Vertices[initialIndex];
                    Vertex terminal = terminalIndex < 0
                        ? new Vertex(terminalNode)
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
                    initial.AddEdge(terminal, weight);
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
    }
}