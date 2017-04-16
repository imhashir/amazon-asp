<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="myAmazon_v1.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	

	<div class="row" style="height:auto; width:100%;">
		<div class="col-md-4">
			<asp:Image runat="server" ID="id_product_image" CssClass="img-rounded" Width="250px" ImageUrl="~/ProductsData/Images/Default.jpg"/>
		</div>
		<div class="col-md-8">
			<h2><asp:Label runat="server" ID="id_product_name"/></h2><br/>
			<h4>Category: <asp:Label runat="server" ID="id_product_category"/></h4>
			<h4>Brand: <asp:Label runat="server" ID="id_product_brand"/></h4>
			<h3>Price: Rs. <asp:Label runat="server" ID="id_product_price"/></h3>
		</div>
	</div><br/>
	<div class="row" style="width:100%;">
		<div class="container">
			<div class="jumbotron">
				<h2><u>Description</u></h2><br/>
				<asp:Label runat="server" ID="id_desc_text"/>
			</div>
		</div>
	</div>

</asp:Content>
