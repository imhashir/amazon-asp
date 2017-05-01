using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myAmazon_v1.Model
{
	public class FeaturedProduct
	{
		public int id { get; set; }
		public string image { get; set; }

		public FeaturedProduct(int cId, string cImage)
		{
			id = cId;
			image = cImage;
		}
	}
}