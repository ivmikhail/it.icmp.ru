using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using ITCommunity.Core;
using ITCommunity.Db.Tables;


namespace ITCommunity {

    public class Global : HttpApplication {

        private List<TimerTask> _tasks;

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "IndexPage",
                "",
                new { controller = "Post", action = "List" }
            );

            routes.MapRoute(
                "PostPopulars",
                "post/popularlist/{period}",
                new { controller = "Post", action = "PopularList", period = "All" }
            );

            routes.MapRoute(
                "PostDiscussibles",
                "post/discussiblelist/{period}",
                new { controller = "Post", action = "DiscussibleList", period = "All" }
            );

            routes.MapRoute(
                "UserList",
                "user/list/{role}",
                new { controller = "User", action = "List", role = "all" }
            );

            routes.MapRoute(
                "User",
                "user/{action}/{nick}",
                new { controller = "User", action = "Profile" }
            );

            routes.MapRoute(
                "MessageSend",
                "message/send/{receiver}",
                new { controller = "Message", action = "Send" }
            );

            routes.MapRoute(
                "MessageDeleteAll",
                "message/deleteall/{messages}",
                new { controller = "Message", action = "DeleteAll" }
            );

            routes.MapRoute(
                "Admin",
                "admin",
                new { controller = "Header", action = "List" }
            );

            routes.MapRoute(
                "Post",
                "post/view/{id}",
                new { controller = "Post", action = "View", id = 0 }
            );

            routes.MapRoute(
                "OldPost",
                "news.aspx",
                new { controller = "Post", action = "View" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "BaseController", action = "NotFound", id = 0 }
            );
        }

        protected void Application_Start(object sender, EventArgs e) {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            Logger.Log.Info("Application started ...");

            _tasks = new List<TimerTask>();

            var recoveriesPurgeTimer = Config.GetDouble("RecoveriesPurgeTimerMinutes");
            _tasks.Add(new TimerTask(recoveriesPurgeTimer, Recoveries.Purge));
        }

        protected void Application_End(object sender, EventArgs e) {
            //            Indexer.GetInstance().Close();
            Logger.Log.Info("Application stopped ...");
        }

        protected void Application_Error(object sender, EventArgs e) {
            var ex = Server.GetLastError().GetBaseException() as HttpException;

            if (ex != null && ex.GetHttpCode() == 404) {
                //ignore 404 error
            } else {
                Logger.Log.Error("Произошла непредвиденная ошибка: пользователь - " + CurrentUser.User.Nick + "(" + CurrentUser.Ip + "), запрошенный URL - " + Request.Url, ex);
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {
            //Note: Здесь нельзя получить доступ к сессии
            if (HttpContext.Current.Request.IsAuthenticated) {
                if (HttpContext.Current.User.Identity.AuthenticationType == "Forms") {
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    string[] roles = id.Ticket.UserData.Split(','); // на самом деле у чела не может быть по 2 роли одновременно
                    Context.User = new System.Security.Principal.GenericPrincipal(id, roles);
                }
            }
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e) {
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

            } else if (acceptEncoding.Contains("deflate")) {
                // deflate
                app.Response.Filter = new DeflateStream(prevUncompressedStream, CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "deflate");
            }
        }
    }
}