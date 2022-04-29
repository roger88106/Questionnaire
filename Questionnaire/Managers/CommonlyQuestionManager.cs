﻿using Questionnaire.Models;
using Questionnaire.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Managers
{
    public class CommonlyQuestionManager
    {
        public List<CommonlyQuestionModel> GetCommonlyList()
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query =
                        from item in contextModel.CommonlyQuestions
                        orderby item.CommonlyID
                        select new CommonlyQuestionModel
                        {
                            CommonlyID = item.CommonlyID,
                            QuestionType = item.QuestionType,
                            QuestionContent = item.QuestionContent,
                            QuestionOptions = item.QuestionOptions
                        };
                    return query.ToList();
                }
            }
            catch
            {
                return new List<CommonlyQuestionModel>();
            }
        }

        public void DeleteCommonly(List<int> IDs)
        {
            using (ContextModel contextModel = new ContextModel())
            {
                foreach (var ID in IDs)
                {
                    var item = contextModel.CommonlyQuestions.First(c => c.CommonlyID == ID);
                    contextModel.CommonlyQuestions.Remove(item);
                }
                contextModel.SaveChanges();
            }
        }
        public void InsertCommonly(CommonlyQuestionModel commonly)
        {
            using (ContextModel contextModel = new ContextModel())
            {
                int ID = 1;
                var query =
                    from item in contextModel.CommonlyQuestions
                    orderby item.CommonlyID descending
                    select new CommonlyQuestionModel
                    {
                        CommonlyID = item.CommonlyID,
                        QuestionType = item.QuestionType,
                        QuestionContent = item.QuestionContent,
                        QuestionOptions = item.QuestionOptions
                    };
                //取得最大的ID
                foreach (var item in query.ToList())
                {
                    ID = item.CommonlyID+1;
                    break;
                }
                
                var commonlyQuestion = new CommonlyQuestion()
                {
                    CommonlyID = ID,
                    QuestionContent = commonly.QuestionContent,
                    QuestionOptions = commonly.QuestionOptions,
                    QuestionType = commonly.QuestionType
                };
                contextModel.CommonlyQuestions.Add(commonlyQuestion);
                contextModel.SaveChanges();
            }
        }
    }
}
