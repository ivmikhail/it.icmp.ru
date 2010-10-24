using log4net;
using log4net.Config;
using System.Web;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace ITCommunity.Core {

    public static class Logger {

        private static ILog _instance;

        public static ILog Log {
            get { return GetInstance(); }
        }
        
        public static string GetUserInfo() {
            string request = "";
            NameValueCollection httpHeaders = CurrentUser.HttpHeaders;
            foreach(string header in httpHeaders.AllKeys) {
                request += Environment.NewLine + "   " + header + ": " + httpHeaders[header];
            }

            return Environment.NewLine + "Данные пользователя:" +
                Environment.NewLine + "   NICK: " + CurrentUser.User.Nick +
                Environment.NewLine + "   IP:   " + CurrentUser.Ip +
                Environment.NewLine + "   URL:  " + HttpContext.Current.Request.Url.ToString() +
                Environment.NewLine + "Данные реквеста:" + request;
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
