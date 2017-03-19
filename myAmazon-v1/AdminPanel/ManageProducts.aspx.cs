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
    public partial class ManageProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
                string cmd = "SELECT * FROM ProductDetails";    //ProductDetails is a VIEW
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
                    log_manage_product.Text += ex.ToString();
                    conn.Close();
                }

                productListView.DataSource = ds;
                productListView.DataBind();
            }

            if (Request.HttpMethod.ToString() == "POST")
            {
                switch (HttpContext.Current.Request.Form["Action"])
                {
                    case "Delete":
                        {
                            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
                            string cmd = "DELETE FROM Brand WHERE id=@brandId";
                            SqlCommand sqlCmd = new SqlCommand(cmd, conn);
                            sqlCmd.Parameters.AddWithValue("brandId", HttpContext.Current.Request["id"]);

                            try
                            {
                                conn.Open();
                                sqlCmd.ExecuteNonQuery();
                                conn.Close();
                            }
                            catch (Exception ex)
                            {
                                log_manage_product.Text += ex.ToString();
                                conn.Close();
                            }
                            break;
                        }
                    case "Edit":
                        {
                            Session["isEdit"] = "1";
                            Session["ProductId"] = HttpContext.Current.Request["id"];
                            Response.Redirect(@"..\AdminPanel\AddNewProduct.aspx");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}