using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.IO;
using myAmazon_v1.Model;
using myAmazon_v1.DAL;

namespace myAmazon_v1.AdminPanel
{
    public partial class AddNewCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["isEdit"] != null && Session["isEdit"].Equals("1"))
                {
                    string log = "";
                    bool flag = true;
                    id_category_title.Text = "Edit Category";
                    this.Title = "Edit Category";
                    id_submit_category.Text = "Update";
                    CategoriesDAL catDal = new CategoriesDAL();
                    Category category = catDal.getCategoryDetails(ref(flag), ref(log), "id", Session["CatId"].ToString());
                    if(flag)
                    {
                        id_category_name.Text = category.name;
                        if (category.image != "")
                            id_category_image.ImageUrl = category.image;

                        if (!category.desc.Equals(""))
                        {
                            StreamReader stream = new StreamReader(Server.MapPath(category.desc));
                            id_category_desc.Text = stream.ReadToEnd();
                            stream.Close();
                        }
                    }
                    else
                    {
                        id_log_category.Text += log;
                    }
                    
                }
            }
        }

        protected void On_Click(object sender, EventArgs e)
        {
            bool isEdit = false;
            string log = "";
            CategoriesDAL catDal = new CategoriesDAL();
            if(Session["isEdit"] != null && Session["isEdit"].Equals("1"))
            {
                isEdit = true;
            }
            
            int id = 0;

            if(isEdit)
                id = Convert.ToInt32(Session["CatId"]);

            catDal.updateCategoryInfo(id_category_name.Text, ref(id), isEdit, ref(log));
            string imagePath = "~/CategoriesData/Images/" + id + ".jpg";
            string descPath = "~/CategoriesData/" + id.ToString() + ".txt";
            if (id_image_uploader.HasFile)
            {
                try
                {
                    id_image_uploader.SaveAs(Server.MapPath(imagePath));
                }
                catch (Exception ex)
                {
                    id_log_category.Text += ex.ToString();
                }
            }

            try
            {
                bool newDesc = false;
                try
                {
                    if (!Directory.Exists(Server.MapPath("~/CategoriesData/")))
                        Directory.CreateDirectory(Server.MapPath("~/CategoriesData/"));
                } catch (Exception ex) {
                    id_log_category.Text += ex.ToString();
                }
                if (!File.Exists(Server.MapPath(descPath)))
                {
                    File.Create(Server.MapPath(descPath)).Close();
                    newDesc = true;
                }
                File.WriteAllText(Server.MapPath(descPath), id_category_desc.Text);
                if (!isEdit || newDesc)
                { 
                    if(!newDesc)
                        id_category_desc.Text = "";
                } 
                else
                {
                    Session["isEdit"] = "0";
                    Response.Redirect(@"..\AdminPanel\ManageCategories.aspx");
                }
            }
            
            catch (Exception ex) {
                log += ex.ToString();
            }
            catDal.updateCategoryImageAndDesc(id, imagePath, descPath, isEdit, ref (log));
            id_log_category.Text += log;
        }
    }
}