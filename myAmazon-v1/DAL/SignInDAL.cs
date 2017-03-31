using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace myAmazon_v1.DAL
{
    public class SignInDAL
    {
        public int signInUser(string username, string pwd, ref string log) {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("SignInUser", conn);

            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@username", username);
            sqlCmd.Parameters.AddWithValue("@pass", pwd);

            SqlParameter outputFlag = sqlCmd.Parameters.Add("@flag", SqlDbType.Int);
            outputFlag.Direction = ParameterDirection.Output;
            int flag = 0;
            try
            {
                conn.Open();
                sqlCmd.ExecuteNonQuery();
                flag = (int)sqlCmd.Parameters["@flag"].Value;
                if (flag != 0)
                    throw new Exception();
                //Session["SignedInUser"] = id_username.Text.ToString();
                //id_log_signin.Text = "SignIn Successful!\nSignIn Id: " + Session["SignedInUser"];
                //Response.Redirect(@"..\");
            }
            catch (Exception ex)
            {
                log += ex.ToString();
                if (flag != 0)
                {
                    switch (flag)
                    {
                        case 1:
                            log = "Invalid Username.";
                            break;
                        case 2:
                            log = "Invalid Password";
                            break;
                    }
                }
                else
                {
                    log = ex.ToString();
                }
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }
    }
}