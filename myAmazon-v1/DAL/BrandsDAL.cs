﻿using System;
using System.Data.SqlClient;
using myAmazon_v1.Model;

namespace myAmazon_v1.DAL
{
    public class BrandsDAL
    {
        public Brand getBrandDetails(ref bool flag, ref string log, string where, string whereCondition) {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
            string cmd = "SELECT id AS id, Name AS name, [Desc] AS [desc], [Image], CategoryId AS catId FROM Brand";
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
                conn.Close();
                flag = true;
            }
            catch (Exception ex)
            {
                log += ex.ToString();
                conn.Close();
                flag = false;
            }
            return brand;
        }

        public bool updateBrandInfo(string name, ref int id, string catId, bool isEdit, ref string log) {
            bool flag = true;
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
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
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");

            bool flag = true;
            if (image != null)
            {
                try
                {
                    conn.Open();
                    SqlCommand query = new SqlCommand("UPDATE Brand SET [Image] ='" + "~/BrandsData/Images/" + (id.ToString()) + ".jpg' WHERE id=@cid", conn);
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
                    SqlCommand query = new SqlCommand("UPDATE Brand SET [Desc] ='" + "~/BrandsData/" + id.ToString() + ".txt" + "' WHERE id=@cid", conn);
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
    }
}