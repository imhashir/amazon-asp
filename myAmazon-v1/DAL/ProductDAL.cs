using System;
using System.Data;
using System.Data.SqlClient;
using myAmazon_v1.Model;
using System.IO;
using System.Collections.Generic;

namespace myAmazon_v1.DAL
{
	public class ProductDAL
	{
		public Product getProductDetails(ref bool flag, ref string log, string where, string whereCondition)
		{
			Product product = new Product();
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT * FROM ProductDetails WHERE id=" + whereCondition;
			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			SqlDataReader reader = null;
			try
			{
				conn.Open();
				reader = sqlCmd.ExecuteReader();
				reader.Read();
				product.name = reader["Name"].ToString();
				product.price = (int)reader["Price"];
				product.catId = (int)reader["CatId"];
				product.brandId = (int)reader["BrandId"];
				if (reader["Image"].ToString() != "")
					product.image = reader["Image"].ToString();

				product.desc = reader["Desc"].ToString();
			}
			catch (Exception ex)
			{
				log += ex.ToString();
				flag = false;
			}
			finally
			{
				conn.Close();
			}
			return product;
		}

		public bool updateProductInfo(ref int id, string name, string brandId, string catId, string price, bool isEdit, ref string log)
		{
			bool flag = true;
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand insert = null;
			
			insert = new SqlCommand("UpdateProduct", conn);
			insert.CommandType = CommandType.StoredProcedure;

			insert.Parameters.AddWithValue("@id", id.ToString() ?? (object)DBNull.Value);
			insert.Parameters.AddWithValue("@name", name);
			insert.Parameters.AddWithValue("@brandId", brandId);
			insert.Parameters.AddWithValue("@categoryId", catId);
			insert.Parameters.AddWithValue("@price", price);
			insert.Parameters.AddWithValue("@updateType", isEdit);
			SqlParameter outputId = insert.Parameters.Add("@productId", SqlDbType.Int);
			outputId.Direction = ParameterDirection.Output;
			SqlParameter outputFlag = insert.Parameters.Add("@flag", SqlDbType.Int);
			outputFlag.Direction = ParameterDirection.Output;
			int flagOut = 0;
			try
			{
				conn.Open();
				insert.ExecuteNonQuery();
				if (!isEdit)
					id = (int) insert.Parameters["@productId"].Value;
				flagOut = (int) insert.Parameters["@flag"].Value;

				if (flagOut == 0)
				{
					if (!isEdit)
					{
						log += "Successfully Inserted";
					}
					else
						log += "Successfully Updated";
				}
				else
				{
					log += "Error in insertion. Price must be greater than 0.";
					flag = false;
				}
			}
			catch (Exception ex)
			{
				log += "Error! Unable to Insert: \n" + ex.ToString();
				flag = false;
			}
			finally
			{
				conn.Close();
			}
			return flag;
		}

		public bool updateImageAndDesc(int id, string image, string desc, bool isEdit, ref string log)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			bool flag = true;
			string str = "SELECT COUNT(*) FROM ProductInfo WHERE ProductId = " + id.ToString();
			try
			{
				conn.Open();
				SqlCommand sqlcmd = new SqlCommand(str, conn);
				if ((int) sqlcmd.ExecuteScalar() < 1) {
					str = "INSERT INTO ProductInfo([ProductId]) VALUES(" + id.ToString() + ")";
					SqlCommand sqlCmd = new SqlCommand(str, conn);
					sqlCmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				log += ex.ToString();
			}
			finally
			{
				conn.Close();
			}
			if (image != null)
			{
				try
				{
					conn.Open();
					SqlCommand query = new SqlCommand("UPDATE ProductInfo SET [Image] ='" + "~/ProductsData/Images/" + id.ToString() + ".jpg' WHERE [ProductId]=@cid", conn);
					query.Parameters.AddWithValue("@cid", id.ToString());
					query.ExecuteNonQuery();
					conn.Close();
				}
				catch (Exception ex)
				{
					log += ex.ToString();
				}
			}

			try
			{
				if (!isEdit || desc != null)
				{
					conn.Open();
					string query = "UPDATE ProductInfo SET [Desc] = '" + "~/ProductsData/" + id.ToString() + ".txt" + "' WHERE [ProductId]=@cid";
					SqlCommand sqlCmd = new SqlCommand(query, conn);
					sqlCmd.Parameters.AddWithValue("@cid", id);
					sqlCmd.ExecuteNonQuery();
					conn.Close();
				}
			}
			catch (Exception ex)
			{
				log += ex.ToString();
			}
			return flag;
		}

