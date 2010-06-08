using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ITCommunity.Core;
using ITCommunity.Db.Tables;

namespace ITCommunity {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class Global : System.Web.HttpApplication {

        private static string _connectionString = null;
        private List<TimerTask> _tasks;

        public static System.Web.Caching.Cache RuntimeCache() {
            return HttpRuntime.Cache;
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Post", action = "Index", id = "0" }  // Parameter defaults
            );

        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            Logger.Log.Info("Application started ...");

            _tasks = new List<TimerTask>();
            var recoveriesPurgeTimer = Config.GetDouble("RecoveriesPurgeTimer");
            _tasks.Add(new TimerTask(recoveriesPurgeTimer, Recoveries.Purge));
        }

        /// <summary>
        /// Строка соединения с SQL сервером
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString() {
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
}