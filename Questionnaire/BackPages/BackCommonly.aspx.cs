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
                        $"<td><input type=\"checkbox\" name=\"checkBox_Delete\" value = \"{item.CommonlyID}\"  /></td>" +
                        $"<td>{_i + 1}</td>" +
                        $"<td>{item.QuestionContent}</td>" +
                        $"<td>{type}</td>" +
                        $"<td>{item.QuestionOptions}</td>" +
                        $"</tr>";
                    _i += 1;
                }
            }
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            List<int> IDs = new List<int>();
            //if (!string.IsNullOrEmpty(Request.Form["checkBox_Delete"]))
            //{
            //    string[] delete = Request.Form["checkBox_Delete"].Split(',');
            //    foreach (var item in delete)
            //    {
            //        IDs.Add(questionList[Convert.ToInt32(item)].CommonlyID);
            //    }
            //}

            if (!string.IsNullOrEmpty(Request.Form["checkBox_Delete"]))
            {
                string[] delete = Request.Form["checkBox_Delete"].Split(',');
                foreach (var item in delete)
                {
                    IDs.Add(Convert.ToInt32(item));
                }
            }

            if (IDs.Count() > 0)
            {
                if (!_mgr.DeleteCommonly(IDs))
                    Label1.Text = "資料異動，請再次選擇希望刪除的項目";
                else
                    Label1.Text = "";
            }
            questionList = _mgr.GetCommonlyList();
            GetTable();
            //Response.Redirect("BackCommonly.aspx");
        }

        protected void Button_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox_Question.Text))
            {
                Label1.Text = "問題不能為空";
            }
            else if (string.IsNullOrEmpty(RemoveNullOpthion(TextBox_Answer.Text.Trim())) && (DropDownList_Type.SelectedIndex == 1 || DropDownList_Type.SelectedIndex == 2))
            {
                Label1.Text = "選項不能為空";
            }
            else
            {
                CommonlyQuestionModel commonly = new CommonlyQuestionModel()
                {
                    CommonlyID = 0,
                    QuestionType = DropDownList_Type.SelectedIndex,
                    QuestionContent = TextBox_Question.Text,
                    QuestionOptions = RemoveNullOpthion(TextBox_Answer.Text.Trim())
                };
                _mgr.InsertCommonly(commonly);


                questionList = _mgr.GetCommonlyList();
                GetTable();

                //恢復預設
                TextBox_Answer.Text = "";
                TextBox_Question.Text = "";
                Label1.Text = "";
                DropDownList_Type.SelectedIndex = 0;
                TextBox_Answer.Enabled = false;
            }

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

        /// <summary>
        /// 刪除空白選項
        /// </summary>
        /// <param name="option">選項內文</param>
        /// <returns>刪除空白選項後的選項內文</returns>
        private string RemoveNullOpthion(string option)
        {
            char last = ';';
            string newOption = "";
            foreach (var item in option)
            {
                //如果連續兩個都是分號，就跳過
                if (last == ';' && item == ';')
                {

                }
                else
                {
                    newOption += item;
                }
                last = item;
            }

            if (last == ';')
            {
                return newOption.TrimEnd(';');
            }

            return newOption;
        }
    }
}