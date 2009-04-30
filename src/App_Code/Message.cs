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
    /// Приватные сообщения
    /// </summary>
    public class Message
    {
        private int _id;
        private int _receiver_id;
        private int _sender_id;
        private string _title;
        private string _text;
        private bool _receiver_read;
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

        public User Receiver
        {
            get
            {
                return User.GetById(_receiver_id);
            }
            set
            {
                _receiver_id = value.Id;
            }
        }

        public User Sender
        {
            get
            {
                return User.GetById(_sender_id);
            }
            set
            {
                _sender_id = value.Id;
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

        public bool ReceiverRead
        {
            get
            {
                return _receiver_read;
            }
            set
            {
                _receiver_read = value;
            }
        }
        public Message()
        {
            _id = -1;
            _receiver_id = -1;
            _sender_id = -1;
            _text = "";
            _title = "";
            _receiver_read = false;
        }

        public Message(int id, int receiver_id, int sender_id, string title, string text, DateTime cdate, bool receiver_read)
        {
            _id = id;
            _receiver_id = receiver_id;
            _sender_id = sender_id;
            _title = title;
            _text = text;
            _cdate = cdate;
            _receiver_read = receiver_read;
        }
        public static List<Message> GetByReceiver(int receiver_id, int page, int count, ref int mess_count)
        {
            return GetMessFromTable(Database.MessageGetByReceiver(receiver_id, page, count, ref mess_count));
        }

        public static List<Message> GetBySender(int sender_id, int page, int count, ref int mess_count)
        {
            return GetMessFromTable(Database.MessageGetBySender(sender_id, page, count, ref mess_count));
        }

        public static Message Send(int receiver_id, int sender_id, string title, string text)
        {
            return GetMessFromRow(Database.MessageAdd(receiver_id, sender_id, title, text));
        }

        public static Message GetById(int id)
        {
            return GetMessFromRow(Database.MessageGetById(id));
        }
        public static int GetNewCount(int rec_id)
        {
            return (int)Database.MessageGetNewCount(rec_id);
        }
        public void DeleteByReceiver()
        {
            Database.MessageDelByReceiver(this._id);
        }

        public void DeleteBySender()
        {
            Database.MessageDelBySender(this._id);
            ;
        }
        public void MarkAsRead()
        {
            Database.MessageMarkAsRead(this._id);
        }

        private static List<Message> GetMessFromTable(DataTable dt)
        {
            List<Message> messages = new List<Message>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                messages.Add(GetMessFromRow(dt.Rows[i]));
            }
            return messages;
        }

        private static Message GetMessFromRow(DataRow dr)
        {
            Message mess;
            if (dr == null)
            {
                mess = new Message();
            } else
            {
                mess = new Message(Convert.ToInt32(dr["id"]),
                                     Convert.ToInt32(dr["receiver_id"]),
                                     Convert.ToInt32(dr["sender_id"]),
                                     Convert.ToString(dr["title"]),
                                     Convert.ToString(dr["text"]),
                                     Convert.ToDateTime(dr["cdate"]),
                                     Convert.ToBoolean(dr["receiver_read"]));
            }
            return mess;
        }
    }
}