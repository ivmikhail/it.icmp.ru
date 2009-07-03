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

    //NOTE: bydlo style code

    public partial class Mail : System.Web.UI.Page
    {
        private bool is_input;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadMessage();
        }

        private void LoadMessage()
        {
            Message mess = Message.GetById(GetMessageId());
            if (mess.Receiver.Id == CurrentUser.User.Id || mess.Sender.Id == CurrentUser.User.Id)
            {
                MessageTitle.Text = mess.Title;
                MessageText.Text = mess.Text;
                mess.MarkAsRead();

                DeterminateIsInput(mess);

                if (is_input)
                {
                    ReplyLink.Text = "<a href='mailsend.aspx?receiver=" + mess.Sender.Nick + "'>��������</a>";
                    LiteralWho.Text = "<h3>�� ����</h3>" + mess.Sender.Nick;
                } else
                {
                    LiteralWho.Text = "<h2>����������</h2>" + mess.Receiver.Nick;  
                }
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
        private void DeterminateIsInput(Message mess)
        {
            if (mess.Receiver.Id == CurrentUser.User.Id)
            {
                is_input = true;
            } else if (mess.Sender.Id == CurrentUser.User.Id)
            {
                is_input = false;
            } else
            {
                Response.Redirect("mailview.aspx");
            }
        }
        protected void DeleteLink_Click(object sender, EventArgs e)
        {
            Message mess = Message.GetById(GetMessageId()); 
            DeleteMessage(mess);
        }

        private void DeleteMessage(Message mess)
        {
            if (is_input)
            {
                mess.DeleteByReceiver();
            } else
            {
                mess.DeleteBySender();
            }
            Response.Redirect(GetBackUrl());
        }
        protected void LinkButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetBackUrl());
        }

        private string GetBackUrl()
        {
            string url = "mailview.aspx";
            if (!is_input)
            {
                url += "?a=output";
            }
            return url;
        }
}
}