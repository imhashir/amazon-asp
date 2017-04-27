<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="AddNewCategory.aspx.cs" Inherits="myAmazon_v1.AdminPanel.AddNewCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container">
        <div class="jumbotron">        
            <div class="row">
                <div class="col-md-1"></div>
                    <div class="col-md-10">
                        <h3><b><asp:Label ID="id_category_title" runat="server" Text="Add New Category"></asp:Label></b></h3>
                        Name: <asp:TextBox ID="id_category_name" CssClass="form-control" runat="server"></asp:TextBox><br />
                        Description:<br/>
                        <asp:TextBox ID="id_category_desc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox><br />
                        <div>
                            <asp:Image ID="id_category_image" ImageUrl="~/ProductsData/Images/Default.jpg" runat="server" />
                            <asp:FileUpload ID="id_image_uploader" Height="20%" runat="server"/>
                        </div>
                        <asp:Button ID="id_submit_category" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="On_Click"/>
                        <br />
                        <asp:Label ID="id_log_category" runat="server" Text=" "></asp:Label>
                    </div>
                <div class="col-md-1"></div>
            </div>
        </div>
    </div>
</asp:Content>
