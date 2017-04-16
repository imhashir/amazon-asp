<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="myAmazon_v1._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
	<link href="Content/shop-homepage.css" rel="stylesheet" />

    <div class="row">
		<h3>Our Platinum Sponsors Feature</h3>
		<div class="col-md-12">
			<asp:Image runat="server" ID="feature_p1" CssClass="slide-image" alt=""/>
		</div>
	</div>

	<div class="row">
		<h3>Our Gold Sponsors Feature</h3>
		<div class="col-md-6">
			<asp:Image runat="server" ID="feature_g1" CssClass="slide-image" alt=""/>
		</div>
		<div class="col-md-6">
			<asp:Image runat="server" ID="feature_g2" CssClass="slide-image" alt=""/>
		</div>
	</div>

	<div class="row">
		<h3>Our Silver Sponsors Feature</h3>
		<div class="col-md-4">
			<asp:Image runat="server" ID="feature_s1" CssClass="slide-image" alt=""/>
		</div>
		<div class="col-md-4">
			<asp:Image runat="server" ID="feature_s2" CssClass="slide-image" alt=""/>
		</div>
		<div class="col-md-4">
			<asp:Image runat="server" ID="feature_s3" CssClass="slide-image" alt=""/>
		</div>
	</div>
	
</asp:Content>
