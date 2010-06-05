using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ITCommunity.Core;
using ITCommunity.Db.Models;
using ITCommunity.Db.Tables;
using ITCommunity.Models;

namespace ITCommunity.Controllers {

    [HandleError]
    public class AccountController : Controller {

        [HttpGet]
        public ActionResult Recovery() {
            var recovery = GetRecovery();

            if (recovery != null) {
                var model = new RecoveryModel();
                var user = Users.Get(recovery.UserId);
                model.UserNick = user.Nick;
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Recovery(RecoveryModel model) {
            var recovery = GetRecovery();

            if (recovery != null) {

                if (ModelState.IsValid) {

                    var user = Users.Get(recovery.UserId);
                    user.Password = CurrentUser.HashPass(model.NewPassword.Trim(), user.Nick);
                    Users.Update(user);

                    var guid = Request.QueryString["id"];
                    Recoveries.Delete(guid);

                    return RedirectToAction("Login", "Account");
                }

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        private Recovery GetRecovery() {
            var guid = Request.QueryString["guid"];

            if (guid != null) {
                return Recoveries.Get(guid);
            }

            return null;
        }

        public ActionResult Send() {
            return View();
        }

        [HttpPost]
        public ActionResult Send(RecoveryModel model) {
            if (ModelState.IsValid) {
                User user = Users.Get(model.UserNick);
                if (user != null && user.Id > 0) {
                    var recovery = Recoveries.Add(user.Id);
                    bool sended = SendEmail.SendRecoveryEmail(user, recovery.Guid.ToString());
                    if (sended) {
                        RedirectToAction("Index", "Home");
                    } else {
                        ModelState.AddModelError("", "Письмо не отправлено. Причина записана в логах (увы вам она не видна). Попробуйте еще раз. Если все равно не работает, обратитесь к администрации");
                    }
                } else {
                    ModelState.AddModelError("", "The user name not exist");
                }
            }
            return View(model);
        }

        public ActionResult Logout() {
            CurrentUser.Logout();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult Login(LogOnModel model, string returnUrl) {
            if (ModelState.IsValid) {
                if (CurrentUser.Login(model.UserNick, model.Password, model.RememberMe)) {
                    if (!String.IsNullOrEmpty(returnUrl)) {
                        return Redirect(returnUrl);
                    } else {
                        return RedirectToAction("Index", "Home");
                    }
                } else {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model) {
            if (ModelState.IsValid) {
                // Attempt to register the user

                var user = CurrentUser.Register(model.UserNick, model.Password, model.Email);

                if (user.Id > 0) {
                    CurrentUser.Login(model.UserNick, model.Password, false);
                    return RedirectToAction("Index", "Home");
                } else {
                    ModelState.AddModelError("", "Can't create new user");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus) {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus) {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

    }
}
