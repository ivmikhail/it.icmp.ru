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

            Logger.Log.Warn("Страница не найдена" + Logger.GetUserInfo());
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
            Logger.Log.Error("Попытка взлома" + Logger.GetUserInfo(), context.HttpContext.Error);
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
