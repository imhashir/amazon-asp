<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="myAmazon_v1.AdminPanel.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <title>Admin Panel</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
        <div class="row">
        </div>
        <div class="row">
            <div class="col-md-1"></div>
            <div class="col-md-10">
                <div class="jumbotron">
                    <b>Admin Home</b><br />
                    <h3><b>Add New data</b></h3>
                    <button type="button" class="btn btn-primary btn-lg" onclick="location.href = 'AddNewCategory.aspx';">Add New Category</button>
                    <button type="button" class="btn btn-primary btn-lg" onclick="location.href = 'AddNewBrand.aspx';">Add New Brand</button>
                    <button type="button" class="btn btn-primary btn-lg" onclick="location.href = 'AddNewProduct.aspx';">Add New Product</button>
                    <h3><b>View Data</b></h3>
                    <button type="button" class="btn btn-info btn-lg" onclick="location.href = 'ManageCategories.aspx';">View Categories</button>
                    <button type="button" class="btn btn-info btn-lg" onclick="location.href = 'ManageBrands.aspx';">View Brands</button>
                    <button type="button" class="btn btn-info btn-lg" onclick="location.href = 'ManageProducts.aspx';">View Products</button>
                </div>
            </div>
            <div class="col-md-1"></div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
