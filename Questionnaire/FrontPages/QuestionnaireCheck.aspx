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
            <table>
                <tr>
                    <td>姓名</td>
                    <td><asp:Label ID="Label_Name" runat="server" Text="Label" /></td>
                </tr>
                <tr>
                    <td>電話號碼</td>
                    <td><asp:Label ID="Label_Phone" runat="server" Text="Label" /></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td><asp:Label ID="Label_Email" runat="server" Text="Label" /></td>
                </tr>
                <tr>
                    <td>年齡</td>
                    <td><asp:Label ID="Label_Age" runat="server" Text="Label" /></td>
                </tr>
            </table>
        </div>

        <div>
            <asp:Literal ID="Literal_Question" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
