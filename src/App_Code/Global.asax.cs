using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;


using ITCommunity;

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

        /// <summary>
        /// Адрес сайта, например: http://it.icmp.ru (без завершающего слеша).
        /// </summary>
        public static string SiteAddress
        {
            get { return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority); }
        }
        private static String connectionString = null;
        /// <summary>
        /// Строка соединения с SQL сервером
        /// </summary>
        /// <returns></returns>
        public static string ConnectionString() {
            if (connectionString == null) {
                if (ConfigurationManager.ConnectionStrings[Environment.MachineName] != null) {
                    connectionString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
                } else {
                    connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                }
            }
            return connectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string ConfigStringParam(string param)
        {
            string val = "";
            try
            {
               val =  ConfigurationManager.AppSettings[param];
            } catch (Exception ex)
            {
                Logger.Log.Fatal("Ошибка при чтении конфигурации(web.config секция appSettings), параметр " + param + "", ex);
            }
            return val;
        }

        public static int ConfigNumParam(string param)
        {
            int val = -1;
            try
            {
                val = Convert.ToInt32(ConfigurationManager.AppSettings[param]);
            } catch (Exception ex)
            {
                Logger.Log.Fatal("Ошибка при чтении конфигурации(web.config секция appSettings), параметр " + param + "", ex);
            }
            return val;
        }
        public static double ConfigDoubleParam(string param)
        {
            double val = -1;
            try
            {
                val = Convert.ToDouble(ConfigurationManager.AppSettings[param]);
            } catch (Exception ex)
            {
                Logger.Log.Fatal("Ошибка при чтении конфигурации(web.config секция appSettings), параметр " + param + "", ex);
            }
            return val;
        }

        public void Application_Start(object sender, EventArgs e)
        {
            Logger.Log.Info("Application started ...");
        }

        public void Application_End(object sender, EventArgs e)
        {
            Logger.Log.Info("Application stopped ...");
        }

        public void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();

            bool notFoundError = false;
            try
            {
                notFoundError = (ex != null && ((HttpException)ex).GetHttpCode() == 404);
            } catch (InvalidCastException exc) //pzdc
            {
                exc = null;
                notFoundError = false;
            }
            if (!notFoundError) {
                Logger.Log.Error("Произошла непредвиденная ошибка, пользователь - " + CurrentUser.User.Nick + "(" + CurrentUser.Ip + ")", ex);
            }           
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
        
        public void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            string acceptEncoding = app.Request.Headers["Accept-Encoding"];
            Stream prevUncompressedStream = app.Response.Filter;

            if (!(app.Context.CurrentHandler is System.Web.UI.Page) ||
                app.Request["HTTP_X_MICROSOFTAJAX"] != null)
                return;

            if (acceptEncoding == null || acceptEncoding.Length == 0)
                return;

            acceptEncoding = acceptEncoding.ToLower();

            if (acceptEncoding.Contains("gzip"))
            {
                // gzip
                app.Response.Filter = new GZipStream(prevUncompressedStream,
                    CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "gzip");

            } else if (acceptEncoding.Contains("deflate"))
            {               
                // deflate
                app.Response.Filter = new DeflateStream(prevUncompressedStream,
                    CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "deflate");
            }
        }
    }
}