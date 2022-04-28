<%@ Page Title="" Language="C#" MasterPageFile="~/BackPages/BackPageMaster.Master" AutoEventWireup="true" CodeBehind="BackResults.aspx.cs" Inherits="Questionnaire.BackPages.BackResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <table border="1">
            <tr>
                <td>
                    <asp:Button ID="Button_Questionnaire" runat="server" Text="問卷" />
                </td>
                <td>
                    <asp:Button ID="Button_Question" runat="server" Text="問題" />
                </td>
                <td>
                    <asp:Button ID="Button_Result" runat="server" Text="填寫資料" />
                </td>
                <td>
                    <asp:Button ID="Button_BackStatisticalData" runat="server" Text="統計" />
                </td>
            </tr>
            <tr>
                <td colspan="4">

                    <asp:Button ID="Button1" runat="server" Text="匯出" />

                    <table border="1">
                        <tr>
                            <td>#</td>
                            <td>姓名</td>
                            <td>填寫時間</td>
                            <td>觀看細節</td>
                        </tr>
                        <asp:Literal ID="Literal_Table" runat="server"></asp:Literal>

                    </table>

                    <asp:Literal ID="Literal_Pager" runat="server" />
                </td>
            </tr>
        </table>

    </div>


</asp:Content>
