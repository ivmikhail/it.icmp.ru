using System;
using System.Collections.Generic;
using System.Data;

namespace ITCommunity {
	/// <summary>
	/// Типа блокнот
	/// </summary>
	public class Note {

		#region Properties

		private int _id;
		private int _userId;
		private string _title;
		private string _text;
		private DateTime _cdate;

		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public int UserId {
			get { return _userId; }
			set { _userId = value; }
		}

		public string Title {
			get { return _title; }
			set { _title = value; }
		}

		public string TitleFormatted {
			get { return Util.HtmlEncode(_title); }
		}

		public string Text {
			get { return _text; }
			set { _text = value; }
		}

		public string TextFormatted {
			get { return Util.HtmlEncode(_text); }
		}

		public DateTime CreateDate {
			get { return _cdate; }
			set { _cdate = value; }
		}

		#endregion

		#region Constructors

		public Note() {
			_id = -1;
			_userId = -1;
			_title = "";
			_text = "";
			_cdate = DateTime.Now;
		}

		public Note(int id, string title, string text, int userId, DateTime cdate) {
			_id = id;
			_userId = userId;
			_title = title;
			_text = text;
			_cdate = cdate;
		}

		#endregion

		#region Public static methods

		public static Note Add(Note note) {
			return GetNoteFromRow(Database.NotesAdd(note.UserId, note.Title, note.Text, DateTime.Now));
		}

		public static Note Get(int id) {
			return GetNoteFromRow(Database.NotesGetById(id));
		}

		public static void Delete(int id) {
			Database.NotesDel(id);
		}

		public static List<Note> GetPaged(int page, int count, int userId, ref int record_count) {
			return GetNotesFromTable(Database.NotesGet(userId, page, count, ref record_count));
		}

		#endregion

		#region Private static methods

		private static List<Note> GetNotesFromTable(DataTable dt) {
			var notes = new List<Note>();

			for (int i = 0; i < dt.Rows.Count; i++) {
				notes.Add(GetNoteFromRow(dt.Rows[i]));
			}

			return notes;
		}

		private static Note GetNoteFromRow(DataRow dr) {
			Note note;

			if (dr == null) {
				note = new Note();
			}
			else {
				note = new Note(
					Convert.ToInt32(dr["id"]),
					Convert.ToString(dr["title"]),
					Convert.ToString(dr["text"]),
					Convert.ToInt32(dr["user_id"]),
					Convert.ToDateTime(dr["cdate"])
				);
			}

			return note;
		}

		#endregion

	}
}
