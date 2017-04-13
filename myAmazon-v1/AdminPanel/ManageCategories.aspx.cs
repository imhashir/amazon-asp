using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using myAmazon_v1.DAL;

namespace myAmazon_v1.AdminPanel
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
				string log = "";
				CategoriesDAL catDal = new CategoriesDAL();
				DataTable table = catDal.getCategoriesList(ref (log), 2, null);
				if(log != "")
				{
					log_manage_cat.Text += log;
					return;
				}
				categoriesListView.DataSource = table;
                categoriesListView.DataBind();
            }

            if (Request.HttpMethod.ToString() == "POST") {
                switch (HttpContext.Current.Request.Form["Action"]) {
                    case "Delete":
                        {
							string log = "";
							CategoriesDAL catDal = new CategoriesDAL();
							if (!catDal.deleteCategory(HttpContext.Current.Request["id"], ref (log)))
								log_manage_cat.Text = log;
                            break;
                        }
                    case "Edit":
                        {
                            Session["isEdit"] = "1";
                            Session["CatId"] = HttpContext.Current.Request["id"];
                            Response.Redirect(@"..\AdminPanel\AddNewCategory.aspx");
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}