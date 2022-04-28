using Questionnaire.Helpers;
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
    public partial class BackResults : System.Web.UI.Page
    {
        int questionnairesID;
        QuestionnairesManager _Questionnairesmgr = new QuestionnairesManager();
        RespondentManager _mgr = new RespondentManager();
        PaginationHelper _hlp = new PaginationHelper();
        int nowPage, maxPage;
        List<RespondentModel> allRespondentList,nowPageList;

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
            }
            else
            {
                //查無問卷，題是錯誤
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
                            $"<td>{(5-_i)+(maxPage-nowPage)*5}</td>" +
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