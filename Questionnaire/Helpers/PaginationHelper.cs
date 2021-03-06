using Questionnaire.Helpers;
using Questionnaire.Managers;
using Questionnaire.Models;
using Questionnaire.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.Helpers
{
    public class PaginationHelper
    {

        /// <summary>
        /// 取得當前頁面用的列表(問卷)
        /// </summary>
        /// <param name="list">所有分頁內容的表</param>
        /// <param name="Page">當前頁數</param>
        /// <returns>當前頁面列表</returns>
        public List<QuestionnairesModel> GetPageList(List<QuestionnairesModel> list, int Page)
        {
            List<QuestionnairesModel> requestList = new List<QuestionnairesModel>();
            for (int i = (Page - 1) * 5; i != Page * 5; i++)
            {
                try
                {
                    requestList.Add(list[i]);
                }
                catch
                {
                    //這裡索引可能會超出範圍，但超出就無視就好ㄌ
                }
            }

            return requestList;
        }

        /// <summary>
        /// 取得當前頁面用的列表(填答者)
        /// </summary>
        /// <param name="list">所有分頁內容的表</param>
        /// <param name="Page">當前頁數</param>
        /// <returns>當前頁面列表</returns>
        public List<RespondentModel> GetPageList(List<RespondentModel> list, int Page)
        {
            List<RespondentModel> requestList = new List<RespondentModel>();
            for (int i = (Page - 1) * 5; i != Page * 5; i++)
            {
                try
                {
                    requestList.Add(list[i]);
                }
                catch
                {
                    //這裡索引可能會超出範圍，但超出就無視就好
                }
            }

            return requestList;
        }

        //取得總共的頁數
        public int GetPages(int listCount)
        {
            int pages = listCount / 5;
            if (listCount % 5 != 0)
                pages += 1;
            return pages;
        }

        //製作分頁Literal用的HTML字串
        public string GetLiteral_PagerHtml(int nowPage, int maxPage)
        {
            if (maxPage == 0)
                return "<< &nbsp< &nbsp1 &nbsp> &nbsp>>";

            string start = $"<a href=\"?page={1}\"><<</a> &nbsp <a href = \"?page={nowPage - 1}\" ><</a> &nbsp";
            string end = $"<a href=\"?page={nowPage + 1}\"> > </a> &nbsp <a href=\"?page={maxPage}\">>></a>";

            string mid = "";

            if (nowPage == 1 || nowPage == 2)//第一第二頁的情況
            {
                for (int i = 1; i < 6 && i < maxPage + 1; i++)
                {
                    if (i == nowPage)
                        mid += nowPage + "&nbsp";//當前頁面不需要超連結
                    else
                        mid += $"<a href=\"?page={i}\">{i}</a>&nbsp";
                }
            }
            else if (nowPage == maxPage - 1 || nowPage == maxPage)//倒數第二或倒數第一頁的情況
            {
                int startPage;
                if (maxPage > 5)
                    startPage = maxPage - 4;
                else
                    startPage = 1;

                for (int i = startPage; i < maxPage + 1; i++)
                {
                    if (i == nowPage)
                        mid += nowPage + "&nbsp"; //當前頁面不需要超連結
                    else
                        mid += $"<a href=\"?page={i}\">{i}</a>&nbsp";
                }
            }
            else
            {
                for (int i = nowPage - 2; i < nowPage + 3; i++) //其他頁面的情況
                {
                    if (i == nowPage)
                        mid += nowPage + "&nbsp";//當前頁面不需要超連結
                    else
                        mid += $"<a href=\"?page={i}\">{i}</a>&nbsp";
                }
            }

            return start + mid + end;
        }

        //製作分頁Literal用的HTML字串(帶QueryString查詢參數版)
        public string GetLiteral_PagerHtml(int nowPage, int maxPage, string keyword,string startTime, string endTime)
        {
            if (maxPage == 0)
                return "<< &nbsp< &nbsp1 &nbsp> &nbsp>>";


            string queryString_Sencer="";
            if (!string.IsNullOrEmpty(keyword))
                queryString_Sencer += $"Keyword={keyword}&";
            if (!string.IsNullOrEmpty(startTime))
                queryString_Sencer += $"StartTime={startTime}&";
            if (!string.IsNullOrEmpty(endTime))
                queryString_Sencer += $"EndTime={endTime}&";

            string start, end;
            if (nowPage <= 1)
            {
                start = $"<<&nbsp<&nbsp";
                end = $"<a href=\"?{queryString_Sencer}page={nowPage + 1}\"> > </a> &nbsp <a href=\"?{queryString_Sencer}page={maxPage}\">>></a>";
            }
            else if (nowPage >= maxPage)
            {
                start = $"<a href=\"?{queryString_Sencer}page={1}\"><<</a> &nbsp <a href = \"?{queryString_Sencer}page={nowPage - 1}\" ><</a> &nbsp";
                end = $">&nbsp>>";
            }
            else
            {
                start = $"<a href=\"?{queryString_Sencer}page={1}\"><<</a> &nbsp <a href = \"?{queryString_Sencer}page={nowPage - 1}\" ><</a> &nbsp";
                end = $"<a href=\"?{queryString_Sencer}page={nowPage + 1}\"> > </a> &nbsp <a href=\"?{queryString_Sencer}page={maxPage}\">>></a>";
            }


            string mid = "";

            if (nowPage == 1 || nowPage == 2)//第一第二頁的情況
            {
                for (int i = 1; i < 6 && i < maxPage + 1; i++)
                {
                    if (i == nowPage)
                        mid += nowPage + "&nbsp";//當前頁面不需要超連結
                    else
                        mid += $"<a href=\"?{queryString_Sencer}page={i}\">{i}</a>&nbsp";
                }
            }
            else if (nowPage == maxPage - 1 || nowPage == maxPage)//倒數第二或倒數第一頁的情況
            {
                int startPage;
                if (maxPage > 5)
                    startPage = maxPage - 4;
                else
                    startPage = 1;

                for (int i = startPage; i < maxPage + 1; i++)
                {
                    if (i == nowPage)
                        mid += nowPage + "&nbsp"; //當前頁面不需要超連結
                    else
                        mid += $"<a href=\"?{queryString_Sencer}page={i}\">{i}</a>&nbsp";
                }
            }
            else
            {
                for (int i = nowPage - 2; i < nowPage + 3; i++) //其他頁面的情況
                {
                    if (i == nowPage)
                        mid += nowPage + "&nbsp";//當前頁面不需要超連結
                    else
                        mid += $"<a href=\"?{queryString_Sencer}page={i}\">{i}</a>&nbsp";
                }
            }

            return start + mid + end;
        }

        //製作分頁Literal用的HTML字串(帶QueryString ID版)
        public string GetLiteral_PagerHtml(int nowPage, int maxPage ,int ID)
        {
            if (maxPage == 0)
                return "<< &nbsp< &nbsp1 &nbsp> &nbsp>>";

            string start = $"<a href=\"?ID={ID}&page={1}\"><<</a> &nbsp <a href = \"?ID={ID}&page={nowPage - 1}\" ><</a> &nbsp";
            string end = $"<a href=\"?ID={ID}&page={nowPage + 1}\"> > </a> &nbsp <a href=\"?ID={ID}&page={maxPage}\">>></a>";

            string mid = "";

            if (nowPage == 1 || nowPage == 2)//第一第二頁的情況
            {
                for (int i = 1; i < 6 && i < maxPage + 1; i++)
                {
                    if (i == nowPage)
                        mid += nowPage + "&nbsp";//當前頁面不需要超連結
                    else
                        mid += $"<a href=\"?ID={ID}&page={i}\">{i}</a>&nbsp";
                }
            }
            else if (nowPage == maxPage - 1 || nowPage == maxPage)//倒數第二或倒數第一頁的情況
            {
                int startPage;
                if (maxPage > 5)
                    startPage = maxPage - 4;
                else
                    startPage = 1;

                for (int i = startPage; i < maxPage + 1; i++)
                {
                    if (i == nowPage)
                        mid += nowPage + "&nbsp"; //當前頁面不需要超連結
                    else
                        mid += $"<a href=\"?ID={ID}&page={i}\">{i}</a>&nbsp";
                }
            }
            else
            {
                for (int i = nowPage - 2; i < nowPage + 3; i++) //其他頁面的情況
                {
                    if (i == nowPage)
                        mid += nowPage + "&nbsp";//當前頁面不需要超連結
                    else
                        mid += $"<a href=\"?ID={ID}&page={i}\">{i}</a>&nbsp";
                }
            }

            return start + mid + end;
        }
    }
}