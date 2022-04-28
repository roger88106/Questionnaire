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
    public partial class BackQuestionnaire : System.Web.UI.Page
    {
        int questionnairesID;//讀到對應的ID就是修改模式，讀不到的話把ID轉成-1就是新增模式
        QuestionnairesManager _mgr = new QuestionnairesManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            //嘗試讀取ID         
            try
            {//排除掉ID被竄改成不是整數的情形
                questionnairesID = Convert.ToInt32(Request.QueryString["ID"]);
            }
            catch (Exception)
            {
                questionnairesID = -1;
            }

            if (!IsPostBack)
            { 
                //頁面初始化
                Label1.Text = "";
                Button_Question.Enabled = true;
                this.Button_Result.Visible = true;
                this.Button_BackStatisticalData.Visible = true;

                //自己頁面的按鈕要關掉
                Button_Questionnaire.Enabled = false;

                //如果讀取到QueryString的ID值，且此ID是合法且找的到資料的話才進入編輯模式           
                try
                {//排除掉ID為空或被竄改成不是整數的情形
                    questionnairesID = Convert.ToInt32(Request.QueryString["ID"]);
                }
                catch (Exception)
                {
                    questionnairesID = -1;
                }

                //判斷此ID是否有對應的問卷
                bool hasThisQuestionnairesID = _mgr.SelectQuestionnaireIDinDatabase(0, questionnairesID);
                if (questionnairesID >= 0 && hasThisQuestionnairesID)
                {
                    //有的話就進入編輯模式
                    Mode_Revise();
                }
                else
                {
                    questionnairesID = -1;
                    //沒有的話就進入新增模式
                    Mode_New();
                }
            }
            
        }

        //新增模式
        private void Mode_New()
        {
            Button_Question.Enabled = false;
            this.Button_Result.Visible = false;
            this.Button_BackStatisticalData.Visible = false;
            Button_OK.Text = "下一步";
            Button_OK.OnClientClick = "Button_OK_Click";
        }

        //編輯模式
        private void Mode_Revise()
        {
            QuestionnairesModel questionnaire = _mgr.GetQuestionnaire(0, questionnairesID);
            if (questionnaire == null)
            {
                //提示錯誤
            }
            else
            {
                string endTime;
                if (questionnaire.EndTime.HasValue)
                    endTime = questionnaire.EndTime.Value.ToString("yyyy-MM-dd");
                else
                    endTime = "";

                bool Checked;
                if (questionnaire.QuestionnaireState == 1)
                    Checked = true;
                else
                    Checked = false;

                TextBox_QuestionnaireName.Text = questionnaire.QuestionnaireTital;
                TextBox_Content.Text = questionnaire.QuestionnaireContent;
                TextBox_Start.Text = questionnaire.StartTime.ToString("yyyy-MM-dd");
                TextBox_End.Text = endTime;
                CheckBox_State.Checked = Checked;
            }

        }

        protected void Button_OK_Click(object sender, EventArgs e)
        {
            //新增模式的按鈕(下一步)
            if (questionnairesID == -1)
            {

                if (string.IsNullOrEmpty(TextBox_QuestionnaireName.Text) ||
                    string.IsNullOrEmpty(TextBox_Content.Text) ||
                    string.IsNullOrEmpty(TextBox_Start.Text))
                {//有漏寫的(end可以不寫)
                    Label1.Text = "問卷資料請確實填寫";
                }
                else
                {
                    DateTime startTime = Convert.ToDateTime(TextBox_Start.Text);
                    DateTime? endTime = null;
                    if (!string.IsNullOrEmpty(TextBox_End.Text))
                        endTime = Convert.ToDateTime(TextBox_End.Text);

                    if (endTime != null && startTime > endTime)
                    {//如果有填寫結束時間且起始時間大於結束時間
                        Label1.Text = "起始時間不可以大於結束時間";
                    }
                    else
                    {//都通過的話，儲存並存入session
                        int state = 0;
                        if (CheckBox_State.Checked)
                            state = 1;

                        QuestionnairesModel questionnaires = new QuestionnairesModel
                        {
                            QuestionnaireID = -1,
                            QuestionnaireTital = TextBox_QuestionnaireName.Text.Trim(),
                            QuestionnaireContent = TextBox_Content.Text.Trim(),
                            StartTime = startTime,
                            EndTime = endTime,
                            QuestionnaireState = state
                        };
                        //存入session並跳轉頁面
                        HttpContext.Current.Session["QuestionnairesData"] = questionnaires;
                        Response.Redirect("BackQuestion.aspx");
                    }
                }

            }
            //編輯模式的按鈕(送出)
            else
            {

                if (string.IsNullOrEmpty(TextBox_QuestionnaireName.Text) ||
                    string.IsNullOrEmpty(TextBox_Content.Text) ||
                    string.IsNullOrEmpty(TextBox_Start.Text))
                {//有漏寫的(end可以不寫)
                    Label1.Text = "問卷資料請確實填寫";
                }
                else
                {
                    DateTime startTime = Convert.ToDateTime(TextBox_Start.Text);
                    DateTime? endTime = null;
                    if (!string.IsNullOrEmpty(TextBox_End.Text))
                        endTime = Convert.ToDateTime(TextBox_End.Text);

                    if (endTime != null && startTime > endTime)
                    {//如果有填寫結束時間且起始時間大於結束時間
                        Label1.Text = "起始時間不可以大於結束時間";
                    }
                    else
                    {
                        //都通過的話就修改
                        int state = 0;
                        if (CheckBox_State.Checked)
                            state = 1;

                        string QuestionnaireContent = TextBox_Content.Text.Trim();

                        QuestionnairesModel newQuestionnaire = new QuestionnairesModel
                        {
                            QuestionnaireID = questionnairesID,
                            QuestionnaireTital = TextBox_QuestionnaireName.Text.Trim(),
                            QuestionnaireContent = TextBox_Content.Text.Trim(),
                            StartTime = startTime,
                            EndTime = endTime,
                            QuestionnaireState = state
                        };

                        _mgr.UpdateQuestionnaire(questionnairesID, newQuestionnaire);
                        Label1.Text = "資料已修改";
                    }
                }
            }
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