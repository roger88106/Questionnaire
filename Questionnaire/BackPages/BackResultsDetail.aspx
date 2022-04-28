<%@ Page Title="" Language="C#" MasterPageFile="~/BackPages/BackPageMaster.Master" AutoEventWireup="true" CodeBehind="BackResultsDetail.aspx.cs" Inherits="Questionnaire.BackPages.BackResultsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <table>
            <tr>
                <td>姓名</td>
                <td>
                    <asp:TextBox ID="TextBox_Name" runat="server" Enabled="false"></asp:TextBox>
                </td>

                <td>手機</td>
                <td>
                    <asp:TextBox ID="TextBox_Phone" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Email</td>
                <td>
                    <asp:TextBox ID="TextBox_Email" runat="server" Enabled="false"></asp:TextBox>
                </td>

                <td>年齡</td>
                <td>
                    <asp:TextBox ID="TextBox_Age" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td colspan="2">
                    <asp:Label ID="Label_FillTime" runat="server" Text="Label"></asp:Label>
                </td>
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
