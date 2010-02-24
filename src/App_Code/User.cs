using System;
using System.Data;
using System.Collections.Generic;

namespace ITCommunity {
	/// <summary>
	/// Пользователь хранящийся в БД
	/// </summary>
	public class User {

		public enum Roles {
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

		//делегат метода загрузки посл. зарегистр. юзеров из базы, нужен для организации кеширования
		private delegate object LastUsersLoader(int count);
		//делегат метода загрузки активных пользователей за все время, нужен для организации кеширования
		private delegate object TopPostersLoader(int count);
		//делегат метода загрузки статистики юзеров из базы, нужен для организации кеширования
		private delegate object UsersStatLoader();
		//делегат метода загрузки активных пользователей за последние несколько дней, нужен для организации кеширования
		private delegate object LastTopPostersLoader(int count, int days);

		#region Properties

		private static LastUsersLoader _lastUsersLoader = new LastUsersLoader(GetLastRegisteredFromDB);
		private static TopPostersLoader _topPostersLoader = new TopPostersLoader(GetTopPostersFromDB);
		private static LastTopPostersLoader _lastTopPostersLoader = new LastTopPostersLoader(GetLastTopPostersFromDB);

		private int _id;
		private string _pass;
		private string _login;
		private string _email;
		private Roles _role;
		private DateTime _cdate;
		private bool _canAddHeaderText;
		private int _headerTextCounter;
		private int _postsCount;
		private int _commentsCount;

		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public string Pass {
			get { return _pass; }
			set { _pass = value; }
		}

		public string Email {
			get { return _email; }
			set { _email = value; }
		}

		public Roles Role {
			get { return _role; }
			set { _role = value; }
		}

		public string Login {
			get { return _login; }
			set { _login = value; }
		}

		public DateTime CreateDate {
			get { return _cdate; }
			set { _cdate = value; }
		}

		public bool CanAddHeaderText {
			get { return _canAddHeaderText; }
			set { _canAddHeaderText = value; }
		}

		public int HeaderTextCounter {
			get { return _headerTextCounter; }
			set { _headerTextCounter = value; }
		}

		public int CommentsCount {
			get { return _commentsCount; }
			set { _commentsCount = value; }
		}

		public int PostsCount {
			get { return _postsCount; }
			set { _postsCount = value; }
		}

		#endregion

		public User() {
			_id = -1;
			_login = "anonymous";
			_pass = "";
			_cdate = DateTime.Now;
			_role = User.Roles.Poster;
			_email = "bill@microsoft.com";
			_canAddHeaderText = true;
			_headerTextCounter = 0;
			_commentsCount = 0;
			_postsCount = 0;
		}

		public User(int id, string login, string pass, DateTime cdate, User.Roles role, string email, bool canAddHeaderText, int headerTextCounter, int commentsCount, int postsCount) {
			_id = id;
			_login = login;
			_pass = pass;
			_cdate = cdate;
			_role = role;
			_email = email;
			_canAddHeaderText = canAddHeaderText;
			_headerTextCounter = headerTextCounter;
			_commentsCount = commentsCount;
			_postsCount = postsCount;
		}

		/// <summary>
		/// Проверяем забанен ли текущий пользователь
		/// </summary>
		public bool IsBanned() {
			return (this.Role == Roles.Banned);
		}

		public bool AbleToAddHeaderText() {
			bool result = CanAddHeaderText;
			result &= (HeaderTextCounter >= Config.Num("HeaderTextPostsCount"));
			result |= (this._role == Roles.Admin);
			return result;
		}

		public void Update() {
			byte canAddHeaderText = (byte)(_canAddHeaderText ? 1 : 0);
			Database.UserUpdate(_id, _pass, (byte)_role, _email, canAddHeaderText, _headerTextCounter);
			RemoveUserFromCache(_id);
		}

		/// <summary>
		/// Получаем пользователя из базы по логину
		/// </summary>
		/// <param name="login">login он же nick</param>
		public static User GetByLogin(string login) {
			return GetUserFromRow(Database.UserGetByLogin(login));
		}

