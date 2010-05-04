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

		#region For caching

		public const string LAST_USERS_CACHE_KEY = "LastUsers";
		public const string LAST_TOP_POSTERS_CACHE_KEY = "LastTopPosters";
		public const string TOP_POSTERS_CACHE_KEY = "TopPosters";
		public const string USERS_STAT_CACHE_KEY = "UsersStat";
		private const string USER_CACHE_NAME_PREFIX = "UserCacheNamePrefix";

		//делегат метода загрузки посл. зарегистр. юзеров из базы, нужен для организации кеширования
		private delegate object LastUsersLoader(int count);
		//делегат метода загрузки активных пользователей за все время, нужен для организации кеширования
		private delegate object TopPostersLoader(int count);
		//делегат метода загрузки статистики юзеров из базы, нужен для организации кеширования
		private delegate object UsersStatLoader();
		//делегат метода загрузки активных пользователей за последние несколько дней, нужен для организации кеширования
		private delegate object LastTopPostersLoader(int count, int days);
		//
		private delegate object UserLoader(int id);

		private static LastUsersLoader _lastUsersLoader = new LastUsersLoader(GetLastRegisteredFromDB);
		private static TopPostersLoader _topPostersLoader = new TopPostersLoader(GetTopPostersFromDB);
		private static LastTopPostersLoader _lastTopPostersLoader = new LastTopPostersLoader(GetLastTopPostersFromDB);
		private static UsersStatLoader _usersStatLoader = new UsersStatLoader(GetStatsFromDB);
		private static UserLoader _userLoader = new UserLoader(GetUserFromDB);

		#endregion

		#region Properties

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

		#region Constructors

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

		#endregion

		/// <summary>
		/// Проверяем забанен ли текущий пользователь
		/// </summary>
		public bool IsBanned() {
			return (this.Role == Roles.Banned);
		}

		public bool AbleToAddHeaderText() {
			bool result = CanAddHeaderText;
			result &= (HeaderTextCounter >= Config.GetInt("HeaderTextPostsCount"));
			result |= (this._role == Roles.Admin);
			return result;
		}

		public void Update() {
			byte canAddHeaderText = (byte)(_canAddHeaderText ? 1 : 0);
			Database.UserUpdate(_id, _pass, (byte)_role, _email, canAddHeaderText, _headerTextCounter);
			RemoveUserFromCache(_id);
		}

		#region Public static methods

		/// <summary>
		/// Получаем пользователя из базы по логину
		/// </summary>
		/// <param name="login">login он же nick</param>
		public static User Get(string login) {
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
		public static User Get(int id) {
			var user = AppCache.Get(
				Config.Get("UserCacheNamePrefix") + id.ToString(),
				_userLoader,
				new object[] { id },
				Config.GetDouble("UserCachePer")
			);
			return (User)user;
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
			var lastUsers = AppCache.Get(
				LAST_USERS_CACHE_KEY,
				_lastUsersLoader,
				new object[] { count }
			);
			return (List<User>)lastUsers;
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
			var top = AppCache.Get(
				TOP_POSTERS_CACHE_KEY,
				_topPostersLoader,
				new object[] { count }
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
			var top = AppCache.Get(
				LAST_TOP_POSTERS_CACHE_KEY,
				_lastTopPostersLoader,
				new object[] { count, days }
			);
			return (List<KeyValuePair<string, string>>)top;
		}

		/// <summary>
		/// Получаем статистику по пользователям(кол-во пользователей, админов, постеров) из кеша
		/// </summary>
		public static List<KeyValuePair<string, string>> GetStats() {
			var stat = AppCache.Get(USERS_STAT_CACHE_KEY, _usersStatLoader);
			return (List<KeyValuePair<string, string>>)stat;
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

		#endregion

		#region Private static methods

		private static User GetUserFromDB(int id) {
			return GetUserFromRow(Database.UserGetById(id));
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
			return (User)AppCache.Get(Config.Get(USER_CACHE_NAME_PREFIX) + userId);
		}

		private static void AddUserToCache(User usr) {
			AppCache.Insert(Config.Get(USER_CACHE_NAME_PREFIX) + usr.Id, usr, Config.GetDouble("UserCachePer"));
		}

		private static void RemoveUserFromCache(int userId) {
			AppCache.Remove(Config.Get(USER_CACHE_NAME_PREFIX) + userId);
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

		#endregion
	}
}
