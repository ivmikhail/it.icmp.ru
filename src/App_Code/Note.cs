using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using ITCommunity;


namespace ITCommunity
{
/// <summary>
/// Типа блокнот
/// </summary>
    public class Note
    {
        private int _id;
        private int _user_id;
        private string _title;
        private string _text;
        private DateTime _cdate;

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

        public int UserId
        {
            get
            {
                return _user_id;
            }
            set
            {
                _user_id = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public Note()
        {
            _id = -1;
            _user_id = -1;
            _title = "";
            _text = "";
            _cdate = DateTime.Now;
        }

        public Note(int id, string title, string text, int user_id, DateTime cdate)
        {
            _id = id;
            _user_id = user_id;
            _title = title;
            _text = text;
            _cdate = cdate;
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

        public static Note Add(string title, string text, int user_id, DateTime cdate)
        {
            return GetNoteFromRow(Database.NotesAdd(user_id, title, text, cdate));
        }

        public static Note GetById(int id)
        {
            return GetNoteFromRow(Database.NotesGetById(id));
        }

        public static void Delete(int id)
        {
            Database.NotesDel(id);
        }

        public static List<Note> Get(int page, int count, int user_id, ref int record_count)
        {
            return GetNotesFromTable(Database.NotesGet(user_id, page, count, ref record_count));
        }

        private static List<Note> GetNotesFromTable(DataTable dt)
        {
            List<Note> notes = new List<Note>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                notes.Add(GetNoteFromRow(dt.Rows[i]));
            }
            return notes;
        }

        private static Note GetNoteFromRow(DataRow dr)
        {
            Note note;
            if (dr == null)
            {
                note = new Note();
            }
            else
            {
                int id = Convert.ToInt32(dr["id"]);
                note = new Note(id,
                             Convert.ToString(dr["title"]),
                             Convert.ToString(dr["text"]),
                             Convert.ToInt32(dr["user_id"]),
                             Convert.ToDateTime(dr["cdate"]));
            }
            return note;
        }
    }
}
