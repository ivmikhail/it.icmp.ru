using log4net;
using log4net.Config;
using System.Web;
using System;


namespace ITCommunity.Core {

    public static class Logger {

        private static ILog _instance;

        public static ILog Log {
            get { return GetInstance(); }
        }
        
        public static string GetUserInfo() {
            return "\nДанные пользователя:" +
                "\n   NICK: " + CurrentUser.User.Nick +
                "\n   IP:   " + CurrentUser.Ip +
                "\n   URL:  " + HttpContext.Current.Request.Url.ToString();
        }

        private static ILog GetInstance() {
            if (_instance == null) {
                _instance = LogManager.GetLogger(typeof(Logger));
                XmlConfigurator.Configure();
            }
            return _instance;
        }
    }
}
