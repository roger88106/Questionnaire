using Questionnaire.Models;
using Questionnaire.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Managers
{
    public class RespondentManager
    {
        public List<RespondentModel> GetRespondentList(int ID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query =
                        from item in contextModel.Respondents
                        where item.QuestionnairesID == ID
                        orderby item.FillTime descending
                        select new RespondentModel
                        {
                            QuestionnairesID=item.QuestionnairesID,
                            Age = item.Age,
                            Email=item.Email,
                            FillTime=item.FillTime,
                            Name=item.Name,
                            PhoneNumber=item.PhoneNumber,
                            RespondentID=item.RespondentID
                        };
                    if (query.ToList().Count == 0)
                        return new List<RespondentModel>();

                    return query.ToList();
                }
            }
            catch
            {
                return new List<RespondentModel>();
            }
        }

        public RespondentModel GetRespondent(Guid ID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query =
                        from item in contextModel.Respondents
                        where item.RespondentID == ID
                        select new RespondentModel
                        {
                            QuestionnairesID = item.QuestionnairesID,
                            Age = item.Age,
                            Email = item.Email,
                            FillTime = item.FillTime,
                            Name = item.Name,
                            PhoneNumber = item.PhoneNumber,
                            RespondentID = item.RespondentID
                        };
                    foreach (var item in query.ToArray())
                    {
                        return item;
                    }
                }
            }
            catch{ }
            return null;
        }

        public List<CsvModel> GetCsvList(int ID)
        {
            using (ContextModel contextModel = new ContextModel())
            {
                var query = from item in contextModel.Respondents
                            where item.QuestionnairesID == ID
                            orderby item.FillTime
                            select new RespondentModel
                            {
                                Age = item.Age,
                                Email=item.Email,
                                FillTime=item.FillTime,
                                Name = item.Name,
                                QuestionnairesID = item.QuestionnairesID,
                                PhoneNumber = item.PhoneNumber,
                                RespondentID = item.RespondentID
                            };
                 List<RespondentModel>rList = query.ToList();

                var query2 = from item in contextModel.Questions
                            where item.QuestionnairesID == ID
                            orderby item.QuestionOrder
                            select new QuestionModel
                            {
                                QuestionContent= item.QuestionContent,
                                QuestionID = item.QuestionID,
                                QuestionnairesID = item.QuestionnairesID,
                                QuestionOptions=item.QuestionOptions,
                                QuestionOrder= item.QuestionOrder,
                                QuestionType=item.QuestionType,
                                Required = item.Required
                            };
                List<QuestionModel> qList = query2.ToList();

                var query3 = from item in contextModel.Answers
                             where item.QuestionnaireID == ID
                             select new AnswerModel
                             {
                                 Answer = item.Answer1,
                                 AnswerID=item.AnswerID,
                                 QuestionID =item.QuestionID.Value,
                                 QuestionnaireID = item.QuestionnaireID.Value,
                                 RespondentID = item.RespondentID.Value
                             };
                List<AnswerModel> aList = query3.ToList();

                List<CsvModel> csvList = new List<CsvModel>();

                List<string> Question = new List<string>();
                foreach (var item in qList)
                {
                    Question.Add(item.QuestionContent);
                }

                foreach (var item in rList)
                {
                    List<string> answerList = new List<string>();

                    foreach (var item2 in qList)
                    {
                        string answerText = "";
                        foreach (var item3 in aList)
                        {
                            if (item3.QuestionID == item2.QuestionID && item3.RespondentID == item.RespondentID)
                            {
                                if (item2.QuestionType == 1 || item2.QuestionType==2)
                                {
                                    string[] option = item2.QuestionOptions.Split(';');
                                    try
                                    {
                                        foreach (var item4 in item3.Answer.Split(','))
                                        {
                                            answerText += option[Convert.ToInt32(item4)] + "、";
                                        }
                                        answerText = answerText.Remove(answerText.LastIndexOf("、"), 1);
                                    }
                                    catch (Exception)
                                    {

                                    }   
                                }
                                else
                                {
                                    answerText = item3.Answer;
                                }

                                answerList.Add(answerText);
                                break;
                            }
                        }

                    }

                    csvList.Add(new CsvModel 
                    { 
                        Age=item.Age.ToString(),
                        Email=item.Email,
                        Name=item.Name,
                        Phone=item.PhoneNumber,
                        Question= Question,
                        Answer = answerList,
                    });
                }
                return csvList;
            }




            return new List<CsvModel>();
        }
    }
}