using Questionnaire.Models;
using Questionnaire.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Managers
{
    public class StatisticalDataManager
    {

        /// <summary>
        /// 取得統計結果
        /// </summary>
        /// <param name="questionnaireID">問卷的ID</param>
        /// <returns>每題的結果列表</returns>
        public List<StatisticalDataModel> GetStatisticalData(int questionnaireID)
        {
            List<StatisticalDataModel> statisticalDataList;
            List<AnswerModel> answerList;

            //取得不含統計數據的資料
            using (ContextModel contextModel = new ContextModel())
            {
                var query =
                    from item in contextModel.Questions
                    where item.QuestionnairesID == questionnaireID
                    orderby item.QuestionOrder
                    select new StatisticalDataModel
                    {
                        QuestionID = item.QuestionID,
                        Content = item.QuestionContent,
                        option = item.QuestionOptions,
                        type = item.QuestionType
                    };
                statisticalDataList = query.ToList();

                var query2 =
                    from item in contextModel.Answers
                    where item.QuestionnaireID == questionnaireID
                    select new AnswerModel
                    {
                        Answer = item.Answer1,
                        AnswerID = item.AnswerID,
                        QuestionID = item.QuestionID.Value,
                        QuestionnaireID = item.QuestionnaireID.Value,
                        RespondentID = item.RespondentID.Value
                    };
                answerList = query2.ToList();
            }

            int _i = 0;
            //問題的迴圈
            foreach (var item in statisticalDataList)
            {
                if ((item.type == 1 || item.type == 2) && !string.IsNullOrEmpty(item.option))
                {
                    int[] countAnswers;
                    int count = 0;

                    //先計算出這個問題有多少選項，並產生負責加總的陣列
                    countAnswers = new int[item.option.Split(';').Count()];
                    //回答者的迴圈
                    foreach (var item2 in answerList)
                    {
                        //如果是回答這題，而且有作答
                        if ((item.QuestionID == item2.QuestionID) && !string.IsNullOrEmpty(item2.Answer))
                        {
                            foreach (var item3 in item2.Answer.Split(','))
                            {
                                //把他選的選項加一
                                countAnswers[Convert.ToInt32(item3)]++;
                                //把總計也加一
                                count++;
                            }
                        }
                    }

                    List<int> answerCount = new List<int>();
                    List<double> answerPercent = new List<double>();
                    

                    //取得每個選項的回答總數
                    foreach (var item3 in countAnswers)
                    {
                        answerCount.Add(item3);

                        //做除法要先轉成浮點數，不然除出來會是零
                        answerPercent.Add((double)item3 / (double)count);
                        
                    }

                    statisticalDataList[_i].answerCount = answerCount;
                    statisticalDataList[_i].answerPercent = answerPercent;
                }
                _i++;
            }
            return statisticalDataList;
        }
    }
}