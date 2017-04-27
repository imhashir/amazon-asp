<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="myAmazon_v1.AdminPanel.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <button type="button" class="btn btn-primary btn-lg" onclick="location.href = 'AddNewCategory';">Add New Category</button>
                    <button type="button" class="btn btn-primary btn-lg" onclick="location.href = 'AddNewBrand';">Add New Brand</button>
                    <button type="button" class="btn btn-primary btn-lg" onclick="location.href = 'AddNewProduct';">Add New Product</button>
                    <h3><b>View Data</b></h3>
                    <button type="button" class="btn btn-info btn-lg" onclick="location.href = 'ManageCategories';">View Categories</button>
                    <button type="button" class="btn btn-info btn-lg" onclick="location.href = 'ManageBrands';">View Brands</button>
                    <button type="button" class="btn btn-info btn-lg" onclick="location.href = 'ManageProducts';">View Products</button>
                    <button type="button" class="btn btn-info btn-lg" onclick="location.href = 'ManageFeaturedProduct';">Manage Featured Products</button>
					<h3><b>Manage Users</b></h3>
                    <button type="button" class="btn btn-warning btn-lg" onclick="location.href = 'ManageCreditRequests';">Manage Credit Requests</button>
				</div>
            </div>
            <div class="col-md-1"></div>
        </div>
    </div>
    </div>
</asp:Content>
