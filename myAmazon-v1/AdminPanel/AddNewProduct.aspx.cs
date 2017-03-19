﻿using System;
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
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
                string cmd = "SELECT id, Name FROM Category";
                SqlCommand sqlCmd = new SqlCommand(cmd, conn);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                try
                {
                    conn.Open();
                    adapter.Fill(table);
                    conn.Close();
                }
                catch (Exception ex)
                {
                    id_log_product.Text += "Error opting Brand Names: " + ex.ToString();
                    conn.Close();
                }
                id_category_name.DataSource = table;
                id_category_name.DataTextField = "Name";
                id_category_name.DataValueField = "id";
                id_category_name.DataBind();
                id_category_name.Items.Insert(0, new ListItem("--Select--", "NA"));
                id_brand_name.Items.Insert(0, new ListItem("--Select--", "NA"));

                if (Session["isEdit"] != null && Session["isEdit"].Equals("1"))
                {
                    Page.Title = "Edit Product";
                    id_page_title.Text = "Edit Product";
                    id_submit_product.Text = "Update";

                    cmd = "SELECT p.Name, p.Price, p.[Desc], p.[Image], p.CategoryId, p.BrandId FROM Product AS p WHERE p.id=" + Session["ProductId"];
                    sqlCmd = new SqlCommand(cmd, conn);
                    SqlDataReader reader = null;
                    try {
                        conn.Open();
                        reader = sqlCmd.ExecuteReader();
                        reader.Read();
                        id_product_name.Text = reader["Name"].ToString();
                        id_product_price.Text = reader["Price"].ToString();
                        id_category_name.SelectedValue = reader["CategoryId"].ToString();
                        populateBrandDropDown();
                        id_brand_name.SelectedValue = reader["BrandId"].ToString();
                        id_product_image.ImageUrl = reader["Image"].ToString();

                        //File handling for Description
                        string path = reader["Desc"].ToString();
                        if(!path.Equals(""))
                        {
                            StreamReader file = new StreamReader(Server.MapPath(path));
                            id_product_desc.Text = file.ReadToEnd();
                            file.Close();
                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        id_log_product.Text = ex.ToString();
                        conn.Close();
                    }
                }
                else
                {
                    Session["ProductId"] = null;
                }
                
            }
        }

        protected void Press_Submit(object sender, EventArgs e)
        {
            bool isEdit = false;
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HO7NA1P;Initial Catalog=myAmazon;User ID=sa;Password=root");
            SqlCommand insert = null;
            if (Session["isEdit"] != null && Session["isEdit"].Equals("1"))
                isEdit = true;

            insert = new SqlCommand("UpdateProduct", conn);
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("@id", Session["ProductId"] ?? (object)DBNull.Value);
            insert.Parameters.AddWithValue("@name", id_product_name.Text);
            insert.Parameters.AddWithValue("@brandId", id_brand_name.Text);
            insert.Parameters.AddWithValue("@categoryId", id_category_name.Text);
            insert.Parameters.AddWithValue("@price", id_product_price.Text);
            insert.Parameters.AddWithValue("@updateType", isEdit);
            SqlParameter outputId = insert.Parameters.Add("@productId", SqlDbType.Int);
            outputId.Direction = ParameterDirection.Output;
            SqlParameter outputFlag = insert.Parameters.Add("@flag", SqlDbType.Int);
            outputFlag.Direction = ParameterDirection.Output;

            int id = 0, flag = 0;
            try
            {
                conn.Open();
                insert.ExecuteNonQuery();
                if (!isEdit)
                    id = (int) insert.Parameters["@productId"].Value;
                else
                    id = Convert.ToInt32(Session["ProductId"]);
                flag = (int) insert.Parameters["@flag"].Value;

                if(flag == 0)
                {
                    if (!isEdit)
                    {
                        id_log_product.Text = "Successfully Inserted";
                        id_product_name.Text = "";
                        id_product_price.Text = "";
                        id_category_name.SelectedIndex = 0;
                        id_brand_name.SelectedIndex = 0;
                    }
                    else
                        id_log_product.Text = "Successfully Updated";
                } 
                else
                    id_log_product.Text = "Error in insertion. Price must be greater than 0.";

                conn.Close();
            }
            catch (Exception ex)
            {
                id_log_product.Text = "Error! Unable to Insert: \n" + ex.ToString();
                conn.Close();
            }

            if (id_image_uploader.HasFile) {
                try
                {
                    id_image_uploader.SaveAs(Server.MapPath("~/ProductsData/Images/" + id + ".jpg"));
                    conn.Open();
                    SqlCommand query = new SqlCommand("UPDATE Product SET [Image] ='" + "~/ProductsData/Images/" + (isEdit ? Session["ProductId"] : id.ToString()) + ".jpg' WHERE id=@cid", conn);
                    query.Parameters.AddWithValue("@cid", id);
                    query.ExecuteNonQuery();
                    conn.Close();
                } catch (Exception ex)
                {
                    id_log_product.Text += ex.ToString();
                }
            }

            try
            {
                bool newDesc = false;
                try
                {
                    if (!Directory.Exists(Server.MapPath("~/ProductsData/")))
                        Directory.CreateDirectory(Server.MapPath("~/ProductsData/"));
                }
                catch (Exception ex)
                {
                    id_log_product.Text += ex.ToString();
                }
                if (!File.Exists(Server.MapPath("~/ProductsData/" + id.ToString() + ".txt"))) { 
                    File.Create(Server.MapPath("~/ProductsData/" + id.ToString() + ".txt")).Close();
                    newDesc = true;
                }
                
                File.WriteAllText(Server.MapPath("~/ProductsData/" + id.ToString() + ".txt"), id_product_desc.Text);
                if(!isEdit || newDesc)
                {
                    if(!isEdit)
                        id_product_desc.Text = "";
                    conn.Open();
                    SqlCommand query = new SqlCommand("UPDATE Product SET [Desc] ='" + "~/ProductsData/" + (isEdit ? Session["ProductId"] : id.ToString()) + ".txt" + "' WHERE id=@cid", conn);
                    query.Parameters.AddWithValue("@cid", id);
                    query.ExecuteNonQuery();
                    conn.Close();
                } else
                {
                    Session["isEdit"] = "0";
                    Response.Redirect(@"..\AdminPanel\ManageProducts.aspx");
                }
            }
            catch (Exception ex)
            {
                id_log_product.Text += ex.ToString();
            }
        }

        protected void id_category_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateBrandDropDown();
        }

        private void populateBrandDropDown() {
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
            catch (Exception ex)
            {
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