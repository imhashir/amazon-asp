using System;
using myAmazon_v1.DAL;

namespace myAmazon_v1
{
	public partial class RequestProduct : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void onRequestProduct(object sender, EventArgs e)
		{
			ProductDAL pDal = new ProductDAL();
			string log = "";
			if (!pDal.requestProduct(Session["SignedInUser"].ToString(), id_request_name.Text, ref (log)))
			{
				id_log_div.InnerHtml = @"<strong>Error! </strong>";
				id_log_div.Attributes["class"] = "alert alert-danger";
				id_log_div.InnerHtml += log;
			}
			else
			{
				id_log_div.InnerHtml = @"<strong>Success! </strong> Successfully Requested Product!";
				id_log_div.Attributes["class"] = "alert alert-success";
			}
		}
	}
}