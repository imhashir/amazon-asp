using System;
using System.Web.UI;
using myAmazon_v1.DAL;
using System.Collections.Generic;

namespace myAmazon_v1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			List<string> paths;
			ProductDAL pDal = new ProductDAL();
			string log = "";
			paths = pDal.getFeaturedImagePath(1, ref (log));
			if (paths.Count > 0)
				feature_p1.ImageUrl = paths[0];
			else
				feature_p1.ImageUrl = "http://placehold.it/800x300";

			paths = pDal.getFeaturedImagePath(2, ref (log));
			if (paths.Count > 0)
				feature_g1.ImageUrl = paths[0];
			else
				feature_g1.ImageUrl = "http://placehold.it/800x300";

			if (paths.Count > 1)
				feature_g2.ImageUrl = paths[1];
			else
				feature_g2.ImageUrl = "http://placehold.it/800x300";

			paths = pDal.getFeaturedImagePath(3, ref (log));
			if (paths.Count > 0)
				feature_s1.ImageUrl = paths[0];
			else
				feature_s1.ImageUrl = "http://placehold.it/800x300";

			paths = pDal.getFeaturedImagePath(3, ref (log));
			if (paths.Count > 1)
				feature_s2.ImageUrl = paths[1];
			else
				feature_s2.ImageUrl = "http://placehold.it/800x300";

			paths = pDal.getFeaturedImagePath(3, ref (log));
			if (paths.Count > 2)
				feature_s3.ImageUrl = paths[2];
			else 
				feature_s3.ImageUrl = "http://placehold.it/800x300";
		}
	}
}