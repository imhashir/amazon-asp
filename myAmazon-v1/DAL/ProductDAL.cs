﻿using System;
using System.Data;
using System.Data.SqlClient;
using myAmazon_v1.Model;
using System.IO;
using System.Collections.Generic;

namespace myAmazon_v1.DAL
{
	public class ProductDAL
	{
		public Product getProductDetails(ref bool flag, ref string log, string whereCondition)
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
				product.fillWithSqlReader(reader);
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
					SqlCommand sqlCmd2 = new SqlCommand(str, conn);
					sqlCmd2.ExecuteNonQuery();
				}
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
			if (image != null)
			{
				try
				{
					conn.Open();
					SqlCommand query = new SqlCommand("UPDATE ProductInfo SET [Image] ='" + image + "' WHERE [ProductId]=@cid", conn);
					query.Parameters.AddWithValue("@cid", id.ToString());
					query.ExecuteNonQuery();
					conn.Close();
				}
				catch (Exception ex)
				{
					log += ex.ToString();
					flag = false;
				}
			}

			try
			{
				if (!isEdit || desc != null)
				{
					conn.Open();
					string query = "UPDATE ProductInfo SET [Desc] = '" + desc + "' WHERE [ProductId]=@cid";
					SqlCommand sqlCmd = new SqlCommand(query, conn);
					sqlCmd.Parameters.AddWithValue("@cid", id);
					sqlCmd.ExecuteNonQuery();
					conn.Close();
				}
			}
			catch (Exception ex)
			{
				log += ex.ToString();
				flag = false;
			}
			return flag;
		}

		public bool deleteProduct(string id, ref string log) {
			bool done = true;
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand sqlCmd = new SqlCommand("DeleteProduct", conn);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@productId", id);

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
				cmd = "SELECT [id], [Name], ISNULL([Desc], '[No Description]') AS [Desc], ISNULL([Image], '[No Image]') AS [Image], [Price], [CatId], [Category], [BrandId], [Brand], [Quantity] FROM ProductDetails";

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

		public DataTable getProductsByUser(ref string log, string username)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT * FROM GetProductsByUser('" + username + "')";
			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			DataTable ds = new DataTable();
			using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd))
			{
				try
				{
					conn.Open();
					adapter.Fill(ds);
				}
				catch (Exception ex)
				{
					log += ex.ToString();
				}
				finally
				{
					conn.Close();
				}
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

		public List<FeaturedProduct> getFeaturedProducts(int level, ref string log)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT [ProductId], CoverImage FROM FeaturedDetails WHERE Level = " + level.ToString();    //FeaturedDetails is a VIEW

			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			DataTable ds = new DataTable();
			SqlDataReader reader = null;
			List<FeaturedProduct> products = new List<FeaturedProduct>();

			try
			{
				conn.Open();
				reader = sqlCmd.ExecuteReader();
				while (reader.Read()) {
					products.Add(new FeaturedProduct((int) reader["ProductId"], reader["CoverImage"].ToString()));
				}
			}
			catch (Exception ex)
			{
				log += ex.ToString();
			}
			finally {
				reader.Close();
				conn.Close();
			}

			return products;
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

		public bool addReviewToProduct(string customerId, string productId, Nullable<int> rate, string comment, ref string log)
		{
			bool done = true;

			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
							.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string[] cmd = {"AddRating", "AddComment"};
			string[] inputs = {rate.ToString(), comment};
			for (int i = 0; i < 2; i++)
			{
				if ((rate != 0 && i == 0) || (comment != null && i == 1))
				{
					SqlCommand sqlcmd = new SqlCommand(cmd[i], conn);
					sqlcmd.CommandType = CommandType.StoredProcedure;
					sqlcmd.Parameters.AddWithValue("@Cid", customerId);
					sqlcmd.Parameters.AddWithValue("@Pid", productId);
					sqlcmd.Parameters.AddWithValue("@input", inputs[i]);

					SqlParameter param = sqlcmd.Parameters.Add("@flag", SqlDbType.Int);
					param.Direction = ParameterDirection.Output;
					int flag = 0;

					try
					{
						conn.Open();
						sqlcmd.ExecuteNonQuery();
						flag = (int) sqlcmd.Parameters["@flag"].Value;
						
						if (flag == 0) 
							throw new Exception("User must buy the product before giving review.");
					}
					catch (Exception ex)
					{
						log += ex.ToString();
						done = false;
						break;
					}
					finally
					{
						conn.Close();
					}
				}
			}
			return done;
		}

		public int buyProduct(string username, string productId, int quantity, ref int orderId, ref string log)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
							.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand sqlCmd = new SqlCommand("BuyProduct", conn);
			sqlCmd.CommandType = CommandType.StoredProcedure;

			sqlCmd.Parameters.AddWithValue("@username", username);
			sqlCmd.Parameters.AddWithValue("@productId", productId);
			sqlCmd.Parameters.AddWithValue("@quantity", quantity);

			SqlParameter flagParam = sqlCmd.Parameters.Add("@flag", SqlDbType.Int);
			flagParam.Direction = ParameterDirection.Output;
			SqlParameter flagOrderId = sqlCmd.Parameters.Add("@orderId", SqlDbType.Int);
			flagOrderId.Direction = ParameterDirection.Output;

			int flag = 0;

			try
			{
				conn.Open();
				sqlCmd.ExecuteNonQuery();
				flag = (int) sqlCmd.Parameters["@flag"].Value;
				orderId = (int) sqlCmd.Parameters["@orderId"].Value;
			}
			catch (Exception ex)
			{
				log += ex.ToString();
			} 
			finally
			{
				conn.Close();
			}
			return flag;
		}

		public bool addToWishlist(string username, string productId, ref string log)
		{
			bool done = true;
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
							.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand sqlCmd = new SqlCommand("AddToWishlist", conn);
			sqlCmd.CommandType = CommandType.StoredProcedure;

			sqlCmd.Parameters.AddWithValue("@customerId", username);
			sqlCmd.Parameters.AddWithValue("@productId", productId);

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
		
		public bool updateStock(string productId, int quantity, ref string log)
		{
			bool done = true;

			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
								.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand sqlCmd = new SqlCommand("UpdateStock", conn);
			sqlCmd.CommandType = CommandType.StoredProcedure;

			sqlCmd.Parameters.AddWithValue("@Pid", productId);
			sqlCmd.Parameters.AddWithValue("@amount", quantity.ToString());

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

		public bool addUserProductInfo(string productId, string customerId, ref string log)
		{
			bool done = true;

			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
								.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "INSERT INTO [UserProducts] VALUES(@Pid, @Cid)";
			SqlCommand sqlCmd = new SqlCommand(cmd, conn);

			sqlCmd.Parameters.AddWithValue("@Pid", productId);
			sqlCmd.Parameters.AddWithValue("@Cid", customerId);

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

		public bool requestProduct(string customerId, string desc, ref string log)
		{
			bool done = true;
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
								.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand sqlCmd = new SqlCommand("HandleProductRequest", conn);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@username", customerId);
			sqlCmd.Parameters.AddWithValue("@request", desc);

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

		public bool deleteProductRequest(string reqId, ref string log)
		{
			bool done = true;
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
								.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			SqlCommand sqlCmd = new SqlCommand("DeleteProductRequest", conn);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@reqId", reqId);

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

		public DataTable getProductRequestList(ref string log, int flag, string where)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT * FROM ProductRequestDetails";
			
			if (where != null)
				cmd += " " + where;

			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			DataTable ds = new DataTable();

			try
			{
				conn.Open();
				using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd))
				{
					adapter.Fill(ds);
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
			return ds;
		}

		public ProductRequest getProductRequestDetails(ref bool flag, ref string log, string whereCondition)
		{
			ProductRequest request = new ProductRequest();
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT * FROM ProductRequestDetails WHERE id=" + whereCondition;
			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			SqlDataReader reader = null;
			try
			{
				conn.Open();
				reader = sqlCmd.ExecuteReader();
				reader.Read();
				request.fillWithSqlReader(reader);
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
			return request;
		}

		public DataTable getProductCommentsList(string productId, ref string log)
		{
			DataTable table = new DataTable();
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT * FROM GetCommentsOnProduct(@pId)";
			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			sqlCmd.Parameters.AddWithValue("@pId", productId);
			using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd))
			{
				try
				{
					conn.Open();
					adapter.Fill(table);
					conn.Close();
				}
				catch (Exception ex)
				{
					log += ex.ToString();
					conn.Close();
				}
			}
			return table;
		}

		public double getProductRating(string productId, ref string log)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT dbo.GetProductRating(@pId)";
			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			sqlCmd.Parameters.AddWithValue("@pId", productId);
			double rating = 0;
			string temp;
			try
			{
				conn.Open();
				temp = sqlCmd.ExecuteScalar().ToString();
				if (temp != "")
					rating = Convert.ToDouble(temp);
			}
			catch (Exception ex)
			{
				log += ex.ToString();
			}
			finally
			{
				conn.Close();
			}
			return rating;
		}
	}
}