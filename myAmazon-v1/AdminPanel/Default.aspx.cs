using System;

namespace myAmazon_v1.AdminPanel
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Session["isEdit"] = "0";
		}
	}
}