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
        private static Poll current;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                current = Poll.GetActive();
                BindPollControlsAndData();
                bool userAlreadyVote = current.UserAlreadyVoted(CurrentUser.User);
                if (userAlreadyVote)
                {
                    LinkButtonVote.Visible = false;
                    UserVotedText.Visible = true;
                } else
                {
                    LinkButtonVote.Visible = true;
                    UserVotedText.Visible = false;
                }
            }
        }

        private void BindPollControlsAndData()
        {
            LiteralPollTopic.Text = current.Topic;

            if (current.IsOpen)
            {
                LiteralNote.Text = "��� �������� �����. ������ ����������� ����� ������, ��� ��� ������������.";
            } else
            {
                LiteralNote.Text = "��� �������� �����. ��� ��� ������������ �������� ������ ������.";
            }

            List<PollAnswer> source = current.Answers;

            if (current.IsMultiSelect)
            {
                BindCheckBoxList(source);

            } else
            {
                BindRadioButtonList(source);
            }
        }
        private void BindCheckBoxList(List<PollAnswer> answers)
        {
            CheckBoxListAnswer.Visible = true;
            RadioButtonListAnswer.Visible = false;

            CheckBoxListAnswer.DataSource = answers;

            CheckBoxListAnswer.DataValueField = "id";
            CheckBoxListAnswer.DataTextField = "text";
            CheckBoxListAnswer.DataBind();
        }
        private void BindRadioButtonList(List<PollAnswer> answers)
        {
            CheckBoxListAnswer.Visible = false;
            RadioButtonListAnswer.Visible = true;

            RadioButtonListAnswer.DataSource = answers;

            RadioButtonListAnswer.DataValueField = "id";
            RadioButtonListAnswer.DataTextField = "text";

            RadioButtonListAnswer.DataBind();
        }

        private string GetVotedIds()
        {
            string vote_ids = string.Empty;
            if (current.IsMultiSelect)
            {
                foreach (ListItem item in CheckBoxListAnswer.Items)
                {
                    if (item.Selected)
                    {
                        if (vote_ids.Length > 0)
                        {
                            vote_ids += ",";
                        }
                        vote_ids += item.Value;
                    }
                }
            } else
            {
                vote_ids = RadioButtonListAnswer.SelectedItem.Value;// SelectedValue;
            }

            return vote_ids;
        }


        protected void LinkButtonVote_Click(object sender, EventArgs e)
        {
            string poll_message = "�� �� ������ ����������, ����� ����� ����� ����������� ����������������� ��� ���������������.";
            
            if (CurrentUser.isAuth)
            {
                if (current.UserAlreadyVoted(CurrentUser.User))
                {
                    poll_message = "��� ����� �� ��������. �� ��� ����������.";
                } else
                {
                    string vote_ids = GetVotedIds();
                    if (vote_ids == string.Empty)
                    {
                        poll_message = "��� ����� �� ��������. ����� ������� ���� �� ���� ������� ������.";
                    } else
                    {
                        current.Vote(CurrentUser.User, vote_ids);
                        poll_message = "��� ����� ��������. ������� �� ����������� ����������!";
                    }
                }
            }
            Session["poll_message"] = poll_message; // Note: �����
            Response.Redirect("pollresult.aspx");
        }
}
}
