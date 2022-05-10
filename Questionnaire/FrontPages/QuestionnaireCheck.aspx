<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionnaireCheck.aspx.cs" Inherits="Questionnaire.FrontPages.QuestionnaireCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <table style="width:99%;">
            <tr>
                <td><h1>動態問卷填答系統</h1></td>
                <td align="right">
                    <asp:Label ID="Label_state" runat="server" Text="投票中"></asp:Label><br />
                    <asp:Label ID="Label_Time" runat="server" Text="123/456 "></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        <div>
            <table>
                <tr>
                    <td colspan="2">
                        <h3>
                            <asp:Label ID="Label_Title" runat="server" Text="問卷標題"></asp:Label>
                        </h3>
                    </td>
                </tr>
                <tr>
                    <td>姓名</td>
                    <td>
                        <asp:Label ID="Label_Name" runat="server" Text="Label" /></td>
                </tr>
                <tr>
                    <td>電話號碼</td>
                    <td>
                        <asp:Label ID="Label_Phone" runat="server" Text="Label" /></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td>
                        <asp:Label ID="Label_Email" runat="server" Text="Label" /></td>
                </tr>
                <tr>
                    <td>年齡</td>
                    <td>
                        <asp:Label ID="Label_Age" runat="server" Text="Label" /></td>
                </tr>
            </table>
        </div>

        <div>
            <asp:Literal ID="Literal_Question" runat="server"></asp:Literal>
        </div>
        <div>
            <%--<input type="button" onclick="javascript:window.history.go(-1);"value="取消" />--%>
            <asp:Button ID="Button_Cancel" runat="server" Text="取消" OnClick="Button_Cancel_Click"/>

            <asp:Button ID="Button_OK" runat="server" Text="確認" OnClick="Button_OK_Click"/>
        </div>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
