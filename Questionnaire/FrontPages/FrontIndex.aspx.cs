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
        List<QuestionnairesModel> questionnairesList;
        protected void Page_Load(object sender, EventArgs e)
        {
            questionnairesList = _mgr.GetQuestionnaires();
            GetTable(questionnairesList);
        }


        protected void Button_Search_Click(object sender, EventArgs e)
        {

            List<QuestionnairesModel> resultList = _searchHelper.SearchQuestionnaires(questionnairesList, TextBox_Search.Text, TextBox_Start.Text, TextBox_End.Text);
            Literal_Table.Text = "";//清空列表

            GetTable(resultList);


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


    }
}