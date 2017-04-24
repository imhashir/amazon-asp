<%@ Page Language="C#" Title="Browse Categories" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="BrowseCategories.aspx.cs" Inherits="myAmazon_v1.BrowseCategories"  %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Label runat="server" ID="log_browse_category"></asp:Label>
    <div>
        <asp:DataList ID="CategoryDataList" runat="server" BorderColor="#202020"
            BorderStyle="Solid" BorderWidth="5px" CellPadding="3" CellSpacing="2" Font-Bold="True"
            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="4" RepeatDirection="Horizontal">
            <HeaderStyle Font-Bold="True" Font-Size="Large" ForeColor="202020"
                HorizontalAlign="Center" VerticalAlign="Middle"/>
            <HeaderTemplate>
                Categories
            </HeaderTemplate>
            <ItemStyle BackColor="#dbdbdb" ForeColor="#202020" Height="250px" Width="150px" HorizontalAlign="Center"/>
            <ItemTemplate>
                <a href="BrowseProducts?CatId=<%# Eval("id") %>">
                <table>
                    <tr>
                        <td>
                            <asp:Image ID="id_category_image" runat="server" Width="95%" ImageUrl='<%# Eval("Image") %>'/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="id_category_label" runat="server" Text='<%# Eval("Name") %>' ForeColor="#202020" />
                        </td>
                    </tr>
                </table>
                </a>
            </ItemTemplate>
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        </asp:DataList>
    </div>
</asp:Content>