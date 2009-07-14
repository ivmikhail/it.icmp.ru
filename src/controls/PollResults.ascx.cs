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

using OpenFlashChart;

namespace ITCommunity
{
    public partial class controls_PollResults : System.Web.UI.UserControl
    {

        OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();
        Poll current_poll;
        public void BindData(Poll poll)
        {
            current_poll = poll;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                //загрузка опроса, первое что должно быть сделано
                LoadPollData();
                //загрузка сообщений
                LoadMessages();

                //загрузка данных об избирателях
                LiteralVoters.Text = GetVotersData();
            }
        }
        private void LoadMessages()
        {
            // сообщение, защитан ли голос
            string vote_message = (string)Session["poll_message"];
            if (vote_message != "")
            {
                LiteralPollMessage.Text = vote_message;
                Session.Remove("poll_message");
            }

            //еще одно сообщение
            string how_select = "Можно выбрать только один вариант";
            string del_link = "";
            if(CurrentUser.User.Role == User.Roles.Admin)
            {
                del_link = "(<a href='addpoll.aspx?del=" + current_poll.Id + "'title='удалить опрос'>удалить этот опрос</a>)";
            }
            if (current_poll.IsMultiSelect)
            {
                how_select = "Можно выбрать несколько вариантов";
            }
            LiteralPollInfo.Text = how_select + ", всего проголосовало - " + current_poll.VotesCount + " чел." + del_link;
        }
        private void LoadPollData()
        {
           // current_poll = Poll.GetActive();
            chart.AddElement(GetPieData(current_poll));
            chart.Title = new Title(current_poll.Topic);
            chart.Title.Style = "{font-size: 20px; color: #767676;}";

            OpenFlashChartControl.EnableCache = false;
            chart.Bgcolor = "#ffffff";
            OpenFlashChartControl.Chart = chart;
        }
        private OpenFlashChart.Pie GetPieData(Poll poll)
        {
            OpenFlashChart.Pie pie = new OpenFlashChart.Pie();

            List<PieValue> values = new List<PieValue>();
            foreach (PollAnswer ans in poll.Answers)
            {
                values.Add(new PieValue(ans.VotesCount, ans.Text));
            }
            pie.Values = values;
            pie.FontSize = 10;
            pie.Alpha = .5;
            //pie.Colours = new string[] { "#04f", "#1ff", "#6ef", "#f30"} 
            pie.Colours = new string[] { "#f00ccc", "#2b00db", "#f29100", "#9cc400", "#d6000", "#b1006a", "#29ce00", "#00cb61" };
            pie.Tooltip = "#label#, #val# of #total##percent# of 100%";
            return pie;
        }

        /// <summary>
        /// Возвращает хтмл данные: какой пользователь за что голосовал
        /// 
        /// Довольно дорогостоящая операция.
        /// </summary>
        /// <returns></returns>
        private string GetVotersData()
        {
            string result = "Голосование закрытое, данные не доступны.";
            if (current_poll.IsOpen)
            {
                result = "<dl id='voters-data'>";
                foreach (PollAnswer ans in current_poll.Answers)
                {
                    result += "<dt><h3>" + ans.Text + "</h3></dt>";
                    string users = "<dd>";
                    foreach (User user in ans.GetUsers())
                    {
                        users += "<span><a href='mailsend.aspx?receiver=" + user.Nick + "' title='Отправить ообщение' >" + user.Nick + "</a></span>";
                    }
                    users += "</dd>";
                    result += users;
                }
                result += "</dl>";
            }
            return result;
        }
    }
}

