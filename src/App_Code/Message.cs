using System;
using System.Collections.Generic;
using System.Data;

namespace ITCommunity {
	/// <summary>
	/// Приватные сообщения
	/// </summary>
	public class Message {

		#region Properties

		private int _id;
		private int _receiverId;
		private int _senderId;
		private string _title;
		private string _text;
		private bool _receiverRead;
		private DateTime _cdate;

		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public User Receiver {
			get {
				return User.Get(_receiverId);
			}
			set {
				_receiverId = value.Id;
			}
		}

		public User Sender {
			get {
				return User.Get(_senderId);
			}
			set {
				_senderId = value.Id;
			}
		}

		public string Title {
			get { return _title; }
			set { _title = value; }
		}

		public string Text {
			get { return _text; }
			set { _text = value; }
		}

		/// <summary>
		/// Безопасный от хтмла тайтл
		/// </summary>
		public string TitleFormatted {
			get {
				return Util.HtmlEncode(_title);
			}
		}

		/// <summary>
		/// Безопасный от хтмла текст
		/// </summary>
		public string TextFormatted {
			get {
				return Util.HtmlEncode(_text);
			}
		}

		public DateTime CreateDate {
			get { return _cdate; }
			set { _cdate = value; }
		}

		public bool ReceiverRead {
			get { return _receiverRead; }
			set { _receiverRead = value; }
		}

		#endregion

		#region Constructors

		public Message() {
			_id = -1;
			_receiverId = -1;
			_senderId = -1;
			_text = "";
			_title = "";
			_receiverRead = false;
		}

		public Message(int id, int receiverId, int senderId, string title, string text, DateTime cdate, bool receiverRead) {
			_id = id;
			_receiverId = receiverId;
			_senderId = senderId;
			_title = title;
			_text = text;
			_cdate = cdate;
			_receiverRead = receiverRead;
		}

		#endregion

		public void DeleteByReceiver() {
			Database.MessageDelByReceiver(_id);
		}

		public void DeleteBySender() {
			Database.MessageDelBySender(_id);
		}

		public void MarkAsRead() {
			Database.MessageMarkAsRead(_id);
		}

		#region Public static methods

		public static List<Message> GetByReceiver(int receiverId, int page, int count, ref int messCount) {
			return GetMessFromTable(Database.MessageGetByReceiver(receiverId, page, count, ref messCount));
		}

		public static List<Message> GetBySender(int senderId, int page, int count, ref int messCount) {
			return GetMessFromTable(Database.MessageGetBySender(senderId, page, count, ref messCount));
		}

		public static Message Send(int receiverId, int senderId, string title, string text) {
			return GetMessFromRow(Database.MessageAdd(receiverId, senderId, title, text));
		}

		public static Message Get(int id) {
			return GetMessFromRow(Database.MessageGetById(id));
		}

		public static int GetNewCount(int recId) {
			return (int)Database.MessageGetNewCount(recId);
		}

		#endregion

		#region Private static methods

		private static List<Message> GetMessFromTable(DataTable dt) {
			var messages = new List<Message>();

			for (int i = 0; i < dt.Rows.Count; i++) {
				messages.Add(GetMessFromRow(dt.Rows[i]));
			}

			return messages;
		}

		private static Message GetMessFromRow(DataRow dr) {
			Message mess;

			if (dr == null) {
				mess = new Message();
			}
			else {
				mess = new Message(
					Convert.ToInt32(dr["id"]),
					Convert.ToInt32(dr["receiver_id"]),
					Convert.ToInt32(dr["sender_id"]),
					Convert.ToString(dr["title"]),
					Convert.ToString(dr["text"]),
					Convert.ToDateTime(dr["cdate"]),
					Convert.ToBoolean(dr["receiver_read"])
				);
			}

			return mess;
		}

		#endregion

	}
}