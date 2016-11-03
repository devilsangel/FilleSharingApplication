<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadView.aspx.cs" Inherits="FileSharingApplication.uploadView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="Form1" method="post" enctype="multipart/form-data" runat="server">
        <INPUT type=file id=File1 name=File1 runat="server" />
        <br>
        <input type="submit" id="Submit1" value="Upload" runat="server" />
        <div>
    <h1>Downloads</h1>
    <asp:Repeater id="DataGrid" runat="server" OnItemCommand="Repeater_btn" >

        <ItemTemplate>
            <table cellspacing=5>
                <tr>
                    <td><asp:LinkButton  ID="Filename" Text= '<%# Container.DataItem.ToString() %>' visible="true" runat="server" CommandName='<%# Container.DataItem.ToString() %>' CommandArgument="dwn"/> </td>
                    <td>
                        <asp:DropDownList ID="userlist" runat="server">
                        </asp:DropDownList>
                        <asp:LinkButton  ID="grant" Text= "grant"  runat="server" CommandName='<%# Container.DataItem.ToString() %>' CommandArgument="bt"/>
                    </td>
                </tr>
            </table>
          </ItemTemplate>

    </asp:Repeater>
    </div>
    </form>
</body>
</html>
