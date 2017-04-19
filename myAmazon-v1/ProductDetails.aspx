<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="myAmazon_v1.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<script>  
       $(document).ready(function () {  
           $("#input-21a").on("rating.change", function (event, value, caption) {
           	var myHidden = $('#<%=id_rating_input.ClientID%>').val(value);
           });  
       });  
   </script> 
	<link href="Content/star-rating.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>  
	<script src="Scripts/star-rating.js"></script>
	<asp:Label runat="server" ID="id_log_productinfo" />
	<div class="row" style="height:auto; width:100%;">
		<div class="col-md-4">
			<asp:Image runat="server" ID="id_product_image" CssClass="img-rounded" Width="250px" ImageUrl="~/ProductsData/Images/Default.jpg"/>
		</div>
		<div class="col-md-8">
			<h2><asp:Label runat="server" ID="id_product_name"/></h2><br/>
			<h4>Category: <asp:Label runat="server" ID="id_product_category"/></h4>
			<h4>Brand: <asp:Label runat="server" ID="id_product_brand"/></h4>
			<h3>Price: Rs. <asp:Label runat="server" ID="id_product_price"/></h3>
			<asp:HiddenField ID="id_product" runat="server" />
		</div>
	</div><br/>
	<div class="row" style="width:100%;">
		<div class="col-md-8" style="height:100%;">
			<h2><u>Description</u></h2>
			<h5><asp:Label runat="server" ID="id_product_desc"/></h5>
		</div>
		<div class="col-md-4" style="height:100%;">
			<h3><u>Rate</u></h3>
			<asp:HiddenField ID="id_rating_input" runat="server" />  
			<input id="input-21a" value="0" type="number" class="rating" data-symbol="*" min=0 max=5 step=1 data-size="sm" >   
			<h3><u>Comment</u></h3>
			<asp:Textbox runat="server" ID="id_product_comment" CssClass="form-control" TextMode="MultiLine"/>
			<asp:Button runat="server" ID="id_product_review" Text="Submit" CssClass="form-control btn btn-primary" OnClick="onSubmitReview"/>
		</div>
	</div>

</asp:Content>
