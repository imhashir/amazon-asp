using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myAmazon_v1.AdminPanel
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Visit_Products(object sender, EventArgs e)
        {
            Session["isEdit"] = "0";
            Response.Redirect(@"..\AdminPanel\ManageProducts.aspx");
        }

    }
}