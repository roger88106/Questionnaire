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
        CommonlyQuestionManager _commonlyMgr = new CommonlyQuestionManager();
        List<QuestionModel> questionList = new List<QuestionModel>();
        QuestionnairesModel questionnaire;
        List<CommonlyQuestionModel> commonlyList;
        Guid editQuestionID;

        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "";
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
                if (Guid.TryParse(Request.QueryString["Edit"], out editQuestionID))
                { }
            }
            else
            {
                //頁面初始化
                Button_Questionnaire.Enabled = true;
                Button_Result.Visible = true;

                Literal_QuestionTable.Text = "";

                //自己頁面的按鈕要關掉
                Button_Question.Enabled = false;

                //讀取常用問題
                commonlyList = _commonlyMgr.GetCommonlyList();
                foreach (var item in commonlyList)
                {
                    DropDownList_Question.Items.Add(item.QuestionContent);
                }


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

                //如果Edit有值的話才做
                if (Guid.TryParse(Request.QueryString["Edit"], out editQuestionID))
                {
                    foreach (var item in questionList)
                    {
                        if (item.QuestionID == editQuestionID)
                        {
                            TextBox_Answer.Text = item.QuestionOptions;
                            TextBox_Question.Text = item.QuestionContent;
                            DropDownList_Type.SelectedIndex = item.QuestionType;
                            CheckBox_Required.Checked = item.Required;
                            DropDownList_Type.Enabled = false;//不能調整回答模式，防止已經被回答的問題發生錯誤
                            if (!(item.QuestionType == 1 || item.QuestionType == 2))
                            {
                                TextBox_Answer.Enabled = false;
                            }
                            else
                            {
                                TextBox_Answer.Enabled = true;
                            }
                            Button_Add.Text = "修改";
                        }
                    }
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
            Button_OK.Text = "儲存問卷";
            Button_OK.OnClientClick = "Button_OK_Click";



            questionnaire = (QuestionnairesModel)HttpContext.Current.Session["QuestionnairesData"];

            //如果是從編輯問題過來的話
            if (Request.QueryString["Edit"] != null)
                questionList = (List<QuestionModel>)HttpContext.Current.Session["questionList"];

            HttpContext.Current.Session["questionList"] = questionList;
            GetTable(questionList);

            //如果讀不到Session內的問卷資料，就提示錯誤並關閉所有功能(例如超時)
            if (questionnaire == null)
            {
                Label1.Text = "出現錯誤，請重新製作問卷";
            }

        }
        private void Mode_Revise()
        {
            //如果是從編輯問題過來的話
            if (Request.QueryString["Edit"] != null)
            {
                questionList = (List<QuestionModel>)HttpContext.Current.Session["questionList"];
            }

            //如果沒有進入上面的IF，或者進了但卻沒取到東西的話
            if (questionList == null || questionList.Count() == 0)
            {
                questionList = _mgr.GetQuestionList(questionnairesID);
                HttpContext.Current.Session["questionList"] = questionList;
            }

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
                    string textID = "";
                    if (questionnairesID > 0)
                        textID = $"ID={questionnairesID}&";

                    Literal_QuestionTable.Text += $"<tr>" +
                        $"<td><input type=\"checkbox\" name=\"checkBox_Delete\" value = \"{_i}\" /></td>" +
                        $"<td>{_i + 1}</td>" +
                        $"<td>{item.QuestionContent}</td>" +
                        $"<td>{type}</td>" +
                        $"<td><input type=\"checkbox\" name=\"checkBox_Required\" value = \"{_i}\" {required} /></td>" +
                        $"<td><a href=\"?{textID}Edit={item.QuestionID}\">編輯</a></td>" +
                        $"</tr>";
                    _i += 1;
                }
            }
        }

        protected void Button_Add_Click(object sender, EventArgs e)
        {
            if (questionList == null)
                questionList = new List<QuestionModel>();

            if (Button_Add.Text == "修改")
            {
                int _i = 0;
                foreach (var item in questionList)
                {
                    if (editQuestionID == item.QuestionID)
                    {
                        questionList[_i].Required = CheckBox_Required.Checked;
                        questionList[_i].QuestionContent = TextBox_Question.Text;
                        questionList[_i].QuestionOptions = TextBox_Answer.Text;
                    }
                    _i++;
                }
                HttpContext.Current.Session["questionList"] = questionList;
                Button_Add.Text = "新增";
                TextBox_Answer.Text = "";
                TextBox_Question.Text = "";
                DropDownList_Type.SelectedIndex = 0;
                CheckBox_Required.Checked = false;
                DropDownList_Type.Enabled = true;//不能調整回答模式，防止已經被回答的問題發生錯誤
                TextBox_Answer.Enabled = false;
            }
            else
            {
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
            }
            HttpContext.Current.Session["questionList"] = questionList;
            GetTable(questionList);

            DropDownList_Question.SelectedIndex = 0;
            DropDownList_Type.SelectedIndex = 0;
            DropDownList_Type.Enabled = true;
            TextBox_Answer.Text = "";
            TextBox_Question.Text = "";
            TextBox_Answer.Enabled = false;
        }

        protected void DropDownList_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList_Type.SelectedIndex == 0)
            {
                TextBox_Answer.Enabled = false;
                TextBox_Answer.Text = "";
            }

            else
                TextBox_Answer.Enabled = true;
        }

        protected void DropDownList_Question_SelectedIndexChanged(object sender, EventArgs e)
        {
            //0為自訂問題，把輸入選項回到預設
            if (DropDownList_Question.SelectedIndex == 0)
            {
                TextBox_Question.Text = "";
                TextBox_Answer.Text = "";
                DropDownList_Type.SelectedIndex = 0;
                DropDownList_Type.Enabled = true;
                CheckBox_Required.Checked = false;
            }
            else
            {
                int selectIndex = DropDownList_Question.SelectedIndex - 1;
                commonlyList = _commonlyMgr.GetCommonlyList();

                TextBox_Question.Text = commonlyList[selectIndex].QuestionContent;
                DropDownList_Type.SelectedIndex = commonlyList[selectIndex].QuestionType;
                DropDownList_Type.Enabled = false;
                if (DropDownList_Type.SelectedIndex == 1 || DropDownList_Type.SelectedIndex == 2)
                {
                    TextBox_Answer.Text = commonlyList[selectIndex].QuestionOptions;
                    TextBox_Answer.Enabled = true;
                }
                else
                {
                    TextBox_Answer.Text = "";
                    TextBox_Answer.Enabled = false;
                }

            }
        }

        protected void Button_OK_Click(object sender, EventArgs e)
        {
            if (questionList == null || questionList == new List<QuestionModel>())
            {

            }
            else
            {
                //新增模式
                if (questionnairesID == -1)
                {
                    //儲存問卷
                    QuestionnairesModel questionnaire = (QuestionnairesModel)HttpContext.Current.Session["QuestionnairesData"];
                    questionnairesID = _Questionnairesmgr.InsertQuestionnaires(questionnaire);

                    //儲存問題
                    _mgr.UpdateQuestionnaire(questionnairesID, questionList);
                    Response.Redirect("BackIndex.aspx");
                }
                //編輯模式
                else
                {
                    int _i = 0;
                    if (!string.IsNullOrEmpty(Request.Form["checkBox_Required"]))
                    {
                        string[] required = Request.Form["checkBox_Required"].Split(',');
                        foreach (var item in questionList)
                        {
                            if (Array.IndexOf(required, _i.ToString()) == -1)
                                questionList[_i].Required = false;
                            else
                                questionList[_i].Required = true;

                            _i++;
                        }
                    }

                    _mgr.UpdateQuestionnaire(questionnairesID, questionList);

                    Mode_Revise();
                    Label1.Text = "問題已處存";
                }
            }
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            string[] poshion;

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

        protected void Button_Questionnaire_Click(object sender, EventArgs e)
        {
            Response.Redirect($"BackQuestionnaire.aspx?ID={questionnairesID}");
        }

        protected void Button_Cancle_Click(object sender, EventArgs e)
        {
            if (questionnairesID == -1)
            {
                Response.Redirect("BackIndex.aspx");
            }
            else
            {
                questionList = _mgr.GetQuestionList(questionnairesID);
                HttpContext.Current.Session["questionList"] = questionList;

                GetTable(questionList);
            }
        }
    }
}