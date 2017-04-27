<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="myAmazon_v1.AdminPanel.ManageProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div>
        <div class="container">
        <div class="col-md-1"></div>
        <div class="col-md-10">
            <asp:ListView ID="productListView" runat="server" ItemPlaceholderID="productPlaceHolder">
                <LayoutTemplate>
                    <h3>Products List</h3>
                    <table class="table table-hover" style="display:block">
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Category</th>
                            <th>Brand</th>
                            <th>Description</th>
							<th>Image</th>
                            <th>Price</th>
                            <th>Stock</th>
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
							<td>
								<input type="text" size="4" name="quantity" value="<%#Eval("Quantity")%>">
								<input type="submit" class="btn btn-primary" name="Action" value="Update">
							</td>
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
</asp:Content>
