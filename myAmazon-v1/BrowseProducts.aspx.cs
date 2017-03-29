using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace myAmazon_v1
{
    public partial class BrowseProducts : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myAmazonConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                /*
                string defaultImage = "~/ProductsData/Images/Default.jpg";
                string sql = "SELECT [id], [Name], ISNULL([Image], '" + defaultImage + "') AS [Image] FROM ProductDetails";

                try
                {
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    log_browse_product.Text += ex.ToString();
                }
                ProductDataList.DataSource = dt;
                ProductDataList.DataBind();
                */
                getProductTable(null, null);
                string sqlCat = "SELECT id, Name FROM Category";
                string sqlBrand = "SELECT id, Name FROM Brand";

                SqlCommand sqlCmdCat = new SqlCommand(sqlCat, con);
                SqlCommand sqlCmdBrand = new SqlCommand(sqlBrand, con);

                DataTable tableCat = new DataTable();
                DataTable tableBrand = new DataTable();

                SqlDataAdapter adapterCat = new SqlDataAdapter(sqlCmdCat);
                SqlDataAdapter adapterBrand = new SqlDataAdapter(sqlCmdBrand);

                try
                {
                    con.Open();
                    adapterCat.Fill(tableCat);
                    adapterBrand.Fill(tableBrand);
                    con.Close();
                }
                catch (Exception ex)
                {
                    log_browse_product.Text += "Error opting Brand Names: " + ex.ToString();
                    con.Close();
                }
                CategoryDropdownList.DataTextField = "Name";
                CategoryDropdownList.DataValueField = "id";
                CategoryDropdownList.DataSource = tableCat;
                CategoryDropdownList.DataBind();
                CategoryDropdownList.Items.Insert(0, new ListItem("--Select--", "NA"));
                BrandDropdownList.DataTextField = "Name";
                BrandDropdownList.DataValueField = "id";
                BrandDropdownList.DataSource = tableBrand;
                BrandDropdownList.DataBind();
                BrandDropdownList.Items.Insert(0, new ListItem("--Select--", "NA"));
            }
        }

        private void getProductTable(Nullable<int> categoryId, Nullable<int> brandId) {
            string defaultImage = "~/ProductsData/Images/Default.jpg";
            string sql = "SELECT [id], [Name], ISNULL([Image], '" + defaultImage + "') AS [Image] FROM ProductDetails";
            if (categoryId != null || brandId != null)
            {
                sql += " WHERE ";
                if (categoryId != null)
                    sql += " Category LIKE (SELECT Name FROM Category WHERE id = " + categoryId + ")";
                if (brandId != null)
                {
                    if (categoryId != null)
                        sql += " AND ";
                    sql += " Brand LIKE (SELECT Name FROM Brand WHERE id = " + brandId + ")";
                }
            }

            try
            {
                con.Open();
                cmd = new SqlCommand(sql, con);
                da.SelectCommand = cmd;
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                log_browse_product.Text += ex.ToString();
            }
            ProductDataList.DataSource = dt;
            ProductDataList.DataBind();
        }

        private void populateBrandDropDown()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
            string cmd = "SELECT id, Name FROM Brand WHERE CategoryId = @id";
            SqlCommand sqlCmd = new SqlCommand(cmd, conn);
            sqlCmd.Parameters.AddWithValue("@id", CategoryDropdownList.SelectedValue);
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
                log_browse_product.Text += "Error opting Brand Names: " + ex.ToString();
                conn.Close();
            }
            adapter.Fill(table);
            BrandDropdownList.DataSource = table;
            BrandDropdownList.DataBind();
            BrandDropdownList.Items.Insert(0, new ListItem("--Select--", "NA"));
        }

        protected void ButtonFilterProducts_Click(object sender, EventArgs e)
        {
            Nullable<int> catId = null;
            Nullable<int> brandId = null;

            if (!CategoryDropdownList.SelectedValue.Equals("NA"))
            {
                catId = Convert.ToInt32(CategoryDropdownList.SelectedValue);
            }
            if (!BrandDropdownList.SelectedValue.Equals("NA"))
            {
                brandId = Convert.ToInt32(BrandDropdownList.SelectedValue);
            }
            getProductTable(catId, brandId);
        }

        protected void DropdownSelectedIndexChanged(object sender, EventArgs e)
        {
            Nullable<int> catId = null;
            Nullable<int> brandId = null;

            if (!CategoryDropdownList.SelectedValue.Equals("NA"))
            {
                catId = Convert.ToInt32(CategoryDropdownList.SelectedValue);
            }
            if (!BrandDropdownList.SelectedValue.Equals("NA"))
            {
                brandId = Convert.ToInt32(BrandDropdownList.SelectedValue);
            }
            populateBrandDropDown();
            getProductTable(catId, brandId);
        }
    }
}