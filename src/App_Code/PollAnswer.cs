using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace ITCommunity
{
    /// <summary>
    /// Ответ на опрос
    /// </summary>
    public class PollAnswer
    {
        private int _id;
        private int _poll_id;
        private string _text;
        private int _votes_count;  

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public Poll Poll
        {
            get
            {
                return Poll.GetById(_poll_id);
            }
            set
            {
                _poll_id = value.Id;
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public int VotesCount
        {
            get
            {
                return _votes_count;
            }
            set
            {
                _votes_count = value;
            }
        }

        public PollAnswer()
        {
            _id = -1;
            _poll_id = -1;
            _text = "null";
            _votes_count = 0;
        }

        public PollAnswer(int id, int poll_id, string text, int votes_count)
        {
            _id = id;
            _poll_id = poll_id;
            _text = text;
            _votes_count = votes_count;
        }
        /// <summary>
        /// Возвращает список прогосовавших за этот пункт, если опрос открытый, иначе пустой список
        /// </summary>
        /// <returns>Список проголосоваших</returns>
        public List<User> GetUsers()
        {
            List<User> users;
            if (Poll.IsOpen)
            {
                users = User.GetAnswerVoters(_id);
            } else
            {
                users = new List<User>();
            }

            return users;
        }

        /// <summary>
        /// Возвращает список вариантов ответа опроса
        /// </summary>
        /// <param name="poll_id">Идентификатор опроса</param>
        /// <returns>Список обьектов PollAnswer(пустой если опроса не существует)</returns>
        public static List<PollAnswer> Get(int poll_id)
        {
            return GetAnswersFromTable(Database.PollGetAnswers(poll_id));
        }

        private static List<PollAnswer> GetAnswersFromTable(DataTable dt)
        {
            List<PollAnswer> answers = new List<PollAnswer>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                answers.Add(GetAnswerFromRow(dt.Rows[i]));
            }
            return answers;
        }

        private static PollAnswer GetAnswerFromRow(DataRow dr)
        {
            PollAnswer answer;
            if (dr == null)
            {
                answer = new PollAnswer();
            } else
            {
                answer = new PollAnswer(
                                Convert.ToInt32(dr["id"]),
                                Convert.ToInt32(dr["poll_id"]),
                                Convert.ToString(dr["text"]),
                                Convert.ToInt32(dr["vote_count"]));
            }
            return answer;
        }

    }
}
