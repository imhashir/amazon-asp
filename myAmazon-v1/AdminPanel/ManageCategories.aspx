<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCategories.aspx.cs" Inherits="myAmazon_v1.AdminPanel.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <title>Manage Categories</title>
</head>
<body>
    <div class="container">
        <div class="col-md-1"></div>
        <div class="col-md-10">
            <asp:ListView ID="categoriesListView" runat="server" ItemPlaceholderID="categoryPlaceHolder">
                <LayoutTemplate>
                    <h3>Categories List</h3>
                    <table class="table table-hover">
                        <tr>
                            <th>ID</th><th>Name</th><th>Description</th><th>Edit</th><th>Delete</th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="categoryPlaceHolder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <form method="post">
                        <input type="hidden" name="id" value="<%#Eval("id")%>"/>
                        <tr>
                            <td><%#Eval("id")%></td>
                            <td><%#Eval("Name")%></td>
                            <td><%#Eval("Desc")%></td>
                            <td><input type="submit" class="btn btn-warning" name="Action" value="Edit"></td>
                            <td><input type="submit" class="btn btn-danger" name="Action" value="Delete"></td>
                        </tr>
                    </form>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col-md-1">
            <asp:Label ID="log_manage_cat" runat="server" Text=""></asp:Label>
        </div>
    </div>
</body>
</html>
