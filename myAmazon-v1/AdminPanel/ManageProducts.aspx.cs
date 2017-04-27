using System;
using System.Data;
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
				populateTable();
			}

			if (Request.HttpMethod.ToString() == "POST")
			{
				string log = "";
				switch (HttpContext.Current.Request.Form["Action"])
				{
					case "Delete":
						{
							ProductDAL productDal = new ProductDAL();
							if (!productDal.deleteProduct(HttpContext.Current.Request["id"], ref (log)))
							{
								log_manage_product.Text += log;
							}
							else
							{
								populateTable();
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
					case "Update":
						{

							ProductDAL productDal = new ProductDAL();
							if (!productDal.updateStock(HttpContext.Current.Request["id"], Convert.ToInt32(HttpContext.Current.Request["quantity"]), ref (log)))
							{
								log_manage_product.Text += log;
							}
							else
							{
								populateTable();
							}
						}
						break;
					default:
						break;
				}
			}
		}

		private void populateTable()
		{
			string log = "";
			ProductDAL productDal = new ProductDAL();
			DataTable table = productDal.getProductList(ref (log), 2, null);
			if (log != "")
			{
				log_manage_product.Text += log;
				return;
			}
			productListView.DataSource = table;
			productListView.DataBind();
		}
	}
}