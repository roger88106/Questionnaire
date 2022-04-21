<%@ Page Title="" Language="C#" MasterPageFile="~/BackPages/BackPageMaster.Master" AutoEventWireup="true" CodeBehind="BackResultsDetail.aspx.cs" Inherits="Questionnaire.BackPages.BackResultsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <table>
            <tr>
                <td>姓名</td>
                <td></td>
                <td>手機</td>
                <td></td>
            </tr>
            <tr>
                <td>Email</td>
                <td></td>
                <td>年齡</td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label_Date" runat="server" Text="Label">填寫時間: </asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <asp:Literal ID="Literal_Main" runat="server"></asp:Literal>
        </table>
    </div>
</asp:Content>
