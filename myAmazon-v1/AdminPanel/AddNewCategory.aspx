<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewCategory.aspx.cs" Inherits="myAmazon_v1.AdminPanel.AddNewCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <title>Add New Category</title>
</head>
<body>
    <form id="form1" runat="server"  class="form-inline">
        <div class="container">
            <div class="jumbotron">        
                <div class="row">
                    <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <h3><b>Add New Category</b></h3><br />
                            Name: <asp:TextBox ID="id_category_name" CssClass="form-control" runat="server"></asp:TextBox><br />
                            Description:<br/>
                            <asp:TextBox ID="id_category_desc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox><br />
                            <asp:Button ID="id_submit_category" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="On_Click"/>
                            <br />
                            <asp:Label ID="id_log_category" runat="server" Text=" "></asp:Label>
                        </div>
                    <div class="col-md-1"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
