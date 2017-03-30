<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="myAmazon_v1.User.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="jumbotron">        
            <div class="row">
                <div class="col-md-1"></div>
                    <div class="col-md-10">
                        <h3><b><asp:Label ID="id_page_title" runat="server" Text="Register a New Account"></asp:Label></b></h3>
                        First Name:  <asp:TextBox ID="id_user_fname" runat="server" CssClass="form-control"></asp:TextBox><br />
                        Last Name:  <asp:TextBox ID="id_user_lname" runat="server" CssClass="form-control"></asp:TextBox><br />
                        Username:  <asp:TextBox ID="id_username" runat="server" CssClass="form-control"></asp:TextBox><br />
                        Email:  <asp:TextBox ID="id_email" runat="server" CssClass="form-control"></asp:TextBox><br />
                        Password:  <asp:TextBox ID="id_password" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox><br />
                        Contact Number:  <asp:TextBox ID="id_cnumber" runat="server" CssClass="form-control"></asp:TextBox><br />
                        <div>
                            <asp:Image ID="id_cimage" Width="200px" ImageUrl="~/ProductsData/Images/Default.jpg" runat="server" />
                            <asp:FileUpload ID="id_image_uploader" runat="server" onchange="if (confirm('Upload ' + this.value + '?')) this.form.submit();"/>
                        </div>
                        <asp:Button ID="id_submit_signup" runat="server" CssClass="btn btn-primary" Text="Signup" OnClick="id_submit_signup_Click"/><br />
                        <asp:Label ID="id_log_signup" runat="server" CssClass="alert-warning" Text=" "></asp:Label>
                    </div>
                <div class="col-md-1"></div>
            </div>
        </div>
    </div>
</asp:Content>
