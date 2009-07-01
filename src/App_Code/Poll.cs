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
    /// ����� - ������(�����������). ���������� ����� ������ ������������� ������������
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
        /// ������� �����(��������� �����������)
        /// </summary>
        /// <returns>���������� �����</returns>
        public static Poll GetActive()
        {
            //�� ����������! � �� ���������� ����� ����������� � ���������. ��� ����������?
            return GetPollFromRow(Database.PollGetLast());
        }

        /// <summary>
        /// �������� ����� 
        /// </summary>
        /// <param name="id">������������� ������</param>
        /// <returns>������ Poll</returns>
        public static Poll GetById(int id)
        {
            return GetPollFromRow(Database.PollGetById(id));
        }

        /// <summary>
        /// ��������� �������, ����� ��� ������
        /// </summary>
        /// <param name="page">������� ��������</param>
        /// <param name="count">���-�� ������� �� ��������</param>
        /// <param name="polls_count">����� ���-�� �������</param>
        /// <returns></returns>
        public static List<Poll> Get(int page, int count,ref int polls_count)
        {
            return GetPollsFromTable(Database.PollGet(page, count, ref polls_count));
        }

        /// <summary>
        /// �����������, ����� ��� �������� ����� �� ������ ������� ���������� � ������� ��������.
        /// </summary>
        /// <param name="answers">�������������� ������� ����� �������, ���� ����� ������� ������ ���� �������, �� �� ����� ��������� ������(�������) ���� �������</param>
        public void Vote(User user, string answers)
        {
            Database.PollVote(_id, user.Id, answers);
        }

        /// <summary>
        /// ����� �� ���������� ������� ������������(������������� ��������������)
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
        /// ��������� �� ������ �������������� ������������
        /// </summary>
        /// <param name="user">������������</param>
        /// <returns></returns>
        public bool UserAlreadyVoted(User user)
        {
            return (0 < (int)Database.PollIsUserVoted(user.Id, this.Id));
        }

        /// <summary>
        /// ������� ����� �� ����� ���������
        /// </summary>
        /// <param name="id">������������� ������</param>
        public static void Delete(int id)
        {
            Database.PollDel(id);
        }

        /// <summary>
        /// ���������� ������ ������
        /// </summary>
        /// <param name="topic">�����(������)</param>
        /// <param name="author_id">������������� ������</param>
        /// <param name="is_multiselect">����� �� ������� ��������� ��������� ������</param>
        /// <param name="is_open">�������� �� ����� ��������(����� �������� ��� ��� ���������)</param>
        /// <param name="answers">������� ����� �������</param>
        /// <returns></returns>
        public static Poll Add(string topic, int author_id, bool is_multiselect, bool is_open, string answers)
        {
            return GetPollFromRow(Database.PollAdd(topic, author_id, is_multiselect == true ? 1 : 0, is_open == true ? 1 : 0, answers));
        }
    }
}
