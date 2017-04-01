using System;
using System.Data;
using System.Data.SqlClient;
using myAmazon_v1.Model;

namespace myAmazon_v1.DAL
{
	public class ProductDAL
	{
		public Product getProductDetails(ref bool flag, ref string log, string where, string whereCondition)
		{
			Product product = new Product();
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT p.Name, p.Price, p.[Desc], p.[Image], p.CategoryId, p.BrandId FROM Product AS p WHERE p.id=" + whereCondition;
			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			SqlDataReader reader = null;
			try
			{
				conn.Open();
				reader = sqlCmd.ExecuteReader();
				reader.Read();
				product.name = reader["Name"].ToString();
				product.price = (int)reader["Price"];
				product.catId = (int)reader["CategoryId"];
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
			if (image != null)
			{
				try
				{
					conn.Open();
					SqlCommand query = new SqlCommand("UPDATE Product SET [Image] ='" + "~/ProductsData/Images/" + id.ToString() + ".jpg' WHERE id=@cid", conn);
					query.Parameters.AddWithValue("@cid", id);
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
					SqlCommand query = new SqlCommand("UPDATE Product SET [Desc] ='" + "~/ProductsData/" + id.ToString() + ".txt" + "' WHERE id=@cid", conn);
					query.Parameters.AddWithValue("@cid", id);
					query.ExecuteNonQuery();
					conn.Close();
				}
			}
			catch (Exception ex)
			{
				log += ex.ToString();
			}
			return flag;
		}
	}
}