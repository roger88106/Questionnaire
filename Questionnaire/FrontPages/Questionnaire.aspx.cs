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
        QuestionnairesManager _questionnairesManager = new QuestionnairesManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            Button_OK.Enabled = true;
            Label1.Text = "";
            try//排除掉ID被竄改成不是整數的情形
            {
                questionnairesID = Convert.ToInt32(Request.QueryString["ID"]);
            }
            catch (Exception)
            {
                questionnairesID = -1;
            }

            //判斷此ID是否有對應的可填寫問卷
            bool hasThisQuestionnairesID = _questionnairesManager.SelectQuestionnaireIDinDatabase(1, questionnairesID);

            if (questionnairesID >= 0 && hasThisQuestionnairesID)
            {
                //設定問卷標題跟內文
                QuestionnairesModel questionnaire = _questionnairesManager.GetQuestionnaire(1, questionnairesID);
                Label_Title.Text = questionnaire.QuestionnaireTital;
                Label_Content.Text = questionnaire.QuestionnaireContent;

                //顯示起始結束時間
                Label_Time.Text = questionnaire.StartTime.ToString("yyyy/MM/dd");
                if (questionnaire.EndTime.HasValue)
                    Label_Time.Text += " ~ "+questionnaire.EndTime.Value.ToString("yyyy/MM/dd");

                //判斷是否是投票中的問卷，不是的話就關掉送出功能
                DateTime end;
                if (questionnaire.EndTime.HasValue)
                    end = questionnaire.EndTime.Value;
                else
                    end = DateTime.MaxValue.AddDays(-1);
                if (questionnaire.StartTime > DateTime.Now)
                {
                    Label_state.Text = "尚未開放";
                    Button_OK.Enabled = false;
                }
                else if (end.AddDays(1) < DateTime.Now)//到隔天的0點0分才結束
                {
                    Label_state.Text = "已結束";
                    Button_OK.Enabled = false;
                }
                else
                    Label_state.Text = "投票中";

                //顯示下方選項區
                questionList = _mgr.GetQuestionList(questionnairesID);
                questionCount = questionList.Count;

                int _i = 0;//迴圈數
                string questionsHtml = "";

                foreach (var item in questionList)
                {
                    string requiredString = "";
                    if (item.Required)
                        requiredString = "(必填)";

                    questionsHtml += $"<p>{_i+1}. {item.QuestionContent.Trim()}{requiredString}</p>"; //題目的HTML

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
            }
            else
            {
                Literal_Questions.Text = "";//保險起見，先清空還原
                Literal_Questions.Text = "問卷讀取出錯";
                Button_OK.Enabled = false;
            }
        }

        protected void Button_OK_Click(object sender, EventArgs e)
        {
            //填答人個資
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

            //判斷問卷填寫狀況，並做成方便儲存的資料結構
            bool required = true;//必填是否都有完成
            List<AnswerModel> answerList = new List<AnswerModel>();
            for (int i = 0; i < questionCount; i++)
            {
                string _answer = "";
                var item = questionList[i];
                var value = Request.Form.GetValues($"Questions_{i}");

                if (value == null)
                {
                    if (item.Required)//如果這題是必填
                        required = false;//有必填為空
                    else
                        _answer = "";//不是必填且沒填，給他空字串
                }
                //需要先判斷過是否為Null才能判斷是否為空字串，不然 value[0] 的地方會Error
                else if (string.IsNullOrEmpty(value[0].Trim()))
                {
                    if (item.Required)
                        required = false;
                    else
                        _answer = "";
                }
                else
                {
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
                }

                answerList.Add(new AnswerModel
                {
                    AnswerID = Guid.NewGuid(),
                    QuestionID = item.QuestionID,
                    QuestionnaireID = questionnairesID,
                    RespondentID = RespondentID,
                    Answer = _answer
                });
            }

            //判斷輸入的是否完整
            if (string.IsNullOrEmpty(TextBox_Name.Text) || string.IsNullOrEmpty(TextBox_Phone.Text) ||
                string.IsNullOrEmpty(TextBox_Email.Text) || string.IsNullOrEmpty(TextBox_Age.Text))
                Label1.Text = "個人資料請確實填寫";
            else if (TextBox_Phone.Text.Count() != 10)
            {
                Label1.Text = "手機號碼格式錯誤";
            }
            else if (!required)
                Label1.Text = "尚有必填題目為填寫";
            else
            {
                //確定沒問題，儲存進Session並跳轉到確認頁
                HttpContext.Current.Session["Questions_Respondent"] = respondent;
                HttpContext.Current.Session["Questions_AnswerList"] = answerList;
                Response.Redirect($"QuestionnaireCheck.aspx?ID={questionnairesID}");
            }
        }

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrontIndex.aspx");
        }
    }
}