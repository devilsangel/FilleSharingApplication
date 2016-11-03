<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginView.aspx.cs" Inherits="FileSharingApplication.loginView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Username:</td>
                    <td>
                        <asp:TextBox ID="email" runat="server"/>
                        <asp:RequiredFieldValidator ID="rfvUser" ErrorMessage="Please enter email" ControlToValidate="email" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td>
                        <asp:TextBox ID="pwd" runat="server" TextMode="Password"/>
                        <asp:RequiredFieldValidator ID="rfvPWD" runat="server" ControlToValidate="pwd" ErrorMessage="Please enter Password"/>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" />
                        <asp:Button ID="Button1" runat="server" Text="Register" onclick="btnRegister_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