		/// <summary>
		/// Получаем пользователя из базы по емейлу(емейлы уникальны)
		/// </summary>
		/// <param name="email">электропочта</param>
		public static User GetByEmail(string email) {
			return GetUserFromRow(Database.UserGetByEmail(email.Trim()));
		}

		/// <summary>
		/// Получаем пользователя по идентификатору
		/// </summary>
		/// <param name="userId">Идентификатор</param>
		public static User GetById(int userId) {
			User usr = GetUserFromCache(userId);
			if (usr == null) {
				usr = GetUserFromRow(Database.UserGetById(userId));
				if (usr.Id > 0) {
					AddUserToCache(usr);
				}
			}
			return usr;
		}

		/// <summary>
		/// Получаем пользователей по ролям
		/// </summary>
		/// <param name="role">Роль получаемых пользователей</param>
		public static List<User> GetByRole(User.Roles role) {
			//TODO: Закешировать админов и постеров?
			return GetUsersFromTable(Database.UserGetByRole((int)role));
		}

		/// <summary>
		/// Получаем последних зарегистрировашихся
		/// </summary>
		/// <param name="count">Количество нужных пользователей</param>
		public static List<User> GetLastRegistered(int count) {
			object cats = AppCache.Get(
				Config.String("LastUsersCacheName"),
				_lastUsersLoader,
				new object[] { count },
				Config.Double("LastUsersCachePer")
			);
			return (List<User>)cats;
		}

		/// <summary>
		/// Пользователи проголосовашие за данный вариант ответа в опросе.
		/// 
		/// Метод не учитывает "открытость" опроса. Как бы можно хакать.
		/// Чтобы учитывать "открытость" используйте PollAnswer.GetUsers()
		/// </summary>
		/// <param name="answer_id">Идентификатор варианта ответа</param>
		/// <returns>Список обьектов User</returns>
		public static List<User> GetAnswerVoters(int answer_id) {
			return GetUsersFromTable(Database.PollGetAnswerVoters(answer_id));
		}

		/// <summary>
		/// Получаем самых активных постеров из кеша
		/// </summary>
		/// <param name="count">Кол-во нужных пользователей</param>
		public static List<KeyValuePair<string, string>> GetTopPosters(int count) {
			object top = AppCache.Get(
				Config.String("TopPostersCacheName"),
				_topPostersLoader,
				new object[] { count },
				Config.Double("TopPostersCachePer")
			);
			return (List<KeyValuePair<string, string>>)top;
		}

		/// <summary>
		/// Получаем активных постеров за последние N дней
		/// </summary>
		/// <param name="count">Сколько пользователей нужно</param>
		/// <param name="days">Сколько последних дней учитывать</param>
		/// <returns></returns>
		public static List<KeyValuePair<string, string>> GetLastTopPosters(int count, int days) {
			List<KeyValuePair<string, string>> top = (List<KeyValuePair<string, string>>)AppCache.Get(
				Config.String("LastTopPostersCacheName"),
				_lastTopPostersLoader,
				new object[] { count, days },
				Config.Double("LastUsersCachePer"));
			return top;
		}

		/// <summary>
		/// Получаем статистику по пользователям(кол-во пользователей, админов, постеров) из кеша
		/// </summary>
		public static List<KeyValuePair<string, string>> GetStats() {
			UsersStatLoader loader = new UsersStatLoader(GetStatsFromDB);
			List<KeyValuePair<string, string>> stat = (List<KeyValuePair<string, string>>)AppCache.Get(
				Config.String("UsersStatCacheName"),
				loader,
				null,
				Config.Double("UsersStatCachePer"));
			return stat;
		}

		/// <summary>
		/// Удаляем аккаунт. Удаление не делать.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя</param>
		public static void Delete(int userId) {
			throw new NotImplementedException();
			//А надо ли удалять? Юзер оставил посты и т.п их же не удалить.
			//Database.UserDel(userId);
		}

