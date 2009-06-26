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
    /// Собственно новость.
    /// </summary>
    public class Post
    {
        //делегат метода загрузки последних постов из базы, нужен для организации кеширования
        private delegate object LastPostsLoader(int count);
        //делегат метода загрузки популярных постов из базы, нужен для организации кеширования
        private delegate object TopPostsLoader(int period, int count);

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
                //TODO: переделать!!!
                return User.GetById(_userId);
            }
            set
            {
                _userId = value.Id;
            }
        }

        public List<Category> Categories
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
                //TODO: Накладывает кое-какие ограничения.
                if (value.Length != 0)
                {
                    if (0 < value.Length && value.Length < 8)
                    {
                        value = "http://" + value;
                    } else if(value.Substring(0, 7) != "http://")
                    {
                        value = "http://" + value;
                    }
                }
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

        /// <summary>
        /// Выясняем является ли данный пользователь автором(создателем) новости
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>да/нет</returns>
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
        /// Выясняем является ли статья избранным для данного пользователя
        /// </summary>
        /// <param name="user_id">Данный пользователь</param>
        /// <returns>Булеан</returns>
        public bool IsFavorites(int user_id)
        {
            return Convert.ToBoolean(Database.PostIsFavorite(user_id, this.Id));
        }

        /// <summary>
        /// Возвращает ссылку на удаление/добавление новости из/в избранное
        /// </summary>
        public string FavoritesAction
        {
            //TODO: Переделать!
            get
            {
                string value = "<a href='register.aspx?favorites.aspx?a=add&post=" + this.Id + "' title='Добавить в избранное'><img src='media/img/design/non-fav.png' class='fixPNG favorites-link' /></a>"; ;
                if (CurrentUser.isAuth)
                {
                    if (IsFavorites(CurrentUser.User.Id))
                    {

                        value = "<a href='favorites.aspx?&a=del&post=" + this.Id + "' title='Убрать из избранного'><img src='media/img/design/is-fav.png' class='fixPNG' /></a>";
                    } else
                    {
                        value = "<a href='favorites.aspx?a=add&post=" + this.Id + "' title='Добавить в избранное'><img src='media/img/design/non-fav.png' class='fixPNG' /></a>";
                    }
                }
                return value;
            }
        }

        /// <summary>
        /// Обновляем новость и его категории
        /// 
        /// Обновляются такие атрибуты как:
        /// * title - Название 
        /// * description - Краткое описание
        /// * text - Текст новости
        /// * attached - Прикреплено ли
        /// * source - Источник(ссылка на оригинал)
        /// 
        /// * categories - категории новости(отдельные таблицы)
        /// </summary>
        public void UpdateWithCategories()
        {
            Database.PostUpdate(_id, _title, _description, _text, (byte)_attached, _source, _commentsCount);
            Post.PostAttachCategories(_cats, this);
        }

        /// <summary>
        /// Обновляем только новость(категории и кол-во просмотров не обновляются)
        /// 
        /// Обновляются такие атрибуты как:
        /// * title - Название 
        /// * description - Краткое описание
        /// * text - Текст новости
        /// * attached - Прикреплено ли
        /// * source - Источник(ссылка на оригинал)
        /// 
        /// </summary>

        public void Update()
        {
            Database.PostUpdate(_id, _title, _description, _text, (byte)_attached, _source, _commentsCount);
        }

        /// <summary>
        /// Увеличиваем кол-во просмотров новости на 1 единицу
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
        /// Сцепляем новость к категориям
        /// </summary>
        /// <param name="menu">Категории</param>
        /// <param name="post_id">Новость</param>
        private static void PostAttachCategories(List<Category> cats, Post post)
        {
            // лучше не придумалось

            /* 
                Формируется запрос вида                
             
                INSERT INTO post_cat(post_id, cat_id) 
                SELECT 1,2 
                UNION ALL
                SELECT 1,3
                UNION ALL
                SELECT 1,4
              
                (множественная вставка как в мускле не прокатывает)
             */
            string query = "INSERT INTO post_cat(post_id, cat_id) ";
            string param = String.Empty;
            foreach (Category cat in cats)
            {
                if (param.Length > 0)
                {
                    param += " UNION ALL ";
                }
                param += "SELECT " + post.Id + "," + cat.Id ;
            }
            query += param;
            Database.PostAttachCategories(post.Id, query);
        }

        /// <summary>
        /// Полнотекстовый поиск по постам, учитываем title, desc, text
        /// </summary>
        /// <param name="page">текущая страница</param>
        /// <param name="count">кол-во постов на страницу</param>
        /// <param name="query">запрос</param>
        /// <param name="posts_count">кол-во найденных постов</param>
        /// <returns>Список найденных постов</returns>
        public static List<Post> Search(int page, int count, string query, ref int posts_count)
        {
            return GetPostsFromTable(Database.PostSearch(query, page, count, ref posts_count));
        }

        /// <summary>
        /// Забираем посты постранично, с учетом даты и аттачей
        /// </summary>
        /// <param name="page">Страница которая нам нужна</param>
        /// <param name="count">Кол-во постов на страницу</param>
        public static List<Post> Get(int page, int count, ref int posts_count)
        {
            return GetPostsFromTable(Database.PostGet(page, count, ref posts_count));
        }

        /// <summary>
        /// Возвращает последние добавленные посты из кеша
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Post> GetLast(int count)
        {
            LastPostsLoader loader = new LastPostsLoader(GetLastPostsFromDB);
            List<Post> lasts = (List<Post>)AppCache.Get(Global.ConfigStringParam("LastPostsCacheName"),
                                                        new object(),
                                                        loader,
                                                        new object[] { count },
                                                        DateTime.Now.AddHours(Global.ConfigDoubleParam("LastPostsCachePer")));
            return lasts;
        }

        private static List<Post> GetLastPostsFromDB(int count)
        {
            return GetPostsFromTable(Database.PostGetLast(count));
        }

        /// <summary>
        /// Забираем посты постранично, с учетом даты, аттачей и категории
        /// </summary>
        /// <param name="page">Страница которая нам нужна</param>
        /// <param name="count">Кол-во постов на страницу</param>
        /// <param name="count">id категории</param>
        public static List<Post> GetByCategory(int page, int count, int cat_id, ref int posts_count)
        {
            return GetPostsFromTable(Database.PostGetByCat(page, count, cat_id, ref posts_count));
        }

        /// <summary>
        /// Популярные посты. Т.к хранится в кеше, то возможны задержки при изменении 
        /// параметров period и count в течении жизни кеша
        /// </summary>
        /// <param name="period">Период, в днях. Например, популярные посты за последние N дней.</param>
        /// <param name="count">Кол-во нужных постов</param>
        public static List<KeyValuePair<User, Post>> GetTop(int period, int count)
        {
            TopPostsLoader loader = new TopPostsLoader(GetTopPostsFromDB);
            List<KeyValuePair<User, Post>> top_posts = (List<KeyValuePair<User, Post>>)AppCache.Get(Global.ConfigStringParam("TopPostsCacheName"),
                                                                                                    new object(),
                                                                                                    loader,
                                                                                                    new object[] { period, count},
                                                                                                    DateTime.Now.AddHours(Global.ConfigDoubleParam("TopPostsCachePer")));

            return top_posts;
        }

        private static List<KeyValuePair<User, Post>> GetTopPostsFromDB(int period, int count)
        {
            List<Post> posts = GetPostsFromTable(Database.PostGetTop(period, count));
            List<KeyValuePair<User, Post>> top = new List<KeyValuePair<User, Post>>();
            foreach (Post post in posts)
            {
                top.Add(new KeyValuePair<User, Post>(post.Author, post));
            }
            return top;
        }


        /// <summary>
        /// Добавление нового поста
        /// </summary>
        /// <param name="post">Сам пост, CreateDate будет изменен на дату добавления новости в базу.</param>
        public static Post Add(Post post)
        {
            DataRow dr = Database.PostAdd(post.Title,
                                          post.Description,
                                          post.Text,
                                          Convert.ToByte(post.Attached),
                                          post.Source,
                                          post.Author.Id);
            List<Category> cats = post.Categories;
            Post newpost = GetPostFromRow(dr);
            PostAttachCategories(cats, newpost);
            return newpost;
        }

        /// <summary>
        /// Получаем избранные посты пользователя
        /// </summary>
        /// <param name="user_id">идентификатор пользователя</param>
        /// <returns>список постов</returns>
        public static List<Post> GetFavorites(int user_id, int page, int count, ref int total_records)
        {
            return GetPostsFromTable(Database.FavoriteGetByUser(user_id, page, count, ref total_records));
        }

        /// <summary>
        /// Убираем новость из "избранных" данного пользователя
        /// </summary>
        /// <param name="post_id">идентификатор поста</param>
        /// <param name="user_id">идентификатор пользователя</param>
        public static void FavoriteDelete(int post_id, int user_id)
        {
            Database.FavoriteDel(post_id, user_id);
        }

        /// <summary>
        /// Добавляем посты в "избранное" пользователя
        /// </summary>
        /// <param name="post_id">идентификатор поста</param>
        /// <param name="user_id">идентификатор пользователя</param>
        public static Post FavoriteAdd(int post_id, int user_id)
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
                             Category.GetPostCategories(id)); //TODO: MZFK
            }
            return post;
        }
    }
}