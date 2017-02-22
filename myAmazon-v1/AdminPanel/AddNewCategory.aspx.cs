using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

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
            SqlCommand insert = new SqlCommand("INSERT INTO Category(Name, [Desc]) VALUES(@name, @desc)", conn);
            insert.Parameters.AddWithValue("@name", id_category_name.Text);
            insert.Parameters.AddWithValue("@desc", id_category_desc.Text);

            try
            {
                conn.Open();
                insert.ExecuteNonQuery();
                id_log_category.Text = "Successfully Inserted";
                conn.Close();
                id_category_name.Text = "";
                id_category_desc.Text = "";
            }
            catch (Exception ex)
            {
                id_log_category.Text = "Error! Unable to Insert: \n" + ex.ToString();
                conn.Close();
            }
        }
    }
}