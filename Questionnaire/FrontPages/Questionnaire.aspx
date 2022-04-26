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

            <h3>
                <asp:Label ID="Label_Title" runat="server" Text="問卷標題"></asp:Label>
            </h3>
            <asp:Label ID="Content" runat="server" Text="問卷內文"></asp:Label>

            <table>
                <tr>
                    <td>姓名</td>
                    <td>
                        <asp:TextBox ID="TextBox_Name" runat="server">AAA</asp:TextBox></td>
                </tr>
                <tr>
                    <td>電話號碼</td>
                    <td>
                        <asp:TextBox ID="TextBox_Phone" runat="server" TextMode="Number">0912345678</asp:TextBox></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td>
                        <asp:TextBox ID="TextBox_Email" runat="server" TextMode="Email">1@3</asp:TextBox></td>
                </tr>
                <tr>
                    <td>年齡</td>
                    <td>
                        <asp:TextBox ID="TextBox_Age" runat="server" TextMode="Number">12</asp:TextBox></td>
                </tr>
            </table>
        </div>
        <div>

            <asp:Literal ID="Literal_Questions" runat="server"></asp:Literal>
            <table>
                <tr>
                    <td>
                        <input type="button" onclick="javascript:window.history.go(-1);" value="取消" />
                    </td>
                    <td>
                        <asp:Button Text="確認" runat="server" ID="Button_OK" OnClick="Button_OK_Click" />
                    </td>
                </tr>
            </table>

            <asp:Label Text="" runat="server" ID="Label1" />

        </div>
    </form>
</body>
</html>
