<%@ Page Language="C#" AutoEventWireup="true" CodeFile="captcha.aspx.cs" Inherits="captcha" %>
<%@ Register src="~/Controls/ItCaptcha.ascx" tagname="ItCaptcha" tagprefix="uc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc:ItCaptcha ID="ucItCaptcha" runat="server" Visible="True" EnableViewState= "true" />
        <p>
        <asp:Button ID="btnCheck" runat="server" Text="check" OnClick="btnCheck_Click" />
        <asp:Label ID="lblCheck" runat="server" />
        </p>
    </div>
    </form>
</body>
</html>
