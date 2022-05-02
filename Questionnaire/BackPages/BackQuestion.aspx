<%@ Page Title="" Language="C#" MasterPageFile="~/BackPages/BackPageMaster.Master" AutoEventWireup="true" CodeBehind="BackQuestion.aspx.cs" Inherits="Questionnaire.BackPages.BackQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="1">
        <tr>
            <td>
                <asp:Button ID="Button_Questionnaire" runat="server" Text="問卷" OnClick="Button_Questionnaire_Click" />
            </td>
            <td>
                <asp:Button ID="Button_Question" runat="server" Text="問題" />
            </td>
            <td>
                <asp:Button ID="Button_Result" runat="server" Text="填寫資料" OnClick="Button_Result_Click"/>
            </td>
            <td>
                <asp:Button ID="Button_BackStatisticalData" runat="server" Text="統計" OnClick="Button_BackStatisticalData_Click" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="5">

                <table>
                    <tr>
                        <td>種類</td>
                        <td>
                            <asp:DropDownList ID="DropDownList_Question" runat="server">
                                <asp:ListItem>自訂問題</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
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
                            <asp:CheckBox ID="CheckBox_Required" runat="server" />
                            必填
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
                        <td>必填</td>
                        <td></td>
                    </tr>
                    <asp:Literal ID="Literal_QuestionTable" runat="server"></asp:Literal>
                </table>
            </td>
        </tr>
    </table>
    <table width="99%">
        <tr>
            <td width="33%"></td>
            <td width="33%">
                <asp:Button ID="Button_Cancle" runat="server" Text="取消" />
            </td>
            <td width="33%">
                <asp:Button ID="Button_OK" runat="server" Text="修改" OnClick="Button_OK_Click"/>
            </td>
        </tr>
    </table>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</asp:Content>
