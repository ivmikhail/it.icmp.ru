using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ITCommunity.Core;
using ITCommunity.Db.Tables;

namespace ITCommunity {

    public class Global : HttpApplication {

        private static string _connectionString;
        private List<TimerTask> _tasks;

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "IndexPage",
                "",
                new { controller = "Posts", action = "Index" }
            );

            routes.MapRoute(
                "PopularPosts",
                "posts/popular/{period}",
                new { controller = "Posts", action = "popular", period = "All" }
            );

            routes.MapRoute(
                "DiscussiblePosts",
                "posts/discussible/{period}",
                new { controller = "Posts", action = "discussible", period = "All" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "BaseController", action = "NotFound", id = 0 }
            );

        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            Logger.Log.Info("Application started ...");

            _tasks = new List<TimerTask>();
            var recoveriesPurgeTimer = Config.GetDouble("RecoveriesPurgeTimer");
            //_tasks.Add(new TimerTask(recoveriesPurgeTimer, Recoveries.Purge));
        }

        /// <summary>
        /// Строка соединения с SQL сервером
        /// </summary>
        public static string GetConnectionString() {
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
    }
}