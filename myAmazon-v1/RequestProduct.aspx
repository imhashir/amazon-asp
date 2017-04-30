<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequestProduct.aspx.cs" Inherits="myAmazon_v1.RequestProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<div class="row">
		<div class="col-md-2"></div>
		<div class="col-md-8">
			<div runat="server" ID="id_log_div"/>
			<h2>Request a Product</h2>
			Product Name: <asp:TextBox runat="server" ID="id_request_name" CssClass="form-control"></asp:TextBox>
			<asp:Button runat="server" ID="id_request_button" CssClass="btn btn-primary" Text="Request" OnClick="onRequestProduct"/>
		</div>
		<div class="col-md-2"></div>
	</div>
</asp:Content>
