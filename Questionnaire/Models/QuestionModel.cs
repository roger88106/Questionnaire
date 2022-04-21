using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Models
{
    public class QuestionModel
    {
        public Guid Question;
        public Guid QuestionnairesID;
        public int QuestionType;
        public string QuestionContent;
        public int QuestionOrder;//排序
        public int Required;
        public int QuestionOptions;
    }
}