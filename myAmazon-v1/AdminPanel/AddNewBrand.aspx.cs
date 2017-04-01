using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using myAmazon_v1.DAL;
using myAmazon_v1.Model;

namespace myAmazon_v1.AdminPanel
{
    public partial class InsertBrand : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string log = "";
                bool flag = true;
                CategoriesDAL catDal = new CategoriesDAL();
                id_category_name.DataSource = catDal.getCategories(ref(log), null, null);
                id_category_name.DataTextField = "Name";
                id_category_name.DataValueField = "id";
                id_category_name.DataBind();
                id_category_name.Items.Insert(0, new ListItem("--Select--", "NA"));
                
                if(Session["isEdit"] != null && Session["isEdit"].Equals("1"))
                {
                    this.Title = "Edit Brand";
                    id_brand_title.Text = "Edit Brand";
                    id_submit_brand.Text = "Update";
                    BrandsDAL brandDal = new BrandsDAL();
                    Brand brand = brandDal.getBrandDetails(ref(flag), ref(log), "id", Session["BrandId"].ToString());
                    if (flag)
                    {
                        id_brand_name.Text = brand.name;
                        id_category_name.SelectedValue = brand.categoryId.ToString();

                        if (!brand.image.Equals(""))
                            id_brand_image.ImageUrl = brand.image;

                        if (!brand.desc.Equals(""))
                        {
                            StreamReader stream = new StreamReader(Server.MapPath(brand.desc));
                            id_brand_desc.Text = stream.ReadToEnd();
                            stream.Close();
                        }
                    }
                    else {
                        id_log_brand.Text += log;
                    }
                }
            }
        }

        protected void Press_Submit(object sender, EventArgs e)
        {
            string log = "";
            bool isEdit = false;
            int id = 0;

            BrandsDAL brandDal = new BrandsDAL();

            if (Session["isEdit"] != null && Session["isEdit"].Equals("1"))
                isEdit = true;

            brandDal.updateBrandInfo(id_brand_name.Text, ref (id), id_category_name.SelectedValue, isEdit, ref(log));

            string imagePath = "~/BrandsData/Images/" + id + ".jpg";
            string descPath = "~/BrandsData/" + id.ToString() + ".txt";

            if (id_image_uploader.HasFile) {
                try
                {
                    id_image_uploader.SaveAs(Server.MapPath(imagePath));
                }
                catch (Exception ex)
                {
                    log += ex.ToString();
                }
            }

            try
            {
                bool newDesc = false;
                try
                {
                    if (!Directory.Exists(Server.MapPath("~/BrandsData/")))
                        Directory.CreateDirectory(Server.MapPath("~/BrandsData/"));
                }
                catch (Exception ex)
                {
                    id_log_brand.Text += ex.ToString();
                }
                if (!File.Exists(Server.MapPath(descPath)))
                {
                    File.Create(Server.MapPath(descPath)).Close();
                    newDesc = true;
                }
                File.WriteAllText(Server.MapPath(descPath), id_brand_desc.Text);
                if (!isEdit || newDesc)
                {
                    if(!newDesc)
                        id_brand_desc.Text = "";
                    brandDal.updateImageAndDesc(id, imagePath, descPath, isEdit, ref(log));
                }
                else
                {
                    Session["isEdit"] = "0";
                    Response.Redirect(@"..\AdminPanel\ManageBrands.aspx");
                }
            }
            catch (Exception ex)
            {
                log += ex.ToString();
            }

            id_log_brand.Text += log;
        }

    }
}