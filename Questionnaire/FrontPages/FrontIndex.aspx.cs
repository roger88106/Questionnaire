using Questionnaire.Helpers;
using Questionnaire.Managers;
using Questionnaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.FrontPages
{
    public partial class FrontIndex : System.Web.UI.Page
    {
        private QuestionnairesManager _mgr = new QuestionnairesManager();
        private SearchHelper _searchHelper = new SearchHelper();
        private PaginationHelper _paginationHelper = new PaginationHelper();
        List<QuestionnairesModel> allQuestionnairesList, ShowQuestionnairesList, NowPageQuestionnairesList;//保存用(主要用來提供查詢)、顯示用(例如查詢後修改)、現在畫面上的
        int TotalPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            Literal_Table.Text = "";//都先清空，防止狀態保留

            allQuestionnairesList = _mgr.GetQuestionnaires(1);//產生保存用的List
            ShowQuestionnairesList = allQuestionnairesList;

            GetTableData();
        }

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            string Url = "FrontIndex.aspx?";
            if (!string.IsNullOrEmpty(TextBox_Search.Text.Trim()))//如果有值，就保存
                Url += "Keyword=" + TextBox_Search.Text;

            if (!string.IsNullOrEmpty(TextBox_Start.Text))
                Url += "&StartTime=" + TextBox_Start.Text;

            if (!string.IsNullOrEmpty(TextBox_End.Text))
                Url += "&EndTime=" + TextBox_End.Text;
            Response.Redirect(Url);
            //GetTableData();
        }

        //輸出製作表格用的HTML(前後端表格內容不一樣，因此沒抽成相同的方法)
        private void GetTable(List<QuestionnairesModel> list)
        {
            if (list != null)
            {
                int _i = 0;//迴圈數
                string state;
                foreach (var item in list)
                {
                    if (item.QuestionnaireState > 0)
                    {
                        if (item.StartTime > DateTime.Now)
                            state = "尚未開放";
                        else if (item.EndTime.AddDays(1) < DateTime.Now)//到隔天的0點0分才結束
                            state = "已結束";
                        else
                            state = "投票中";


                        Literal_Table.Text += $"<tr>" +
                            $"<td>{item.QuestionnaireID}</td>" +
                            $"<td><a href=\"6\">{item.QuestionnaireTital}</a></td>" +
                            $"<td>{state}</td>" +
                            $"<td>{item.StartTime.ToString("yyyy/MM/dd")}</td>" +
                            $"<td>{item.EndTime.ToString("yyyy/MM/dd")}</td>" +
                            $"<td><a href=\"#\">前往</td>" +
                            $"</tr>";
                    }
                    _i += 1;
                }
            }
        }

        //主要功能
        private void GetTableData()
        {
            string keyword = Request.QueryString["Keyword"];
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







//        private QuestionnairesManager _mgr = new QuestionnairesManager();
//        private SearchHelper _searchHelper = new SearchHelper();
//        private PaginationHelper _paginationHelper = new PaginationHelper();
//        List<QuestionnairesModel> allQuestionnairesList, SearchQuestionnairesList, NowPageQuestionnairesList;//保存用、查詢結果用、現在顯示的
//        int TotalPage;




//        protected void Page_Load(object sender, EventArgs e)
//        {
//            Literal_Table.Text = "";//每次進頁面都先清空，防止狀態保留
//            allQuestionnairesList = _mgr.GetQuestionnaires(1);

//            TotalPage = _paginationHelper.GetPages(allQuestionnairesList.Count);//取得總頁數

//            int nowPage;
//            //取得當前頁碼，並嘗試轉換成INT
//            if (int.TryParse(Request.QueryString["page"], out nowPage))
//            {
//                if (nowPage < 1)//小於一就預設為1
//                    nowPage = 1;
//                else if (nowPage > TotalPage)//超出範圍，就給最大值
//                    nowPage = TotalPage;
//            }
//            else//如果轉換不成，就給預設值為1
//            {
//                nowPage = 1;
//            }

//            Literal_Pager.Text = _paginationHelper.GetLiteral_PagerHtml(nowPage, TotalPage);

//            NowPageQuestionnairesList = _paginationHelper.GetPageList(allQuestionnairesList, nowPage);//取得當前頁面列表
//            GetTable(NowPageQuestionnairesList);
//        }


//        protected void Button_Search_Click(object sender, EventArgs e)
//        {
//            SearchQuestionnairesList = _searchHelper.SearchQuestionnaires(allQuestionnairesList, TextBox_Search.Text, TextBox_Start.Text, TextBox_End.Text);
//            Literal_Table.Text = "";//清空列表

//            GetTable(SearchQuestionnairesList);
//        }


//        //輸出製作表格用的HTML(前後端表格內容不一樣，因此沒抽成相同的方法)
//        private void GetTable(List<QuestionnairesModel> list)
//        {
//            if (list != null)
//            {
//                int _i = 0;//迴圈數
//                string state;
//                foreach (var item in list)
//                {
//                    if (item.QuestionnaireState > 0)
//                    {
//                        if (item.StartTime > DateTime.Now)
//                            state = "尚未開放";
//                        else if (item.EndTime.AddDays(1) < DateTime.Now)//到隔天的0點0分才結束
//                            state = "已結束";
//                        else
//                            state = "投票中";


//                        Literal_Table.Text += $"<tr>" +
//                            $"<td>{item.QuestionnaireID}</td>" +
//                            $"<td><a href=\"6\">{item.QuestionnaireTital}</a></td>" +
//                            $"<td>{state}</td>" +
//                            $"<td>{item.StartTime.ToString("yyyy/MM/dd")}</td>" +
//                            $"<td>{item.EndTime.ToString("yyyy/MM/dd")}</td>" +
//                            $"<td><a href=\"#\">前往</td>" +
//                            $"</tr>";
//                    }
//                    _i += 1;
//                }
//            }
//        }


//    }
//}