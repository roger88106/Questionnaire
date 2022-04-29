using Questionnaire.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.BackPages
{
    public partial class BackStatisticalData : System.Web.UI.Page
    {
        StatisticalDataManager _mgr = new StatisticalDataManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            _mgr.GetStatisticalData(1);
        }
    }
}