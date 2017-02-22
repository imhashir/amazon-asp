using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myAmazon_v1.AdminPanel
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
                string cmd = "SELECT [id], [Name], [Desc] FROM Category";
                SqlCommand sqlCmd = new SqlCommand(cmd, conn);
                DataSet ds = new DataSet();

                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                    adapter.Fill(ds);
                    conn.Close();
                }
                catch (Exception ex)
                {
                    log_manage_cat.Text += ex.ToString();
                    conn.Close();
                }

                categoriesListView.DataSource = ds;
                categoriesListView.DataBind();
            }

            if (Request.HttpMethod.ToString() == "POST") {
                switch (HttpContext.Current.Request.Form["Action"]) {
                    case "Delete": {
                        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
                        string cmd = "DELETE FROM Category WHERE id=@catId";
                        SqlCommand sqlCmd = new SqlCommand(cmd, conn);
                        sqlCmd.Parameters.AddWithValue("catId", HttpContext.Current.Request["id"]);

                        try
                        {
                            conn.Open();
                            sqlCmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            log_manage_cat.Text += ex.ToString();
                            conn.Close();
                        }
                        break;
                    }
                    default:
                        break;
                }
            }
        }
    }
}