using System;
using System.Collections.Generic;
using System.Data;

namespace ITCommunity {
	/// <summary>
	/// Класс текста в хидере
	/// </summary>
	public class HeaderText {

		#region For caching

		public const string HEADER_TEXTS_CACHE_KEY = "HeaderTexts";

		private delegate object CurrentHeaderTextsLoader();

		private static CurrentHeaderTextsLoader _currentHeaderTextsLoader = GetCurrentsFromDB;

		#endregion

		#region Properties

		private static Random _random = new Random();

		private int _id;
		private User _user;
		private string _text;
		private DateTime _createDate;
		private DateTime _showEndDate;
		private bool _isShowing;

		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public User User {
			get { return _user; }
			set { _user = value; }
		}

		public string Text {
			get { return _text; }
			set { _text = value; }
		}

		public DateTime CreateDate {
			get { return _createDate; }
			set { _createDate = value; }
		}

		public DateTime ShowEndDate {
			get { return _showEndDate; }
			set { _showEndDate = value; }
		}

		public bool IsShowing {
			get { return _isShowing; }
			set { _isShowing = value; }
		}

		#endregion

		#region Constructors

		public HeaderText() {
			_id = -1;
			_user = new User();
			_text = "Напиши текст для хидера, " + CurrentUser.User.Login + "!";
			_createDate = DateTime.Now;
			_showEndDate = DateTime.MinValue;
			_isShowing = true;
		}

		public HeaderText(int id, User user, string text, DateTime createDate, DateTime showEndDate, bool isShowing) {
			_id = id;
			_user = user;
			_text = text;
			_createDate = createDate;
			_showEndDate = showEndDate;
			_isShowing = isShowing;
		}

		#endregion

		#region Public static methods

		public static HeaderText Add(int userId, string text) {
			AppCache.Remove(HEADER_TEXTS_CACHE_KEY);

			return GetHeaderTextFromRow(Database.HeaderTextAdd(userId, text));
		}

		public static void Delete(int id) {
			AppCache.Remove(HEADER_TEXTS_CACHE_KEY);

			Database.HeaderTextDel(id);
		}

		public static void EndShow(int id) {
			AppCache.Remove(HEADER_TEXTS_CACHE_KEY);

			Database.HeaderTextUpdateShowEndDate(id);
		}

		public static HeaderText GetRandom() {
			var headerTexts = GetCurrentHeaderTexts();

			if (headerTexts.Count == 0) {
				return new HeaderText();
			}

			return headerTexts[_random.Next(headerTexts.Count)];
		}

		public static List<HeaderText> Get(int page, int count, ref int records_count) {
			return GetHeaderTextsFromTable(Database.HeaderTextGet(page, count, ref records_count));
		}

		#endregion

		#region Private static methods

		private static List<HeaderText> GetCurrentHeaderTexts() {
			var headerTexts = AppCache.Get(
				HEADER_TEXTS_CACHE_KEY,
				_currentHeaderTextsLoader
			);

			return (List<HeaderText>)headerTexts;
		}

		private static List<HeaderText> GetCurrentsFromDB() {
			var headerTexts = GetHeaderTextsFromTable(Database.HeaderTextGetCurrents());
			var toDelete = new List<HeaderText>();

			foreach (var headerText in headerTexts) {
				if (!headerText.IsShowing) {
					Database.HeaderTextUpdateShowEndDate(headerText.Id);
					toDelete.Add(headerText);
				}
			}

			foreach (HeaderText text in toDelete) {
				headerTexts.Remove(text);
			}

			return headerTexts;
		}

		private static List<HeaderText> GetHeaderTextsFromTable(DataTable dt) {
			var headerTexts = new List<HeaderText>();

			for (int i = 0; i < dt.Rows.Count; i++) {
				headerTexts.Add(GetHeaderTextFromRow(dt.Rows[i]));
			}

			return headerTexts;
		}

		private static HeaderText GetHeaderTextFromRow(DataRow dr) {
			HeaderText headerText;

			if (dr == null) {
				headerText = new HeaderText();
			}
			else {
				DateTime createDate = DateTime.Now;
				DateTime showEndDate = DateTime.MinValue;
				if (dr["cdate"] != DBNull.Value) {
					createDate = Convert.ToDateTime(dr["cdate"]);
				}
				if (dr["show_end_date"] != DBNull.Value) {
					showEndDate = Convert.ToDateTime(dr["show_end_date"]);
				}
				else {
					double hours = Config.GetDouble("HeaderTextShowingHours");
					showEndDate = createDate.AddHours(hours);
				}
				bool isShowing = (showEndDate.CompareTo(DateTime.Now) > 0);
				headerText = new HeaderText(
					Convert.ToInt32(dr["id"]),
					User.Get(Convert.ToInt32(dr["user_id"])),
					Convert.ToString(dr["text"]),
					createDate,
					showEndDate,
					isShowing
				);
			}

			return headerText;
		}

		#endregion

	}
}
