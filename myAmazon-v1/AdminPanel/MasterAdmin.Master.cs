using System;

namespace myAmazon_v1.AdminPanel
{
	public partial class MasterAdmin : System.Web.UI.MasterPage
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			if (Session["IsAdmin"] == null || !((bool)Session["IsAdmin"]))
			{
				if (!Request.Url.ToString().Equals(@"http://localhost:29035/AdminPanel/AddNewProduct") &&
					!Request.Url.ToString().Equals(@"http://localhost:29035/AdminPanel/ManageProducts"))
					Response.Redirect(@"..\User\Signin");
			}
		}
	}
}