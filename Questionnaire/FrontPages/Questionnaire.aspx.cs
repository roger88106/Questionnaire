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
    public partial class Questionnaire : System.Web.UI.Page
    {
        int questionnairesID, questionCount;
        List<QuestionModel> questionList;
        QuestionManager _mgr = new QuestionManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            //questionnairesID = Convert.ToInt32(Request.QueryString["ID"]);

            questionnairesID = 1;//測試用
            questionList = _mgr.GetQuestion(questionnairesID);

            questionCount = questionList.Count;
            int _i = 0;//迴圈數
            string questionsHtml = "";

            foreach (var item in questionList)
            {
                questionsHtml += $"<p>{_i}. {item.QuestionContent.Trim()}</p>"; //題目的HTML

                if (item.QuestionType == 1 || item.QuestionType == 2) //單、複選
                {
                    string _type = "";
                    string[] Options = item.QuestionOptions.Split(';');

                    if (item.QuestionType == 1)
                        _type = "radio";
                    else
                        _type = "checkbox";

                    int _i2 = 0;//value用
                    foreach (string OptionString in Options)
                    {
                        questionsHtml += $"<input type=\"{_type}\" name=Questions_{_i} value = \"{_i2}\">{OptionString} <br />";
                        _i2++;
                    }
                }
                else //文字
                {
                    questionsHtml += $"<input type=\"text\" name=Questions_{_i}>";
                }
                _i++;
            }
            Literal_Questions.Text = "";//保險起見，先清空還原
            Literal_Questions.Text = questionsHtml;

            //之後把按鈕改成動態生成的，如果問卷生成出錯，按鈕就不要出現
        }

        protected void Button_OK_Click(object sender, EventArgs e)
        {
            //填答人基本資料格式確認

            //要保存的資料
            Guid RespondentID = Guid.NewGuid();

            RespondentModel respondent = new RespondentModel
            {
                RespondentID = RespondentID,
                QuestionnairesID = questionnairesID,
                Name = TextBox_Name.Text,
                PhoneNumber = TextBox_Phone.Text,
                Email = TextBox_Email.Text,
                Age = Convert.ToInt32(TextBox_Age.Text),
                FillTime = DateTime.Now
            };
            HttpContext.Current.Session["Questions_Respondent"] = respondent;

            //HttpContext.Current.Session["Questions_RespondentID"] = Guid.NewGuid();
            //HttpContext.Current.Session["Questions_QuestionnairesID"] = questionnairesID;
            //HttpContext.Current.Session["Questions_Name"] = TextBox_Name.Text;
            //HttpContext.Current.Session["Questions_PhoneNumber"] = TextBox_Phone.Text;
            //HttpContext.Current.Session["Questions_Email"] = TextBox_Email.Text;
            //HttpContext.Current.Session["Questions_Age"] = Convert.ToInt32(TextBox_Age.Text);
            //HttpContext.Current.Session["Questions_FillTime"] = DateTime.Now;

            List<AnswerModel> answerList = new List<AnswerModel>();

            //List<Guid> Questions_AnswerID = new List<Guid>();
            //List<Guid> Questions_QuestionID = new List<Guid>();
            //List<string> Questions_Answer = new List<string>();

            for (int i = 0; i < questionCount; i++)
            {
                string _answer = "";
                var item = questionList[i];
                if (item.QuestionType == 1 || item.QuestionType == 2) //單、複選的情形
                {
                    string[] _answerArray = Request.Form.GetValues($"Questions_{i}");
                    foreach (var _answerItem in _answerArray)
                    {
                        _answer += _answerItem + ",";
                    }
                    _answer = _answer.TrimEnd(',');
                }
                else//文字方塊的情形
                    _answer = Request.Form[$"Questions_{i}"].Trim();

                answerList.Add(new AnswerModel
                {
                    AnswerID = Guid.NewGuid(),
                    QuestionID = item.QuestionID,
                    QuestionnaireID = questionnairesID,
                    RespondentID = RespondentID,
                    Answer = _answer
                });
                //Questions_AnswerID.Add(Guid.NewGuid());
                //Questions_QuestionID.Add(item.QuestionID);
                //Questions_Answer.Add(_answer);
            }
            HttpContext.Current.Session["Questions_AnswerList"] = answerList;
            //HttpContext.Current.Session["Questions_AnswerID"] = Questions_AnswerID;
            //HttpContext.Current.Session["Questions_QuestionID"] = Questions_QuestionID;
            //HttpContext.Current.Session["Questions_Answer"] = Questions_Answer;

            Response.Redirect($"QuestionnaireCheck.aspx?ID={questionnairesID}");
        }

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrontIndex");
        }
    }
}