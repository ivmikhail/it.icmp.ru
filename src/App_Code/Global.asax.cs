using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using ITCommunity.IndexerLib;

namespace ITCommunity {
	/// <summary>
	/// Мега-глобал класс
	/// </summary>
	public class Global : System.Web.HttpApplication {

        private static string _connectionString = null;
		private TimerTask _timerTask;

		public Global() {
		}

		/// <summary>
		/// Адрес сайта, например: http://it.icmp.ru (без завершающего слеша).
		/// </summary>
		public static string SiteAddress {
			get {
                if(HttpContext.Current.Request.Url.Host=="localhost") {
                    String url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
                    String siteAddr = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    return url.Substring(0, url.IndexOf("/", siteAddr.Length + 1));
                } else {
                    return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority); 
                }
            }
		}

		/// <summary>
		/// Строка соединения с SQL сервером
		/// </summary>
		/// <returns></returns>
		public static string ConnectionString() {
			if (_connectionString == null) {
				if (ConfigurationManager.ConnectionStrings[Environment.MachineName] != null) {
					_connectionString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
				}
				else {
					_connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
				}
			}
			return _connectionString;
		}
		public void Application_Start(object sender, EventArgs e) {
			Logger.Log.Info("Application started ...");
			_timerTask = new TimerTask((double)3600 * 1000 * 12, RecoveryPass.PurgeOldRecoveryTasks);
            Indexer.Init(Global.ConnectionString(), Config.String("IndexerPath"));
		}

		public void Application_End(object sender, EventArgs e) {
			Logger.Log.Info("Application stopped ...");
		}

		public void Application_Error(object sender, EventArgs e) {
			Exception ex = Server.GetLastError().GetBaseException();
			HttpException exc = ex as HttpException;
			if (exc != null && exc.GetHttpCode() == 404) {
				//ignore 404 error
			}
			else {
				Logger.Log.Error("Произошла непредвиденная ошибка: пользователь - " + CurrentUser.User.Login + "(" + CurrentUser.Ip + "), запрошенный URL - " + Request.Url, ex);
			}
		}

		public void Application_AuthenticateRequest(Object src, EventArgs e) {
			//Note: Здесь нельзя получить доступ к сессии
			if (HttpContext.Current.Request.IsAuthenticated) {
				if (HttpContext.Current.User.Identity.AuthenticationType == "Forms") {
					FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
					string[] roles = id.Ticket.UserData.Split(','); // на самом деле у чела не может быть по 2 роли одновременно
					Context.User = new System.Security.Principal.GenericPrincipal(id, roles);
				}
			}
		}

		public void Application_PreRequestHandlerExecute(object sender, EventArgs e) {
			HttpApplication app = sender as HttpApplication;
			string acceptEncoding = app.Request.Headers["Accept-Encoding"];
			Stream prevUncompressedStream = app.Response.Filter;

			if (!(app.Context.CurrentHandler is System.Web.UI.Page) || app.Request["HTTP_X_MICROSOFTAJAX"] != null) {
				return;
			}

			if (acceptEncoding == null || acceptEncoding.Length == 0) {
				return;
			}

			acceptEncoding = acceptEncoding.ToLower();

			if (acceptEncoding.Contains("gzip")) {
				// gzip
				app.Response.Filter = new GZipStream(prevUncompressedStream, CompressionMode.Compress);
				app.Response.AppendHeader("Content-Encoding", "gzip");

			}
			else if (acceptEncoding.Contains("deflate")) {
				// deflate
				app.Response.Filter = new DeflateStream(prevUncompressedStream, CompressionMode.Compress);
				app.Response.AppendHeader("Content-Encoding", "deflate");
			}
		}
	}
}
