using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using ITCommunity;

namespace ITCommunity
{
    /// <summary>
    /// ������������ ���������� � ��
    /// </summary>

    public class User
    {
        //������� ������ �������� ����. ���������. ������ �� ����, ����� ��� ����������� �����������
        private delegate object LastUsersLoader(int count);
        //������� ������ �������� ����. ���������. ������ �� ����, ����� ��� ����������� �����������
        private delegate object TopPostersLoader(int count);
        //������� ������ �������� ���������� ������ �� ����, ����� ��� ����������� �����������
        private delegate object UsersStatLoader(); 

        private int _id;
        private string _pass;
        private string _nick;
        private string _email;
        private int _role;
        private DateTime _cdate;

        public enum Roles
        {
            /// <summary>
            /// ����� ���� "poster" + ����� ��������� �������������, ��������������, 
            /// ����� �������� �����, ����� ������������� ����� �����.
            /// </summary>
            Admin = 1,
            /// <summary>
            /// ����� ���� "user" + ����� ���������/������������� ���� �������
            /// </summary>
            Poster = 2,
            /// <summary>
            /// ������� ������������, ����� ����������, �������������� ��� �����, 
            /// ��������� �������� ������� ������. Readonly ������.
            /// </summary>
            User = 3,
            /// <summary>
            /// ������� �������(�� ����� ������������)
            /// </summary>
            Banned = 4
        }

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

