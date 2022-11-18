using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace GraphsClassProject
{
    public class GetData
    {
        public ArrayList AssociatedInfo { get; private set; }

        public GetData(String server, String database)
        {
            LoadVerticesFromSql(server, database);
        }

        private void LoadVerticesFromSql(String server, String database)
        {
            AssociatedInfo = new ArrayList();
            SqlConnection SqlCon = null;
            try
            {
                String StrConnect = $"Server={server};Database={database};Trusted_Connection=True;";
                SqlCon = new SqlConnection(StrConnect);
                SqlCon.Open();

                // get graph names
                SqlCommand GetAllGraphs = new SqlCommand("spGetGraphNames", SqlCon);
                GetAllGraphs.CommandType = CommandType.StoredProcedure;
                GetAllGraphs.ExecuteNonQuery();
                SqlDataAdapter Da1 = new SqlDataAdapter(GetAllGraphs);
                DataSet Dataset1 = new DataSet();
                Da1.Fill(Dataset1, "Graphs");

                var NrGraphs = Dataset1.Tables["Graphs"].Rows.Count;
                for (int Row = 0; Row < NrGraphs; ++Row)
                {
                    String Name = (String)Dataset1.Tables["Graphs"].Rows[Row].ItemArray[0];
                    bool Weight = (bool)Dataset1.Tables["Graphs"].Rows[Row].ItemArray[1];
                    bool Direct = (bool)Dataset1.Tables["Graphs"].Rows[Row].ItemArray[2];
                    AssociatedInfo.Add(new GraphInfo(Name, Weight, Direct));
                    
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(" " + DateTime.Now.ToLongTimeString() + Ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            finally
            {
                if (SqlCon != null && SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }
            }
        }
        
    }
}