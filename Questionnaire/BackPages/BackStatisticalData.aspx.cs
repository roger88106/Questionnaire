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
    public partial class BackStatisticalData : System.Web.UI.Page
    {
        StatisticalDataManager _mgr = new StatisticalDataManager();
        QuestionnairesManager _Questionnairesmgr = new QuestionnairesManager();

        int questionnairesID;
        protected void Page_Load(object sender, EventArgs e)
        {
            Button_BackStatisticalData.Enabled = false;
            try
            {//排除掉ID被竄改成不是整數的情形
                questionnairesID = Convert.ToInt32(Request.QueryString["ID"]);

                //如果查不到，也當錯誤跳出
                if (!_Questionnairesmgr.SelectQuestionnaireIDinDatabase(0, questionnairesID))
                    throw new Exception("查無此問卷ID或權限不足");
            }
            catch (Exception)
            {
                questionnairesID = -1;
            }

            if (questionnairesID == -1)
            {
                //錯誤，跳出錯誤訊息
                Literal1.Text = "查無此問卷";
            }
            else
            {
                List<StatisticalDataModel> list = _mgr.GetStatisticalData(questionnairesID);

                Literal1.Text = "";

                int _i = 0;
                foreach (var item in list)
                {
                    string htmlText = _i+1+"." + item.Content + "<br />";

                    if (item.type == 1 || item.type == 2)
                    {
                        string[] option = item.option.Split(';');
                        int[] counts = item.answerCount.ToArray();
                        double[] percents = item.answerPercent.ToArray();

                        htmlText += "<table>";
                        int _i2 = 0;//迴圈數
                        foreach (var item3 in option)
                        {
                            htmlText += "<tr><td>" + option[_i2] + "&nbsp; </td><td align=\"right\">" + Math.Round(percents[_i2] * 100, 1) + "% &nbsp; </td><td> (" + counts[_i2] + ") </td></tr>";

                            _i2++;
                        }
                        htmlText += "</table>";
                    }
                    else //文字方塊不用統計
                        htmlText += "<p> &nbsp; - </p>";

                    Literal1.Text += htmlText + "<br />";

                    _i++;
                }
            }
            if (string.IsNullOrEmpty(Literal1.Text))
            {
                Literal1.Text = "此問卷沒有可以統計的資料";
            }
        }

        protected void Button_Questionnaire_Click(object sender, EventArgs e)
        {
            Response.Redirect($"BackQuestionnaire.aspx?ID={questionnairesID}");
        }

        protected void Button_Question_Click(object sender, EventArgs e)
        {
            Response.Redirect($"BackQuestion.aspx?ID={questionnairesID}");
        }

        protected void Button_Result_Click(object sender, EventArgs e)
        {
            Response.Redirect($"BackResults.aspx?ID={questionnairesID}");
        }
    }
}