<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewBrand.aspx.cs" Inherits="myAmazon_v1.AdminPanel.InsertBrand" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server"  class="form-inline">
        <div class="container">
            <div class="jumbotron">        
                <div class="row">
                    <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <h3><b>Insert Brand Info</b></h3><br />
                            Name:  <asp:TextBox ID="id_brand_name" CssClass="form-control" runat="server"></asp:TextBox>
                            <br />
                            Category:<br />
                            <asp:DropDownList ID="id_category_name" CssClass="form-control" runat="server" DataSourceID="brand_category_db" DataTextField="Name" DataValueField="id">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="brand_category_db" runat="server" ConnectionString="<%$ ConnectionStrings:myAmazonConnectionString %>" SelectCommand="SELECT [id], [Name] FROM [Category]"></asp:SqlDataSource>
                            <br />
                            Description:<br/>
                            <asp:TextBox ID="id_brand_desc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox><br />
                            <asp:Button ID="id_submit_brand" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="Press_Submit" />
                            <br />
                            <asp:Label ID="id_log_brand" runat="server" Text=" "></asp:Label>
                        </div>
                    <div class="col-md-1"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
