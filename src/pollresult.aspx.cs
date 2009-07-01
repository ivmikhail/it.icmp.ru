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
    public partial class PollResultPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Poll current = new Poll();
                int poll_id = GetPollId();
                if (poll_id > 0)
                {
                    current = Poll.GetById(poll_id);
                }
                if (current.Id < 1)
                {
                    current = Poll.GetActive();
                }


                PollResult.BindData(current);
            }
        }

        private int GetPollId()
        {
            int poll_id;
            Int32.TryParse(Request.QueryString["id"], out poll_id);
            return poll_id;
        }
    }
}
