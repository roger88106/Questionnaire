using Questionnaire.Helpers;
using Questionnaire.Managers;
using Questionnaire.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.BackPages
{
    public partial class BackResults : System.Web.UI.Page
    {
        int questionnairesID;
        QuestionnairesManager _Questionnairesmgr = new QuestionnairesManager();
        QuestionManager _QuestionMgr = new QuestionManager();

        RespondentManager _mgr = new RespondentManager();
        PaginationHelper _hlp = new PaginationHelper();
        int nowPage, maxPage;
        List<RespondentModel> allRespondentList,nowPageList;

        protected void Button_Questionnaire_Click(object sender, EventArgs e)
        {
            Response.Redirect($"BackQuestionnaire.aspx?ID={questionnairesID}");
        }

        protected void Button_Question_Click(object sender, EventArgs e)
        {
            Response.Redirect($"BackQuestion.aspx?ID={questionnairesID}");
        }

        protected void Button_BackStatisticalData_Click(object sender, EventArgs e)
        {
            Response.Redirect($"BackStatisticalData.aspx?ID={questionnairesID}");
        }

        protected void Button_OutCsv_Click(object sender, EventArgs e)
        {
            //找到windows的文件資料夾
            string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //取得問卷標題
            string tital = _Questionnairesmgr.GetQuestionnaire(0, questionnairesID).QuestionnaireTital;
            FilePath += $"/{tital}.csv";

            List<CsvModel> CsvData = _mgr.GetCsvList(questionnairesID);
            using (var file = new StreamWriter(FilePath,false,System.Text.Encoding.UTF8))
            {
                string hade = $"姓名,電話,Email,年齡";

                List<CsvModel> csvData = _mgr.GetCsvList(questionnairesID);
                //取得問題文字
                foreach (var item in csvData[0].Question)
                {
                    hade += "," + item;
                }

                file.WriteLineAsync(hade);
                foreach (var item in csvData)
                {
                    string data = $"{item.Name},{item.Phone},{item.Email},{item.Age}";
                    foreach (var item2 in item.Answer)
                    {
                        data += "," + item2;
                    }
                    file.WriteLineAsync(data);
                }
            }
 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Button_OutCsv.Enabled = true;

            //嘗試讀取ID         
            try
            {//排除掉ID被竄改成不是整數的情形
                questionnairesID = Convert.ToInt32(Request.QueryString["ID"]);
            }
            catch (Exception)
            {
                questionnairesID = -1;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["page"]))
                nowPage = Convert.ToInt32(Request.QueryString["page"]);
            else
                nowPage = 1;

            //如果這個ID確實有對應的到的問卷
            if (_Questionnairesmgr.SelectQuestionnaireIDinDatabase(0, questionnairesID))
            {
                allRespondentList = _mgr.GetRespondentList(questionnairesID);
                nowPageList = _hlp.GetPageList(allRespondentList, nowPage);

                maxPage = _hlp.GetPages(allRespondentList.Count);

                Literal_Pager.Text = _hlp.GetLiteral_PagerHtml(nowPage, maxPage, questionnairesID);

                GetTable(nowPageList);

                if (allRespondentList.Count() == 0)
                {
                    Button_OutCsv.Enabled = false;
                }
                else
                {
                    Button_OutCsv.Enabled = true;
                }
            }
            else
            {
                //查無問卷，提示錯誤
                Literal_Table.Text = "ID錯誤";
                Button_OutCsv.Enabled = false;
            }
        }

        private void GetTable(List<RespondentModel> list)
        {
            Literal_Table.Text = "";
            if (list != null)
            {
                int _i = 0;
                foreach (var item in list)
                {
                    Literal_Table.Text += (
                        "<tr>" +
                            $"<td>{(6-_i)+(maxPage-nowPage-1)*5}</td>" +
                            $"<td> {item.Name} </td>" +
                            $"<td> {item.FillTime.ToString("yyyy/MM/dd")} </td>" +
                            $"<td> <a href=\"BackResultsDetail.aspx?ID={item.RespondentID}\">前往</a> </td>" +
                        "</tr> ");
                    _i++;
                }
            }
        }
    }
}