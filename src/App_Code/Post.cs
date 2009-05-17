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
    /// ���������� �������.
    /// </summary>
    public class Post
    {
        private int _id;
        private string _title;
        private string _description;
        private string _text;
        private DateTime _cdate;
        private int _userId;
        private List<Category> _cats;
        private int _attached;
        private int _views;
        private string _source;
        private int _commentsCount;

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

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
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

        public User Author
        {
            get
            {
                //TODO: ����������!!!
                return User.GetById(_userId);
            }
            set
            {
                _userId = value.Id;
            }
        }

        public List<Category> Cats
        {
            get
            {
                return _cats;
            }
            set
            {
                _cats = value;
            }
        }

        public bool Attached
        {
            get
            {
                return !(_attached == 0);
            }
            set
            {
                _attached = value ? 1 : 0;
            }
        }

        public int Views
        {
            get
            {
                return _views;
            }
        }

        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }
        public int CommentsCount
        {
            get
            {
                return _commentsCount;
            }
            set
            {
                _commentsCount = value;
            }
        }

        public bool IsPostOwner(User user)
        {
            bool is_owner = false;
            if (user.Id > 0)
            {
                if (user.Id == this.Author.Id)
                {
                    is_owner = true;
                }
            }
            if (this.Id < 1)
            {
                is_owner = true;
            }
            return is_owner;
        }
        /// <summary>
        /// �������� �������� �� ������ ��������� ��� ������� ������������
        /// </summary>
        /// <param name="user_id">������ ������������</param>
        /// <returns>���</returns>
        public bool IsFavorites(int user_id)
        {
            return Convert.ToBoolean(Database.PostIsFavorite(user_id, this.Id));
        }
        /// <summary>
        /// ���������� ������ �� ��������/���������� ������� ��/� ���������
        /// </summary>
        public string FavoritesAction
        {
            get
            {
                string value = "<a href='register.aspx?favorites.aspx?a=add&post=" + this.Id + "' title='�������� � ���������'><img src='media/img/non-fav.png' class='fixPNG' /></a>"; ;
                if (CurrentUser.isAuth)
                {
                    if (IsFavorites(CurrentUser.User.Id))
                    {

                        value = "<a href='favorites.aspx?&a=del&post=" + this.Id + "' title='������ �� ����������'><img src='media/img/is-fav.png' class='fixPNG' /></a>";
                    } else
                    {
                        value = "<a href='favorites.aspx?a=add&post=" + this.Id + "' title='�������� � ���������'><img src='media/img/non-fav.png' class='fixPNG' /></a>";
                    }
                }
                return value;
            }
        }

        /// <summary>
        /// ��������� ������� � ��� ���������
        /// </summary>
        public void UpdateWithCategories()
        {
            Database.PostUpdate(_id, _title, _description, _text, (byte)_attached, _source, _commentsCount);
            Post.PostAttachCategories(_cats, this);
        }
        /// <summary>
        /// ��������� ������ �������
        /// </summary>
        public void Update()
        {
            Database.PostUpdate(_id, _title, _description, _text, (byte)_attached, _source, _commentsCount);
        }
        /// <summary>
        /// ��������� ���-� ����������
        /// </summary>
        public void UpdateViews()
        {
            Database.PostUpdateViews(_id);
        }

        public Post(int id, string title, string description, string text, DateTime cdate, int userId, bool attached, int views, string source, int commentsCount, List<Category> cats)
        {
            _id = id;
            _title = title;
            _description = description;
            _text = text;
            _cdate = cdate;
            _userId = userId;
            _attached = attached ? 1 : 0;
            _views = views;
            _source = source;
            _commentsCount = commentsCount;
            _cats = cats;
        }

        public Post()
        {
            _id = -1;
            _title = "";
            _description = "";
            _text = "";
            _cdate = DateTime.Now;
            _userId = -1;
            _cats = new List<Category>();
            _attached = 0;
            _views = 0;
            _source = "";
            _commentsCount = 0;
        }

        public static Post GetById(int id)
        {
            return GetPostFromRow(Database.PostGetById(id));
        }

        public static void Delete(Post post)
        {
            Database.PostDel(post.Id);
        }
        /// <summary>
        /// �������� ������� � ����������
        /// </summary>
        /// <param name="cats">���������</param>
        /// <param name="post_id">�������</param>
        private static void PostAttachCategories(List<Category> cats, Post post)
        {
            // ����� �� �����������
            string query = "INSERT INTO post_cat(post_id, cat_id) VALUES";
            string param = String.Empty;
            foreach (Category cat in cats)
            {
                if (param.Length > 0)
                {
                    param += ", ";
                }
                param += "(" + post.Id + "," + cat.Id + ")";
            }
            query += param;
            Database.PostAttachCategories(post.Id, query);
        }

        /// <summary>
        /// ����� �� ������, ��������� title, desc, text
        /// </summary>
        /// <param name="page">������� ��������</param>
        /// <param name="count">���-�� ������ �� ��������</param>
        /// <param name="query">������</param>
        /// <param name="posts_count">���-�� ��������� ������</param>
        /// <returns>������ ��������� ������</returns>
        public static List<Post> Search(int page, int count, string query, ref int posts_count)
        {
            return GetPostsFromTable(Database.PostSearch(query, page, count, ref posts_count));
        }
        /// <summary>
        /// �������� ����� �����������, � ������ ���� � �������
        /// </summary>
        /// <param name="page">�������� ������� ��� �����</param>
        /// <param name="count">���-�� ������ �� ��������</param>
        public static List<Post> Get(int page, int count, ref int posts_count)
        {
            return GetPostsFromTable(Database.PostGet(page, count, ref posts_count));
        }
        /// <summary>
        /// �������� ����� �����������, � ������ ����, ������� � ���������
        /// </summary>
        /// <param name="page">�������� ������� ��� �����</param>
        /// <param name="count">���-�� ������ �� ��������</param>
        /// <param name="count">id ���������</param>
        public static List<Post> GetByCategory(int page, int count, int cat_id, ref int posts_count)
        {
            return GetPostsFromTable(Database.PostGetByCat(page, count, cat_id, ref posts_count));
        }



        /// <summary>
        /// ���������� �����
        /// </summary>
        /// <param name="period">������, � ����. �������� ���������� ����� �� ��������� N ����.</param>
        /// <param name="count">���-�� ������ ������</param>
        public static List<Post> GetTop(int period, int count)
        {
            return GetPostsFromTable(Database.PostGetTop(period, count));
        }

        /// <summary>
        /// ���������� ������ �����
        /// </summary>
        /// <param name="post">��� ����, CreateDate ����� ������� �� ���� ���������� ������� � ����.</param>
        public static Post Add(Post post)
        {
            DataRow dr = Database.PostAdd(post.Title,
                                          post.Description,
                                          post.Text,
                                          Convert.ToByte(post.Attached),
                                          post.Source,
                                          post.Author.Id);
            List<Category> cats = post.Cats;
            Post newpost = GetPostFromRow(dr);
            PostAttachCategories(cats, newpost);
            return newpost;
        }
        /// <summary>
        /// ��������� �����
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static List<Post> GetFavorites(int user_id, int page, int count, ref int total_records)
        {
            return GetPostsFromTable(Database.FavoriteGetByUser(user_id, page, count, ref total_records));
        }

        public static void FavoriteDelete(int post_id, int user_id)
        {
            Database.FavoriteDel(post_id, user_id);
        }

        public static Post FavoriteAdd(int user_id, int post_id)
        {
            return GetPostFromRow(Database.FavoriteAdd(user_id, post_id));
        }

        private static List<Post> GetPostsFromTable(DataTable dt)
        {
            List<Post> posts = new List<Post>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                posts.Add(GetPostFromRow(dt.Rows[i]));
            }
            return posts;
        }

        private static Post GetPostFromRow(DataRow dr)
        {
            Post post;
            if (dr == null)
            {
                post = new Post();
            } else
            {
                int id = Convert.ToInt32(dr["id"]);
                post = new Post(id,
                             Convert.ToString(dr["title"]),
                             Convert.ToString(dr["description"]),
                             Convert.ToString(dr["text"]),
                             Convert.ToDateTime(dr["cdate"]),
                             Convert.ToInt32(dr["user_id"]),
                             Convert.ToBoolean(dr["attached"]),
                             Convert.ToInt32(dr["views"]),
                             Convert.ToString(dr["source"]),
                             Convert.ToInt32(dr["comments_count"]),
                             Category.GetPostCategrories(id)); //TODO: MZFK
            }
            return post;
        }
    }
}