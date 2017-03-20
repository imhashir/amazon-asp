﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace myAmazon_v1
{
    public partial class BrowseCategories : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myAmazonConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string defaultImage = "~/CategoriesData/Images/Default.jpg";
            string sql = "SELECT [id], [Name], ISNULL([Image], '" + defaultImage + "') AS [Image] FROM CategoryDetails";

            try
            {
                con.Open();
                cmd = new SqlCommand(sql, con);
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                log_browse_category.Text += ex.ToString();
            }
            CategoryDataList.DataSource = dt;
            CategoryDataList.DataBind();
        }
    }
}