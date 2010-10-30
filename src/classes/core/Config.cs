using System;
using System.Configuration;
using System.Web;


namespace ITCommunity.Core {

    public static class Config {

        private static string _connectionString;

        /// <summary>
        /// Адрес сайта, например: http://it.icmp.ru (без завершающего слеша).
        /// </summary>
        public static string SiteAddress {
            get {
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            }
        }

        /// <summary>
        /// Строка соединения с SQL сервером
        /// </summary>
        public static string ConnectionString {
            get {
                if (_connectionString == null) {
                    if (ConfigurationManager.ConnectionStrings[Environment.MachineName] != null) {
                        _connectionString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
                    } else {
                        _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    }
                }
                return _connectionString;
            }
        }

        private static string Get(string param) {
            string result = null;
            try {
                result = ConfigurationManager.AppSettings[param];
            } catch (ConfigurationErrorsException ex) {
                Logger.Log.Fatal("Ошибка при чтении конфигурации, параметр " + param, ex);
            }
            if (result == null) {
                Logger.Log.Info("Не задан параметр " + param);
            }
            return result;
        }

        public static int GetInt(string param) {
            int result = 0;
            try {
                result = Convert.ToInt32(Get(param));
            } catch (FormatException ex) {
                Logger.Log.Fatal("Ошибка при конвертации параметра в int, параметр " + param, ex);
            }
            return result;
        }

        public static double GetDouble(string param) {
            double result = 0;
            try {
                result = Convert.ToDouble(Get(param));
            } catch (FormatException ex) {
                Logger.Log.Fatal("Ошибка при конвертации параметра в double, параметр " + param, ex);
            }
            return result;
        }

        public static string BrowserDefaultDirectory {
            get { return Get("BrowserDefaultDirectory"); }
        }
        public static string BrowserBasePath {
            get { return Get("BrowserBasePath"); }
        }
        public static string BrowserBaseUrl {
            get { return Get("BrowserBaseUrl"); }
        }

        public static string PictureContentTypes {
            get { return Get("PictureContentTypes"); }
        }

        public static int PictureMaxSize {
            get { return GetInt("PictureMaxSize"); }
        }

        public static string PostPicturesFolder {
            get { return Get("PostPicturesFolder"); }
        }

        public static string RfcFolder {
            get { return Get("RfcFolder"); }
        }

        public static String HeaderDefaultFormat {
            get { return Get("HeaderDefaultFormat"); }
        }

        public static string GreetingPath {
            get { return Get("GreetingPath"); }
        }

        public static string PersonalGreetingPath {
            get { return Get("PersonalGreetingPath"); }
        }
        public static string TrustedSites {
            get { return Get("TrustedSites"); }
        }
        public static string IndexerPath {
            get { return Get("IndexerPath"); }
        }
        public static string WsusContentPath {
            get { return Get("WsusContentPath"); }
        }
        public static string WsusConnectionString {
            get { return Get("WsusConnectionString"); }
        }

        public static int MaxThumbWidth {
            get { return GetInt("MaxThumbWidth"); }
        }
        public static int MaxThumbHeight {
            get { return GetInt("MaxThumbHeight"); }
        }
        public static int LastCommentsSize {
            get { return GetInt("LastCommentsSize"); }
        }
        public static int HeaderShowingHours {
            get { return GetInt("HeaderShowingHours"); }
        }
        public static int HeaderRequiredPostsCount {
            get { return GetInt("HeaderRequiredPostsCount"); }
        }
        public static int HeaderMaxLength {
            get { return GetInt("HeaderMaxLength"); }
        }
        public static int PollCategoryId {
            get { return GetInt("PollCategoryId"); }
        }
        public static int LastCommentsCount {
            get { return GetInt("LastCommentsCount"); }
        }
        public static int PopularPostsDays {
            get { return GetInt("PopularPostsDays"); }
        }
        public static int PopularPostsCount {
            get { return GetInt("PopularPostsCount"); }
        }
        public static int DiscussiblePostsDays {
            get { return GetInt("DiscussiblePostsDays"); }
        }
        public static int DiscussiblePostsCount {
            get { return GetInt("DiscussiblePostsCount"); }
        }
        public static int RatedPostsDays {
            get { return GetInt("RatedPostsDays"); }
        }
        public static int RatedPostsCount {
            get { return GetInt("RatedPostsCount"); }
        }
        public static int RecoveryPurgeDays {
            get { return GetInt("RecoveryPurgeDays"); }
        }
        public static int ActivePostersCount {
            get { return GetInt("ActivePostersCount"); }
        }
        public static int ActivePostersDays {
            get { return GetInt("ActivePostersDays"); }
        }
        public static int TopPostersCount {
            get { return GetInt("TopPostersCount"); }
        }
        public static int ActiveCommentatorsCount {
            get { return GetInt("ActiveCommentatorsCount"); }
        }
        public static int ActiveCommentatorsDays {
            get { return GetInt("ActiveCommentatorsDays"); }
        }
        public static int TopCommentatorsCount {
            get { return GetInt("TopCommentatorsCount"); }
        }
        public static int LastRegisteredUsersCount {
            get { return GetInt("LastRegisteredUsersCount"); }
        }
        public static int PollWidth {
            get { return GetInt("PollWidth"); }
        }
        public static int PollHeight {
            get { return GetInt("PollHeight"); }
        }
        public static int PollThumbWidth {
            get { return GetInt("PollThumbWidth"); }
        }
        public static int PollThumbHeight {
            get { return GetInt("PollThumbHeight"); }
        }
        public static int RssPostsCount {
            get { return GetInt("RssPostsCount"); }
        }
        public static int PostsPerPage {
            get { return GetInt("PostsPerPage"); }
        }
        public static int CaptchasPerPage {
            get { return GetInt("CaptchasPerPage"); }
        }
        public static int HeadersPerPage {
            get { return GetInt("HeadersPerPage"); }
        }
        public static int MessagesPerPage {
            get { return GetInt("MessagesPerPage"); }
        }
        public static int UserCommentsPerPage {
            get { return GetInt("UserCommentsPerPage"); }
        }
        public static int UsersPerPage {
            get { return GetInt("UsersPerPage"); }
        }
        public static String RatedPostsTimeText {
            get { return Get("RatedPostsTimeText"); }
        }
        public static String DiscussiblePostsTimeText {
            get { return Get("DiscussiblePostsTimeText"); }
        }
        public static String PopularPostsTimeText {
            get { return Get("PopularPostsTimeText"); }
        }
        public static String ActiveCommentatorsTimeText {
            get { return Get("ActiveCommentatorsTimeText"); }
        }
        public static String ActivePostersTimeText {
            get { return Get("ActivePostersTimeText"); }
        }
    }
}
