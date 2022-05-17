using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace GraphsClassProject
{
    class Digraph
    {
        public List<Node> Nodes { get; set;}

            public Digraph()
        {
            Nodes = new List<Node>();
        }

        public Digraph(List<Node> nodes)
        {
            Nodes = nodes;
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }

        public bool LoadVerticesFromSQL(String server, String database)
        {
            SqlConnection sqlCon = null;
            bool retVal = true;
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
                
                SqlCommand sqlCommand1 = new SqlCommand("spName1", sqlCon);
                sqlCommand1.CommandType = CommandType.StoredProcedure;
                sqlCommand1.ExecuteNonQuery();

                SqlCommand sqlCommand2 = new SqlCommand("spName2", sqlCon);
                sqlCommand2.CommandType = CommandType.StoredProcedure;
                sqlCommand2.ExecuteNonQuery();

                SqlCommand sqlCommand3 = new SqlCommand("spName3", sqlCon);
                sqlCommand3.CommandType = CommandType.StoredProcedure;
                sqlCommand3.ExecuteNonQuery();

                SqlCommand sqlCommand4 = new SqlCommand("spName4", sqlCon);
                sqlCommand4.CommandType = CommandType.StoredProcedure;
                sqlCommand4.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand1);

            }
            catch (Exception ex)

            {
                retVal = false;
                MessageBox.Show(" " + DateTime.Now.ToLongTimeString() + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally

            {

                if (sqlCon != null && sqlCon.State == ConnectionState.Open)

                    sqlCon.Close();

            }

            return retVal;


        }

        public bool LoadVertices(String fileName)
        {

            bool retVal = true;

            try
            {
                using (TextReader reader = new StreamReader(fileName))
                {
                    // assume 1 line per vertex, first element is name of vertex, rest is name of neighbors (unidirectional)
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Trim().Length == 0) continue;

                        string[] vertices = line.Split();
                        string vertexName = vertices[0];
                        Node newNode = new Node(vertexName);

                        for (int edges = 1; edges < vertices.Length; edges++)
                        {
                            Node neighbor = new Node(vertices[edges]);
                            newNode.addEdge(neighbor);
                        }

                        Nodes.Add(newNode);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                retVal = false;
            }


            return retVal;
        }

     
    }

}