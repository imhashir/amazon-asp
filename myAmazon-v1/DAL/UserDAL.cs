using System;
using System.Data.SqlClient;
using System.Data;
using myAmazon_v1.Model;

namespace myAmazon_v1.DAL
{
	public class UserDAL
	{
		public int signInUser(string username, string pwd, ref string log)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand sqlCmd = new SqlCommand("SignInUser", conn);

			sqlCmd.CommandType = CommandType.StoredProcedure;

			sqlCmd.Parameters.AddWithValue("@username", username);
			sqlCmd.Parameters.AddWithValue("@pass", pwd);

			SqlParameter outputFlag = sqlCmd.Parameters.Add("@flag", SqlDbType.Int);
			outputFlag.Direction = ParameterDirection.Output;
			int flag = 0;
			try
			{
				conn.Open();
				sqlCmd.ExecuteNonQuery();
				flag = (int)sqlCmd.Parameters["@flag"].Value;
				if (flag != 0)
					throw new Exception();
				//Session["SignedInUser"] = id_username.Text.ToString();
				//id_log_signin.Text = "SignIn Successful!\nSignIn Id: " + Session["SignedInUser"];
				//Response.Redirect(@"..\");
			}
			catch (Exception ex)
			{
				log += ex.ToString();
				if (flag != 0)
				{
					switch (flag)
					{
						case 1:
							log = "Invalid Username.";
							break;
						case 2:
							log = "Invalid Password";
							break;
					}
				}
				else
				{
					log = ex.ToString();
				}
			}
			finally
			{
				conn.Close();
			}
			return flag;
		}

		public int signUpUser(string fName, string lName, string email, string number, string img, string username, string pwd, ref string log)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand sqlCmd = new SqlCommand("SignUp", conn);

			sqlCmd.CommandType = CommandType.StoredProcedure;

			sqlCmd.Parameters.AddWithValue("@username", username);
			sqlCmd.Parameters.AddWithValue("@firstName", fName);
			sqlCmd.Parameters.AddWithValue("@lastName", lName);
			sqlCmd.Parameters.AddWithValue("@email", email);
			sqlCmd.Parameters.AddWithValue("@pass", pwd);
			sqlCmd.Parameters.AddWithValue("@number", number);
			sqlCmd.Parameters.AddWithValue("@img", img ?? (object)DBNull.Value);

			SqlParameter outputFlag = sqlCmd.Parameters.Add("@flag", SqlDbType.Int);
			outputFlag.Direction = ParameterDirection.Output;
			int flag = 0;
			try
			{
				conn.Open();
				sqlCmd.ExecuteNonQuery();
				flag = (int)sqlCmd.Parameters["@flag"].Value;
				if (flag != 0)
					throw new Exception();
				log += "SignUp Successful!";
			}
			catch (Exception ex)
			{
				if (flag != 0)
				{
					switch (flag)
					{
						case 1:
							log += "Username already exists";
							break;
						case 2:
							log += "Invalid Password";
							break;
						case 3:
							log += "Email already registered!";
							break;
					}
				}
				else
				{
					log += ex.ToString();
				}
			}
			finally
			{
				conn.Close();
			}
			return flag;
		}

		public Customer getUserDetails(string username, ref string log)
		{
			Customer customer = new Customer();
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT [Username], [Password], [FirstName], [LastName], [ContactNumber], [Email], [Image] FROM CustomerDetails WHERE [Username]='" + username + "'";
			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			SqlDataReader reader = null;
			try
			{
				conn.Open();
				reader = sqlCmd.ExecuteReader();
				reader.Read();
				customer.fillWithSqlReader(reader);
			}
			catch (Exception ex)
			{
				log += ex.ToString();
			}
			finally
			{
				conn.Close();
			}
			return customer;
		}

		public bool updateUserInfo(string username, string firstname, string lastname, string password, string number, ref string log)
		{
			bool done = true;
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand sqlCmd = new SqlCommand("UpdateUserInfo", conn);

			sqlCmd.CommandType = CommandType.StoredProcedure;

			sqlCmd.Parameters.AddWithValue("@fname", firstname);
			sqlCmd.Parameters.AddWithValue("@lname", lastname);
			sqlCmd.Parameters.AddWithValue("@pass", password);
			sqlCmd.Parameters.AddWithValue("@number", number);
			sqlCmd.Parameters.AddWithValue("@username", username);

			try
			{
				conn.Open();
				sqlCmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				log += ex.ToString();
				done = false;
			}
			finally
			{
				conn.Close();
			}

			return done;
		}

		public bool requestCredit(string username, string amount, ref string log) {
			bool done = true;
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand sqlCmd = new SqlCommand("RequestCredit", conn);

			sqlCmd.CommandType = CommandType.StoredProcedure;

			sqlCmd.Parameters.AddWithValue("@username", username);
			sqlCmd.Parameters.AddWithValue("@amount", amount);
			SqlParameter param = sqlCmd.Parameters.Add("@flag", SqlDbType.Int);
			param.Direction = ParameterDirection.Output;
			int flag = 0;
			try
			{
				conn.Open();
				sqlCmd.ExecuteNonQuery();
				flag = (int) sqlCmd.Parameters["@flag"].Value;
				if (flag == 2)
					throw new Exception("A Request is already pending.");
			}
			catch (Exception ex)
			{
				log += ex.ToString();
				done = false;
			}
			finally
			{
				conn.Close();
			}

			return done;
		}
	}
}