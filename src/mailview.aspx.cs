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
using System.Collections.Generic;
using ITCommunity;

namespace ITCommunity
{
    public partial class Mailview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMessages();
            }
        }
        private void LoadMessages()
        {
            int total_records = 0;
            int page = GetPage();
            List<Message> messages = new List<Message>();
            string pageparam = "";
            if (IsOutput())
            {
                ListTitle.Text = "Исходящие";
                messages = Message.GetBySender(CurrentUser.User.Id, page, Global.ConfigNumParam("MaxMessageCount"), ref total_records);
                pageparam = "&a=output";
            } else
            {
                ListTitle.Text = "Входящие";
                messages = Message.GetByReceiver(CurrentUser.User.Id, page, Global.ConfigNumParam("MaxMessageCount"), ref total_records);
            }
            RepeaterMessages.DataSource = messages;
            RepeaterMessages.DataBind();
            MessagePager.Fill("mailview.aspx", pageparam, "page", page, total_records, Global.ConfigNumParam("MaxMessageCount"));
        }

        protected void RepeaterMessages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                Message current = (Message)item.DataItem;
                Literal who = (Literal)item.FindControl("Who");
                if (IsOutput())
                {
                    who.Text = "получатель: <a href='mailsend.aspx?receiver=" + current.Receiver.Nick + "' title='Отправить сообщение'>" + current.Receiver.Nick + "</a>";
                } else
                {
                    who.Text = "автор: <a href='mailsend.aspx?receiver=" + current.Sender.Nick + "' title='Отправить сообщение'>" + current.Sender.Nick + "</a>"; 
                }
            }
        }

        private int GetPage()
        {
            int page_num;
            Int32.TryParse(Request.QueryString["page"], out page_num);
            return page_num == 0 ? 1 : page_num;
        }
        private bool IsOutput()
        {
            return Request.QueryString["a"] == "output";
        }
    }
}
