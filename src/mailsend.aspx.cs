using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI;
using ITCommunity;

namespace ITCommunity
{
    public partial class Mailsend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MessageReceiver.Text = GetReceiver();
            }
        }
        protected void LinkButtonSend_Click(object sender, EventArgs e)
        {
            ITCommunity.User receiver = ITCommunity.User.GetByLogin(MessageReceiver.Text);
            if (receiver.Id > 0)
            {
                Message.Send(receiver.Id, CurrentUser.User.Id, Server.HtmlEncode(MessageTitle.Text), Server.HtmlEncode(MessageText.Text));
                Response.Redirect("mailview.aspx?a=output");
            } else
            {
                Errors.Text = "ѕользователь с таким логином у нас не живет.";
            }
        }
        private string GetReceiver()
        {
            return Request.QueryString["receiver"];
        }
    }
}