using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Models
{
    public class CsvModel
    {
        public string Name;
        public string Phone;
        public string Email;
        public string Age;
        public List<string> Question;
        public List<string> Answer;
    }
}