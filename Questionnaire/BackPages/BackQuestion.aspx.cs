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
    public partial class BackQuestion : System.Web.UI.Page
    {
        int questionnairesID;//讀到對應的ID就是修改模式，讀不到的話把ID轉成-1就是新增模式
        QuestionnairesManager _Questionnairesmgr = new QuestionnairesManager();
        QuestionManager _mgr = new QuestionManager();
        List<QuestionModel> questionList = new List<QuestionModel>();
        QuestionnairesModel questionnaire;

        protected void Page_Load(object sender, EventArgs e)
        {
            //取得ID
            try
            {//排除掉ID被竄改成不是整數的情形
                questionnairesID = Convert.ToInt32(Request.QueryString["ID"]);
            }
            catch (Exception)
            {
                questionnairesID = -1;
            }


            if (IsPostBack)
            {
                bool hasThisQuestionnairesID = _Questionnairesmgr.SelectQuestionnaireIDinDatabase(0, questionnairesID);
                if (!(questionnairesID >= 0 && hasThisQuestionnairesID))
                    questionnairesID = -1;

                questionList = (List<QuestionModel>)HttpContext.Current.Session["questionList"];
            }
            else
            {
                //頁面初始化
                Button_Questionnaire.Enabled = true;
                this.Button_Result.Visible = true;
                this.Button_BackStatisticalData.Visible = true;
                Literal_QuestionTable.Text = "";

                //自己頁面的按鈕要關掉
                Button_Question.Enabled = false;

                //讀取常用問題


                //判斷ID是否有對應的問卷
                bool hasThisQuestionnairesID = _Questionnairesmgr.SelectQuestionnaireIDinDatabase(0, questionnairesID);
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

            //如果選項是文字，就不需要填回答的TextBox
            if (DropDownList_Type.SelectedIndex == 0)
                TextBox_Answer.Enabled = false;
        }

        private void Mode_New()
        {
            Button_Questionnaire.Enabled = false;
            this.Button_Result.Visible = false;
            this.Button_BackStatisticalData.Visible = false;
            Button_OK.Text = "下一步";
            Button_OK.OnClientClick = "Button_OK_Click";

            questionnaire = (QuestionnairesModel)HttpContext.Current.Session["QuestionnairesData"];
            //如果讀不到Session內的問卷資料，就提示錯誤並關閉所有功能
            if (questionnaire == null)
            {
                Label1.Text = "出現錯誤，請重新製作問卷";
            }
            
        }
        private void Mode_Revise()
        {
            questionList = _mgr.GetQuestionList(questionnairesID);
            HttpContext.Current.Session["questionList"] = questionList;

            GetTable(questionList);
        }

        private void GetTable(List<QuestionModel> list)
        {
            //這邊整個之後要改成用資料繫結製作，以解決必填勾選變化後不會反映到session跟編輯按鈕製作上的問題
            Literal_QuestionTable.Text = "";
            if (list != null)
            {
                int _i = 0;//迴圈數
                foreach (var item in list)
                {
                    string type;
                    if (item.QuestionType == 1)
                        type = "單選方塊";
                    else if (item.QuestionType == 2)
                        type = "複選方塊";
                    else
                        type = "文字方塊";

                    string required = "";
                    if (item.Required)
                        required = "checked = \"true\"";

                    Literal_QuestionTable.Text += $"<tr>" +
                        $"<td><input type=\"checkbox\" name=\"checkBox_Delete\" value = \"{_i}\" /></td>" +
                        $"<td>{_i + 1}</td>" +
                        $"<td>{item.QuestionContent}</td>" +
                        $"<td>{type}</td>" +
                        $"<td><input type=\"checkbox\" name=\"checkBox_Required\" value = \"{_i}\" {required} /></td>" +
                        $"<td><a href=\"\">編輯</a></td>" +
                        $"</tr>";
                    _i += 1;
                }
            }
        }

        protected void Button_Add_Click(object sender, EventArgs e)
        {
            if (questionList == null)
                questionList = new List<QuestionModel>();
            
            questionList.Add(new QuestionModel
            {
                QuestionnairesID = questionnairesID,
                QuestionID = Guid.NewGuid(),
                QuestionType = DropDownList_Type.SelectedIndex,
                QuestionContent = TextBox_Question.Text,
                QuestionOptions = TextBox_Answer.Text,
                Required = CheckBox_Required.Checked,
                QuestionOrder = questionList.Count()
            });

            HttpContext.Current.Session["questionList"] = questionList;
            GetTable(questionList);
        }

        protected void DropDownList_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList_Type.SelectedIndex == 0)
                TextBox_Answer.Enabled = false;
            else
                TextBox_Answer.Enabled = true;
        }

        protected void Button_OK_Click(object sender, EventArgs e)
        {
            //新增模式
            if (questionnairesID == -1)
            {
                //儲存問卷
                QuestionnairesModel questionnaire = (QuestionnairesModel)HttpContext.Current.Session["QuestionnairesData"];
                questionnairesID = _Questionnairesmgr.InsertQuestionnaires(questionnaire);

                //儲存問題
                _mgr.UpdateQuestionnaire(questionnairesID, questionList);
            }
            //編輯模式
            else
            {
                _mgr.UpdateQuestionnaire(questionnairesID, questionList);
            }
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            string[] poshion;

            int _i = 0;
            if (!string.IsNullOrEmpty(Request.Form["checkBox_Delete"]))
            {
                poshion = Request.Form["checkBox_Delete"].Split(',');
                //反轉排序，從後面開始刪，才不會遇到排序變動導致刪錯的問題
                Array.Reverse(poshion);

                foreach (var item in poshion)
                {
                    questionList.RemoveAt(Convert.ToInt32(item));
                }
            }
            HttpContext.Current.Session["QuestionnairesData"] = questionList;
            GetTable(questionList);
        }
    }
}