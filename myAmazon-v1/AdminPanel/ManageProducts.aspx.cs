using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using myAmazon_v1.DAL;

namespace myAmazon_v1.AdminPanel
{
    public partial class ManageProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
				string log = "";
				ProductDAL productDal = new ProductDAL();
				DataTable table = productDal.getProductList(ref (log));
                if(log != "")
				{
					log_manage_product.Text += log;
					return;
				}
                productListView.DataSource = table;
                productListView.DataBind();
            }

            if (Request.HttpMethod.ToString() == "POST")
            {
                switch (HttpContext.Current.Request.Form["Action"])
                {
                    case "Delete":
                        {
							string log = "";
							ProductDAL productDal = new ProductDAL();
							if (!productDal.deleteProduct(HttpContext.Current.Request["id"], ref (log))) {
								log_manage_product.Text += log;
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