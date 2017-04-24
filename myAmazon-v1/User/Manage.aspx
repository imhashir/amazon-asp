<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="myAmazon_v1.User.Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<div class="" runat="server" id="id_log_div">
	</div>
	<div class="row" style="height:auto; width:100%;">
		<div class="col-md-4">
			<asp:Image runat="server" ID="id_user_image" CssClass="img-rounded" Width="250px" ImageUrl="~/ProductsData/Images/Default.jpg"/>
		</div>
		<div class="col-md-6">
			<h3><b>Username:</b> <asp:Label runat="server" ID="id_username"/></h3>
			First name: <asp:Textbox runat="server" CssClass="form-control" ID="id_user_fname"/><br/>
			Last name: <asp:Textbox runat="server" CssClass="form-control" ID="id_user_lname"/><br/>
			Contact Number: <asp:Textbox runat="server" CssClass="form-control" ID="id_user_number"/><br/>
			<b>Email:</b> <asp:Label runat="server" ID="id_user_email"/><br/>
			Password: <asp:Textbox runat="server" TextMode="Password" CssClass="form-control" ID="id_user_pass"/><br/>
			<asp:Button runat="server" ID="id_update_info" CssClass="btn btn-primary" Text="Update" OnClick="onUpdateUserInfo"/>
		</div>
	</div><br/>
	<h3><b>Request Credit</b></h3><br/>
	<asp:TextBox runat="server" ID="id_credit_value" CssClass="form-control" TextMode="SingleLine" />
	<asp:Button runat="server" ID="id_request_button" CssClass="btn btn-primary" Text="Update" OnClick="onRequestCredit"/>

</asp:Content>
