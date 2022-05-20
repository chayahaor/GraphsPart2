using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



namespace GraphsClassProject
{
    public class GetData
    {
        /*
        private Digraph digraph = new Digraph();
        private WeightedDigraph weightedDigraph = new WeightedDigraph();
        private Graph graph = new Graph();
        private WeightedGraph weightedGraph = new WeightedGraph();
        */

        public Dictionary<String, String> GraphTypes { get; set; } 

        public GetData(String server, String database)
        {
            LoadVerticesFromSQL(server, database);
        }

        private Dictionary<String, String> LoadVerticesFromSQL(String server, String database)
        {
            // table contains graphName, graphType
            GraphTypes = new Dictionary<string, string>();

            SqlConnection sqlCon = null;
            try
            {
                String strConnect = $"Server={server};Database={database};Trusted_Connection=True;";
                sqlCon = new SqlConnection(strConnect);
                sqlCon.Open();

                // get all of the graphs and filter them,
                // or reduce the list we have to unweighted digraphs
                // get all graphs from SQL, via sp, and then only turn unweighted digraphs into this class

                // stored procedure #1) to get all graph ids
                // stored procedure #2) get graph type from graph id
                // for all relevant graph ids, (unweighted digraph):
                // graph type should be a parameter for the following stored procedure:
                // stored procedure #3) get the edge information for each graph
                // stored procedure #4) get the nodes for each graph

                // get graph names
                SqlCommand getAllGraphs = new SqlCommand("spGetGraphNames", sqlCon);
                getAllGraphs.CommandType = CommandType.StoredProcedure;
                getAllGraphs.ExecuteNonQuery();
                SqlDataAdapter da1 = new SqlDataAdapter(getAllGraphs);
                DataSet dataset1 = new DataSet();
                da1.Fill(dataset1, "Graphs");

                var nrGraphs = dataset1.Tables["Graphs"].Rows.Count;
                for (int row = 0; row < nrGraphs; ++row)
                {
                    String name = (String)dataset1.Tables["Graphs"].Rows[row].ItemArray[0];
                    String type = (String)dataset1.Tables["Graphs"].Rows[row].ItemArray[1];
                    GraphTypes.Add(name, type);
                }

                foreach (KeyValuePair<String, String> entry in GraphTypes)
                {
                    SqlCommand getEdgesForGraph = new SqlCommand("spGetEdges", sqlCon);

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@GraphName";
                    sqlParameter.Value = entry.Key;
                    getEdgesForGraph.Parameters.Add(sqlParameter);

                    getEdgesForGraph.CommandType = CommandType.StoredProcedure;
                    getEdgesForGraph.ExecuteNonQuery();
                    SqlDataAdapter da2 = new SqlDataAdapter(getEdgesForGraph);
                    DataSet dataset2 = new DataSet();
                    da2.Fill(dataset2, "Edges");

                    // edge table: initialNode, terminalNode, weight

                    /*
                    switch (entry.Value)
                    {
                        case "WeightedDigraph":
                            weightedDigraph.LoadGraph(dataset2);
                            break;

                        case "UnweightedDigraph":
                            digraph.LoadGraph(dataset2);
                            break;

                        case "WeightedGraph":
                            weightedGraph.LoadGraph(dataset2);
                            break;

                        case "UnweightedGraph":
                            graph.LoadGraph(dataset2);
                            break;
                    }
                    */

                }
            }
            catch (Exception ex)

            {
                 MessageBox.Show(" " + DateTime.Now.ToLongTimeString() + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            finally

            {

                if (sqlCon != null && sqlCon.State == ConnectionState.Open)

                    sqlCon.Close();

            }

            return GraphTypes;
        }
    }

    /*
     * Return the dictionary so that the form will access all the graphs that we're dealing with
     * The GUI displays a list of all the graph names
     * Clicking on the graph name will load that graph
     * 
     * Graph1 Graph2 Graph3
     * 
     * Click on Graph1 (unweighted digraph):
     * the form calls new Digraph(), digraph.LoadData(Graph1);
     */
}
