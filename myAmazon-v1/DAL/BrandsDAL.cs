using System;
using System.Data.SqlClient;
using myAmazon_v1.Model;
using System.Data;
using System.Web;

namespace myAmazon_v1.DAL
{
    public class BrandsDAL
    {
        public DataTable getBrands(ref string log, string where, string whereCondition)
        {
            DataTable table = new DataTable();
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "SELECT id, Name FROM Brand";
            if (where != null)
                cmd += " WHERE " + where + " = " + whereCondition;
            SqlCommand sqlCmd = new SqlCommand(cmd, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
            try
            {
                conn.Open();
                sqlCmd.ExecuteNonQuery();
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                log += "Error opting Brand Names: " + ex.ToString();
            }
            finally
            {
                conn.Close();
            }

            return table;
        }

        public Brand getBrandDetails(ref bool flag, ref string log, string where, string whereCondition) {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
            string cmd = "SELECT id AS id, Name AS name, [Desc] AS [desc], [Image], CatId AS catId FROM BrandDetails";
            if (where != null)
                cmd += " WHERE " + where + " = " + whereCondition;
            SqlCommand sqlCmd = new SqlCommand(cmd, conn);
            SqlDataReader reader = null;
            Brand brand = new Brand();
            try
            {
                conn.Open();
                reader = sqlCmd.ExecuteReader();
                reader.Read();
                brand.fillWithSqlReader(reader);
                flag = true;
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
            return brand;
        }

		public DataTable getBrandsList(ref string log, int flag, string where) {
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string defaultImage = "~/BrandsData/Images/Default.jpg";
			string cmd = "";
			if (flag == 0)
				cmd = "SELECT * FROM BrandDetails";
			else if (flag == 1)
				cmd = "SELECT [id], [Name], [Desc], ISNULL([Image], '" + defaultImage + "') AS [Image], Category, CatId FROM BrandDetails";
			else if (flag == 2)
				cmd = "SELECT [id], [Name], ISNULL([Desc], '[No Description]') AS [Desc], ISNULL([Image], '[No Image]') AS [Image], Category, CatId FROM BrandDetails";

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

        public bool updateBrandInfo(string name, ref int id, string catId, bool isEdit, ref string log) {
            bool flag = true;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);

			string cmd = "";
            
            if (isEdit)
                cmd = "UPDATE Brand SET Name=@name, [CategoryId]=@category WHERE id=" + id.ToString();
            else
                cmd = "INSERT INTO Brand(Name, [CategoryId]) OUTPUT inserted.id VALUES(@name, @category)";

            SqlCommand sqlCmd = new SqlCommand(cmd, conn);
            sqlCmd.Parameters.AddWithValue("@name", name);
            sqlCmd.Parameters.AddWithValue("@category", catId);
            try
            {
                conn.Open();
                if (isEdit)
                {
                    sqlCmd.ExecuteNonQuery();
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

        public bool updateImageAndDesc(int id, string image, string desc, bool isEdit, ref string log) {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);

            bool flag = true;

			string str = "SELECT COUNT(*) FROM BrandInfo WHERE BrandId = " + id.ToString();
			try
			{
				conn.Open();
				SqlCommand sqlcmd = new SqlCommand(str, conn);
				if ((int)sqlcmd.ExecuteScalar() < 1)
				{
					str = "INSERT INTO BrandInfo([BrandId]) VALUES(" + id.ToString() + ")";
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
                    SqlCommand query = new SqlCommand("UPDATE BrandInfo SET [Image] ='" + image + "' WHERE BrandId=@cid", conn);
                    query.Parameters.AddWithValue("@cid", id);
                    query.ExecuteNonQuery();
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
            }

            try
            {
                if (desc != null && !isEdit)
                {
                    conn.Open();
                    SqlCommand query = new SqlCommand("UPDATE BrandInfo SET [Desc] ='" + desc + "' WHERE BrandId=@cid", conn);
                    query.Parameters.AddWithValue("@cid", id);
                    query.ExecuteNonQuery();
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

		public bool deleteBrand(string id, ref string log) {
			bool done = true;
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
						.ConnectionStrings["myAmazonConnectionString"].ConnectionString);
			string cmd = "DELETE FROM Brand WHERE id=@brandId";
			SqlCommand sqlCmd = new SqlCommand(cmd, conn);
			sqlCmd.Parameters.AddWithValue("brandId", id);
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
    }
}