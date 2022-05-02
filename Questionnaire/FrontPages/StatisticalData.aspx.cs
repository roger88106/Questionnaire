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
    public partial class StatisticalData : System.Web.UI.Page
    {
        StatisticalDataManager _mgr = new StatisticalDataManager();
        QuestionnairesManager _Questionnairesmgr = new QuestionnairesManager();
        int questionnairesID;
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal1.Text = "";
            try
            {//排除掉ID被竄改成不是整數的情形
                questionnairesID = Convert.ToInt32(Request.QueryString["ID"]);

                //如果查不到，也當錯誤跳出(只查已經開放的)
                if (!_Questionnairesmgr.SelectQuestionnaireIDinDatabase(1, questionnairesID))
                    throw new Exception("查無此問卷ID或權限不足");
            }
            catch (Exception)
            {
                questionnairesID = -1;
            }

            if (questionnairesID != -1)
            {
                //設定標題
                QuestionnairesModel questionnaire = _Questionnairesmgr.GetQuestionnaire(1, questionnairesID);
                Label_Title.Text = questionnaire.QuestionnaireTital;

                List<StatisticalDataModel> list = _mgr.GetStatisticalData(questionnairesID);

                int _i = 0;

                string htmlText = "<table>";
                foreach (var item in list)
                {
                    htmlText += "<tr><td>" + (_i + 1) + "." + item.Content + "</td></tr>";

                    if (item.type == 1 || item.type == 2)
                    {
                        string[] option = item.option.Split(';');
                        int[] counts = item.answerCount.ToArray();
                        double[] percents = item.answerPercent.ToArray();

                        htmlText += "<tr><td><table>";
                        int _i2 = 0;
                        foreach (var item2 in option)
                        {
                            htmlText += "<tr><td>" + option[_i2] + "</tr></td>";
                            double percent = Math.Round(percents[_i2] * 100, 1);

                            //橫條圖的部分
                            htmlText += "<tr><td>" +
                                "<div style=\"border-width:4px;border-style:outset;border-color:#000; width: 500px; height: 20px;\">" +
                                $"<div style=\"background-color: #aaa; width: {percent}%; height: 100%;\"></div>" +
                                "</div>" +
                                $"</td><td align=\"right\">{percent}%</td><td> ({counts[_i2]})</td></tr>";
                            _i2++;
                        }
                        htmlText += "</table></tr></td>";
                    }
                    else
                    {
                        htmlText += "<tr><td>-</tr></td>";
                    }
                    htmlText += "<tr><td></tr></td>";

                    _i++;
                }
                htmlText += "</table>";

                Literal1.Text = htmlText;

                //判斷Literal裡面是否有內容，沒有內容就提示還沒人寫過
                if (Literal1.Text == "<table></table>")
                {
                    Literal1.Text = "<h4>這個問卷還沒有人寫過喔~</h4>";
                }
            }

        }
    }
}