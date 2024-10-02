<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CaptchaLoginApp.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login with CAPTCHA</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtUsername" runat="server" Placeholder="Username"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Password"></asp:TextBox>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        </div>
        
        <!-- CAPTCHA hiển thị khi login thất bại 3 lần -->
        <asp:Image ID="captchaImage" runat="server" Visible="false" />
        <asp:TextBox ID="txtCaptcha" runat="server" Visible="false" Placeholder="Enter CAPTCHA"></asp:TextBox>
        <asp:Button ID="btnSubmitCaptcha" runat="server" Visible="false" Text="Submit CAPTCHA" OnClick="btnSubmitCaptcha_Click" />
    </form>
</body>
</html>
