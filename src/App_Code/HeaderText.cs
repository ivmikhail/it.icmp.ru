using System;
using System.Collections.Generic;
using System.Data;


namespace ITCommunity {
	/// <summary>
	/// Класс для управления текстов в хидере
	/// </summary>
	public class HeaderText {
		private int _id = -1;
		private User _user = new User();
		private string _text = "";
		private DateTime _createDate = DateTime.Now;
		private DateTime _showBeginDate = DateTime.MinValue;
		private DateTime _showEndDate = DateTime.MinValue;

		private delegate object CurrentHeaderTextLoader();
		private static Random random = new Random();

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

		public DateTime ShowBeginDate {
			get { return _showBeginDate; }
			set { _showBeginDate = value; }
		}

		public DateTime ShowEndDate {
			get { return _showEndDate; }
			set { _showEndDate = value; }
		}

		public HeaderText() {
			Text = "Напиши текст для хидера, " + CurrentUser.User.Login + "!";
		}

		public HeaderText(int id, User user, string text, DateTime createDate, DateTime showBeginDate, DateTime showEndDate) {
			_id = id;
			_user = user;
			_text = text;
			_createDate = createDate;
			_showBeginDate = showBeginDate;
			_showEndDate = showEndDate;
		}

		public static HeaderText Add(int userId, string text) {
			AppCache.Remove(Global.ConfigStringParam("HeaderTextCacheName"));
			return GetHeaderTextFromRow(Database.HeaderTextAdd(userId, text));
		}

		public static HeaderText GetCurrent() {
			List<HeaderText> currents = GetCurrents();
			return currents[random.Next(currents.Count)];
		}

		private static List<HeaderText> GetCurrents() {
			CurrentHeaderTextLoader loader = new CurrentHeaderTextLoader(GetCurrentsFromDB);
			List<HeaderText> current = (List<HeaderText>)AppCache.Get(
				Global.ConfigStringParam("HeaderTextCacheName"),
				new object(),
				loader,
				new object[] {},
				DateTime.Now.AddHours(Global.ConfigDoubleParam("HeaderTextCachePer")));
			return current;
		}

		private static List<HeaderText> GetCurrentsFromDB() {
			List<HeaderText> currents = GetHeaderTextsFromTable(Database.HeaderTextGetCurrents());
			if (currents.Count == 0){
				currents.Add(new HeaderText());
				return currents;
			}
			foreach (HeaderText current in currents) {
				// проверяем показывался ли, если нет, то сохраняем дату начала показа
				if (current.ShowBeginDate == DateTime.MinValue) {
					current.ShowBeginDate = DateTime.Now;
					Database.HeaderTextUpdateShowBeginDate(current.Id);
					//Message.Send(current.User.Id, 0, "Уведомление", "Добрый день!<br />Ваш текст теперь показывется в хидере, поздравляем!");
				}
				// проверяем закончился ли период показа, если да, то загружаем следующий текст
				double hours = Global.ConfigDoubleParam("HeaderTextShowingHours");
				DateTime date = DateTime.Now.AddHours(-hours);
				int comp = current.ShowBeginDate.CompareTo(date);
				if (comp <= 0) {
					current.ShowEndDate = DateTime.Now;
					Database.HeaderTextUpdateShowEndDate(current.Id);
					// Внимание рекурсия!!!
					return GetCurrentsFromDB();
				}
			}
			return currents;
		}

		public static void Delete(int id) {
			Database.HeaderTextDel(id);
			AppCache.Remove(Global.ConfigStringParam("HeaderTextCacheName"));
		}

		public static List<HeaderText> Get(int page, int count, ref int records_count) {
			return GetHeaderTextsFromTable(Database.HeaderTextGet(page, count, ref records_count));
		}

		private static List<HeaderText> GetHeaderTextsFromTable(DataTable dt) {
			List<HeaderText> headerText = new List<HeaderText>();
			for (int i = 0; i < dt.Rows.Count; i++) {
				headerText.Add(GetHeaderTextFromRow(dt.Rows[i]));
			}
			return headerText;
		}

		private static HeaderText GetHeaderTextFromRow(DataRow dr) {
			HeaderText headerText;
			if (dr == null) {
				headerText = new HeaderText();
			} else {
				DateTime showBeginDate = DateTime.MinValue;
				DateTime showEndDate = DateTime.MinValue;
				if (dr["show_begin_date"] != DBNull.Value) {
					showBeginDate = Convert.ToDateTime(dr["show_begin_date"]);
				}
				if (dr["show_end_date"] != DBNull.Value) {
					showEndDate = Convert.ToDateTime(dr["show_end_date"]);
				}
				headerText = new HeaderText(Convert.ToInt32(dr["id"]),
											User.GetById(Convert.ToInt32(dr["user_id"])),
											Convert.ToString(dr["text"]),
											Convert.ToDateTime(dr["cdate"]),
											showBeginDate,
											showEndDate);
			}
			return headerText;
		}
	}
}
