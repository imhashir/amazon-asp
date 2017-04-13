using System;
using System.Data;
using System.Data.SqlClient;
using myAmazon_v1.DAL;

namespace myAmazon_v1
{
    public partial class BrowseCategories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			CategoriesDAL catDal = new CategoriesDAL();
			string log = "";
			DataTable table = catDal.getCategoriesList(ref (log), 1, null);
			if (log != "")
			{
				log_browse_category.Text += log;
				return;
			}
			CategoryDataList.DataSource = table;
            CategoryDataList.DataBind();
        }
    }
}