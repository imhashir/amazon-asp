using System.Data.SqlClient;

namespace myAmazon_v1.Model
{
	public class Customer
	{
		public string username { get; set; }
		public string password { get; set; }
		public string firsName { get; set; }
		public string lastName { get; set; }
		public string number { get; set; }
		public string image { get; set; }
		public string email { get; set; }

		public void fillWithSqlReader(SqlDataReader reader)
		{
			username = reader["Username"].ToString();
			firsName = reader["FirstName"].ToString();
			lastName = reader["LastName"].ToString();
			if (reader["Image"].ToString() != "")
				image = reader["Image"].ToString();
			number = reader["ContactNumber"].ToString();
			email = reader["Email"].ToString();
			password = reader["Password"].ToString();
		}
	}
}