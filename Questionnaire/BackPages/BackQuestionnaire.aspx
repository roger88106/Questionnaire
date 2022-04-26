<%@ Page Title="" Language="C#" MasterPageFile="~/BackPages/BackPageMaster.Master" AutoEventWireup="true" CodeBehind="BackQuestionnaire.aspx.cs" Inherits="Questionnaire.BackPages.BackQuestionnaire" %>

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
                <td></td>
            </tr>
            <tr>
                <td colspan="5">

                    <table>
                        <tr>
                            <td>問卷名稱</td>
                            <td>
                                <asp:TextBox ID="TextBox_QuestionnaireName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>描述內容</td>
                            <td>
                                <asp:TextBox ID="TextBox_Content" runat="server" TextMode="MultiLine" Rows="5" />
                            </td>
                        </tr>
                        <tr>
                            <td>開始時間</td>
                            <td>
                                <asp:TextBox ID="TextBox_Start" runat="server" TextMode="Date"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>結束時間</td>
                            <td>
                                <asp:TextBox ID="TextBox_End" runat="server" TextMode="Date"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:CheckBox ID="CheckBox_State" runat="server" />
                                已啟用
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
                                <asp:Button ID="Button_OK" runat="server" Text="修改" OnClick="Button_OK_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
