using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ITCommunity
{
    public partial class PollsArchive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPolls();
            }
        }
        private void LoadPolls()
        {
            int total_records = 0;
            int page = GetPage();

            PollsView.PollSource = Poll.Get(page, Global.ConfigNumParam("PollsCount"), ref total_records);

            PollsPager.DataBind("polls.aspx", "", "page", page, total_records, Global.ConfigNumParam("PollsCount"));
        }
        private int GetPage()
        {
            int page_num;
            Int32.TryParse(Request.QueryString["page"], out page_num);
            return page_num == 0 ? 1 : page_num;
        }
    }
}
