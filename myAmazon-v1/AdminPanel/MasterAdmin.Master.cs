using System;

namespace myAmazon_v1.AdminPanel
{
	public partial class MasterAdmin : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(Session["IsAdmin"] == null || !((bool)Session["IsAdmin"]))
			{
				Response.Redirect(@"..\User\Signin");
			}
		}
	}
}