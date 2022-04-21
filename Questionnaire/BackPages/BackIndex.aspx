<%@ Page Title="" Language="C#" MasterPageFile="~/BackPages/BackPageMaster.Master" AutoEventWireup="true" CodeBehind="BackIndex.aspx.cs" Inherits="Questionnaire.BackPages.BackIndex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <table border="1">
            <tr>
                <td>問卷標題 
                    <asp:TextBox ID="TextBox_Titel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>開始/結束 
                    <asp:TextBox ID="StartEnd" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox_End" runat="server"></asp:TextBox>
                    <asp:Button ID="TextBox_Search" runat="server" Text="搜尋" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table border="1">
            <tr>
                <td>  </td>
                <td>#</td>
                <td>問卷</td>
                <td>狀態</td>
                <td>開始時間</td>
                <td>結束時間</td>
                <td>觀看統計</td>
            </tr>
            <asp:Literal ID="Literal_" runat="server" />
        </table>
    </div>
</asp:Content>
