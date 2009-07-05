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
    public partial class Addpoll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckPollDel();
                LoadPolls();
            }
        }

        private void CheckPollDel()
        {
            int del_id = GetDel();
            if (del_id > 0)
            {
                Poll del_poll = Poll.GetById(del_id);
                if (del_poll.Id > 0)
                {
                    Poll.Delete(del_poll.Id);
                }
            }
        }
        private void LoadPolls()
        {
            int total_records = 0;
            int page = GetPage();

            PollsView.PollSource = Poll.Get(page, Global.ConfigNumParam("PollsCount"), ref total_records);

            PollsPager.DataBind("addpoll.aspx", "", "page", page, total_records, Global.ConfigNumParam("PollsCount"));
        }
        private int GetPage()
        {
            int page_num;
            Int32.TryParse(Request.QueryString["page"], out page_num);
            return page_num == 0 ? 1 : page_num;
        }
        private int GetDel()
        {
            int id;
            Int32.TryParse(Request.QueryString["del"], out id);
            return id;
        }
        protected void LinkButtonAddPoll_Click(object sender, EventArgs e)
        {
            if (NewPollIsValid())
            {
                string topic = HttpUtility.HtmlEncode(TextBoxTopic.Text);
                bool is_multiselect = RadioButtonListMultiselect.Items[1].Selected;
                bool is_open = RadioButtonListIsOpen.Items[1].Selected;

                string[] pre_answers = TextBoxAnswers.Text.Replace("\r", "").Split('\n');
                string answers = "";
                for (int i = 0; i < pre_answers.Length; i++)
                {
                    if (answers != "")
                    {
                        answers += ",";
                    }
                    if (pre_answers[i] != "")
                    {
                        answers += HttpUtility.HtmlEncode(pre_answers[i]);
                    }
                }

                Poll.Add(topic, CurrentUser.User.Id, is_multiselect, is_open, answers);
                Response.Redirect("addpoll.aspx");
            }
        }
        private bool NewPollIsValid()
        {
            RequiredTopic.Validate();
            RequiredAnswers.Validate();

            return RequiredTopic.IsValid && RequiredAnswers.IsValid;
        }
    }
}