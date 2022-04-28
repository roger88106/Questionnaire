using Questionnaire.Models;
using Questionnaire.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Managers
{
    public class QuestionManager
    {
        public List<QuestionModel> GetQuestionList(int ID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query =
                        from item in contextModel.Questions
                        where item.QuestionnairesID == ID
                        orderby item.QuestionOrder
                        select new QuestionModel
                        {
                            QuestionID = item.QuestionID,                       //問題ID
                            QuestionnairesID = item.QuestionnairesID,           //問卷ID
                            QuestionType = item.QuestionType,                   //問題種類，0文字1單選2複選
                            QuestionContent = item.QuestionContent,             //問題內容
                            QuestionOrder = item.QuestionOrder,                 //排序
                            Required = item.Required,                           //必填
                            QuestionOptions = item.QuestionOptions              //單多選的內文
                        };
                    return query.ToList();
                }
            }
            catch
            {
                return new List<QuestionModel>();
            }
        }

        /// <summary>
        /// 尋找單筆問卷
        /// </summary>
        /// <param name="ID">問卷ID</param>
        /// <returns>問卷資料</returns>
        public QuestionModel GetQuestion(Guid ID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query =
                        from item in contextModel.Questions
                        where item.QuestionID == ID
                        orderby item.QuestionOrder
                        select new QuestionModel
                        {
                            QuestionID = item.QuestionID,                       //問題ID
                            QuestionnairesID = item.QuestionnairesID,           //問卷ID
                            QuestionType = item.QuestionType,                   //問題種類，0文字1單選2複選
                            QuestionContent = item.QuestionContent,             //問題內容
                            QuestionOrder = item.QuestionOrder,                 //排序
                            Required = item.Required,                           //必填
                            QuestionOptions = item.QuestionOptions              //單多選的內文
                        };
                    foreach (var item in query.ToList())//應該也只會有一筆
                    {
                        return item;
                    }
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// 刪除同問卷ID的所有問題，並儲存新的問題
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="questionList"></param>
        public void UpdateQuestionnaire(int ID, List<QuestionModel> questionList)
        {
            //這邊其實不是Update，是先Delete後再INSERT
            //因為前後的比數會不同，沒辦法直接用Update做
            //所以採用這個流程，就結果而言是Update
            using (ContextModel contextModel = new ContextModel())
            {
                try
                {
                    while (true)
                    {
                        //無限迴圈刪除查到的第一筆，直到沒有符合導致跳出錯誤，停止迴圈
                        var item = contextModel.Questions.First(c => c.QuestionnairesID == ID);
                        contextModel.Questions.Remove(item);
                        contextModel.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    //這裡的ERROR只是用來結束無限迴圈，所以不需要對錯誤訊息做出反應
                }


                //儲存
                if (questionList!= null)
                {
                    int _i = 0;
                    foreach (var questionItem in questionList)
                    {
                        var question = new Question()
                        {
                            QuestionnairesID = ID,
                            QuestionContent = questionItem.QuestionContent,
                            QuestionID = questionItem.QuestionID,
                            QuestionOptions = questionItem.QuestionOptions,
                            QuestionOrder = _i,
                            QuestionType = questionItem.QuestionType
                        };
                        contextModel.Questions.Add(question);
                        _i++;
                    }
                }
                contextModel.SaveChanges();
            }

        }

    }
}