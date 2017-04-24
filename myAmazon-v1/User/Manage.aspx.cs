using System;
using myAmazon_v1.DAL;
using myAmazon_v1.Model;

namespace myAmazon_v1.User
{
	public partial class Manage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				UserDAL uDal = new UserDAL();
				Customer customer = new Customer();
				string log = "";
				customer = uDal.getUserDetails(Session["SignedInUser"].ToString(), ref(log));
				if(log != "")
				{
					id_log_div.InnerHtml = @"<strong>Error! </strong>" + log;
					id_log_div.Attributes["class"] = "alert alert-danger";
				}
				else
				{
					id_username.Text = customer.username;
					id_user_email.Text = customer.email;
					id_user_fname.Text = customer.firsName;
					id_user_lname.Text = customer.lastName;
					id_user_number.Text = customer.number;
					id_user_pass.Text = customer.password;
					id_user_image.ImageUrl = customer.image;
				}
			}
		}

		protected void onUpdateUserInfo(object sender, EventArgs e)
		{
			UserDAL uDal = new UserDAL();
			string log = "";
			if(uDal.updateUserInfo(id_username.Text, id_user_fname.Text, id_user_lname.Text, id_user_pass.Text, id_user_number.Text, ref(log)))
			{
				id_log_div.InnerHtml = @"<strong>Success! </strong> Successfully Updated Info!";
				id_log_div.Attributes["class"] = "alert alert-success";
			} 
			else
			{
				id_log_div.InnerHtml = @"<strong>Error! </strong>";
				id_log_div.Attributes["class"] = "alert alert-danger";
				id_log_div.InnerHtml += log;
			}
		}

		protected void onRequestCredit(object sender, EventArgs e)
		{
			UserDAL uDal = new UserDAL();
			string log = "";
			if (uDal.requestCredit(id_username.Text, id_credit_value.Text, ref (log)))
			{
				id_log_div.InnerHtml = @"<strong>Success! </strong> Request Sent!";
				id_log_div.Attributes["class"] = "alert alert-success";
			}
			else
			{
				id_log_div.InnerHtml = @"<strong>Error! </strong>";
				id_log_div.Attributes["class"] = "alert alert-danger";
				id_log_div.InnerHtml += log;
			}
		}
	}
}