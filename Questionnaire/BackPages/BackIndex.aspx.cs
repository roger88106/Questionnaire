using Questionnaire.Helpers;
using Questionnaire.Managers;
using Questionnaire.Models;
using Questionnaire.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.BackPages
{
    public partial class BackIndex : System.Web.UI.Page
    {
        private QuestionnairesManager _mgr = new QuestionnairesManager();
        private SearchHelper _searchHelper = new SearchHelper();
        private PaginationHelper _paginationHelper = new PaginationHelper();
        List<QuestionnairesModel> allQuestionnairesList, ShowQuestionnairesList, NowPageQuestionnairesList;//保存用(主要用來提供查詢)、顯示用(例如查詢後修改)、現在畫面上的
        int TotalPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            Literal_Table.Text = "";//都先清空，防止狀態保留

            allQuestionnairesList = _mgr.GetQuestionnaires(0);//產生保存用的List
            ShowQuestionnairesList = allQuestionnairesList;

            GetTableData();
        }



        protected void Button2_Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("/BackPage/BackQuestionnaire");
        }

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            string Url = "BackIndex.aspx?";
            if (!string.IsNullOrEmpty(TextBox_Search.Text.Trim()))//如果有值，就保存
                Url += "Keyword="+  TextBox_Search.Text;

            if (!string.IsNullOrEmpty(TextBox_Start.Text))
                Url += "&StartTime=" + TextBox_Start.Text ;

            if (!string.IsNullOrEmpty(TextBox_End.Text))
                Url += "&EndTime=" + TextBox_End.Text;
            Response.Redirect(Url);
            //GetTableData();
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            string request = Request.Form["checkBox"];//取得ChechBox勾選的值，形式是 值,值,值
            if (!string.IsNullOrEmpty(request))
            {
                string[] requestArray = request.Split(',');
            
                foreach (string item in requestArray)
                {
                    QuestionnairesModel Questionnaires = NowPageQuestionnairesList[Convert.ToInt32(item)];
                    _mgr.DeleteQuestionnaires(Questionnaires.QuestionnaireID);
                }
            }

            Literal_Table.Text = "";//清空防止狀態保留
            allQuestionnairesList = _mgr.GetQuestionnaires(0);//更新總列表
            GetTableData();
        }

        private void GetTable(List<QuestionnairesModel> list)
        {
            if (list != null)
            {
                int _i = 0;//迴圈數
                string state;
                foreach (var item in list)
                {
                    if (item.QuestionnaireState > 0)
                        state = "開放";
                    else
                        state = "已關閉";

                    Literal_Table.Text += $"<tr>" +
                        $"<td><input type=\"checkbox\" name=\"checkBox\" value = \"{_i}\" /></td>" +
                        $"<td>{item.QuestionnaireID}</td>" +
                        $"<td><a href=\"#\">{item.QuestionnaireTital}</a></td>" +
                        $"<td>{state}</td>" +
                        $"<td>{item.StartTime.ToString("yyyy/MM/dd")}</td>" +
                        $"<td>{item.EndTime.ToString("yyyy/MM/dd")}</td>" +
                        $"<td><a href=\"#\">前往</td>" +
                        $"</tr>";

                    _i += 1;
                }
            }
        }

        //主要功能
        private void GetTableData()
        {
            string keyword  = Request.QueryString["Keyword"];
            string startTime = Request.QueryString["startTime"];
            string endTime = Request.QueryString["endTime"];

            Literal_Table.Text = ""; //清空
            ShowQuestionnairesList = _searchHelper.SearchQuestionnaires
                (allQuestionnairesList, keyword, startTime, endTime);//取得展示用的列表(如果有keyword就是展示查詢結果，沒有就是全部)

            TotalPage = _paginationHelper.GetPages(ShowQuestionnairesList.Count);//取得總頁數

            int nowPage;
            //取得當前頁碼，並嘗試轉換成INT
            if (int.TryParse(Request.QueryString["page"], out nowPage))
            {
                if (nowPage < 1)//小於一就預設為1
                    nowPage = 1;
                else if (nowPage > TotalPage)//超出範圍，就給最大值
                    nowPage = TotalPage;
            }
            else//如果轉換不成，就給預設值為1
            {
                nowPage = 1;
            }

            Literal_Pager.Text = _paginationHelper.GetLiteral_PagerHtml(nowPage, TotalPage, keyword, startTime, endTime);

            NowPageQuestionnairesList = _paginationHelper.GetPageList(ShowQuestionnairesList, nowPage);//取得當前頁面列表
            GetTable(NowPageQuestionnairesList);
        }
    }
}