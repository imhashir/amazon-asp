using System;
using System.Data;
using System.Web;
using myAmazon_v1.DAL;

namespace myAmazon_v1.AdminPanel
{
    public partial class ManageBrands : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
				BrandsDAL brandDal = new BrandsDAL();
				string log = "";
				DataTable table = brandDal.getBrandsList(ref(log), 2, null);
				if(log != "")
				{
					log_manage_brand.Text = log;
					return;
				}
                brandListView.DataSource = table;
                brandListView.DataBind();
            }

            if (Request.HttpMethod.ToString() == "POST")
            {
                switch (HttpContext.Current.Request.Form["Action"])
                {
                    case "Delete":
                        {
							string log = "";
							BrandsDAL brandDal = new BrandsDAL();
							brandDal.deleteBrand(HttpContext.Current.Request["id"], ref(log));
							log_manage_brand.Text += log;
                            break;
                        }
                    case "Edit":
                        {
                            Session["isEdit"] = "1";
                            Session["BrandId"] = HttpContext.Current.Request["id"];
                            Response.Redirect(@"..\AdminPanel\AddNewBrand.aspx");
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}