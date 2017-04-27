<%@ Page Title="Add New Brand" Language="C#" MasterPageFile="~/AdminPanel/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="AddNewBrand.aspx.cs" Inherits="myAmazon_v1.AdminPanel.AddNewBrand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container">
        <div class="jumbotron">        
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <h3><b><asp:Label ID="id_brand_title" runat="server" Text="Insert Brand Info"></asp:Label></b></h3>
                    Name:  <asp:TextBox ID="id_brand_name" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    Category:
                    <asp:DropDownList ID="id_category_name" CssClass="form-control" runat="server" AutoPostBack="True" DataTextField="Name" DataValueField="id">
                    </asp:DropDownList><br />
                    Description:<br/>
                    <asp:TextBox ID="id_brand_desc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox><br />
                    <div>
                        <asp:Image ID="id_brand_image" ImageUrl="~/ProductsData/Images/Default.jpg" runat="server" />
                        <asp:FileUpload ID="id_image_uploader" Height="20%" runat="server"/>
                    </div>
                    <asp:Button ID="id_submit_brand" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="Press_Submit" />
                    <br />
                    <asp:Label ID="id_log_brand" runat="server" Text=" "></asp:Label>
                </div>
                <div class="col-md-1"></div>
            </div>
        </div>
    </div>
</asp:Content>
