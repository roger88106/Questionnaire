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
    }
}