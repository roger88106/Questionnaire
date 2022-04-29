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
    public partial class BackCommonly : System.Web.UI.Page
    {
        QuestionnairesManager _Questionnairesmgr = new QuestionnairesManager();
        CommonlyQuestionManager _mgr = new CommonlyQuestionManager();
        List<CommonlyQuestionModel> questionList = new List<CommonlyQuestionModel>();
        protected void Page_Load(object sender, EventArgs e)
        {
            questionList = _mgr.GetCommonlyList();
            GetTable();

            if (DropDownList_Type.SelectedIndex == 0)
                TextBox_Answer.Enabled = false;
            else
                TextBox_Answer.Enabled = true;
        }

        private void GetTable()
        {
            Literal_QuestionTable.Text = "";
            if (questionList != null)
            {
                int _i = 0;//迴圈數
                foreach (var item in questionList)
                {
                    string type;
                    if (item.QuestionType == 1)
                        type = "單選方塊";
                    else if (item.QuestionType == 2)
                        type = "複選方塊";
                    else
                        type = "文字方塊";

                    Literal_QuestionTable.Text += $"<tr>" +
                        $"<td><input type=\"checkbox\" name=\"checkBox_Delete\" value = \"{_i}\" /></td>" +
                        $"<td>{_i + 1}</td>" +
                        $"<td>{item.QuestionContent}</td>" +
                        $"<td>{type}</td>" +
                        $"<td><a href=\"\">編輯</a></td>" +
                        $"</tr>";
                    _i += 1;
                }
            }
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            List<int> IDs = new List<int>();
            if (!string.IsNullOrEmpty(Request.Form["checkBox_Delete"]))
            {
                string[] delete = Request.Form["checkBox_Delete"].Split(',');
                foreach (var item in delete)
                {
                    IDs.Add(questionList[Convert.ToInt32(item)].CommonlyID);
                }
            }
            if (IDs.Count() > 0)
            {
                _mgr.DeleteCommonly(IDs);
            }
            questionList = _mgr.GetCommonlyList();
            GetTable();
        }

        protected void Button_Add_Click(object sender, EventArgs e)
        {
            CommonlyQuestionModel commonly = new CommonlyQuestionModel()
            {
                CommonlyID = 0,
                QuestionType = DropDownList_Type.SelectedIndex,
                QuestionContent = TextBox_Question.Text,
                QuestionOptions = TextBox_Answer.Text
            };
            _mgr.InsertCommonly(commonly);

            questionList = _mgr.GetCommonlyList();
            GetTable();

            //恢復預設
            TextBox_Answer.Text = "";
            TextBox_Question.Text = "";
            DropDownList_Type.SelectedIndex = 0;
            TextBox_Answer.Enabled = false;
        }

        protected void DropDownList_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList_Type.SelectedIndex == 0)
                TextBox_Answer.Enabled = false;
            else
                TextBox_Answer.Enabled = true;
        }
    }
}