using Questionnaire.Helpers;
using Questionnaire.Managers;
using Questionnaire.Models;
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
        List<QuestionnairesModel> questionnairesList;

        protected void Page_Load(object sender, EventArgs e)
        {
            questionnairesList = _mgr.GetQuestionnaires();
            GetTable(questionnairesList);
        }


        protected void Button2_Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("/BackPage/BackQuestionnaire");
        }

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(TextBox_Search.Text))
            //{
            //    Literal_Table.Text = "";//清空列表
            //    GetTable(questionnairesList);
            //}
            //else
            //{
            //    List<QuestionnairesModel> resultList = _searchHelper.SearchQuestionnaires(questionnairesList, TextBox_Search.Text, TextBox_Start.Text, TextBox_End.Text);
            //    Literal_Table.Text = "";//清空列表

            //    GetTable(resultList);
            //}
            List<QuestionnairesModel> resultList = _searchHelper.SearchQuestionnaires(questionnairesList, TextBox_Search.Text, TextBox_Start.Text, TextBox_End.Text);
            Literal_Table.Text = "";//清空列表

            GetTable(resultList);
        }

        private void GetTable(List<QuestionnairesModel> list)
        {
            if (list != null)
            {
                int _i = 0;//迴圈數
                string state;
                foreach (var item in list)
                {
                    if (item.QuestionnaireState < 0)
                        state = "開放";
                    else
                        state = "已關閉";

                    Literal_Table.Text += $"<tr>" +
                        $"<td></td>" + //這裡要動態加入勾選方塊
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

    }
}