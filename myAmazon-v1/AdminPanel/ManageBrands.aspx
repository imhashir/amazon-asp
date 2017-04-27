<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ManageBrands.aspx.cs" Inherits="myAmazon_v1.AdminPanel.ManageBrands" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container">
        <div class="col-md-1"></div>
        <div class="col-md-10">
            <asp:ListView ID="brandListView" runat="server" ItemPlaceholderID="brandPlaceHolder">
                <LayoutTemplate>
                    <h3>Brands List</h3>
                    <table class="table table-hover">
                        <tr>
                            <th>ID</th><th>Name</th><th>Category</th><th>Description</th><th>Image</th><th>Edit</th><th>Delete</th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="brandPlaceHolder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <form method="post">
                        <input type="hidden" name="id" value="<%#Eval("id")%>"/>
                        <tr>
                            <td><%#Eval("id")%></td>
                            <td><%#Eval("Name")%></td>
                            <td><%#Eval("Category")%></td>
                            <td><%#Eval("Desc")%></td>
							<td><%#Eval("Image")%></td>
                            <td><input type="submit" class="btn btn-warning" name="Action" value="Edit"></td>
                            <td><input type="submit" class="btn btn-danger" name="Action" value="Delete"></td>
                        </tr>
                    </form>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col-md-1">
            <asp:Label ID="log_manage_brand" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
