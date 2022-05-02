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
        public List<QuestionnairesModel> GetQuestionnaireList(int state)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query =
                        from item in contextModel.Questionnaires
                        where item.QuestionnaireState >= state
                        orderby item.QuestionnaireID
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
                return new List<QuestionnairesModel>();
            }
        }

        /// <summary>
        /// 查詢單筆問卷資料
        /// </summary>
        /// <param name="state">狀態，-1為查詢全部，0為查詢除了刪除外的，1只查詢已啟用的</param>
        /// <param name="ID">想搜尋的ID</param>
        /// <returns></returns>
        public QuestionnairesModel GetQuestionnaire(int state, int ID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query =
                        from item in contextModel.Questionnaires
                        where item.QuestionnaireState >= state && item.QuestionnaireID == ID
                        select new QuestionnairesModel
                        {
                            QuestionnaireID = item.QuestionnaireID,
                            StartTime = item.StartTime,
                            EndTime = item.EndTime,
                            QuestionnaireTital = item.QuestionnaireTital,
                            QuestionnaireContent = item.QuestionnaireContent,
                            QuestionnaireState = item.QuestionnaireState
                        };
                    return query.ToArray()[0];
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 修改問卷資料
        /// </summary>
        /// <param name="ID">問卷ID</param>
        /// <param name="questionnaire">新的問卷資料</param>
        public void UpdateQuestionnaire(int ID, QuestionnairesModel questionnaire)
        {
            using (ContextModel contextModel = new ContextModel())
            {
                var item = contextModel.Questionnaires.First(c => c.QuestionnaireID == ID);
                item.QuestionnaireTital = questionnaire.QuestionnaireTital;
                item.QuestionnaireContent = questionnaire.QuestionnaireContent;
                item.StartTime = questionnaire.StartTime;
                item.EndTime = questionnaire.EndTime;
                item.QuestionnaireState = questionnaire.QuestionnaireState;
                contextModel.SaveChanges();
            }
        }

        /// <summary>
        /// 查詢問卷資料庫內是否有該ID存在
        /// </summary>
        /// <param name="state">狀態(0為只查沒被刪除的，1為只查已經啟用的，-1為包括已刪除的全部查詢)</param>
        /// <param name="ID">希望查詢的ID</param>
        /// <returns>是否存在資料庫內</returns>
        public bool SelectQuestionnaireIDinDatabase(int state, int ID)
        {
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

        /// <summary>
        /// 刪除問卷(軟刪除)
        /// </summary>
        /// <param name="ID">想刪除的問卷ID</param>
        public void DeleteQuestionnaires(int ID)
        {
            using (ContextModel contextModel = new ContextModel())
            {
                var item = contextModel.Questionnaires.First(c => c.QuestionnaireID == ID);
                item.QuestionnaireState = -1;//軟刪除
                contextModel.SaveChanges();
            }
        }


        public int InsertQuestionnaires(QuestionnairesModel questionnaire)
        {
            if (questionnaire == null)
            {
                return -1;
            }
            else
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var Questionnaire = new Questionnaire.ORM.Questionnaire()
                    {
                        QuestionnaireContent = questionnaire.QuestionnaireContent,
                        QuestionnaireState = questionnaire.QuestionnaireState,
                        QuestionnaireTital = questionnaire.QuestionnaireTital,
                        EndTime = questionnaire.EndTime,
                        StartTime = questionnaire.StartTime
                    };
                    contextModel.Questionnaires.Add(Questionnaire);
                    contextModel.SaveChanges();

                    var query =
                    from item in contextModel.Questionnaires
                    orderby item.QuestionnaireID descending
                    select item.QuestionnaireID;
                    //最新的問卷會在最後面，所以找到最後一個ID並回傳
                    foreach (var ID in query.ToArray())
                    {
                        return ID;
                    }
                }
            }
            return -1;

        }
    }
}