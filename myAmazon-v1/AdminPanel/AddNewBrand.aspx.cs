using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace myAmazon_v1.AdminPanel
{
    public partial class InsertBrand : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
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