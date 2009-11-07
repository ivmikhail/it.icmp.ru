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
            //NOTE: bydlo-style code
            ITCommunity.User receiver = ITCommunity.User.GetByLogin(MessageReceiver.Text);
            if (receiver.Id > 0)
            {
                if (receiver.Id == CurrentUser.User.Id)
                {
                    Errors.Text = "Зачем отправлять сообщение самому себе? Не хватает общения? У нас этого делать нельзя.";
                } else
                {
                    Message.Send(receiver.Id, CurrentUser.User.Id, Server.HtmlEncode(MessageTitle.Text), Server.HtmlEncode(MessageText.Text));
                    Response.Redirect("mailview.aspx?a=output");
                }
            } else 
            {
                Errors.Text = "Пользователь с таким логином у нас не живет.";
            }
        }
        private string GetReceiver()
        {
            return Request.QueryString["receiver"];
        }
    }
}