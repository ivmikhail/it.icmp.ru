using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using ITCommunity.Core;
using ITCommunity.DB.Tables;
using ITCommunity.IndexerLib;


namespace ITCommunity {

    public class Global : HttpApplication {

        private List<TimerTask> _tasks;

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Rfc",
                "rfc",
                new { controller = "Rfc", action = "search" }
            );

            routes.MapRoute(
                "WinUpdates",
                "winupdates",
                new { controller = "WinUpdates", action = "search" }
            );
            routes.MapRoute(
                "WinUpdatesDownload",
                "winupdates/file",
                new { controller = "WinUpdates", action = "file" }
            );

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
                new { controller = "User", action = "List", role = "All" }
            );

            routes.MapRoute(
                "User",
                "user/{action}/{nick}",
                new { controller = "User", action = "NoFound" }
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
                "PostPoll",
                "post/poll{id}/{isThumb}",
                new { controller = "Post", action = "PollChart", id = 0, isThumb = "false" }
            );

            routes.MapRoute(
                "OldPost",
                "news.aspx",
                new { controller = "Post", action = "View" }
            );

            routes.MapRoute(
                "Rss",
                "rss",
                new { controller = "Rss", action = "Feed" }
            );
            
            routes.MapRoute(
                "OldRss",
                "rss.aspx",
                new { controller = "Rss", action = "Feed" }
            );

            routes.MapRoute(
                "BrowserEditDesc",
                "editdesc",
                new { controller = "Browser", action = "EditDesc" }
            );

            routes.MapRoute(
                "Browser",
                "browser/{*link}",
                new { controller = "Browser", action = "Files", link = BrowseItem.DefaultDir }
            );

            routes.MapRoute(
                "Picture",
                "picture/upload/{*basePath}",
                new { controller = "Picture", action = "Upload", basePath = "pictures" }
            );

            routes.MapRoute(
                "Forbidden",
                "forbidden",
                new { controller = "Base", action = "Forbidden" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { id = 0 }
            );

            routes.MapRoute(
                "Notfound",
                "{*anyurl}",
                new { controller = "Base", action = "Notfound" }
            );
        }



        protected void Application_Start(object sender, EventArgs e) {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            Logger.Log.Info("Application started ...");

            _tasks = new List<TimerTask>();

            var recoveriesPurgeTimer = Config.GetDouble("RecoveriesPurgeTimerMinutes");
            _tasks.Add(new TimerTask(recoveriesPurgeTimer, Recoveries.Purge));
        
            Indexer.Init(Config.ConnectionString, Config.IndexerPath);
        }

        protected void Application_End(object sender, EventArgs e) {
            //            Indexer.GetInstance().Close();
            Logger.Log.Info("Application stopped ...");
        }

        protected void Application_Error(object sender, EventArgs e) {
            var ex = Server.GetLastError().GetBaseException();
            var httpEx = ex as HttpException;

            if (httpEx != null && httpEx.GetHttpCode() == 404) {
                //ignore 404 error
            } else {
                Logger.Log.Error("Произошла непредвиденная ошибка" + Logger.GetUserInfo(), ex);
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