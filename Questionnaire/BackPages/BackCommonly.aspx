<%@ Page Title="" Language="C#" MasterPageFile="~/BackPages/BackPageMaster.Master" AutoEventWireup="true" CodeBehind="BackCommonly.aspx.cs" Inherits="Questionnaire.BackPages.BackCommonly" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="1">
        <tr>
            <td colspan="5">

                <table>
                    <tr>
                        <td>問題</td>
                        <td>
                            <asp:TextBox ID="TextBox_Question" runat="server"></asp:TextBox>
                            <asp:DropDownList ID="DropDownList_Type" runat="server" OnSelectedIndexChanged="DropDownList_Type_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>文字</asp:ListItem>
                                <asp:ListItem>單選</asp:ListItem>
                                <asp:ListItem>複選</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>回答</td>
                        <td>
                            <asp:TextBox ID="TextBox_Answer" runat="server"></asp:TextBox>(多個答案以 ; 分隔) 
                        </td>
                        <td>
                            <asp:Button ID="Button_Add" runat="server" Text="加入" OnClick="Button_Add_Click" />
                        </td>
                    </tr>
                </table>

                <asp:Button ID="Button_Delete" runat="server" Text="刪除" OnClick="Button_Delete_Click" />
                <table border="1">
                    <tr>
                        <td></td>
                        <td>#</td>
                        <td>問題</td>
                        <td>種類</td>
                        <td>修改</td>
                    </tr>
                    <asp:Literal ID="Literal_QuestionTable" runat="server"></asp:Literal>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
