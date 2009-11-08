using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

using ITCommunity;

namespace ITCommunity
{
    /// <summary>
    /// Комментарий к посту
    /// </summary>
    /// 
    public class Comment
    {
        //делегат метода загрузки последних комментов из базы, нужен для организации кеширования
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
        /// Пользователь оставивший комментарий, если это сделал не авторизованный человек,
        /// то возвращается "пустой" пользователь(конструктор по умолчанию)
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
        // "as is", может содержать небезопасный хтмл, bbcode не учитывается.
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

        /// <summary>
        /// BBCode преобразован в безопасный хтмл, по идее xss не должно быть.
        /// </summary>
        public string TextFormatted
        {
            get
            {
                return BBCodeParser.Format(HttpUtility.HtmlEncode(_text));
            }
        }

        public string ShortText
        {
            get
            {
                string safely_text = HttpUtility.HtmlEncode(_text);
                safely_text = Regex.Replace(safely_text, "\\[(.*?)\\](.*?)\\[\\/(.*?)\\]", "$2", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                return (safely_text.Length > 80) ? safely_text.Substring(0, 80) + " ..." : safely_text;
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

        /// <summar>y
        /// Удаляем коммент
        /// </summary>
        /// <param name="id">Id коммента</param>
        public static void Delete(int id)
        {            
            Database.CommentDel(id);
            AppCache.Remove(Global.ConfigStringParam("LastCommentsCacheName"));
        }

        /// <summary>
        /// Забираем комменты поста
        /// </summary>
        /// <param name="postId">Идентификатор поста</param>
        public static List<Comment> GetByPost(int postId)
        {
            return GetCommentsFromTable(Database.CommentGetByPost(postId));
        }

        /// <summary>
        /// Забираем последние комментарии из кеша
        /// </summary>
        /// <param name="count">Кол-во нужных комментов</param>
        public static List<KeyValuePair<User, Comment>> GetLasts(int count)
        {

            LastCommentsLoader loader = new LastCommentsLoader(GetLastCommentsFromDB);
            List<KeyValuePair<User, Comment>> last_comments = (List<KeyValuePair<User, Comment>>)AppCache.Get(Global.ConfigStringParam("LastCommentsCacheName"), 
                                                                                                              new object(), 
                                                                                                              loader, 
                                                                                                              new object[] { count }, 
                                                                                                              DateTime.Now.AddHours(Global.ConfigDoubleParam("CacheLastCommentPer")));

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
        /// Добавление комментария в базу. Кол-во комментариев поста обновляется на уровне базы
        /// </summary>
        /// <param name="comment">Сам коммент</param>
        public static Comment Add(Comment comment)
        {
            Comment comm = GetCommentFromRow(Database.CommentAdd(comment.Post.Id, comment.Author.Id, comment.Ip, comment.Text));
            AppCache.Remove(Global.ConfigStringParam("LastCommentsCacheName"));
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
