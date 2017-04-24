using System;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using myAmazon_v1.DAL;

namespace myAmazon_v1
{
    public partial class BrowseProducts : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myAmazonConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
			if (!Page.IsPostBack)
			{
				string log = "";

				CategoriesDAL cDal = new CategoriesDAL();
				DataTable tableCat = cDal.getCategoriesList(ref (log), 0, null);
				CategoryDropdownList.DataTextField = "Name";
				CategoryDropdownList.DataValueField = "id";
				CategoryDropdownList.DataSource = tableCat;
				CategoryDropdownList.DataBind();
				CategoryDropdownList.Items.Insert(0, new ListItem("--Select--", "NA"));
				populateBrandDropDown();
				if (Request.QueryString["CatId"] != null)
				{
					getProductTable(Convert.ToInt32(Request.QueryString["CatId"]), null);
					CategoryDropdownList.SelectedValue = Request.QueryString["CatId"];
				}
				else if(Request.QueryString["BrandId"] != null)
				{
					getProductTable(null, Convert.ToInt32(Request.QueryString["BrandId"]));
					BrandDropdownList.SelectedValue = Request.QueryString["BrandId"];
				} 
				else
				{
					getProductTable(null, null);
				}
			}
		}

        private void getProductTable(Nullable<int> categoryId, Nullable<int> brandId) {
			string where = null;
			if (categoryId != null || brandId != null)
            {
                where += "WHERE ";
                if (categoryId != null)
					where += " CatId = " + categoryId;
                if (brandId != null)
                {
                    if (categoryId != null)
						where += " AND ";
					where += " BrandId = " + brandId;
                }
            }
			ProductDAL pDal = new ProductDAL();
			string log = "";
			DataTable table = pDal.getProductList(ref(log), 1, where);
            ProductDataList.DataSource = table;
            ProductDataList.DataBind();
        }

        private void populateBrandDropDown()
        {
			DataTable table = new DataTable();
			string log = "", where = null;
			BrandsDAL bDal = new BrandsDAL();
			if (CategoryDropdownList.SelectedValue != "NA")
				where += "WHERE CatId = " + CategoryDropdownList.SelectedValue;
			table = bDal.getBrandsList(ref(log), 0, where);
			BrandDropdownList.DataTextField = "Name";
			BrandDropdownList.DataValueField = "id";
			BrandDropdownList.DataSource = table;
            BrandDropdownList.DataBind();
            BrandDropdownList.Items.Insert(0, new ListItem("--Select--", "NA"));
        }

        protected void ButtonFilterProducts_Click(object sender, EventArgs e)
        {
            Nullable<int> catId = null;
            Nullable<int> brandId = null;

            if (!CategoryDropdownList.SelectedValue.Equals("NA"))
            {
                catId = Convert.ToInt32(CategoryDropdownList.SelectedValue);
            }
            if (!BrandDropdownList.SelectedValue.Equals("NA"))
            {
                brandId = Convert.ToInt32(BrandDropdownList.SelectedValue);
            }
            getProductTable(catId, brandId);
        }

        protected void DropdownSelectedIndexChanged(object sender, EventArgs e)
        {
            Nullable<int> catId = null;
            Nullable<int> brandId = null;

            if (!CategoryDropdownList.SelectedValue.Equals("NA"))
            {
                catId = Convert.ToInt32(CategoryDropdownList.SelectedValue);
            }
            if (!BrandDropdownList.SelectedValue.Equals("NA"))
            {
                brandId = Convert.ToInt32(BrandDropdownList.SelectedValue);
            }
            populateBrandDropDown();
            getProductTable(catId, brandId);
        }
    }
}