﻿<%@ Page Language="C#" Title="Browse Products" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrowseProducts.aspx.cs" Inherits="myAmazon_v1.BrowseProducts" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        <b>Filter By: </b> Category <asp:DropDownList ID="CategoryDropdownList" runat="server" OnSelectedIndexChanged="DropdownSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        Brand <asp:DropDownList ID="BrandDropdownList" runat="server" OnSelectedIndexChanged="DropdownSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </div>
    <asp:Label runat="server" ID="log_browse_product"></asp:Label>
    <div>
        <asp:DataList ID="ProductDataList" runat="server" BorderColor="#202020"
            BorderStyle="Solid" BorderWidth="5px" CellPadding="3" CellSpacing="2" Font-Bold="True"
            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="4" RepeatDirection="Horizontal">
            <HeaderStyle Font-Bold="True" Font-Size="Large" ForeColor="202020"
                HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderTemplate>
                Products
            </HeaderTemplate>
            <ItemStyle BackColor="#dbdbdb" ForeColor="#202020" Height="249px" Width="150px" HorizontalAlign="Center"/>
            <ItemTemplate>
                <a href="ProductDetails?id=<%# Eval("id") %>">
                <table>
                    <tr>
                        <td>
                            <asp:Image ID="id_product_image" runat="server" Width="95%" ImageUrl='<%# Eval("Image") %>'/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="id_product_label" runat="server" Text='<%# Eval("Name") %>' ForeColor="#202020" />
                        </td>
                    </tr>
                </table>
                </a>
            </ItemTemplate>
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        </asp:DataList>
    </div>
</asp:Content>