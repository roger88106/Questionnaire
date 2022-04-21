<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Questionnaire.aspx.cs" Inherits="Questionnaire.FrontPages.Questionnaire" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <h1>動態問卷填答系統</h1>

            <h3 id="Title">問卷標題</h3>
            <p id="Content">問卷內文</p>

            <table>
                <tr>
                    <td>姓名</td>
                    <td><asp:TextBox ID="TextBox_Name" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>電話號碼</td>
                    <td><asp:TextBox ID="TextBox_Phone" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td><asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>年齡</td>
                    <td><asp:TextBox ID="TextBox_Age" runat="server"></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Literal ID="Literal_Question" runat="server"></asp:Literal>

        </div>
    </form>
</body>
</html>
