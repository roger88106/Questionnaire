using Questionnaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Helpers
{
    public class SearchHelper
    {
        /// <summary>
        /// 搜尋問卷功能
        /// </summary>
        /// <param name="list">要搜尋的問卷列表</param>
        /// <param name="keyword">關鍵字</param>
        /// <param name="start">開始時間</param>
        /// <param name="end">結束時間</param>
        /// <returns>時間</returns>
        public List<QuestionnairesModel> SearchQuestionnaires(List<QuestionnairesModel> list, string keyword, string startTime, string endTime)
        {
            List<QuestionnairesModel> resultList = new List<QuestionnairesModel>();

            //如果start跟end Time有可轉換成DateTime的輸入值就使用，否則儲存成最大跟最小值
            DateTime start, end;
            if (!DateTime.TryParse(startTime, out start))
                start = DateTime.MinValue;
            if (!DateTime.TryParse(endTime,out end))
                end = DateTime.MaxValue;

            if (keyword == null)
                keyword = "";

            DateTime questionnaireEnd;

            foreach (var item in list)
            {
                if (!item.EndTime.HasValue)
                    questionnaireEnd = DateTime.MaxValue;
                else
                    questionnaireEnd = item.EndTime.Value;

                //如果包含關鍵字且時間在範圍內，就儲存 關鍵字如果為空值，就忽略關鍵字搜尋，僅搜尋時間
                if ((item.QuestionnaireTital.Contains(keyword) || string.IsNullOrEmpty(keyword))
                    && item.StartTime >= start && questionnaireEnd <= end)
                {
                    resultList.Add(item);
                }
            }

            return resultList;
        }


    }
}