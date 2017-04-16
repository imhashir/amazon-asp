using System;
using myAmazon_v1.DAL;
using System.Data;
using System.Web;
using System.IO;

namespace myAmazon_v1.AdminPanel
{
    public partial class ManageFeaturedProduct : System.Web.UI.Page
    {
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				populateTable();
			}

			if (Request.HttpMethod.ToString() == "POST")
			{
				switch (HttpContext.Current.Request.Form["Action"])
				{
					case "Delete":
						{
							string log = "";
							ProductDAL productDal = new ProductDAL();
							string imagePath = "";
							if (!productDal.deleteFromFeaturedFeatured(HttpContext.Current.Request["id"], ref(imagePath), ref (log)))
							{
								id_log_sponsor.Text += log;
							}
							File.Delete(Server.MapPath(imagePath));
							populateTable();
							break;
						}
					default:
						break;
				}
			}
		}

		protected void Press_Submit(object sender, EventArgs e)
        {
			ProductDAL pDal = new ProductDAL();
			string log = "";
			string imagePath = "~/FeaturedData/Images/" + id_product.Text + ".jpg";
			if (pDal.addFeaturedProduct(id_product.Text, id_level.Text, imagePath, ref (log)))
			{
				id_image_uploader.SaveAs(Server.MapPath(imagePath));
				populateTable();
			}
			id_log_sponsor.Text = log;
        }

		protected void populateTable() {
			string log = "";
			ProductDAL productDal = new ProductDAL();
			DataTable table = productDal.getFeaturedList(ref (log));
			if (log != "")
			{
				id_log_sponsor.Text += log;
				return;
			}
			featuredListView.DataSource = table;
			featuredListView.DataBind();
		}
    }
}