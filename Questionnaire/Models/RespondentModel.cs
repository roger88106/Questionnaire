using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Models
{
    public class RespondentModel
    {
        public Guid RespondentID;       //回答者的ID
        public int QuestionnairesID;    //問卷ID
        public string Name;             //回答者姓名
        public string PhoneNumber;      //回答者電話號碼
        public string Email;            //回答者Email
        public int Age;              //回答者年齡
        public DateTime FillTime;       //填寫時間
    }
}