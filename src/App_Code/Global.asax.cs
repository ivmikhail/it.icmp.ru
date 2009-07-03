using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.IO;
using System.IO.Compression;


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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string ConfigStringParam(string param)
        {
            try
            {
                return ConfigurationManager.AppSettings[param];
            } catch (Exception ex)
            {
                throw (new Exception("Ошибка при чтении конфигурации(web.config секция appSettings), параметр " + param + "", ex));
            }
        }

        public static int ConfigNumParam(string param)
        {
            try
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings[param]);
            } catch (Exception ex)
            {
                throw (new Exception("Ошибка при чтении конфигурации(web.config секция appSettings), параметр " + param + "", ex));
            }
        }
        public static double ConfigDoubleParam(string param)
        {
            try
            {
                return Convert.ToDouble(ConfigurationManager.AppSettings[param]);
            } catch (Exception ex)
            {
                throw (new Exception("Ошибка при чтении конфигурации(web.config секция appSettings), параметр " + param + "", ex));
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