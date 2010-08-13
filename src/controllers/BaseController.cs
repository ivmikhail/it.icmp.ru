using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITCommunity.Core;

namespace ITCommunity.Controllers {

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

    public class AccessDeniedResult : ViewResult {

        public AccessDeniedResult()
            : base() {
            this.ViewName = "AccessDenied";
        }

        public override void ExecuteResult(ControllerContext context) {
            base.ExecuteResult(context);
            context.HttpContext.Response.StatusCode = 403;
        }
    }

    public class BaseController : Controller {

        public NotFoundResult NotFound() {
            return new NotFoundResult();
        }

        public AccessDeniedResult AccessDenied() {
            return new AccessDeniedResult();
        }
    }
}
