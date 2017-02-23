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
    public partial class AddNewProduct : System.Web.UI.Page
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
                    id_log_product.Text += "Error opting Brand Names: " + ex.ToString();
                    conn.Close();
                }
                adapter.Fill(table);
                id_category_name.DataSource = table;
                id_category_name.DataTextField = "Name";
                id_category_name.DataValueField = "id";
                id_category_name.DataBind();
                id_category_name.Items.Insert(0, new ListItem("--Select--", "NA"));
                id_brand_name.Items.Insert(0, new ListItem("--Select--", "NA"));
            }
        }

        protected void Press_Submit(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
            SqlCommand insert = new SqlCommand("INSERT INTO Product(Name, [BrandId], [CategoryId], [Price]) OUTPUT inserted.id VALUES(@name, @brand, @category, @price)", conn);
            insert.Parameters.AddWithValue("@name", id_product_name.Text);
            insert.Parameters.AddWithValue("@brand", id_brand_name.Text);
            insert.Parameters.AddWithValue("@category", id_category_name.Text);
            insert.Parameters.AddWithValue("@price", id_product_price.Text);
            int id = 0;
            try
            {
                conn.Open();
                id = (int) insert.ExecuteScalar();
                id_log_product.Text = "Successfully Inserted";
                conn.Close();
                id_product_name.Text = "";
                id_product_price.Text = "";
            }
            catch (Exception ex)
            {
                id_log_product.Text = "Error! Unable to Insert: \n" + ex.ToString();
                conn.Close();
            }
            try
            {
                try
                {
                    if (!Directory.Exists(Server.MapPath("~/ProductsData/")))
                        Directory.CreateDirectory(Server.MapPath("~/ProductsData/"));
                }
                catch (Exception ex)
                {
                    id_log_product.Text += ex.ToString();
                }
                File.Create(Server.MapPath("~/ProductsData/" + id.ToString() + ".txt")).Close();
                File.WriteAllText(Server.MapPath("~/ProductsData/" + id.ToString() + ".txt"), id_product_desc.Text);
                id_product_desc.Text = "";
                conn.Open();
                SqlCommand query = new SqlCommand("UPDATE Product SET [Desc] ='" + "~/ProductsData/" + id.ToString() + ".txt" + "' WHERE id=@cid", conn);
                query.Parameters.AddWithValue("@cid", id);
                query.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                id_log_product.Text += ex.ToString();
            }
        }

        protected void id_category_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
            string cmd = "SELECT id, Name FROM Brand WHERE CategoryId = @id";
            SqlCommand sqlCmd = new SqlCommand(cmd, conn);
            sqlCmd.Parameters.AddWithValue("@id", id_category_name.SelectedValue);
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
            try
            {
                conn.Open();
                sqlCmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex) {
                id_log_product.Text += "Error opting Brand Names: " + ex.ToString();
                conn.Close();
            }
            adapter.Fill(table);
            id_brand_name.DataSource = table;
            id_brand_name.DataTextField = "Name";
            id_brand_name.DataValueField = "id";
            id_brand_name.DataBind();
            id_brand_name.Items.Insert(0, new ListItem("--Select--", "NA"));
        }
    }
}