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
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
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

                if(Session["isEdit"] != null && Session["isEdit"].Equals("1"))
                {
                    this.Title = "Edit Brand";
                    id_brand_title.Text = "Edit Brand";
                    id_submit_brand.Text = "Update";

                    cmd = "SELECT Name AS name, [Desc] AS [desc], [Image], CategoryId AS catId FROM Brand WHERE id=" + Session["BrandId"];
                    sqlCmd = new SqlCommand(cmd, conn);
                    SqlDataReader reader = null;

                    try
                    {
                        conn.Open();
                        reader = sqlCmd.ExecuteReader();
                        reader.Read();
                        id_brand_name.Text = reader["name"].ToString();
                        id_category_name.SelectedValue = reader["catId"].ToString();

                        if (!reader["Image"].ToString().Equals(""))
                            id_brand_image.ImageUrl = reader["Image"].ToString();

                        if (!reader["desc"].ToString().Equals(""))
                        {
                            StreamReader stream = new StreamReader(Server.MapPath(reader["desc"].ToString()));
                            id_brand_desc.Text = stream.ReadToEnd();
                            stream.Close();
                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        id_log_brand.Text += ex.ToString();
                        conn.Close();
                    }
                }
            }
        }

        protected void Press_Submit(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
            string cmd = "";
            bool isEdit = false;

            if (Session["isEdit"] != null && Session["isEdit"].Equals("1"))
                isEdit = true;

            if(isEdit)
                cmd = "UPDATE Brand SET Name=@name, [CategoryId]=@category WHERE id=" + Session["BrandId"];
            else
                cmd = "INSERT INTO Brand(Name, [CategoryId]) OUTPUT inserted.id VALUES(@name, @category)";

            SqlCommand sqlCmd = new SqlCommand(cmd, conn);
            sqlCmd.Parameters.AddWithValue("@name", id_brand_name.Text);
            sqlCmd.Parameters.AddWithValue("@category", id_category_name.SelectedValue);
            int id = 0;
            try
            {
                conn.Open();
                if(isEdit)
                {
                    id = Convert.ToInt32(Session["BrandId"]);
                    sqlCmd.ExecuteNonQuery();
                    id_log_brand.Text = "Successfully Updated";
                } 
                else
                {
                    id = (int)sqlCmd.ExecuteScalar();
                    id_brand_name.Text = "";
                    id_log_brand.Text = "Successfully Inserted";
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                id_log_brand.Text = "Error! Unable to Insert: \n" + ex.ToString();
                conn.Close();
            }

            if (id_image_uploader.HasFile) {
                try
                {
                    id_image_uploader.SaveAs(Server.MapPath("~/BrandsData/Images/" + id + ".jpg"));
                    conn.Open();
                    SqlCommand query = new SqlCommand("UPDATE Brand SET [Image] ='" + "~/BrandsData/Images/" + (isEdit ? Session["BrandId"] : id.ToString()) + ".jpg' WHERE id=@cid", conn);
                    query.Parameters.AddWithValue("@cid", id);
                    query.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    id_log_brand.Text += ex.ToString();
                }
            }

            try
            {
                bool newDesc = false;
                try
                {
                    if (!Directory.Exists(Server.MapPath("~/BrandsData/")))
                        Directory.CreateDirectory(Server.MapPath("~/BrandsData/"));
                }
                catch (Exception ex)
                {
                    id_log_brand.Text += ex.ToString();
                }
                if (!File.Exists(Server.MapPath("~/BrandsData/" + id.ToString() + ".txt")))
                {
                    File.Create(Server.MapPath("~/BrandsData/" + id.ToString() + ".txt")).Close();
                    newDesc = true;
                }
                File.WriteAllText(Server.MapPath("~/BrandsData/" + id.ToString() + ".txt"), id_brand_desc.Text);
                if (!isEdit || newDesc)
                {
                    if(!newDesc)
                        id_brand_desc.Text = "";
                    conn.Open();
                    SqlCommand query = new SqlCommand("UPDATE Brand SET [Desc] ='" + "~/BrandsData/" + id.ToString() + ".txt" + "' WHERE id=@cid", conn);
                    query.Parameters.AddWithValue("@cid", id);
                    query.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    Session["isEdit"] = "0";
                    Response.Redirect(@"..\AdminPanel\ManageBrands.aspx");
                }
            }
            catch (Exception ex)
            {
                id_log_brand.Text += ex.ToString();
            }
        }

    }
}