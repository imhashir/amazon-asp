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
            SignInDAL signInDal = new SignInDAL();
            if (signInDal.signInUser(id_username.Text, id_password.Text, ref (log)) != 0)
            {
                id_log_signin.Text += log;
            } else
            {
                Session["SignedInUser"] = id_username.Text.ToString();
                Response.Redirect(@"..\");
            }
        }
    }
}