        public string Pass
        {
            get
            {
                return _pass;
            }
            set
            {
                _pass = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public User.Roles Role
        {
            get
            {
                return (Roles)Enum.ToObject(typeof(Roles), _role);
            }
            set
            {
                _role = (int)value;
            }
        }

        public string Nick
        {
            get
            {
                return _nick;
            }
            set
            {
                _nick = value;
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

        public User(int id, string nick, string pass, DateTime cdate, User.Roles role, string email)
        {
            _id = id;
            _nick = nick;
            _pass = pass;
            _cdate = cdate;
            _role = (int)role;
            _email = email;
        }

        public User()
        {
            _id = -1;
            _nick = "anonymous";
            _pass = "";
            _cdate = DateTime.Now;
            _role = 3;
            _email = "bill@microsoft.com";
        }



        /// <summary>
        /// ��������� ������� �� ������� ������������
        /// </summary>
        public bool IsBanned()
        {
            return (this.Role == Roles.Banned);
        }


        public void Update()
        {
            Database.UserUpdate(this._id, this._pass, (byte)this._role, this._email);
        }

        /// <summary>
        /// �������� ������������ �� ���� �� ������
        /// </summary>
        /// <param name="login">login �� �� nick</param>
        public static User GetByLogin(string login)
        {
            return GetUserFromRow(Database.UserGetByLogin(login));
        }

        /// <summary>
        /// �������� ������������ �� ���� �� ������(������ ���������)
        /// </summary>
        /// <param name="email">������������</param>
        public static User GetByEmail(string email)
        {
            return GetUserFromRow(Database.UserGetByEmail(email.Trim()));
        }

        /// <summary>
        /// �������� ������������ �� ��������������
        /// </summary>
        /// <param name="userId">�������������</param>
        public static User GetById(int userId)
        {
            return GetUserFromRow(Database.UserGetById(userId));
        }

        /// <summary>
        /// �������� ������������� �� �����
        /// </summary>
        /// <param name="role">���� ���������� �������������</param>
        public static List<User> GetByRole(User.Roles role)
        {
            //TODO: ������������ ������� � ��������?
            return GetUsersFromTable(Database.UserGetByRole((int)role));
        }

        /// <summary>
        /// �������� ��������� �������������������
        /// </summary>
        /// <param name="count">���������� ������ �������������</param>
        public static List<User> GetLastRegistered(int count)
        {
            LastUsersLoader loader = new LastUsersLoader(GetLastRegisteredFromDB);
            List<User> cats = (List<User>)AppCache.Get(Global.ConfigStringParam("LastUsersCacheName"),
                                                       new object(),
                                                       loader,
                                                       new object[] { count },
                                                       DateTime.Now.AddHours(Global.ConfigDoubleParam("LastUsersCachePer")));
            return cats;
  

        }

        private static List<User> GetLastRegisteredFromDB(int count)
        {
            return GetUsersFromTable(Database.UserGetLastRegistered(count));
        }

        /// <summary>
        /// ������������ �������������� �� ������ ������� ������ � ������.
        /// 
        /// ����� �� ��������� "����������" ������. ��� �� ����� ������.
        /// ����� ��������� "����������" ����������� PollAnswer.GetUsers()
        /// </summary>
        /// <param name="answer_id">������������� �������� ������</param>
        /// <returns>������ �������� User</returns>
        public static List<User> GetAnswerVoters(int answer_id)
        {
            return GetUsersFromTable(Database.PollGetAnswerVoters(answer_id));
        }

        /// <summary>
        /// �������� ����� �������� �������� �� ����
        /// </summary>
        /// <param name="count">���-�� ������ �������������</param>
        public static List<KeyValuePair<string, string>> GetTopPosters(int count)
        {
            TopPostersLoader loader = new TopPostersLoader(GetTopPostersFromDB);
            List<KeyValuePair<string, string>> top = (List<KeyValuePair<string, string>>)AppCache.Get(Global.ConfigStringParam("TopPostersCacheName"),
                                                                                                      new object(),
                                                                                                      loader,
                                                                                                      new object[] { count },
                                                                                                      DateTime.Now.AddHours(Global.ConfigDoubleParam("TopPostersCachePer")));

            return top;
   
        }

        private static List<KeyValuePair<string, string>> GetTopPostersFromDB(int count)
        {
            List<KeyValuePair<string, string>> top = new List<KeyValuePair<string, string>>();
            DataTable dt = Database.UserGetTopPosters(count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string username = dt.Rows[i]["usernick"].ToString();
                string text = dt.Rows[i]["postcount"].ToString();
                top.Add(new KeyValuePair<string, string>(username, text));
            }
      
            return top;
        }

        /// <summary>
        /// �������� ���������� �� �������������(���-�� �������������, �������, ��������) �� ����
        /// </summary>
        public static List<KeyValuePair<string, string>> GetStats()
        {
            UsersStatLoader loader = new UsersStatLoader(GetStatsFromDB);
            List<KeyValuePair<string, string>> stat = (List<KeyValuePair<string, string>>)AppCache.Get(Global.ConfigStringParam("UsersStatCacheName"),
                                                                                                      new object(),
                                                                                                      loader,
                                                                                                      null,
                                                                                                      DateTime.Now.AddHours(Global.ConfigDoubleParam("UsersStatCachePer")));

            return stat;
   
        }

        public static List<KeyValuePair<string, string>> GetStatsFromDB()
        {
            List<KeyValuePair<string, string>> top = new List<KeyValuePair<string, string>>();
            DataTable dt = Database.UserGetStat();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string key = dt.Rows[i]["key"].ToString();
                string value = dt.Rows[i]["value"].ToString();
                top.Add(new KeyValuePair<string, string>(key, value));
            }
            return top;
        }

        /// <summary>
        /// ������� �������. �������� �� ������.
        /// </summary>
        /// <param name="userId">������������� ������������</param>
        public static void Delete(int userId)
        {
            throw new NotImplementedException();
            //� ���� �� �������? ���� ������� ����� � �.� �� �� �� �������.
            //Database.UserDel(userId);
        }

        /// <summary>
        /// ��������� ������� � ��
        /// </summary>
        /// <param name="User">������������ �������� ����� �������� � ����</param>
        public static User Add(User user)
        {
            DataRow dr = Database.UserAdd(user.Nick, user.Pass, (byte)user.Role, user.Email);
            return GetUserFromRow(dr);
        }

        private static List<User> GetUsersFromTable(DataTable dt)
        {
            List<User> users = new List<User>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                users.Add(GetUserFromRow(dt.Rows[i]));
            }
            return users;
        }

        private static User GetUserFromRow(DataRow dr)
        {
            User user;
            if (dr == null)
            {
                user = new User();
            } else
            {
                user = new User(Convert.ToInt32(dr["id"]),
                                     Convert.ToString(dr["nick"]),
                                     Convert.ToString(dr["pass"]),
                                     Convert.ToDateTime(dr["cdate"]),
                                     (User.Roles)Convert.ToInt16(dr["role"]),
                                     Convert.ToString(dr["email"]));
            }
            return user;
        }
    }
}