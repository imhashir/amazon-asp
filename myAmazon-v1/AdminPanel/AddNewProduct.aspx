<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewProduct.aspx.cs" Inherits="myAmazon_v1.AdminPanel.AddNewProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <title>Add New Product</title>
</head>
<body>
    <form id="form1" runat="server" class="form-inline">
        <div class="container">
            <div class="jumbotron">        
                <div class="row">
                    <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <h3><b>Insert New Product</b></h3>
                            Name:  <asp:TextBox ID="id_product_name" runat="server" CssClass="form-control"></asp:TextBox><br />
                            Description:<br/>
                            <asp:TextBox ID="id_product_desc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox><br />
                            Category: <br />
                            <asp:DropDownList ID="id_category_name" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="id" OnSelectedIndexChanged="id_category_name_SelectedIndexChanged" AutoPostBack="True" >
                            </asp:DropDownList>
                            <br />
                            Brand: <br />
                            <asp:DropDownList ID="id_brand_name" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="id">
                            </asp:DropDownList>
                            <br />
                            Price:<br />
                            <asp:TextBox ID="id_product_price" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox><br />
                            <asp:Button ID="id_submit_product" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="Press_Submit"/><br />
                            <asp:Label ID="id_log_product" runat="server" CssClass="alert-warning" Text=" "></asp:Label>
                        </div>
                    <div class="col-md-1"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
