using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
namespace myAmazon_v1.User
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(id_image_uploader.HasFile)
            {
                onImageUploaded();
            }
        }

        protected void id_submit_signup_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("SignUp", conn);
            string imagePath = null;
            
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@username", id_username.Text);
            sqlCmd.Parameters.AddWithValue("@firstName", id_user_fname.Text);
            sqlCmd.Parameters.AddWithValue("@lastName", id_user_lname.Text);
            sqlCmd.Parameters.AddWithValue("@email", id_email.Text);
            sqlCmd.Parameters.AddWithValue("@pass", id_password.Text);
            sqlCmd.Parameters.AddWithValue("@number", id_cnumber.Text);
            sqlCmd.Parameters.AddWithValue("@img", imagePath ?? (object)DBNull.Value);

            if(File.Exists(Server.MapPath(@"~\UserData\Images\" + "_temp" + ".jpg")))
            {
                File.Copy(Server.MapPath(@"~\UserData\Images\" + "_temp" + ".jpg"), Server.MapPath(@"~\UserData\Images\" + id_username.Text + ".jpg"));
                File.Delete(Server.MapPath(@"~\UserData\Images\" + "_temp" + ".jpg"));
            }
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
                conn.Close();
                id_log_signup.Text = "SignUp Successful!";
            } catch (Exception ex)
            {
                if (flag != 0) {
                    switch (flag)
                    {
                        case 1:
                            id_log_signup.Text = "Username already exists";
                            break;
                        case 2:
                            id_log_signup.Text = "Invalid Password";
                            break;
                        case 3:
                            id_log_signup.Text = "Email already registered!";
                            break;
                    }
                }
                else {
                    id_log_signup.Text = ex.ToString();
                }
                conn.Close();
            }
        }

        protected void onImageUploaded() {
            string imagePath = @"~\UserData\Images\" + "_temp" + ".jpg";
            id_image_uploader.SaveAs(Server.MapPath(imagePath));
            id_cimage.ImageUrl = imagePath;
        }
    }
}