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


        /// <summary>
        /// 查詢問卷資料
        /// </summary>
        /// <param name="stat">狀態，-1為查詢全部，0為查詢除了刪除外的，1只查詢已啟用的</param>
        /// <returns></returns>
        public List<QuestionnairesModel> GetQuestionnaires(int state)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query =
                        from item in contextModel.Questionnaires
                        where item.QuestionnaireState >= state
                        select new QuestionnairesModel
                        {
                            QuestionnaireID = item.QuestionnaireID,
                            StartTime = item.StartTime,
                            EndTime = item.EndTime,
                            QuestionnaireTital = item.QuestionnaireTital,
                            QuestionnaireContent = item.QuestionnaireContent,
                            QuestionnaireState = item.QuestionnaireState
                        };
                    return query.ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 查詢問卷資料庫內是否有該ID存在
        /// </summary>
        /// <param name="state">狀態(0為只查沒被刪除的，1為只查已經啟用的，-1為包括已刪除的全部查詢)</param>
        /// <param name="ID">希望查詢的ID</param>
        /// <returns>是否存在資料庫內</returns>
        public bool QuestionnaireIDinDatabase(int state, int ID)
        {
            int[] IDs;
            using (ContextModel contextModel = new ContextModel())
            {
                var query =
                    from item in contextModel.Questionnaires
                    where item.QuestionnaireState >= state && item.QuestionnaireID == ID
                    select item.QuestionnaireID;
                if (query.ToArray().Count() > 0) //如果有查到值
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }



        public void DeleteQuestionnaires(int ID)
        {
            using (ContextModel contextModel = new ContextModel())
            {
                var item = contextModel.Questionnaires.First(c => c.QuestionnaireID == ID);
                item.QuestionnaireState = -1;//軟刪除
                contextModel.SaveChanges();
            }
        }
    }
}