<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Questionnaire.Models.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="front" runat="server" Text="前台" OnClick="front_Click"/>
            <asp:Button ID="back" runat="server" Text="後台" OnClick="back_Click" />
        </div>
    </form>
</body>
</html>
