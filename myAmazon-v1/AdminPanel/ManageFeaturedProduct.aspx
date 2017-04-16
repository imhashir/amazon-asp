﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageFeaturedProduct.aspx.cs" Inherits="myAmazon_v1.AdminPanel.ManageFeaturedProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <title>Manage Featured Product</title>
</head>
<body>
    <form id="form1" runat="server" class="form-inline">
        <div class="container">
            <div class="jumbotron">        
                <div class="row">
                    <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <h3><b>Manage Featured Product</b></h3>
							Product Id: <asp:TextBox runat="server" ID="id_product" CssClass="form-control" TextMode="Number"/><br/>
							Level: <asp:TextBox runat="server" ID="id_level" CssClass="form-control" TextMode="Number"/>
							<asp:Button ID="id_submit_featured" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="Press_Submit"/><br />
                            <asp:Label ID="id_log_sponsor" runat="server" CssClass="alert-warning" Text=" "></asp:Label>
							<br/><br/>
							<asp:ListView ID="featuredListView" runat="server" ItemPlaceholderID="featuredPlaceHolder">
								<LayoutTemplate>
									<h3><b>Featured Products List</b></h3>
									<table class="table table-hover">
										<tr>
											<th>ProductId</th>
											<th>Name</th>
											<th>Level</th>
										</tr>
										<asp:PlaceHolder runat="server" ID="featuredPlaceHolder"></asp:PlaceHolder>
									</table>
								</LayoutTemplate>
								<ItemTemplate>
									<form method="post">
										<input type="hidden" name="id" value="<%#Eval("id")%>"/>
										<tr>
											<td><%#Eval("ProductId")%></td>
											<td><%#Eval("Name")%></td>
											<td><%#Eval("Level")%></td>
											<td><input type="submit" class="btn btn-danger" name="Action" value="Delete"></td>
										</tr>
									</form>
								</ItemTemplate>
							</asp:ListView>
                        </div>
                    <div class="col-md-1"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
