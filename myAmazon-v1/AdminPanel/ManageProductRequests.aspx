<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ManageProductRequests.aspx.cs" Inherits="myAmazon_v1.AdminPanel.ManageProductRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container">
		<h2><b>Handle Product Requests</b></h2>
        <div class="col-md-1"></div>
        <div class="col-md-10">
            <asp:ListView ID="requestsListView" runat="server" ItemPlaceholderID="requestPlaceHolder">
                <LayoutTemplate>
                    <h3>Categories List</h3>
                    <table class="table table-hover">
                        <tr>
                            <th>ID</th><th>Username</th><th>Customer Name</th><th>Request</th><th>Request</th><th>Completed</th><th>Decline</th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="requestPlaceHolder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <form method="post">
                        <input type="hidden" name="id" value="<%#Eval("id")%>"/>
                        <input type="hidden" name="CustomerId" value="<%#Eval("CustomerId")%>"/>

                        <tr>
                            <td><%#Eval("id")%></td>
                            <td><%#Eval("CustomerId")%></td>
                            <td><%#Eval("Name")%></td>
							<td><%#Eval("Desc")%></td>
                            <td><%#Eval("DateOfRequest")%></td>
                            <td><input type="submit" class="btn btn-warning" name="Action" value="Completed"></td>
                            <td><input type="submit" class="btn btn-danger" name="Action" value="Decline"></td>
                        </tr>
                    </form>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col-md-1">
            <asp:Label ID="log_manage_request" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
