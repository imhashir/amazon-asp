<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signin.aspx.cs" Inherits="myAmazon_v1.User.Signin" %>
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
                        Username:  <asp:TextBox ID="id_username" runat="server" CssClass="form-control"></asp:TextBox><br />
                        Password:  <asp:TextBox ID="id_password" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox><br />
                        <asp:Button ID="id_submit_signin" runat="server" CssClass="btn btn-primary" Text="Signin" OnClick="id_submit_signin_Click"/><br />
                        <asp:Label ID="id_log_signin" runat="server" CssClass="alert-warning" Text=" "></asp:Label>
                    </div>
                <div class="col-md-1"></div>
            </div>
        </div>
    </div>
</asp:Content>
