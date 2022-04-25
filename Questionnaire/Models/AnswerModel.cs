using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Models
{
    public class AnswerModel
    {
        public Guid AnswerID;           //這題答案的ID
        public Guid RespondentID;       //回答者的ID
        public int QuestionnaireID;     //問卷ID(方便搜尋)
        public Guid QuestionID;         //問題的ID
        public string Answer;           //答案(多選答案用,分割)
    }
}