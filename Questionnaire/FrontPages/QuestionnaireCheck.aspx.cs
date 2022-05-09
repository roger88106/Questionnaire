using Questionnaire.Managers;
using Questionnaire.Models;
using Questionnaire.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.FrontPages
{
    public partial class QuestionnaireCheck : System.Web.UI.Page
    {
        int questionnairesID, questionCount;
        List<QuestionModel> questionList;
        RespondentModel respondent;
        List<AnswerModel> answersList = new List<AnswerModel>();
        QuestionManager _mgr = new QuestionManager();
        QuestionnairesManager _questionnairesManager = new QuestionnairesManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            Button_OK.Enabled = true;
            try
            {
                questionnairesID = Convert.ToInt32(Request.QueryString["ID"]);
                questionList = _mgr.GetQuestionList(questionnairesID);
                questionCount = questionList.Count;

                respondent = (RespondentModel)HttpContext.Current.Session["Questions_Respondent"];
                answersList = (List<AnswerModel>)HttpContext.Current.Session["Questions_AnswerList"];

                //如果Session內的ID跟QueryString的ID對不上，就防呆，跳過所有內容
                if (respondent == null)
                    throw new Exception("ID錯誤");
                if (questionnairesID != respondent.QuestionnairesID)
                    throw new Exception("ID錯誤");

                //設定問卷標題跟內文
                QuestionnairesModel questionnaire = _questionnairesManager.GetQuestionnaire(1, questionnairesID);
                Label_Title.Text = questionnaire.QuestionnaireTital;

                //顯示起始結束時間
                Label_Time.Text = questionnaire.StartTime.ToString("yyyy/MM/dd");
                if (questionnaire.EndTime.HasValue)
                    Label_Time.Text += " ~ " + questionnaire.EndTime.Value.ToString("yyyy/MM/dd");

                Label_Name.Text = respondent.Name;
                Label_Phone.Text = respondent.PhoneNumber;
                Label_Email.Text = respondent.Email;
                Label_Age.Text = respondent.Age.ToString();

                int _i = 0;//迴圈數
                string questionsHtml = "";
                foreach (var item in questionList)
                {
                    questionsHtml += $"<p>{_i + 1}.{item.QuestionContent}</p>";

                    if (item.QuestionType == 1 || item.QuestionType == 2)//單、多選的情況
                    {

                        if (answersList[_i].Answer != "") //防止非必填的選項的空字串導致下面 Split 出錯
                        { 
                            string[] _answersArray = answersList[_i].Answer.Split(',');//勾選的位置值
                            string[] _optionsArray = item.QuestionOptions.Split(';');//題目的文字
                            foreach (var _answersItem in _answersArray)
                            {
                                //取得勾選選項的對應文字
                                questionsHtml += $"<p>{_optionsArray[Convert.ToInt32(_answersItem)]}</p>";
                            }
                        }
                    }
                    else//文字方塊的情況
                        questionsHtml += $"<p>{answersList[_i].Answer}</p>";//直接輸出使用者輸入的文字
                    _i++;
                }

                Literal_Question.Text = questionsHtml;
            }
            catch (Exception ex)
            {
                Literal_Question.Text = "讀取錯誤，請重新填寫問卷";
                Button_OK.Enabled = false;
            }
        }
        protected void Button_OK_Click(object sender, EventArgs e)
        {
            if (Button_OK.Enabled == true)
            {
                try
                {
                    using (ContextModel contextModel = new ContextModel())
                    {
                        //把respondent變數的內容寫進資料庫
                        var ORM_Respondent = new Respondent
                        {
                            Age = respondent.Age,
                            Email = respondent.Email,
                            FillTime = respondent.FillTime,
                            Name = respondent.Name,
                            PhoneNumber = respondent.PhoneNumber,
                            QuestionnairesID = respondent.QuestionnairesID,
                            RespondentID = respondent.RespondentID
                        };
                        contextModel.Respondents.Add(ORM_Respondent);


                        //把answersList變數的內容都寫進資料庫
                        Answer ORM_Answers;
                        foreach (var item in answersList)
                        {
                            ORM_Answers = new Answer
                            {
                                Answer1 = item.Answer,
                                AnswerID = item.AnswerID,
                                QuestionID = item.QuestionID,
                                QuestionnaireID = item.QuestionnaireID,
                                RespondentID = item.RespondentID
                            };
                            contextModel.Answers.Add(ORM_Answers);
                        }

                        contextModel.SaveChanges();


                        //跳轉前先清除剛剛填寫的資料
                        Session.Remove("Questions_Respondent");
                        Session.Remove("Questions_AnswerList");
                        //跳回index
                        Response.Redirect("FrontIndex.aspx");
                    }
                }
                catch
                {
                    Label1.Text = "資料儲存錯誤，請再確認一次您的資料";
                }
            }
        }
    }
}