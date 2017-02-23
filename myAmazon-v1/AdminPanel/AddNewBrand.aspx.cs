using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace myAmazon_v1.AdminPanel
{
    public partial class InsertBrand : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
                string cmd = "SELECT id, Name FROM Category";
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
                    id_log_brand.Text += "Error opting Brand Names: " + ex.ToString();
                    conn.Close();
                }
                adapter.Fill(table);
                id_category_name.DataSource = table;
                id_category_name.DataTextField = "Name";
                id_category_name.DataValueField = "id";
                id_category_name.DataBind();
                id_category_name.Items.Insert(0, new ListItem("--Select--", "NA"));
            }
        }

        protected void Press_Submit(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
            SqlCommand insert = new SqlCommand("INSERT INTO Brand(Name, [CategoryId]) OUTPUT inserted.id VALUES(@name, @category)", conn);
            insert.Parameters.AddWithValue("@name", id_brand_name.Text);
            insert.Parameters.AddWithValue("@category", id_category_name.SelectedValue);
            int id = 0;
            try
            {
                conn.Open();
                id = (int )insert.ExecuteScalar();
                conn.Close();
                id_log_brand.Text = "Successfully Inserted";
                id_brand_name.Text = "";
            }
            catch (Exception ex)
            {
                id_log_brand.Text = "Error! Unable to Insert: \n" + ex.ToString();
                conn.Close();
            }

            try
            {
                try
                {
                    if (!Directory.Exists(Server.MapPath("~/BrandsData/")))
                        Directory.CreateDirectory(Server.MapPath("~/BrandsData/"));
                }
                catch (Exception ex)
                {
                    id_log_brand.Text += ex.ToString();
                }
                File.Create(Server.MapPath("~/BrandsData/" + id.ToString() + ".txt")).Close();
                File.WriteAllText(Server.MapPath("~/BrandsData/" + id.ToString() + ".txt"), id_brand_desc.Text);
                id_brand_desc.Text = "";
                conn.Open();
                SqlCommand query = new SqlCommand("UPDATE Brand SET [Desc] ='" + "~/BrandsData/" + id.ToString() + ".txt" + "' WHERE id=@cid", conn);
                query.Parameters.AddWithValue("@cid", id);
                query.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                id_log_brand.Text += ex.ToString();
            }
        }

    }
}