using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Models
{
    public class StatisticalDataModel
    {
        public Guid QuestionID;
        public string Content;//問題文字
        public int type; //問題種類
        public string option;//選項內容
        public List<double> answerPercent; // 百分比，選項間用;隔開
        public List<int> answerCount;//選此選項的人數，選項間用;隔開

    }
}