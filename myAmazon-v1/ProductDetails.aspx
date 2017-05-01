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
	<link href="Content/star-rating.min.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>  
	<script src="Scripts/star-rating.js"></script>
	<div class="" runat="server" id="id_log_div">
	</div>
	<div class="row" style="height:auto; width:100%;">
		<div class="col-md-4">
			<asp:Image runat="server" ID="id_product_image" CssClass="img-rounded" Width="250px" ImageUrl="~/ProductsData/Images/Default.jpg"/>
		</div>
		<div class="col-md-6">
			<h2><asp:Label runat="server" ID="id_product_name"/></h2><br/>
			<h4>Category: <asp:Label runat="server" ID="id_product_category"/></h4>
			<h4>Brand: <asp:Label runat="server" ID="id_product_brand"/></h4>
			<h4>Availability: <asp:Label runat="server" ID="id_product_available"/></h4>
			<h3>Price: Rs. <asp:Label runat="server" ID="id_product_price"/></h3>
			<asp:HiddenField ID="id_product" runat="server" />
		</div>
		<div class="col-md-2">
			Quantity <asp:TextBox runat="server" TextMode="Number" ID="id_product_quantity"/>
			<asp:Button runat="server" ID="button_buy_product" CssClass="form-control btn btn-primary" OnClick="onBuyProduct" Text="Buy this Product"/><br/><br/>
			<asp:Button runat="server" ID="button_addto_wishlist" CssClass="form-control btn btn-primary" OnClick="onAddToWishlist" Text="Add to Wishlist"/> <br/>
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
			
			<asp:DataList ID="CommentDataList" runat="server" BorderColor="#202020" Font-Bold="True"
				Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="1" RepeatDirection="Vertical">
				<ItemStyle BackColor="#FFFFFF" ForeColor="#202020" Width="100%" HorizontalAlign="Center"/>
				<ItemTemplate>
					<div style="float:left; padding-top:1em;">
						<asp:Image ID="id_product_image" runat="server" Width="60px" ImageUrl='<%# Eval("Image") %>'/>
					</div>
					<div style="float:right; padding-left:1em;">
						<table>
							<tr>
								<td>
									<b><h5><asp:Label ID="id_name_commentor" runat="server" Text='<%# Eval("Name") %>' /></h5></b>
								</td>
							</tr>
							<tr>
								<td>
									<h6><asp:Label ID="id_comment_desc" runat="server" Text='<%# Eval("text") %>' /></h6>
								</td>
							</tr>
						</table>
					</div>
				</ItemTemplate>
			</asp:DataList>
		</div>
	</div>

</asp:Content>
