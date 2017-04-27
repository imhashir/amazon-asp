using System;
using System.Data.SqlClient;
using System.Data;
using myAmazon_v1.DAL;

namespace myAmazon_v1.User
{
    public partial class Signin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void id_submit_signin_Click(object sender, EventArgs e)
        {
            string log = "";
            UserDAL signInDal = new UserDAL();
			int flag = signInDal.signInUser(id_username.Text, id_password.Text, ref (log));

			if (flag < 0)
            {
                id_log_signin.Text += log;
            } else
            {
                Session["SignedInUser"] = id_username.Text.ToString();
				if (flag == 1)
					Session["IsAdmin"] = true;
				else
					Session["IsAdmin"] = false;
				Response.Redirect(@"..\");
            }
        }
    }
}