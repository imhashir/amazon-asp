using System;
using System.Data;
using System.Data.SqlClient;
using myAmazon_v1.DAL;

namespace myAmazon_v1
{
    public partial class BrowseBrands : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BrandsDAL brandDal = new BrandsDAL();
			string log = "";
			DataTable table = brandDal.getBrandsList(ref (log), 1, null);
			if (log != "")
			{
				log_browse_brand.Text += log;
				return;
			}
			BrandDataList.DataSource = table;
            BrandDataList.DataBind();
		}
	}
}