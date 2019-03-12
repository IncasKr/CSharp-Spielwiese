<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppArchitect.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# this.List %>'>
                <ItemTemplate>
                    <%# (Container.DataItem as DTO.PersonEntity).ID %>, 
                <%# (Container.DataItem as DTO.PersonEntity).FirstName %>, 
                <%# (Container.DataItem as DTO.PersonEntity).LastName %><br />
                </ItemTemplate>
            </asp:Repeater>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
