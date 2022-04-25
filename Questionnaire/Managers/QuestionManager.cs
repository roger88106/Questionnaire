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
        public List<QuestionModel> GetQuestion(int ID)
        {
            //try
            //{
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
            //}
            //catch
            //{
            //    return null;
            //}
        }

    }
}