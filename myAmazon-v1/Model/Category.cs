using System.Data;
using System.Data.SqlClient;

namespace myAmazon_v1.Model
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string image { get; set; }

        public void fillWithSqlReader(SqlDataReader reader)
        {
            id = (int) reader["id"];
            name = reader["name"].ToString();
            desc = reader["desc"].ToString();
            image = reader["Image"].ToString();
        }
    }
}