<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="myAmazon_v1.AdminPanel.ManageProducts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <title>Manage Products</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
        <div class="col-md-1"></div>
        <div class="col-md-10">
            <asp:ListView ID="productListView" runat="server" ItemPlaceholderID="productPlaceHolder">
                <LayoutTemplate>
                    <h3>Products List</h3>
                    <table class="table table-hover">
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Category</th>
                            <th>Brand</th>
                            <th>Description</th>
							<th>Image</th>
                            <th>Price</th>
                            <th>Edit</th>
                            <th>Delete</th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="productPlaceHolder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <form method="post">
                        <input type="hidden" name="id" value="<%#Eval("id")%>"/>
                        <tr>
                            <td><%#Eval("id")%></td>
                            <td><%#Eval("Name")%></td>
                            <td><%#Eval("Category")%></td>
                            <td><%#Eval("Brand")%></td>
                            <td><%#Eval("Desc")%></td>
                            <td><%#Eval("Image")%></td>
                            <td><%#Eval("Price")%></td>
                            <td><input type="submit" class="btn btn-warning" name="Action" value="Edit"></td>
                            <td><input type="submit" class="btn btn-danger" name="Action" value="Delete"></td>
                        </tr>
                    </form>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col-md-1">
            <asp:Label ID="log_manage_product" runat="server" Text=""></asp:Label>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
