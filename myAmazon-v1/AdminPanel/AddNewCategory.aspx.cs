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
            if (!Page.IsPostBack)
            {
                if (Session["isEdit"] != null && Session["isEdit"].Equals("1"))
                {
                    id_category_title.Text = "Edit Category";
                    this.Title = "Edit Category";
                    id_submit_category.Text = "Update";

                    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
                    string cmd = @"SELECT Name, [Desc] FROM Category WHERE id=" + Session["CatId"];
                    SqlCommand sqlCmd = new SqlCommand(cmd, conn);
                    SqlDataReader reader = null;
                    try
                    {
                        conn.Open();
                        reader = sqlCmd.ExecuteReader();
                        reader.Read();
                        id_category_name.Text = reader["Name"].ToString();

                        if (!reader["Desc"].ToString().Equals(""))
                        {
                            StreamReader stream = new StreamReader(Server.MapPath(reader["Desc"].ToString()));
                            id_category_desc.Text = stream.ReadToEnd();
                            stream.Close();
                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        id_log_category.Text += ex.ToString();
                        conn.Close();
                    }

                }
            }
        }

        protected void On_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
            string cmd;
            bool isEdit = false;
            SqlCommand sqlCmd = null;

            if(Session["isEdit"] != null && Session["isEdit"].Equals("1"))
            {
                isEdit = true;
            }

            if (isEdit)
                cmd = "UPDATE Category SET Name=@name WHERE id=@cid";
            else
                cmd = "INSERT INTO Category(Name) OUTPUT inserted.id VALUES(@name)";

            sqlCmd = new SqlCommand(cmd, conn);
            sqlCmd.Parameters.AddWithValue("@name", id_category_name.Text);
            
            int id = 0;
            try
            {
                conn.Open();
                if(isEdit)
                {
                    sqlCmd.Parameters.AddWithValue("@cid", Session["CatId"]);
                    sqlCmd.ExecuteNonQuery();
                    id = Convert.ToInt32(Session["CatId"]);
                    id_log_category.Text = "Successfully Updated";
                }
                else
                {
                    id = (int)sqlCmd.ExecuteScalar();
                    id_log_category.Text = "Successfully Inserted";
                    id_category_name.Text = "";
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                id_log_category.Text += "Error! Unable to Insert: \n" + ex.ToString();
                conn.Close();
            }

            try
            {
                bool newDesc = false;
                try
                {
                    if (!Directory.Exists(Server.MapPath("~/CategoriesData/")))
                        Directory.CreateDirectory(Server.MapPath("~/CategoriesData/"));
                } catch (Exception ex) {
                    id_log_category.Text += ex.ToString();
                }
                if (!File.Exists(Server.MapPath("~/CategoriesData/" + id.ToString() + ".txt")))
                {
                    File.Create(Server.MapPath("~/CategoriesData/" + id.ToString() + ".txt")).Close();
                    newDesc = true;
                }
                File.WriteAllText(Server.MapPath("~/CategoriesData/" + id.ToString() + ".txt"), id_category_desc.Text);
                if (!isEdit || newDesc)
                { 
                    if(!newDesc)
                        id_category_desc.Text = "";
                    conn.Open();
                    SqlCommand query = new SqlCommand("UPDATE Category SET [Desc] ='" + "~/CategoriesData/" + id.ToString() + ".txt" + "' WHERE id=@cid", conn);
                    query.Parameters.AddWithValue("@cid", id);
                    query.ExecuteNonQuery();
                    conn.Close();
                } 
                else
                {
                    Session["isEdit"] = "0";
                    Response.Redirect(@"..\AdminPanel\ManageCategories.aspx");
                }
            }
            catch (Exception ex) {
                id_log_category.Text += ex.ToString();
            }
        }
    }
}