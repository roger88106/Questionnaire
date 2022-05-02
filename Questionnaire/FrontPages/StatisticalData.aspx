<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticalData.aspx.cs" Inherits="Questionnaire.FrontPages.StatisticalData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>動態問卷填答系統</h1>
            <h3>
                <asp:Label ID="Label_Title" runat="server" Text="查無此問卷"></asp:Label>
            </h3>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
