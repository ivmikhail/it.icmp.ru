using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace ITCommunity {
	/// <summary>
	/// Комментарий к посту
	/// </summary>
	/// 
	public class Comment {

		//делегат метода загрузки последних комментов из базы, нужен для организации кеширования
		private delegate object LastCommentsLoader(int count);

		#region Properties

		private static LastCommentsLoader _lastCommentsLoader = new LastCommentsLoader(GetLastCommentsFromDB);

		private int _id;
		private int _postId;
		private int _userId;
		private DateTime _cdate;
		private string _ip;
		private string _text;

		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public int PostId {
			get { return _postId; }
			set { _postId = value; }
		}

		public Post Post {
			get {
				return Post.GetById(_postId);
			}
			set {
				_postId = value.Id;
			}
		}

		/// <summary>
		/// Пользователь оставивший комментарий, если это сделал не авторизованный человек,
		/// то возвращается "пустой" пользователь(конструктор по умолчанию)
		/// </summary>
		public User Author {
			get {
				User user;
				if (_userId > 0) {
					user = User.GetById(_userId);
				}
				else {
					user = new User();
				}
				return user;
			}
			set {
				_userId = value.Id;
			}
		}

		public DateTime CreateDate {
			get { return _cdate; }
			set { _cdate = value; }
		}

		public string Ip {
			get { return _ip; }
			set { _ip = value; }
		}

		/// <summary>
		///  "as is", может содержать небезопасный хтмл, bbcode не учитывается.
		/// </summary>
		public string Text {
			get { return _text; }
			set { _text = value; }
		}

		/// <summary>
		/// BBCode преобразован в безопасный хтмл, по идее xss не должно быть.
		/// </summary>
		public string TextFormatted {
			get {
				return BBCodeParser.Format(Util.HtmlEncode(_text));
			}
		}

		public string ShortText {
			get {
				string safely_text = Util.HtmlEncode(_text);
				safely_text = Regex.Replace(safely_text, "\\[(.*?)\\](.*?)\\[\\/(.*?)\\]", "$2", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				return (safely_text.Length > 80) ? safely_text.Substring(0, 80) + " ..." : safely_text;
			}
		}

		#endregion

		public Comment() {
			_id = -1;
			_postId = -1;
			_userId = -1;
			_cdate = DateTime.Now;
			_ip = "";
			_text = "";
		}

		public Comment(int id, int postId, int userId, DateTime cdate, string ip, string text) {
			_id = id;
			_postId = postId;
			_userId = userId;
			_cdate = cdate;
			_ip = ip;
			_text = text;
		}

		/// <summary>
		/// Добавление комментария в базу. Кол-во комментариев поста обновляется на уровне базы
		/// </summary>
		/// <param name="comment">Сам коммент</param>
		public static Comment Add(Comment comm) {
			Comment comment = GetCommentFromRow(Database.CommentAdd(comm.Post.Id, comm.Author.Id, comm.Ip, comm.Text));
			AppCache.Remove(Config.String("LastCommentsCacheName"));
			return comment;
		}

		/// <summary>
		/// Удаляем коммент
		/// </summary>
		/// <param name="id">Id коммента</param>
		public static void Delete(int id) {
			Database.CommentDel(id);
			AppCache.Remove(Config.String("LastCommentsCacheName"));
		}

		/// <summary>
		/// Забираем комменты поста
		/// </summary>
		/// <param name="postId">Идентификатор поста</param>
		public static List<Comment> GetByPost(int postId) {
			return GetCommentsFromTable(Database.CommentGetByPost(postId));
		}

		/// <summary>
		/// Забираем комменты по автору
		/// </summary>
		/// <param name="authorId">Идентификатор автора</param>
		public static List<Comment> GetByAuthor(int authorId, int page, int count, ref int totalRecords) {
			return GetCommentsFromTable(Database.CommentGetByAuthor(authorId, page, count, ref totalRecords));
		}

		/// <summary>
		/// Забираем последние комментарии из кеша
		/// </summary>
		/// <param name="count">Кол-во нужных комментов</param>
		public static List<KeyValuePair<User, Comment>> GetLasts(int count) {
			object lastComments = AppCache.Get(
				Config.String("LastCommentsCacheName"),
				_lastCommentsLoader,
				new object[] { count },
				Config.Double("CacheLastCommentPer"));

			return (List<KeyValuePair<User, Comment>>)lastComments;
		}

		private static List<KeyValuePair<User, Comment>> GetLastCommentsFromDB(int count) {
			List<Comment> comments = GetCommentsFromTable(Database.CommentGetLasts(count));
			List<KeyValuePair<User, Comment>> lastComments = new List<KeyValuePair<User, Comment>>();

			foreach (Comment comment in comments) {
				lastComments.Add(new KeyValuePair<User, Comment>(comment.Author, comment));
			}

			return lastComments;
		}

		private static List<Comment> GetCommentsFromTable(DataTable dt) {
			List<Comment> comments = new List<Comment>();
			for (int i = 0; i < dt.Rows.Count; i++) {
				comments.Add(GetCommentFromRow(dt.Rows[i]));
			}
			return comments;
		}

		private static Comment GetCommentFromRow(DataRow dr) {
			Comment comment;

			if (dr == null) {
				comment = new Comment();
			}
			else {
				comment = new Comment(
					Convert.ToInt32(dr["id"]),
					Convert.ToInt32(dr["post_id"]),
					Convert.ToInt32(dr["user_id"]),
					Convert.ToDateTime(dr["cdate"]),
					Convert.ToString(dr["ip"]),
					Convert.ToString(dr["text"])
				);
			}

			return comment;
		}
	}
}
