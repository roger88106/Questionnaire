using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.BackPages
{
    public partial class BackQuestionnaire : System.Web.UI.Page
    {
        string _Mode = "NewQuestionnaire";//預設是新增模式
        protected void Page_Load(object sender, EventArgs e)
        {
            //如果讀取到QueryString的ID值，且此ID是合法且找的到資料的話才進入編輯模式

        }
    }
}