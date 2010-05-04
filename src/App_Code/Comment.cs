using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ITCommunity {
	/// <summary>
	/// Комментарий к посту
	/// </summary>
	public partial class Comment {

		#region For caching

		public const string LAST_COMMENTS_CACHE_KEY = "LastComments";

		//делегат метода загрузки последних комментов из базы, нужен для организации кеширования
		private delegate object LastCommentsLoader(int count);

		private static LastCommentsLoader _lastCommentsLoader = GetLastCommentsFromDB;

		#endregion

		#region Properties

		public Post Post {
			get {
				return Post.Get(_PostId);
			}
			set {
				_PostId = value.Id;
			}
		}

		/// <summary>
		/// Пользователь оставивший комментарий, если это сделал не авторизованный человек,
		/// то возвращается "пустой" пользователь(конструктор по умолчанию)
		/// </summary>
		public User Author {
			get {
				return (_UserId > 0) ? User.Get(_UserId) : new User();
			}
			set {
				_UserId = value.Id;
			}
		}

		/// <summary>
		/// BBCode преобразован в безопасный хтмл, по идее xss не должно быть.
		/// </summary>
		public string TextFormatted {
			get {
				return BBCodeParser.Format(Util.HtmlEncode(_Text));
			}
		}

		/// <summary>
		/// Может ли редактировать коммент текущий пользователь
		/// </summary>
		public bool IsCurrentUserCanEdit {
			get {
				DateTime expireDate = this.CreateDate.AddSeconds(Config.GetInt("EditablePeriod"));
				bool isDateNotExpired = expireDate.CompareTo(DateTime.Now) == 1;
				bool isCurrentUserCommAuthor = CurrentUser.isAuth && CurrentUser.User.Id == this.AuthorId;

				return isDateNotExpired && isCurrentUserCommAuthor;
			}
		}

		public bool IsCurrentUserCanDel {
			get {
				return CurrentUser.IsAdmin || IsCurrentUserCanEdit;
			}
		}

		public string ShortText {
			get {
				string safely_Text = Util.HtmlEncode(_Text);
				safely_Text = Regex.Replace(safely_Text, "\\[(.*?)\\](.*?)\\[\\/(.*?)\\]", "$2", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				return (safely_Text.Length > 80) ? safely_Text.Substring(0, 80) + " ..." : safely_Text;
			}
		}

		public int AuthorId {
			get { return _UserId; }
			set { _UserId = value; }
		}

		#endregion

		#region Constructors

		public Comment(int id, int postId, int userId, DateTime cdate, string ip, string text) {
			_Id = id;
			_PostId = postId;
			_UserId = userId;
			_CreateDate = cdate;
			_Ip = ip;
			_Text = text;
		}

		#endregion

		public void Update() {
			AppCache.Remove(LAST_COMMENTS_CACHE_KEY);

			using (var db = new DatabaseLinq()) {
				db.CommentUpdate(Id, Text);
			}
		}

		#region Public static methods

		public static int Add(Comment comment) {
			AppCache.Remove(LAST_COMMENTS_CACHE_KEY);

			using (var db = new DatabaseLinq()) {
				var result = db.CommentAdd(
					comment.PostId,
					comment.UserId,
					comment.Ip,
					comment.Text
				);

				var comments = new List<Comment>(result);
				comment = comments[0];
			}
			return comment.Id;
		}

		public static Comment Get(int id) {
			using (var db = new DatabaseLinq()) {
				var result = db.CommentGetById(id);
				var comments = new List<Comment>(result);
				return comments[0];
			}
		}

		/// <summary>
		/// Удаляем коммент
		/// </summary>
		/// <param name="id">Id коммента</param>
		public static void Delete(int id) {
			AppCache.Remove(LAST_COMMENTS_CACHE_KEY);

			using (var db = new DatabaseLinq()) {
				db.CommentDel(id);
			}
		}

		/// <summary>
		/// Забираем комменты поста
		/// </summary>
		/// <param name="postId">Идентификатор поста</param>
		public static List<Comment> GetCommentsByPost(int postId) {
			using (var db = new DatabaseLinq()) {
				var result = db.CommentGetByPost(postId);
				return new List<Comment>(result);
			}
		}

		/// <summary>
		/// Забираем комменты по автору
		/// </summary>
		/// <param name="authorId">Идентификатор автора</param>
		public static List<Comment> GetCommentsByAuthor(int authorId, int page, int count, ref int totalRecords) {
			using (var db = new DatabaseLinq()) {
				int? records = totalRecords;
				var result = db.CommentGetByAuthor(authorId, page, count, ref records);
				totalRecords = (int)records;
				return new List<Comment>(result);
			}
		}

		/// <summary>
		/// Забираем последние комментарии из кеша
		/// </summary>
		/// <param name="count">Кол-во нужных комментов</param>
		public static List<KeyValuePair<User, Comment>> GetLastComments(int count) {
			var lastComments = AppCache.Get(
				LAST_COMMENTS_CACHE_KEY,
				_lastCommentsLoader,
				new object[] { count }
			);

			return (List<KeyValuePair<User, Comment>>)lastComments;
		}

		#endregion

		#region Private static methods

		private static List<KeyValuePair<User, Comment>> GetLastCommentsFromDB(int count) {
			List<Comment> comments;
			using (var db = new DatabaseLinq()) {
				var result = db.CommentGetLasts(count);
				comments = new List<Comment>(result);
			}

			List<KeyValuePair<User, Comment>> lastComments = new List<KeyValuePair<User, Comment>>();

			foreach (Comment comment in comments) {
				lastComments.Add(new KeyValuePair<User, Comment>(comment.Author, comment));
			}

			return lastComments;
		}

		#endregion
	}
}
