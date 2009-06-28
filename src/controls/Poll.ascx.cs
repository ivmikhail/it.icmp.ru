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

namespace ITCommunity
{
    public partial class controls_Poll : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Poll current = Poll.GetActive();
                LiteralPollTopic.Text = current.Topic;
                List<PollAnswer> source = current.Answers;
                if (current.IsOpen)
                {
                    LiteralNote.Text = "Ёто открытый опрос. ƒругие соучастнеги будут видеть, кто как проголосовал.";
                } else
                {
                    LiteralNote.Text = "Ёто закрытый опрос.  то как проголосовал легально узнать нельз€.";
                }
                if (current.IsMultiSelect)
                {
                    CheckBoxListAnswer.Visible = true;
                    RadioButtonListAnswer.Visible = false;

                    CheckBoxListAnswer.DataSource = source;
                    CheckBoxListAnswer.DataBind();
                } else
                {
                    CheckBoxListAnswer.Visible = false;
                    RadioButtonListAnswer.Visible = true;

                    RadioButtonListAnswer.DataSource = source;
                    RadioButtonListAnswer.DataBind();
                }
            } 
        }
        protected void LinkButtonVote_Click(object sender, EventArgs e)
        {
            Poll current = Poll.GetActive();
            if(CurrentUser.isAuth)
            {
                string vote_ids = string.Empty;
                if(current.IsMultiSelect)
                {
                    for (int i = 0; i < CheckBoxListAnswer.Items.Count; i++)
                    {
                        if (CheckBoxListAnswer.Items[i].Selected)
                        {
                            if (vote_ids.Length > 0)
                            {
                                vote_ids += ",";
                            }
                            vote_ids += CheckBoxListAnswer.Items[i].Value;				
                        }
                    }
                } else 
                {
                    vote_ids = RadioButtonListAnswer.SelectedItem.Value;// SelectedValue;
                }
                if (vote_ids != string.Empty)
                {
                    current.Vote(CurrentUser.User, vote_ids);
                }
            }
        }
        protected void LinkButtonResult_Click(object sender, EventArgs e)
        {

        }
}
}
