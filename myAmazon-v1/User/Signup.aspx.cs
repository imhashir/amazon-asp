using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using myAmazon_v1.DAL;
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
            if(File.Exists(Server.MapPath(@"~\UserData\Images\" + "_temp" + ".jpg")))
            {
				try
				{
					File.Copy(Server.MapPath(@"~\UserData\Images\" + "_temp" + ".jpg"), Server.MapPath(@"~\UserData\Images\" + id_username.Text + ".jpg"), true);
					File.Delete(Server.MapPath(@"~\UserData\Images\" + "_temp" + ".jpg"));
				} catch (Exception ex)
				{
					id_log_signup.Text += ex.ToString();
				}
            }
            string log = "";
            SignUpDAL signUpDal = new SignUpDAL();
            signUpDal.signUpUser(
                id_user_fname.Text,
                id_user_lname.Text,
                id_email.Text,
                id_cnumber.Text,
                id_cimage.ImageUrl,
                id_username.Text,
                id_password.Text,
                ref (log));
            id_log_signup.Text += log;
        }

        protected void onImageUploaded() {
            string imagePath = @"~\UserData\Images\" + "_temp" + ".jpg";
            id_image_uploader.SaveAs(Server.MapPath(imagePath));
            id_cimage.ImageUrl = imagePath;
        }
    }
}