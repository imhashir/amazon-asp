using System;
using myAmazon_v1.DAL;
using myAmazon_v1.Model;

namespace myAmazon_v1
{
	public partial class ProductDetails : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
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
		}
	}
}