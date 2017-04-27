<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ManageCreditRequests.aspx.cs" Inherits="myAmazon_v1.AdminPanel.ManageCreditRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container">
		<h2><b>Handle Credit Requests</b></h2>
        <div class="col-md-1"></div>
        <div class="col-md-10">
            <asp:ListView ID="creditListview" runat="server" ItemPlaceholderID="creditPlaceHolder">
                <LayoutTemplate>
                    <h3>Credit Requests</h3>
                    <table class="table table-hover">
                        <tr>
                            <th>Username</th><th>Requested Amount</th><th>Accept</th><th>Reject</th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="creditPlaceHolder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <form method="post">
                        <input type="hidden" name="id" value="<%#Eval("Username")%>"/>
                        <tr>
							<input type="hidden" name="username" value="<%#Eval("Username")%>"/>
                            <td><%#Eval("Username")%></td>
                            <td><%#Eval("Amount")%></td>
                            <td><input type="submit" class="btn btn-success" name="Action" value="Accept"></td>
                            <td><input type="submit" class="btn btn-danger" name="Action" value="Reject"></td>
                        </tr>
                    </form>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col-md-1">
            <asp:Label ID="log_manage_requests" runat="server" Text=""></asp:Label>
        </div>
	</div>
</asp:Content>
