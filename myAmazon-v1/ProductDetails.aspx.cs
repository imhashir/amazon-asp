using System;
using myAmazon_v1.DAL;
using myAmazon_v1.Model;
using System.IO;

namespace myAmazon_v1
{
	public partial class ProductDetails : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				ProductDAL pDal = new ProductDAL();
				bool flag = false;
				string log = "";
				Product product = pDal.getProductDetails(ref (flag), ref (log), Request.QueryString["id"]);
				id_product_name.Text = product.name;
				id_product_image.ImageUrl = product.image;
				id_product_category.Text = product.category;
				id_product_brand.Text = product.brand;
				id_product_price.Text = product.price.ToString();
				id_product.Value = product.id.ToString();

				if (product.desc != null)
				{
					StreamReader file = new StreamReader(Server.MapPath(product.desc));
					id_product_desc.Text = file.ReadToEnd();
					file.Close();
				}
			}
		}

		protected void onSubmitReview(object sender, EventArgs e) {
			if (Session["SignedInUser"] != null)
			{
				int rate = Convert.ToInt32(id_rating_input.Value);
				string comment = null;
				if (id_product_comment.Text != null)
				{
					comment = "~/Reviews/" + id_product.Value + "-" + Session["SignedInUser"] + ".txt";
				}
				string log = "";
				ProductDAL pDal = new ProductDAL();
				if (pDal.addReviewToProduct(Session["SignedInUser"].ToString(), id_product.Value, Convert.ToInt32(id_rating_input.Value), comment, ref (log))) {
					if(id_product_comment.Text != null) {
						if (!File.Exists(Server.MapPath(comment)))
							File.Create(Server.MapPath(comment)).Close();
						File.WriteAllText(Server.MapPath(comment), id_product_comment.Text);
					}
					id_log_div.InnerHtml = @"<strong>Success! </strong> Successfully Posted Review!";
					id_log_div.Attributes["class"] = "alert alert-success";
				} else
				{
					id_log_div.InnerHtml = @"<strong>Error! </strong>";
					id_log_div.Attributes["class"] = "alert alert-danger";
					id_log_div.InnerHtml += log;
				}
			} else
			{
				id_log_div.InnerHtml = @"<strong>Error! </strong>";
				id_log_div.Attributes["class"] = "alert alert-danger";
				id_log_div.InnerHtml += "User must sign in to give a product review.";
			}
		}
	}
}