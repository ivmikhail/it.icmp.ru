using System.Web.Mvc;


namespace ITCommunity.Core {

    #region NotFoundResult

    public class NotFoundResult : ViewResult {

        public NotFoundResult()
            : base() {
            this.ViewName = "NotFound";
        }

        public override void ExecuteResult(ControllerContext context) {
            base.ExecuteResult(context);
            context.HttpContext.Response.StatusCode = 404;
        }
    }

    #endregion

    #region AccessDeniedResult

    public class AccessDeniedResult : ViewResult {

        public AccessDeniedResult()
            : base() {
            this.ViewName = "AccessDenied";
        }

        public override void ExecuteResult(ControllerContext context) {
            base.ExecuteResult(context);
            context.HttpContext.Response.StatusCode = 401;
        }
    }

    #endregion

    public class BaseController : Controller {

        public NotFoundResult NotFound() {
            return new NotFoundResult();
        }

        public AccessDeniedResult AccessDenied() {
            return new AccessDeniedResult();
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
