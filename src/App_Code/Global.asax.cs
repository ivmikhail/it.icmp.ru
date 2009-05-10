using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ITCommunity;
using System.IO;

namespace ITCommunity
{
    /// <summary>
    /// Мега-глобал класс
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        public Global()
        {
        }

        public void Application_AuthenticateRequest(Object src, EventArgs e)
        {
            //Note: Здесь нельзя получить доступ к сессии
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity.AuthenticationType == "Forms")
                {
                    System.Web.Security.FormsIdentity id = (System.Web.Security.FormsIdentity)HttpContext.Current.User.Identity;
                    string[] roles = id.Ticket.UserData.Split(','); // на самом деле у чела не может быть по 2 роли одновременно
                    Context.User = new System.Security.Principal.GenericPrincipal(id, roles);

                }
            }
        }

        private static string _connectionString = "";
        public static string ConnectionString
        {
            get
            {
                if (_connectionString == "")
                {
                    _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                }
                return _connectionString;
            }
        }
        private static string _rfcFolder = "";
        public static string RfcFolder
        {
            get
            {
                if (_rfcFolder == "")
                {
                    _rfcFolder = ConfigurationManager.AppSettings["RfcFolder"];
                }
                return _rfcFolder;
            }
        }
        private static int _messageCount = -1;
        public static int MaxMessageCount
        {
            get
            {
                if (_messageCount == -1)
                {
                    _messageCount = Convert.ToInt32(ConfigurationManager.AppSettings["MaxMessageCount"].ToString()); ;
                }
                return _messageCount;
            }
        }
        private static int _notesCount = -1;
        public static int MaxNotesCount
        {
            get
            {
                if (_notesCount == -1)
                {
                    _notesCount = Convert.ToInt32(ConfigurationManager.AppSettings["MaxNotesCount"].ToString()); ;
                }
                return _notesCount;
            }
        }
        private static int _postsCount = -1;
        public static int PostsCount
        {
            get
            {
                if (_postsCount == -1)
                {
                    _postsCount = Convert.ToInt32(ConfigurationManager.AppSettings["PostsCount"].ToString()); ;
                }
                return _postsCount;
            }
        }
        private static int _favCount = -1;
        public static int FavoritesCount
        {
            get
            {
                if (_favCount == -1)
                {
                    _favCount = Convert.ToInt32(ConfigurationManager.AppSettings["FavoritesCount"].ToString()); ;
                }
                return _favCount;
            }
        }
        private static int _lastCommentsCount = -1;
        public static int LastCommentsCount
        {
            get
            {
                if (_lastCommentsCount == -1)
                {
                    _lastCommentsCount = Convert.ToInt32(ConfigurationManager.AppSettings["LastCommentsCount"].ToString()); ;
                }
                return _lastCommentsCount;
            }
        }
        private static int _popularPostsCount = -1;
        public static int PopularPostsCount
        {
            get
            {
                if (_popularPostsCount == -1)
                {
                    _popularPostsCount = Convert.ToInt32(ConfigurationManager.AppSettings["PopularPostsCount"].ToString()); ;
                }
                return _popularPostsCount;
            }
        }

        private static int _popularPostsPeriod = -1;
        public static int PopularPostsPeriod
        {
            get
            {
                if (_popularPostsPeriod == -1)
                {
                    _popularPostsPeriod = Convert.ToInt32(ConfigurationManager.AppSettings["PopularPostsPeriod"].ToString()); ;
                }
                return _popularPostsPeriod;
            }
        }
        private static int _topPostersCount = -1;
        public static int TopPostersCount
        {
            get
            {
                if (_topPostersCount == -1)
                {
                    _topPostersCount = Convert.ToInt32(ConfigurationManager.AppSettings["TopPostersCount"].ToString()); ;
                }
                return _topPostersCount;
            }
        }
        private static int _lastRegisteredCount = -1;
        public static int LastRegisteredCount
        {
            get
            {
                if (_lastRegisteredCount == -1)
                {
                    _lastRegisteredCount = Convert.ToInt32(ConfigurationManager.AppSettings["LastRegisteredCount"].ToString()); ;
                }
                return _lastRegisteredCount;
            }
        }

        private static string _postImageOptions = String.Empty;
        public static string PostImageOptions
        {
            get
            {
                if (_postImageOptions == String.Empty)
                {
                    _postImageOptions = "Размер до " + ConfigurationManager.AppSettings["PostImgWidth"].ToString() + "x" + ConfigurationManager.AppSettings["PostImgHeight"].ToString() + "; обьем до " + (Math.Round((decimal.Parse(ConfigurationManager.AppSettings["PostImgSize"])) / 1024, 2)).ToString() + "кб; тип файла изображение(jpeg, gif и т.д).";
                }

                return _postImageOptions;
            }
        }
        private static int _postImageSize = 0;
        public static int PostImageSize
        {
            get
            {
                if (_postImageSize == 0)
                {
                    _postImageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PostImgSize"]);
                }

                return _postImageSize;
            }
        }
        private static int _postImageHeight = 0;
        public static int PostImageHeight
        {
            get
            {
                if (_postImageHeight == 0)
                {
                    _postImageHeight = Convert.ToInt32(ConfigurationManager.AppSettings["PostImgHeight"]);
                }

                return _postImageHeight;
            }
        }

        private static int _postImageWidth = 0;
        public static int PostImageWidth
        {
            get
            {
                if (_postImageWidth == 0)
                {
                    _postImageWidth = Convert.ToInt32(ConfigurationManager.AppSettings["PostImgWidth"]);
                }

                return _postImageWidth;
            }
        }

        private static int _maxThumbWidth = 0;
        public static int MaxThumbWidth
        {
            get
            {
                if (_maxThumbWidth == 0)
                {
                    _maxThumbWidth = Convert.ToInt32(ConfigurationManager.AppSettings["MaxThumbWidth"]);
                }

                return _maxThumbWidth;
            }
        }

        private static string _postImagesFolder = "";
        public static string PostImagesFolder
        {
            get
            {
                if (_postImagesFolder == "")
                {
                    _postImagesFolder = ConfigurationManager.AppSettings["PostImagesFolder"].ToString();
                }

                return _postImagesFolder;
            }
        }
        private static string _filesFolder = "";
        public static string FilesFolder {
            get {
                if (_filesFolder == "") {
                    _filesFolder = ConfigurationManager.AppSettings["FilesFolder"].ToString();
                    if (!Directory.Exists(_filesFolder)) {
                        throw new Exception("Folder in Web.config FilesFolder not exist");
                    }
                    if (!_filesFolder.EndsWith("\\")) {
                        _filesFolder += "\\";
                    }
                }
                return _filesFolder;
            }
        }
        private static string _filesLink = "";
        public static string FilesLink {
            get {
                if (_filesLink == "") {
                    _filesLink = ConfigurationManager.AppSettings["FilesLink"].ToString();
                }

                return _filesLink;
            }
        }
    }
}