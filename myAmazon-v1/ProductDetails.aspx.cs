﻿using System;
using myAmazon_v1.DAL;
using myAmazon_v1.Model;
using System.IO;
using System.Data;

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
				if (product.quantity > 0)
					id_product_available.Text = "Available in Stock";
				else
					id_product_available.Text = "Out of stock. Available on order.";
				if (product.desc != null)
				{
					StreamReader file = new StreamReader(Server.MapPath(product.desc));
					id_product_desc.Text = file.ReadToEnd();
					file.Close();
				}
				populateReview();
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
					populateReview();
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

		protected void onBuyProduct(object sender, EventArgs e)
		{
			ProductDAL pDal = new ProductDAL();
			int orderId = 0, flag = 0;
			string log = "";
			if (Session["SignedInUser"] != null) {
				flag = pDal.buyProduct(Session["SignedInUser"].ToString(), id_product.Value, Convert.ToInt32(id_product_quantity.Text), ref (orderId), ref (log));

				if (flag != 1)
				{
					switch (flag)
					{
						case 2:
							log += "User doesn't have enough credit.";
							break;
						case 3:
							log += "Quantity in stock is lesser than your ordered one.";
							break;
						case 4:
							log += "Qunatity must be greater than or equal to one.";
							break;
					}
					id_log_div.InnerHtml = @"<strong>Error! </strong>";
					id_log_div.Attributes["class"] = "alert alert-danger";
					id_log_div.InnerHtml += log;
				}
				else
				{
					id_log_div.InnerHtml = @"<strong>Success! </strong> Successfully bought Product!";
					id_log_div.Attributes["class"] = "alert alert-success";
				}
			}
			else
			{
				id_log_div.InnerHtml = @"<strong>Error! </strong>";
				id_log_div.Attributes["class"] = "alert alert-danger";
				id_log_div.InnerHtml += "User must sign in to to buy a product.";
			}
		}

		protected void onAddToWishlist(object sender, EventArgs e)
		{
			ProductDAL pDal = new ProductDAL();
			string log = "";
			if (Session["SignedInUser"] != null)
			{
				if(!pDal.addToWishlist(Session["SignedInUser"].ToString(), id_product.Value, ref (log)))
				{
					id_log_div.InnerHtml = @"<strong>Error! </strong>";
					id_log_div.Attributes["class"] = "alert alert-danger";
					id_log_div.InnerHtml += log;
				}
				else
				{
					id_log_div.InnerHtml = @"<strong>Success! </strong> Successfully added to Wishlist!";
					id_log_div.Attributes["class"] = "alert alert-success";
				}
			}
			else
			{
				id_log_div.InnerHtml = @"<strong>Error! </strong>";
				id_log_div.Attributes["class"] = "alert alert-danger";
				id_log_div.InnerHtml += "User must sign in to add a product to wishlist.";
			}
		}

		private void populateReview()
		{
			string log = "", path = "";
			ProductDAL pDal = new ProductDAL();
			DataTable table = pDal.getProductCommentsList(Request.QueryString["id"], ref(log));
			if(log == "")
			{
				foreach (DataRow row in table.Rows)
				{
					path = row["text"].ToString();
					using (StreamReader file = new StreamReader(Server.MapPath(path)))
					{
						row["text"] = file.ReadToEnd();
						file.Close();
					}
				}
				CommentDataList.DataSource = table;
				CommentDataList.DataBind();
				double rating = pDal.getProductRating(Request.QueryString["id"], ref (log));
				if (log == "")
				{
					id_avg_rating.Text = rating.ToString();
				} 
				else
				{
					id_log_div.InnerHtml = @"<strong>Error! </strong>";
					id_log_div.Attributes["class"] = "alert alert-danger";
					id_log_div.InnerHtml += log;
				}
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