		public bool deleteProduct(string id, ref string log) {
			bool done = true;
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "DELETE FROM Product WHERE id=@productId";
			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			sqlCmd.Parameters.AddWithValue("productId", id);

			try
			{
				conn.Open();
				sqlCmd.ExecuteNonQuery();
				conn.Close();
			}
			catch (Exception ex)
			{
				log += ex.ToString();
				conn.Close();
				done = false;
			}
			return done;
		}

		public DataTable getProductList(ref string log, int flag, string where)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "";
			string defaultImage = "~/BrandsData/Images/Default.jpg";
			if (flag == 0)
				cmd = "SELECT * FROM ProductDetails";    //ProductDetails is a VIEW
			else if (flag == 1)
				cmd = "SELECT [id], [Name], [Desc], ISNULL([Image], '" + defaultImage + "') AS [Image], [Price], [CatId], [Category], [BrandId], [Brand] FROM ProductDetails";
			else if(flag == 2)
				cmd = "SELECT [id], [Name], ISNULL([Desc], '[No Description]') AS [Desc], ISNULL([Image], '[No Image]') AS [Image], [Price], [CatId], [Category], [BrandId], [Brand] FROM ProductDetails";

			if (where != null)
				cmd += " " + where;

			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			DataTable ds = new DataTable();

			try
			{
				conn.Open();
				SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
				adapter.Fill(ds);
				conn.Close();
			}
			catch (Exception ex)
			{
				log += ex.ToString();
				conn.Close();
			}
			return ds;
		}

		public bool addFeaturedProduct(string id, string level, string imagePath, ref string log) {
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand insert = null;

			insert = new SqlCommand("AddToFeatured", conn);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters.AddWithValue("@ProductId", id);
			insert.Parameters.AddWithValue("@level", level);
			insert.Parameters.AddWithValue("@image", imagePath);
			SqlParameter outputId = insert.Parameters.Add("@flag", SqlDbType.Int);
			outputId.Direction = ParameterDirection.Output;
			int flagOut = -1;
			try
			{
				conn.Open();
				insert.ExecuteNonQuery();
				flagOut = (int)insert.Parameters["@flag"].Value;

				if (flagOut == 0)
				{
					log += "Successfully Updated";
				}
				else
				{
					switch(flagOut)
					{
						case 1:
							log += "No Space for Platinum Sponsor";
							break;
						case 2:
							log += "No Space for Gold Sponsor";
							break;
						case 3:
							log += "No Space for Silver Sponsor";
							break;
					}
				}
			}
			catch (Exception ex)
			{
				log += "Error! Unable to Insert: \n" + ex.ToString();
			}
			finally
			{
				conn.Close();
			}
			return (flagOut == 0);
		}

		public DataTable getFeaturedList(ref string log)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT * FROM FeaturedDetails ORDER BY Level ASC";    //FeaturedDetails is a VIEW

			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			DataTable ds = new DataTable();

			try
			{
				conn.Open();
				SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
				adapter.Fill(ds);
				conn.Close();
			}
			catch (Exception ex)
			{
				log += ex.ToString();
				conn.Close();
			}
			return ds;
		}

		public List<string> getFeaturedImagePath(int level, ref string log)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT CoverImage FROM FeaturedDetails WHERE Level = " + level.ToString();    //FeaturedDetails is a VIEW

			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			DataTable ds = new DataTable();
			SqlDataReader reader;
			List<string> paths = new List<string>();

			try
			{
				conn.Open();
				reader = sqlCmd.ExecuteReader();
				while (reader.Read()) {
					paths.Add(reader["CoverImage"].ToString());
				}
			}
			catch (Exception ex)
			{
				log += ex.ToString();
			}
			finally {
				conn.Close();
			}

			return paths;
		}

		public bool deleteFromFeaturedFeatured(string id, ref string imagePath, ref string log)
		{
			bool done = true;
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);

			string cmd1 = "SELECT CoverImage FROM Featured WHERE id=@productId";
			SqlCommand sqlCmd1 = new SqlCommand(cmd1, conn);
			sqlCmd1.Parameters.AddWithValue("productId", id);

			string cmd2 = "DELETE FROM Featured WHERE id=@productId";
			SqlCommand sqlCmd2 = new SqlCommand(cmd2, conn);
			sqlCmd2.Parameters.AddWithValue("productId", id);

			try
			{
				conn.Open();
				imagePath = sqlCmd1.ExecuteScalar().ToString();
				sqlCmd2.ExecuteNonQuery();
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