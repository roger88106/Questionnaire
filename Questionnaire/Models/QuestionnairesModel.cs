using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Models
{
    public class QuestionnairesModel
    {
        struct Questionnaires
        {
            public Guid QuestionnaireID;
            public DateTime StartTime;//起始時間
            public DateTime EndTime;//結束時間
            public string QuestionnaireTital;//問卷標題
            public string QuestionnaireContent;//問題簡述
            public int QuestionnaireState;//狀態(-1為刪除,0為未啟用,1為啟用)
        }
    }
}