using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GraphsClassProject
{
    public class GetData
    {
        public ArrayList AssociatedInfo { get; set; }

        public GetData(String server, String database)
        {
            LoadVerticesFromSql(server, database);
        }

        private void LoadVerticesFromSql(String server, String database)
        {
            AssociatedInfo = new ArrayList();
            SqlConnection sqlCon = null;
            try
            {
                String strConnect = $"Server={server};Database={database};Trusted_Connection=True;";
                sqlCon = new SqlConnection(strConnect);
                sqlCon.Open();

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
                    bool weight = (bool)dataset1.Tables["Graphs"].Rows[row].ItemArray[1];
                    bool direct = (bool)dataset1.Tables["Graphs"].Rows[row].ItemArray[2];
                    AssociatedInfo.Add(new GraphInfo(name, weight, direct));
                    
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
                {
                    sqlCon.Close();
                }
            }
        }
        
    }
}