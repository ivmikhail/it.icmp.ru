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
    /// Класс - опросы(голосование). Голосовать могут только авторзованные пользователи
    /// </summary>
    public class Poll
    {
        private int _id;
        private string _topic;
        private int _author_id;
        private int _is_multiselect;
        private int _is_open;
        private int _votes_count;
        private List<PollAnswer> _answers;
        private DateTime _cdate;

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

        public string Topic
        {
            get
            {
                return _topic;
            }
            set
            {
                _topic = value;
            }
        }

        public User Author
        {
            get
            {
                return User.GetById(_author_id);
            }
            set
            {
                _author_id = value.Id;
            }
        }

        public bool IsMultiSelect
        {
            get
            {
                return _is_multiselect != 0;
            }
            set
            {
                _is_multiselect = value ? 1 : 0;
            }
        }

        public bool IsOpen
        {
            get
            {
                return _is_open != 0;
            }
            set
            {
                _is_open = value ? 1 : 0;
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

        public List<PollAnswer> Answers
        {
            get
            {
                return _answers;
            }
            set
            {
                _answers = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return _cdate;
            }
            set
            {
                _cdate = value;
            }
        }
        public Poll(int id,
                    string topic,
                    int author_id,
                    int is_multiselect,
                    int is_open,
                    int votes_count,
                    List<PollAnswer> answers,
            DateTime cdate)
        {
            _id = id;
            _topic = topic;
            _author_id = author_id;
            _is_multiselect = is_multiselect;
            _is_open = is_open;
            _votes_count = votes_count;
            _answers = answers;
            _cdate = cdate;
        }

        public Poll()
        {
            _id = -1;
            _topic = "null";
            _author_id = -1;
            _is_multiselect = 0;
            _is_open = 0;
            _votes_count = 0;
            _answers = new List<PollAnswer>();
            _cdate = DateTime.Now;
        }
        private static List<Poll> GetPollsFromTable(DataTable dt)
        {
            List<Poll> polls = new List<Poll>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                polls.Add(GetPollFromRow(dt.Rows[i]));
            }
            return polls;
        }

        private static Poll GetPollFromRow(DataRow dr)
        {
            Poll poll;
            if (dr == null)
            {
                poll = new Poll();
            } else
            {
                int poll_id = Convert.ToInt32(dr["id"]);
                poll = new Poll(poll_id,
                                Convert.ToString(dr["topic"]),
                                Convert.ToInt32(dr["author_id"]),
                                Convert.ToInt32(dr["is_multiselect"]),
                                Convert.ToInt32(dr["is_open"]),
                                Convert.ToInt32(dr["votes_count"]),
                                PollAnswer.Get(poll_id),
                                Convert.ToDateTime(dr["cdate"]));
            }
            return poll;
        }

        /// <summary>
        /// Текущий опрос(последний добавленный)
        /// </summary>
        /// <returns>Актуальный опрос</returns>
        public static Poll GetActive()
        {
            //Не кешировать! А то результаты будут обновляться с задержкой. Или кешировать?
            return GetPollFromRow(Database.PollGetLast());
        }

        /// <summary>
        /// Получаем опрос 
        /// </summary>
        /// <param name="id">Идентификатор опроса</param>
        /// <returns>Обьект Poll</returns>
        public static Poll GetById(int id)
        {
            return GetPollFromRow(Database.PollGetById(id));
        }

        /// <summary>
        /// Коллекция опросов, нужен для архива
        /// </summary>
        /// <param name="page">Текущая страница</param>
        /// <param name="count">Кол-во опросов на странице</param>
        /// <param name="polls_count">Общее кол-во опросов</param>
        /// <returns></returns>
        public static List<Poll> Get(int page, int count,ref int polls_count)
        {
            return GetPollsFromTable(Database.PollGet(page, count, ref polls_count));
        }

        /// <summary>
        /// Голосование, метод сам проверит может ли данный человек голосовать и обновит счетчики.
        /// </summary>
        /// <param name="answers">Идентификаторы ответов через запятую, если можно выбрать только один вариант, то за ответ возьмется первый(нулевой) член массива</param>
        public void Vote(User user, string answers)
        {
            if (CanVote())
            {
                if (this.IsMultiSelect)
                {
                    Database.PollVote(_id, user.Id, answers);
                } else
                {
                    Database.PollVote(_id, user.Id, answers);
                }
            }
        }

        /// <summary>
        /// Может ли голосовать текущий пользователь(необязательно авторизованный)
        /// </summary>
        /// <param name="user">CurrentUser</param>
        /// <returns></returns>
        public bool CanVote()
        {
            bool result = false;

            if(CurrentUser.isAuth)
            {
                if(!UserAlreadyVoted(CurrentUser.User))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Голосовал ли данный авторизованный пользователь
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public bool UserAlreadyVoted(User user)
        {
            return (1 == (int)Database.PollIsUserVoted(user.Id, this.Id));
        }
    }
}
