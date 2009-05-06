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
    public partial class Mailview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadMessages();
        }
        private void LoadMessages()
        {
            int total_records = 0;
            int page = GetPage();
            if (IsOutput())
            {
                ListTitle.Text = "Исходящие";
                RepeaterMessages.DataSource = Message.GetBySender(CurrentUser.User.Id, page, Global.MaxMessageCount, ref total_records);
            } else
            {
                ListTitle.Text = "Входящие";
                RepeaterMessages.DataSource = Message.GetByReceiver(CurrentUser.User.Id, page, Global.MaxMessageCount, ref total_records);
            }
            RepeaterMessages.DataBind();

            FillPager(total_records, page, "");
        }
        private void FillPager(int total_records, int current_pagenum, string pageparams)
        {
            MessagePager.PagerPage = "default.aspx";
            MessagePager.PageParams = pageparams;
            MessagePager.PageQueryString = "page";
            MessagePager.CurrentPage = current_pagenum;
            MessagePager.TotalRecords = total_records;
            MessagePager.RecordsPerPage = Global.MaxMessageCount;
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
