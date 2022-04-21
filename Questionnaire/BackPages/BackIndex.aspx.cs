using Questionnaire.Managers;
using Questionnaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.BackPages
{
    public partial class BackIndex : System.Web.UI.Page
    {
        private QuestionnairesManager _mgr = new QuestionnairesManager();
        List<QuestionnairesModel> questionnairesList;

        protected void Page_Load(object sender, EventArgs e)
        {
            questionnairesList = _mgr.GetQuestionnaires();

        }

        protected void Button2_Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("/BackPage/BackQuestionnaire");
        }
    }
}