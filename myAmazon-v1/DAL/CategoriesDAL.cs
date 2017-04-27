using System;
using System.Data;
using System.Data.SqlClient;
using myAmazon_v1.Model;
using System.Web;

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

        public Category getCategoryDetails(ref bool flag, ref string log, string where, string whereCondition)
        {
            flag = true;
            Category cat = new Category();

            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
            string cmd = @"SELECT id, Name, [Desc], [Image] FROM CategoryDetails WHERE " + where + " = " + whereCondition;
            SqlCommand sqlCmd = new SqlCommand(cmd, conn);
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                reader = sqlCmd.ExecuteReader();
                reader.Read();
                cat.fillWithSqlReader(reader);
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

            return cat;
        }

        public bool updateCategoryInfo(string name, ref int id, bool isEdit, ref string log)
        {
            bool flag = true;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
            string cmd;
            SqlCommand sqlCmd = null;

            if (isEdit)
                cmd = "UPDATE Category SET Name=@name WHERE id=@cid";
            else
                cmd = "INSERT INTO Category(Name) OUTPUT inserted.id VALUES(@name)";

            sqlCmd = new SqlCommand(cmd, conn);
            sqlCmd.Parameters.AddWithValue("@name", name);
            
            try
            {
                conn.Open();
                if (isEdit)
                {
                    sqlCmd.Parameters.AddWithValue("@cid", id);
                    sqlCmd.ExecuteNonQuery();
                    id = Convert.ToInt32(id);
                    log += "Successfully Updated";
                }
                else
                {
                    id = (int)sqlCmd.ExecuteScalar();
                    log += "Successfully Inserted";
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

        public bool updateCategoryImageAndDesc(int id, string image, string desc, bool isEdit, ref string log) {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);

            bool flag = true;

			string str = "SELECT COUNT(*) FROM CategoryInfo WHERE [CategoryId] = " + id.ToString();
			try
			{
				conn.Open();
				SqlCommand sqlcmd = new SqlCommand(str, conn);
				if ((int)sqlcmd.ExecuteScalar() < 1)
				{
					str = "INSERT INTO CategoryInfo([CategoryId]) VALUES(" + id.ToString() + ")";
					SqlCommand sqlCmd = new SqlCommand(str, conn);
					sqlCmd.ExecuteNonQuery();
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

			if (image != null && !isEdit)
            {
                try
                {
                    conn.Open();
                    SqlCommand query = new SqlCommand("UPDATE CategoryInfo SET [Image] ='" + image + "' WHERE CategoryId=@cid", conn);
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
                    SqlCommand query = new SqlCommand("UPDATE CategoryInfo SET [Desc] ='" + desc + "' WHERE CategoryId=@cid", conn);
                    query.Parameters.AddWithValue("@cid", id);
                    query.ExecuteNonQuery();
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

            return flag;
        }

		public bool deleteCategory(string id, ref string log)
		{
			bool done = true;
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "DELETE FROM Category WHERE id=@catId";
			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			sqlCmd.Parameters.AddWithValue("catId", id);

			try
			{
				conn.Open();
				sqlCmd.ExecuteNonQuery();
				conn.Close();
			}
			catch (Exception ex)
			{
				done = false;
				log += ex.ToString();
				conn.Close();
			}
			return done;
		}

		public DataTable getCategoriesList(ref string log, int flag, string where) {
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "";
			string defaultImage = "~/BrandsData/Images/Default.jpg";
			if (flag == 0)
				cmd = "SELECT * FROM CategoryDetails";
			else if (flag == 1)
				cmd = "SELECT [id], [Name], [Desc], ISNULL([Image], '" + defaultImage + "') AS [Image]  FROM CategoryDetails";
			else if (flag == 2)
				cmd = "SELECT [id], [Name], ISNULL([Desc], '[No Description]') AS [Desc], ISNULL([Image], '[No Image]') AS [Image] FROM CategoryDetails";

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
	}
}