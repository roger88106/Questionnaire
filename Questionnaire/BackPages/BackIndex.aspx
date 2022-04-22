<%@ Page Title="" Language="C#" MasterPageFile="~/BackPages/BackPageMaster.Master" AutoEventWireup="true" CodeBehind="BackIndex.aspx.cs" Inherits="Questionnaire.BackPages.BackIndex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
        <asp:Button ID="Button_Delete" runat="server" Text="刪除" />
        <asp:Button ID="Button2_Add" runat="server" Text="新增" OnClick="Button2_Add_Click" />
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
            <asp:Literal ID="Literal_Table" runat="server" />
        </table>
    </div>
</asp:Content>
