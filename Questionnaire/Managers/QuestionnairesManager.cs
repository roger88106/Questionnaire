using Questionnaire.Models;
using Questionnaire.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Managers
{
    public class QuestionnairesManager
    {

        public List<QuestionnairesModel> GetQuestionnaires()
        {
            using (ContextModel contextModel = new ContextModel())
            {
                var query =
                    from item in contextModel.Questionnaires
                    where item.QuestionnaireState >= 0 //小於零代表已經刪除
                    select new QuestionnairesModel
                    {
                        QuestionnaireID = item.QuestionnaireID,
                        StartTime = item.StartTime,
                        EndTime = item.EndTime,
                        QuestionnaireTital = item.QuestionnaireTital,
                        QuestionnaireContent = item.QuestionnaireContent,
                        QuestionnaireState = item.QuestionnaireState
                    };

                List<QuestionnairesModel> list = query.ToList();
                try
                {
                    return list;
                }
                catch
                {
                    return null;
                }

            }

        }
    }
}