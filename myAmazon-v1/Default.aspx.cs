using System;
using System.Web.UI;
using myAmazon_v1.DAL;
using myAmazon_v1.Model;
using System.Collections.Generic;

namespace myAmazon_v1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			List<FeaturedProduct> paths;
			ProductDAL pDal = new ProductDAL();
			string link = "ProductDetails?id=";
			string log = "";
			paths = pDal.getFeaturedProducts(1, ref (log));

			if (paths.Count > 0)
			{
				feature_p1.ImageUrl = paths[0].image;
				feature_p1_link.NavigateUrl = link + paths[0].id;
			} 
			else
				feature_p1.ImageUrl = "http://placehold.it/800x300";

			paths = pDal.getFeaturedProducts(2, ref (log));
			if (paths.Count > 0)
			{
				feature_g1.ImageUrl = paths[0].image;
				feature_g1_link.NavigateUrl = link + paths[0].id;
			}
			else
				feature_g1.ImageUrl = "http://placehold.it/800x300";

			if (paths.Count > 1)
			{
				feature_g2.ImageUrl = paths[1].image;
				feature_g2_link.NavigateUrl = link + paths[1].id;
			}
			else
				feature_g2.ImageUrl = "http://placehold.it/800x300";

			paths = pDal.getFeaturedProducts(3, ref (log));
			if (paths.Count > 0)
			{
				feature_s1.ImageUrl = paths[0].image;
				feature_s1_link.NavigateUrl = link + paths[0].id;
			}
			else
				feature_s1.ImageUrl = "http://placehold.it/800x300";

			if (paths.Count > 1)
			{
				feature_s2.ImageUrl = paths[1].image;
				feature_s2_link.NavigateUrl = link + paths[1].id;
			}
			else
				feature_s2.ImageUrl = "http://placehold.it/800x300";

			if (paths.Count > 2)
			{
				feature_s3.ImageUrl = paths[2].image;
				feature_s3_link.NavigateUrl = link + paths[2].id;
			}
			else
				feature_s3.ImageUrl = "http://placehold.it/800x300";
		}
	}
}