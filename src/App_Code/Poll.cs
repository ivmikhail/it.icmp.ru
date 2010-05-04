using System;
using System.Data;
using System.Collections.Generic;

namespace ITCommunity {
	/// <summary>
	/// Класс - опросы(голосование). Голосовать могут только авторзованные пользователи
	/// </summary>
	public class Poll {

		#region Properties

		private int _id;
		private string _topic;
		private int _authorId;
		private bool _isMultiselect;
		private bool _isOpen;
		private int _votesCount;
		private List<PollAnswer> _answers;
		private DateTime _cdate;

		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public string Topic {
			get { return _topic; }
			set { _topic = value; }
		}

		public User Author {
			get {
				return User.Get(_authorId);
			}
			set {
				_authorId = value.Id;
			}
		}

		public bool IsMultiSelect {
			get { return _isMultiselect; }
			set { _isMultiselect = value; }
		}

		public bool IsOpen {
			get { return _isOpen; }
			set { _isOpen = value; }
		}

		public int VotesCount {
			get { return _votesCount; }
			set { _votesCount = value; }
		}

		public List<PollAnswer> Answers {
			get { return _answers; }
			set { _answers = value; }
		}

		public DateTime CreateDate {
			get { return _cdate; }
			set { _cdate = value; }
		}

		/// <summary>
		/// Если опрос активный возвращается текущая дата
		/// </summary>
		public DateTime EndDate {
			get {
				return GetNext().CreateDate;
			}
		}

		public string EndDateString {
			get {
				return _id == GetActive().Id ? "активный" : EndDate.ToString("dd MMMM yyyy, HH:mm");
			}
		}

		#endregion

		#region Constructors

		public Poll() {
			_id = -1;
			_topic = "null";
			_authorId = -1;
			_isMultiselect = false;
			_isOpen = false;
			_votesCount = 0;
			_answers = new List<PollAnswer>();
			_cdate = DateTime.Now;
		}

		public Poll(int id, string topic, int authorId, bool isMultiselect, bool isOpen, int votesCount, List<PollAnswer> answers, DateTime cdate) {
			_id = id;
			_topic = topic;
			_authorId = authorId;
			_isMultiselect = isMultiselect;
			_isOpen = isOpen;
			_votesCount = votesCount;
			_answers = answers;
			_cdate = cdate;
		}

		#endregion

		/// <summary>
		/// Голосование, метод сам проверит может ли данный человек голосовать и обновит счетчики.
		/// </summary>
		/// <param name="answers">Идентификаторы ответов через запятую, если можно выбрать только один вариант, то за ответ возьмется первый(нулевой) член массива</param>
		public void Vote(User user, string answers) {
			Database.PollVote(_id, user.Id, answers);
		}

		/// <summary>
		/// Получаем следующий опрос
		/// </summary>
		/// <returns></returns>
		public Poll GetNext() {
			return GetPollFromRow(Database.PollGetNext(_id));
		}

		/// <summary>
		/// Может ли голосовать текущий пользователь(необязательно авторизованный)
		/// </summary>
		/// <param name="user">CurrentUser</param>
		/// <returns></returns>
		public bool CanVote() {
			bool result = false;

			if (CurrentUser.isAuth) {
				if (!UserAlreadyVoted(CurrentUser.User)) {
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
		public bool UserAlreadyVoted(User user) {
			return ((int)Database.PollIsUserVoted(user.Id, _id) > 0);
		}

		#region Public static methods

		/// <summary>
		/// Добавление нового опроса
		/// </summary>
		/// <param name="topic">Топик(вопрос)</param>
		/// <param name="author_id">Идентификатор автора</param>
		/// <param name="is_multiselect">Можно ли выбрать несколько вариантов ответа</param>
		/// <param name="is_open">Является ли опрос открытым(можно смотерть кто как голосовал)</param>
		/// <param name="answers">Вопросы через запятую</param>
		/// <returns></returns>
		public static Poll Add(string topic, int authorId, bool isMultiselect, bool isOpen, string answers) {
			return GetPollFromRow(Database.PollAdd(topic, authorId, isMultiselect == true ? 1 : 0, isOpen == true ? 1 : 0, answers));
		}

		/// <summary>
		/// Удалить опрос со всеми потрохами
		/// </summary>
		/// <param name="id">Идентификатор опроса</param>
		public static void Delete(int id) {
			Database.PollDel(id);
		}

		/// <summary>
		/// Текущий опрос(последний добавленный)
		/// </summary>
		/// <returns>Актуальный опрос</returns>
		public static Poll GetActive() {
			//Не кешировать! А то результаты будут обновляться с задержкой. Или кешировать?
			return GetPollFromRow(Database.PollGetLast());
		}

		/// <summary>
		/// Получаем опрос 
		/// </summary>
		/// <param name="id">Идентификатор опроса</param>
		/// <returns>Обьект Poll</returns>
		public static Poll GetById(int id) {
			return GetPollFromRow(Database.PollGetById(id));
		}

		/// <summary>
		/// Коллекция опросов, нужен для архива
		/// </summary>
		/// <param name="page">Текущая страница</param>
		/// <param name="count">Кол-во опросов на странице</param>
		/// <param name="polls_count">Общее кол-во опросов</param>
		/// <returns></returns>
		public static List<Poll> Get(int page, int count, ref int polls_count) {
			return GetPollsFromTable(Database.PollGet(page, count, ref polls_count));
		}

		#endregion

		#region Private static methods

		private static List<Poll> GetPollsFromTable(DataTable dt) {
			List<Poll> polls = new List<Poll>();
			for (int i = 0; i < dt.Rows.Count; i++) {
				polls.Add(GetPollFromRow(dt.Rows[i]));
			}
			return polls;
		}

		private static Poll GetPollFromRow(DataRow dr) {
			Poll poll;
			if (dr == null) {
				poll = new Poll();
			}
			else {
				int id = Convert.ToInt32(dr["id"]);
				poll = new Poll(
					id,
					Convert.ToString(dr["topic"]),
					Convert.ToInt32(dr["author_id"]),
					Convert.ToInt32(dr["is_multiselect"]) != 0,
					Convert.ToInt32(dr["is_open"]) != 0,
					Convert.ToInt32(dr["votes_count"]),
					PollAnswer.Get(id),
					Convert.ToDateTime(dr["cdate"])
				);
			}
			return poll;
		}

		#endregion
	}
}
