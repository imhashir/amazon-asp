using System;
using System.Data;
using System.Web;
using myAmazon_v1.DAL;

namespace myAmazon_v1.AdminPanel
{
	public partial class ManageProductRequests : System.Web.UI.Page
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
				bool status = true;
				switch (HttpContext.Current.Request.Form["Action"])
				{
					case "Completed":
						{
							status = true;
							break;
						}
					case "Decline":
						{
							status = false; ;
						}
						break;
					default:
						break;
				}
				UserDAL uDal = new UserDAL();
				if (!uDal.handleProductRequest(HttpContext.Current.Request["id"], HttpContext.Current.Request["CustomerId"], status, ref (log)))
				{
					log_manage_request.Text += log;
				}
				else
				{
					populateTable();
				}
			}
		}

		private void populateTable()
		{
			string log = "";
			ProductDAL productDal = new ProductDAL();
			DataTable table;
			table = productDal.getProductRequestList(ref (log), 2, null);
			if (log != "")
			{
				log_manage_request.Text += log;
				return;
			}
			requestsListView.DataSource = table;
			requestsListView.DataBind();
		}

		
	}
}