using System;
using System.Data.SqlClient;

namespace myAmazon_v1.Model
{
	public class ProductRequest
	{
		public int id { get; set; }
		public string username { get; set; }
		public string name { get; set; }
		public string desc { get; set; }

		public void fillWithSqlReader(SqlDataReader reader)
		{
			id = (int)reader["id"];
			name = reader["Name"].ToString();
			desc = reader["Desc"].ToString();
			username = reader["CustomerId"].ToString();
		}
	}
}