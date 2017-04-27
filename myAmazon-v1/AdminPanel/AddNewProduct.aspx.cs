using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using myAmazon_v1.DAL;
using myAmazon_v1.Model;

namespace myAmazon_v1.AdminPanel
{
	public partial class AddNewProduct : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				string log = "";
				CategoriesDAL catDal = new CategoriesDAL();
				DataTable table = catDal.getCategories(ref (log), null, null);
				id_log_product.Text += log;
				log = "";
				id_category_name.DataSource = table;
				id_category_name.DataTextField = "Name";
				id_category_name.DataValueField = "id";
				id_category_name.DataBind();
				id_category_name.Items.Insert(0, new ListItem("--Select--", "NA"));
				id_brand_name.Items.Insert(0, new ListItem("--Select--", "NA"));

				if (Session["isEdit"] != null && Session["isEdit"].Equals("1"))
				{
					bool flag = true;
					Page.Title = "Edit Product";
					id_page_title.Text = "Edit Product";
					id_submit_product.Text = "Update";
					ProductDAL productDal = new ProductDAL();
					Product product = productDal.getProductDetails(ref (flag), ref (log), Session["ProductId"].ToString());
					id_product_name.Text = product.name;
					id_product_price.Text = product.price.ToString();
					id_category_name.SelectedValue = product.catId.ToString();
					populateBrandDropDown();
					id_brand_name.SelectedValue = product.brandId.ToString();
					if (product.image != "")
						id_product_image.ImageUrl = product.image;

					//File handling for Description
					string path = product.desc;
					if (path != null && !path.Equals("[No Description]"))
					{
						StreamReader file = new StreamReader(Server.MapPath(path));
						id_product_desc.Text = file.ReadToEnd();
						file.Close();
					}
					id_log_product.Text += log;
				}
				else
				{
					Session["ProductId"] = null;
				}

			}
		}

		protected void Press_Submit(object sender, EventArgs e)
		{
			bool isEdit = false;
			int id = 0;
			string log = "";
			if (Session["isEdit"] != null && Session["isEdit"].Equals("1"))
				isEdit = true;
			ProductDAL productDal = new ProductDAL();
			if (isEdit)
				id = Convert.ToInt32(Session["ProductId"]);
			if (productDal.updateProductInfo(ref (id), id_product_name.Text, id_brand_name.SelectedValue, id_category_name.SelectedValue, id_product_price.Text, isEdit, ref (log)))
			{
				if (!isEdit)
				{
					id_log_product.Text = "Successfully Inserted";
					id_product_name.Text = "";
					id_product_price.Text = "";
					id_category_name.SelectedIndex = 0;
					id_brand_name.SelectedIndex = 0;
				}
				else
					id_log_product.Text = "Successfully Updated";
			}
			else
				id_log_product.Text = "Error in insertion";

			string imagePath = null;
			string descPath = null;

			if (id_image_uploader.HasFile)
			{
				try
				{
					imagePath = "~/ProductsData/Images/" + id + DateTime.Now.Second.ToString() + ".jpg";
					id_image_uploader.SaveAs(Server.MapPath(imagePath));
				}
				catch (Exception ex)
				{
					log += ex.ToString();
				}
			}
			bool enterFile = false;

			if ((id_product_desc.Text != null && !isEdit) || isEdit)
				enterFile = true;

			if (enterFile)
			{
				descPath = "~/ProductsData/" + id + ".txt";
				try
				{
					bool newDesc = false;
					try
					{
						if (!Directory.Exists(Server.MapPath("~/ProductsData/")))
							Directory.CreateDirectory(Server.MapPath("~/ProductsData/"));
					}
					catch (Exception ex)
					{
						log += ex.ToString();
					}
					if (!File.Exists(Server.MapPath(descPath)))
					{
						File.Create(Server.MapPath(descPath)).Close();
						newDesc = true;
					}
					else if (isEdit)
						newDesc = true;

					File.WriteAllText(Server.MapPath(descPath), id_product_desc.Text);
					if (!isEdit || newDesc)
					{
						if (!newDesc)
							id_product_desc.Text = "";
					}
					else
					{
						Session["isEdit"] = "0";
						Response.Redirect(@"..\AdminPanel\ManageProducts.aspx");
					}
				}
				catch (Exception ex)
				{
					id_log_product.Text += ex.ToString();
				}
			}
			if (imagePath != null || descPath != null)
				productDal.updateImageAndDesc(id, imagePath, descPath, isEdit, ref (log));
			id_log_product.Text += log;
		}

		protected void id_category_name_SelectedIndexChanged(object sender, EventArgs e)
		{
			populateBrandDropDown();
		}

		private void populateBrandDropDown()
		{
			BrandsDAL brandDal = new BrandsDAL();
			string log = "";
			DataTable table = brandDal.getBrands(ref (log), "CategoryId", id_category_name.SelectedValue);
			id_brand_name.DataSource = table;
			id_brand_name.DataTextField = "Name";
			id_brand_name.DataValueField = "id";
			id_brand_name.DataBind();
			id_brand_name.Items.Insert(0, new ListItem("--Select--", "NA"));
		}
	}
}