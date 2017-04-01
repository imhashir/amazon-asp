using System;
using System.Data;
using System.Data.SqlClient;

namespace myAmazon_v1.DAL
{
    public class CategoriesDAL
    {
        public DataTable getCategories(ref string log, string where, string whereCondition)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
            string cmd = "SELECT id, Name FROM Category";
            if (where != null)
                cmd += " WHERE " + where + " = " + whereCondition;
            SqlCommand sqlCmd = new SqlCommand(cmd, conn);
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
            try
            {
                conn.Open();
                sqlCmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                log += "Error opting Brand Names: " + ex.ToString();
                conn.Close();
            }
            adapter.Fill(table);
            return table;
        }
    }
}