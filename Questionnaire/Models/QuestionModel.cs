using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Models
{
    public class QuestionModel
    {
        public Guid QuestionID;             //問題ID
        public int QuestionnairesID;        //問卷ID
        public int QuestionType;            //問題種類，0文字1單選2複選
        public string QuestionContent;      //問題內容
        public int QuestionOrder;           //排序
        public bool Required;               //必填
        public string QuestionOptions;      //單多選的內文
    }
}