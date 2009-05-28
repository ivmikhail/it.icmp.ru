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
using ITCommunity;

namespace ITCommunity
{

    public partial class Mail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadMessage();
        }

        private void LoadMessage()
        {
            Message mess = Message.GetById(GetMessageId());
            if (mess.Receiver.Id == CurrentUser.User.Id || mess.Sender.Id == CurrentUser.User.Id)
            {
                Sender.Text = mess.Receiver.Nick;
                MessageTitle.Text = mess.Title;
                MessageText.Text = mess.Text;
                ReplyLink.Text = "<a href='mailsend.aspx?receiver=" + mess.Sender.Nick + "'>Ответить</a>";
                mess.MarkAsRead();
            } else
            {
                Response.Redirect("mailview.aspx");
            }
        }
        private int GetMessageId()
        {
            int mess_id;
            Int32.TryParse(Request.QueryString["id"], out mess_id);
            return mess_id;
        }
        protected void DeleteLink_Click(object sender, EventArgs e)
        {
            Message.GetById(GetMessageId()).DeleteByReceiver();
        }
}
}