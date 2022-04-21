using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.Models
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void front_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"~\FrontPages\FrontIndex.aspx");
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"~\BackPages\BackIndex.aspx");
        }
    }
}