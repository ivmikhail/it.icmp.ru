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

        public static string Get(string param) {
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
    }
}
