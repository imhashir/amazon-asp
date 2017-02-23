using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

namespace myAmazon_v1.AdminPanel
{
    public partial class AddNewCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void On_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
            SqlCommand insert = new SqlCommand("INSERT INTO Category(Name) OUTPUT inserted.id VALUES(@name)", conn);
            insert.Parameters.AddWithValue("@name", id_category_name.Text);
            
            int id = 0;
            try
            {
                conn.Open();
                id = (int) insert.ExecuteScalar();
                id_log_category.Text = "Successfully Inserted";
                conn.Close();
                id_category_name.Text = "";
            }
            catch (Exception ex)
            {
                id_log_category.Text = "Error! Unable to Insert: \n" + ex.ToString();
                conn.Close();
            }

            try
            {
                try
                {
                    if (!Directory.Exists(Server.MapPath("~/CategoriesData/")))
                        Directory.CreateDirectory(Server.MapPath("~/CategoriesData/"));
                } catch (Exception ex) {
                    id_log_category.Text += ex.ToString();
                }
                File.Create(Server.MapPath("~/CategoriesData/" + id.ToString() + ".txt")).Close();
                File.WriteAllText(Server.MapPath("~/CategoriesData/" + id.ToString() + ".txt"), id_category_desc.Text);
                id_category_desc.Text = "";
                conn.Open();
                SqlCommand query = new SqlCommand("UPDATE Category SET [Desc] ='" + "~/CategoriesData/" + id.ToString() + ".txt" + "' WHERE id=@cid", conn);
                query.Parameters.AddWithValue("@cid", id);
                query.ExecuteNonQuery();
            }
            catch (Exception ex) {
                id_log_category.Text += ex.ToString();
            }
            

        }
    }
}