		/// <summary>
		/// Добавляем аккаунт в бд
		/// </summary>
		/// <param name="User">Пользователь которого хотим добавить в базу</param>
		public static User Add(User user) {
			DataRow dr = Database.UserAdd(user.Login, user.Pass, (byte)user.Role, user.Email);
			return GetUserFromRow(dr);
		}

		/// <summary>
		/// Возвращает список пользователей, которые не могут добавлять текст для хидера
		/// </summary>
		public static List<User> GetBlocked() {
			return GetUsersFromTable(Database.UserGetBlocked());
		}

		private static List<KeyValuePair<string, string>> GetStatsFromDB() {
			List<KeyValuePair<string, string>> top = new List<KeyValuePair<string, string>>();
			DataTable dt = Database.UserGetStat();
			for (int i = 0; i < dt.Rows.Count; i++) {
				string key = dt.Rows[i]["key"].ToString();
				string value = dt.Rows[i]["value"].ToString();
				top.Add(new KeyValuePair<string, string>(key, value));
			}
			return top;
		}

		private static List<KeyValuePair<string, string>> GetLastTopPostersFromDB(int count, int days) {
			List<KeyValuePair<string, string>> top = new List<KeyValuePair<string, string>>();
			DataTable dt = Database.UserGetLastTopPosters(count, 0 - days);
			for (int i = 0; i < dt.Rows.Count; i++) {
				string username = dt.Rows[i]["usernick"].ToString();
				string text = dt.Rows[i]["postcount"].ToString();
				top.Add(new KeyValuePair<string, string>(username, text));
			}
			return top;
		}

		private static List<KeyValuePair<string, string>> GetTopPostersFromDB(int count) {
			List<KeyValuePair<string, string>> top = new List<KeyValuePair<string, string>>();
			DataTable dt = Database.UserGetTopPosters(count);
			for (int i = 0; i < dt.Rows.Count; i++) {
				string username = dt.Rows[i]["usernick"].ToString();
				string text = dt.Rows[i]["postcount"].ToString();
				top.Add(new KeyValuePair<string, string>(username, text));
			}
			return top;
		}

		private static List<User> GetLastRegisteredFromDB(int count) {
			return GetUsersFromTable(Database.UserGetLastRegistered(count));
		}

		/// <summary>
		/// Получаем пользователя из кеша
		/// </summary>
		/// <param name="userId">идентификатор пользователя</param>
		/// <returns>обьект пользователь, либо null</returns>
		private static User GetUserFromCache(int userId) {
			return (User)AppCache.Get(Config.String("UsersListCacheName") + userId);
		}

		private static void AddUserToCache(User usr) {
			AppCache.Insert(Config.String("UsersListCacheName") + usr.Id, usr, Config.Double("UsersListCachePer"));
		}

		private static void RemoveUserFromCache(int userId) {
			AppCache.Remove(Config.String("UsersListCacheName") + userId);
		}

		private static List<User> GetUsersFromTable(DataTable dt) {
			List<User> users = new List<User>();
			for (int i = 0; i < dt.Rows.Count; i++) {
				users.Add(GetUserFromRow(dt.Rows[i]));
			}
			return users;
		}

		private static User GetUserFromRow(DataRow dr) {
			User user;
			if (dr == null) {
				user = new User();
			}
			else {
				user = new User(
					Convert.ToInt32(dr["id"]),
					Convert.ToString(dr["nick"]),
					Convert.ToString(dr["pass"]),
					Convert.ToDateTime(dr["cdate"]),
					(User.Roles)Convert.ToInt16(dr["role"]),
					Convert.ToString(dr["email"]),
					Convert.ToByte(dr["can_add_header_text"]) != 0,
					Convert.ToInt32(dr["header_text_counter"]),
					Convert.ToInt32(dr["comments_count"]),
					Convert.ToInt32(dr["posts_count"])
				);
			}
			return user;
		}
	}
}
