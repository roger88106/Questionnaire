<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrontIndex.aspx.cs" Inherits="Questionnaire.FrontPages.FrontIndex" %>

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

            <div>
                <table border="1">
                    <tr>
                        <td>問卷標題 
                            <asp:TextBox ID="TextBox_Search" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>開始/結束 
                            <asp:TextBox ID="TextBox_Start" runat="server" TextMode="Date"></asp:TextBox>
                            <asp:TextBox ID="TextBox_End" runat="server" TextMode="Date"></asp:TextBox>
                            <asp:Button ID="Button_Search" runat="server" Text="搜尋" OnClick="Button_Search_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table border="1">
                    <tr>
                        <td>#</td>
                        <td>問卷</td>
                        <td>狀態</td>
                        <td>開始時間</td>
                        <td>結束時間</td>
                        <td>觀看統計</td>
                    </tr>
                    <asp:Literal ID="Literal_Table" runat="server" />
                </table>
                <asp:Literal ID="Literal_Pager" runat="server" />

            </div>
        </div>
    </form>
</body>
</html>
