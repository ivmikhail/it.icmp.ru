using System.Web.Mvc;

using ITCommunity.Core;


namespace ITCommunity.Controllers {

    #region NotFoundResult

    public class NotFoundResult : ViewResult {

        public NotFoundResult()
            : base() {
            this.ViewName = "NotFound";
        }

        public override void ExecuteResult(ControllerContext context) {
            base.ExecuteResult(context);
            context.HttpContext.Response.StatusCode = 404;

            var url = context.HttpContext.Request.Url.ToString();
            
            if (context.HttpContext.Request.Params["aspxerrorpath"] != null) {
                url = Config.SiteAddress + context.HttpContext.Request.Params["aspxerrorpath"];
            }

            Logger.Log.Info("Страница не найдена: пользователь - " + CurrentUser.User.Nick + "(" + CurrentUser.Ip + "), запрошенный URL - " + url);
        }
    }

    #endregion

    #region ForbiddenResult

    public class ForbiddenResult : ViewResult {

        public ForbiddenResult()
            : base() {
            this.ViewName = "Forbidden";
        }

        public override void ExecuteResult(ControllerContext context) {
            base.ExecuteResult(context);
            context.HttpContext.Response.StatusCode = 403;
            Logger.Log.Info("Попытка взлома: пользователь - " + CurrentUser.User.Nick + "(" + CurrentUser.Ip + "), запрошенный URL - " + context.HttpContext.Request.Url);
        }
    }

    #endregion

    [ValidateInput(false)]
    public class BaseController : Controller {

        public NotFoundResult NotFound() {
            return new NotFoundResult();
        }

        public ForbiddenResult Forbidden() {
            return new ForbiddenResult();
        }

        /// <summary>
        /// Происходит редирект в реферрер
        /// </summary>
        /// <returns></returns>
        public RedirectResult Redirect() {
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
