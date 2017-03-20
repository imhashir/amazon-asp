<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="BrowseProducts.aspx.cs" Inherits="myAmazon_v1.BrowseProducts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <title>Browse Products</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label runat="server" ID="log_browse_product"></asp:Label>
    <div>
        <asp:DataList ID="ProductDataList" runat="server" BackColor="#57d0ff" BorderColor="#0098d2"
            BorderStyle="None" BorderWidth="3px" CellPadding="3" CellSpacing="2" Font-Bold="True"
            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="4" RepeatDirection="Horizontal">
            <HeaderStyle Font-Bold="True" Font-Size="Large" ForeColor="White"
                HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderTemplate>
                Products
            </HeaderTemplate>
            <ItemStyle BackColor="#b7ebff" ForeColor="#8C4510" Height="250px" Width="150px" HorizontalAlign="Center"/>
            <ItemTemplate>
                <a href="?id=<%# Eval("id") %>">
                <table>
                    <tr>
                        <td>
                            <asp:Image ID="id_product_image" runat="server" Width="95%" ImageUrl='<%# Eval("Image") %>'/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="id_product_label" runat="server" Text='<%# Eval("Name") %>' ForeColor="#000099" />
                        </td>
                    </tr>
                </table>
                </a>
            </ItemTemplate>
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        </asp:DataList>
    </div>
    </form>
</body>
</html>