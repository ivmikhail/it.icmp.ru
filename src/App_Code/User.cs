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
    /// Пользователь хранящийся в БД
    /// </summary>

    public class User
    {
        //делегат метода загрузки посл. зарегистр. юзеров из базы, нужен для организации кеширования
        private delegate object LastUsersLoader(int count);
        //делегат метода загрузки посл. зарегистр. юзеров из базы, нужен для организации кеширования
        private delegate object TopPostersLoader(int count);
        //делегат метода загрузки статистики юзеров из базы, нужен для организации кеширования
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
            /// Права роли "poster" + может управлять голосовалками, пользователями, 
            /// может аттачить посты, может редактировать любые посты.
            /// </summary>
            Admin = 1,
            /// <summary>
            /// Права роли "user" + может добавлять/редактировать свои новости
            /// </summary>
            Poster = 2,
            /// <summary>
            /// Простой пользователь, может голосовать, комментировать без капчи, 
            /// доступные закрытые разделы сайтов. Readonly короче.
            /// </summary>
            User = 3,
            /// <summary>
            /// Аккаунт забанен(не может залогиниться)
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
        /// Проверяем забанен ли текущий пользователь
        /// </summary>
        public bool IsBanned()
        {
            return (this.Role == Roles.Banned);
        }


        public void Update()
        {
            Database.UserUpdate(this._id, this._pass, (byte)this._role, this._email);
            RemoveUserFromList(this._id);
        }

        /// <summary>
        /// Получаем пользователя из базы по логину
        /// </summary>
        /// <param name="login">login он же nick</param>
        public static User GetByLogin(string login)
        {
            return GetUserFromRow(Database.UserGetByLogin(login));
        }

        /// <summary>
        /// Получаем пользователя из базы по емейлу(емейлы уникальны)
        /// </summary>
        /// <param name="email">электропочта</param>
        public static User GetByEmail(string email)
        {
            return GetUserFromRow(Database.UserGetByEmail(email.Trim()));
        }

        /// <summary>
        /// Получаем пользователя по идентификатору
        /// </summary>
        /// <param name="userId">Идентификатор</param>
        public static User GetById(int userId)
        {
            User usr = null;
            GetUsersList().TryGetValue(userId, out usr);
            if (usr == null)
            {
                usr = GetUserFromRow(Database.UserGetById(userId));
                if (usr.Id > 0)
                {
                    AddUserToList(usr);
                }
            }

            return usr;
        }

        /// <summary>
        /// Получаем пользователей по ролям
        /// </summary>
        /// <param name="role">Роль получаемых пользователей</param>
        public static List<User> GetByRole(User.Roles role)
        {
            //TODO: Закешировать админов и постеров?
            return GetUsersFromTable(Database.UserGetByRole((int)role));
        }

        /// <summary>
        /// Получаем последних зарегистрировашихся
        /// </summary>
        /// <param name="count">Количество нужных пользователей</param>
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
        /// Пользователи проголосовашие за данный вариант ответа в опросе.
        /// 
        /// Метод не учитывает "открытость" опроса. Как бы можно хакать.
        /// Чтобы учитывать "открытость" используйте PollAnswer.GetUsers()
        /// </summary>
        /// <param name="answer_id">Идентификатор варианта ответа</param>
        /// <returns>Список обьектов User</returns>
        public static List<User> GetAnswerVoters(int answer_id)
        {
            return GetUsersFromTable(Database.PollGetAnswerVoters(answer_id));
        }

        /// <summary>
        /// Получаем самых активных постеров из кеша
        /// </summary>
        /// <param name="count">Кол-во нужных пользователей</param>
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
        /// Получаем статистику по пользователям(кол-во пользователей, админов, постеров) из кеша
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

        /// <summary>
        /// Кешированный список пользователей.
        /// </summary>
        /// <returns></returns>
        private static Dictionary<int, User> GetUsersList()
        {
            Dictionary<int, User> users = (Dictionary<int, User>)AppCache.Get(Global.ConfigStringParam("UsersListCacheName"));
            if (users == null)
            {
                users = new Dictionary<int, User>();
            }
            return users;
        }

        /// <summary>
        /// Добавление юзера в список пользователей приложения.
        /// </summary>
        /// <param name="usr"></param>
        private static void AddUserToList(User usr)
        {
            Dictionary<int, User> users = GetUsersList();
            users.Add(usr.Id, usr);
            AppCache.Insert(Global.ConfigStringParam("UsersListCacheName"), users, new object(), DateTime.Now.AddHours(Global.ConfigDoubleParam("UsersListCachePer")));
        }

        private static void RemoveUserFromList(int userId)
        {
            Dictionary<int, User> users = GetUsersList();
            users.Remove(userId);
            AppCache.Insert(Global.ConfigStringParam("UsersListCacheName"), users, new object(), DateTime.Now.AddHours(Global.ConfigDoubleParam("UsersListCachePer")));
         }

        private static List<KeyValuePair<string, string>> GetStatsFromDB()
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
        /// Удаляем аккаунт. Удаление не делать.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        public static void Delete(int userId)
        {
            throw new NotImplementedException();
            //А надо ли удалять? Юзер оставил посты и т.п их же не удалить.
            //Database.UserDel(userId);
        }

        /// <summary>
        /// Добавляем аккаунт в бд
        /// </summary>
        /// <param name="User">Пользователь которого хотим добавить в базу</param>
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