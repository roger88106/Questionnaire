<%@ Page Title="" Language="C#" MasterPageFile="~/BackPages/BackPageMaster.Master" AutoEventWireup="true" CodeBehind="BackStatisticalData.aspx.cs" Inherits="Questionnaire.BackPages.BackStatisticalData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table border="1">
        <tr>
            <td>
                <asp:Button ID="Button_Questionnaire" runat="server" Text="問卷" OnClick="Button_Questionnaire_Click" />
            </td>
            <td>
                <asp:Button ID="Button_Question" runat="server" Text="問題" OnClick="Button_Question_Click" />
            </td>
            <td>
                <asp:Button ID="Button_Result" runat="server" Text="填寫資料" OnClick="Button_Result_Click" />
            </td>
            <td>
                <asp:Button ID="Button_BackStatisticalData" runat="server" Text="統計" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
