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
    public partial class BackResultsDetail : System.Web.UI.Page
    {
        RespondentManager _mgr = new RespondentManager();
        QuestionManager _questionmgr = new QuestionManager();
        Guid respondentID;
        int questionnairesID;
        RespondentModel respondent;

        List<RespondentModel> respondentListz;


        protected void Page_Load(object sender, EventArgs e)
        {
            try//排除掉ID被竄改的情形
            {
                respondentID = Guid.Parse(Request.QueryString["ID"]);
                respondent = _mgr.GetRespondent(respondentID);

                questionnairesID = respondent.QuestionnairesID;

                TextBox_Name.Text = respondent.Name;
                TextBox_Phone.Text = respondent.PhoneNumber;
                TextBox_Email.Text = respondent.Email;
                TextBox_Age.Text = respondent.Age.ToString();
                Label_FillTime.Text = respondent.FillTime.ToString("yyyy/MM/dd HH:mm:ss");


                List<answer> answerList;
                //這邊只有這裡用， 所以不寫成Manager
                using (ContextModel contextModel = new ContextModel())
                {
                    var query =
                        from item in contextModel.Answers
                        join item2 in contextModel.Questions on item.QuestionID equals item2.QuestionID
                        where item.RespondentID == respondentID
                        orderby item2.QuestionOrder
                        select new answer
                        {
                            answerText = item.Answer1,
                            questionContent = item2.QuestionContent,
                            questionOption = item2.QuestionOptions,
                            questionType = item2.QuestionType,
                            required = item2.Required
                        };
                    answerList = query.ToList();
                }

                Literal_Main.Text = "";
                int _i = 1;
                foreach (var item in answerList)
                {
                    //判斷問題是不是必填
                    if (item.required)
                        Literal_Main.Text += $"{_i}. {item.questionContent}(必填)";
                    else
                        Literal_Main.Text += $"{_i}. {item.questionContent}";
                    Literal_Main.Text += "<br />";

                    //判斷問題種類(文字方塊、單選、複選)
                    if (item.questionType == 1 || item.questionType == 2)
                    {
                        string[] options;
                        string[] checkOption = new string[0];

                        //判斷選項有沒有值，有就轉成Array
                        if (!string.IsNullOrEmpty(item.questionOption))
                            options = item.questionOption.Split(';');
                        //如果沒選項就不用繼續，跳過這次迴圈
                        else
                            continue;

                        string a = item.answerText;
                        //判斷勾選的有沒有值，有就轉成Array
                        if (!string.IsNullOrEmpty(item.answerText))
                            checkOption = item.answerText.Split(',');
                        

                        string type = "radio";
                        if (item.questionType == 2)
                            type = "checkbox";


                        int _i2 = 0;
                        foreach (var option in options)
                        {
                            string isCheck = "";
                            //判斷該選項是否選擇
                            if (checkOption.Contains(_i2.ToString()))
                                isCheck = "checked";
                            Literal_Main.Text += $"<input type=\"{type}\" disabled=\"disabled\" {isCheck} />"+
                                option + "<br />";
                            _i2++;
                        }
                    }
                    else
                    {
                        Literal_Main.Text += $"<input type=\"text\" disabled=\"disabled\" value=\"{item.answerText}\" />";
                    }
                    Literal_Main.Text += "<br />";
                }

            }
            catch (Exception)
            {
                Literal_Main.Text = "讀取資料錯誤";
            }

            if (respondent != null || respondent != new RespondentModel())
            {

            }


        }

        private class answer
        {
            public string questionContent { get; set; }
            public string questionOption { get; set; }
            public int questionType { get; set; }
            public string answerText { get; set; }
            public bool required { get; set; }
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

        protected void Button_BackStatisticalData_Click(object sender, EventArgs e)
        {
            Response.Redirect($"BackStatisticalData.aspx?ID={questionnairesID}");
        }
    }
}