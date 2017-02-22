using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

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
            SqlCommand insert = new SqlCommand("INSERT INTO Brand(Name, [Desc], [CategoryId]) VALUES(@name, @desc, @category)", conn);
            insert.Parameters.AddWithValue("@name", id_brand_name.Text);
            insert.Parameters.AddWithValue("@desc", id_brand_desc.Text);
            insert.Parameters.AddWithValue("@category", id_category_name.SelectedValue);

            try
            {
                conn.Open();
                insert.ExecuteNonQuery();
                conn.Close();
                id_log_brand.Text = "Successfully Inserted";
                id_brand_name.Text = "";
                id_brand_desc.Text = "";
            }
            catch (Exception ex)
            {
                id_log_brand.Text = "Error! Unable to Insert: \n" + ex.ToString();
                conn.Close();
            }
        }

    }
}