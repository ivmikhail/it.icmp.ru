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
    /// ����������� � �����
    /// </summary>
    /// 
    public class Comment
    {
        //������� ������ �������� ��������� ��������� �� ����, ����� ��� ����������� �����������
        private delegate object LastCommentsLoader(int count);

        private int _id;
        private int _postId;
        private int _userId;
        private DateTime _cdate;
        private string _ip;
        private string _text;

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

        public Post Post
        {
            get
            {
                return Post.GetById(_postId);
            }
            set
            {
                _postId = value.Id;
            }
        }

        public int PostId
        {
            get
            {
                return _postId;
            }
            set
            {
                _postId = value;
            }
        }

        /// <summary>
        /// ������������ ���������� �����������, ���� ��� ������ �� �������������� �������,
        /// �� ������������ "������" ������������(����������� �� ���������)
        /// </summary>
        public User Author
        {
            get
            {
                User user;
                if (_userId > 0)
                {
                    user = User.GetById(_userId);
                } else
                {
                    user = new User();
                }
                return user;
            }
            set
            {
                _userId = value.Id;
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

        public string Ip
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
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
        public string ShortText
        {
            get
            {
                return (_text.Length > 20) ? _text.Substring(0, 20) + " ..." : _text;
            }
        }

        public Comment(int id, int postId, int userId, DateTime cdate, string ip, string text)
        {
            _id = id;
            _postId = postId;
            _userId = userId;
            _cdate = cdate;
            _ip = ip;
            _text = text;
        }

        public Comment()
        {
            _id = -1;
            _postId = -1;
            _userId = -1;
            _cdate = DateTime.Now;
            _ip = "";
            _text = "";
        }

        /// <summary>
        /// ������� �������
        /// </summary>
        /// <param name="id">Id ��������</param>
        public static void Delete(int id)
        {            
            Database.CommentDel(id);
        }

        /// <summary>
        /// �������� �������� �����
        /// </summary>
        /// <param name="postId">������������� �����</param>
        public static List<Comment> GetByPost(int postId)
        {
            return GetCommentsFromTable(Database.CommentGetByPost(postId));
        }

        /// <summary>
        /// �������� ��������� ����������� �� ����
        /// </summary>
        /// <param name="count">���-�� ������ ���������</param>
        public static List<KeyValuePair<User, Comment>> GetLasts(int count)
        {

            LastCommentsLoader loader = new LastCommentsLoader(GetLastCommentsFromDB);
            List<KeyValuePair<User, Comment>> last_comments = (List<KeyValuePair<User, Comment>>)AppCache.Get(Global.ConfigStringParam("LastCommentsCacheName"), 
                                                                                                              new object(), 
                                                                                                              loader, 
                                                                                                              new object[] { count }, 
                                                                                                              DateTime.Now.AddHours(Global.ConfigDoubleParam("CacheLastComment")));

            return last_comments;
        }

        private static List<KeyValuePair<User, Comment>> GetLastCommentsFromDB(int count)
        {
            List<Comment> comments = GetCommentsFromTable(Database.CommentGetLasts(count));
            List<KeyValuePair<User, Comment>> last_comments = new List<KeyValuePair<User, Comment>>();

            foreach (Comment comment in comments)
            {
                last_comments.Add(new KeyValuePair<User, Comment>(comment.Author, comment));
            }

            return last_comments;
        }

        /// <summary>
        /// ���������� ����������� � ����. ���-�� ������������ ����� ����������� �� ������ ����
        /// </summary>
        /// <param name="comment">��� �������</param>
        public static Comment Add(Comment comment)
        {
            Comment comm = GetCommentFromRow(Database.CommentAdd(comment.Post.Id, comment.Author.Id, comment.Ip, comment.Text));
            return comm;
        }

        private static List<Comment> GetCommentsFromTable(DataTable dt)
        {
            List<Comment> comments = new List<Comment>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comments.Add(GetCommentFromRow(dt.Rows[i]));
            }
            return comments;
        }

        private static Comment GetCommentFromRow(DataRow dr)
        {
            Comment comment;
            if (dr == null)
            {
                comment = new Comment();
            } else
            {
                comment = new Comment(Convert.ToInt32(dr["id"]),
                                 Convert.ToInt32(dr["post_id"]),
                                 Convert.ToInt32(dr["user_id"]),
                                 Convert.ToDateTime(dr["cdate"]),
                                 Convert.ToString(dr["ip"]),
                                 Convert.ToString(dr["text"]));
            }

            return comment;
        }
    }
}
