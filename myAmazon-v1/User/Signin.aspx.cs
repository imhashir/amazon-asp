using System;
using System.Data.SqlClient;
using System.Data;
namespace myAmazon_v1.User
{
    public partial class Signin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void id_submit_signin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("SignInUser", conn);
            string imagePath = null;
            
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@username", id_username.Text);
            sqlCmd.Parameters.AddWithValue("@pass", id_password.Text);

            SqlParameter outputFlag = sqlCmd.Parameters.Add("@flag", SqlDbType.Int);
            outputFlag.Direction = ParameterDirection.Output;
            int flag = 0;
            try
            {
                conn.Open();
                sqlCmd.ExecuteNonQuery();
                flag = (int) sqlCmd.Parameters["@flag"].Value;
                if (flag != 0)
                    throw new Exception();
                Session["SignedInUser"] = id_username.Text.ToString(); 
                conn.Close();
                id_log_signin.Text = "SignIn Successful!\nSignIn Id: " + Session["SignedInUser"];
            } catch (Exception ex)
            {
                if (flag != 0) {
                    switch (flag)
                    {
                        case 1:
                            id_log_signin.Text = "Invalid Username.";
                            break;
                        case 2:
                            id_log_signin.Text = "Invalid Password";
                            break;
                    }
                }
                else {
                    id_log_signin.Text = ex.ToString();
                }
                conn.Close();
            }
        }
    }
}