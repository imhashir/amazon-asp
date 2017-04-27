using System;
using System.Data;
using myAmazon_v1.DAL;
using System.Web;

namespace myAmazon_v1.AdminPanel
{
	public partial class ManageCreditRequests : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				populateTable();
			}

			if (Request.HttpMethod.ToString() == "POST")
			{
				string log = "";
				UserDAL uDal = new UserDAL();
				switch (HttpContext.Current.Request.Form["Action"])
				{
					case "Accept":
						{
							uDal.handleCreditRequest(HttpContext.Current.Request["username"], 1, ref (log));
							break;
						}
					case "Reject":
						{
							uDal.handleCreditRequest(HttpContext.Current.Request["username"], 0, ref (log));
							break;
						}
					default:
						break;
				}
				populateTable();
				log_manage_requests.Text += log;
			}
		}

		private void populateTable()
		{
			string log = "";
			UserDAL uDal = new UserDAL();
			DataTable table = uDal.getCreditRequestList(ref (log));
			if (log != "")
			{
				log_manage_requests.Text += log;
				return;
			}
			creditListview.DataSource = table;
			creditListview.DataBind();
		}
	}
}