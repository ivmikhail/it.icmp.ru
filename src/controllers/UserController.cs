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
using ITCommunity.Db;
using ITCommunity.Db.Tables;
using ITCommunity.Models;
using ITCommunity.Models.User;

namespace ITCommunity.Controllers {

    public class UserController : BaseController {

        public ActionResult Logout() {
            CurrentUser.Logout();

            return Redirect("/");
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl) {
            if (ModelState.IsValid) {
                if (CurrentUser.Login(model.UserNick, model.Password, model.RememberMe)) {
                    if (!String.IsNullOrEmpty(returnUrl)) {
                        return Redirect(returnUrl);
                    }
                    else {
                        return Redirect("/");
                    }
                }
                else {
                    ModelState.AddModelError("LoginPassword", "Error");
                }
            }

            return View(model);
        }

        public ActionResult Register() {
            var model = new RegisterModel();
            model.NewCaptcha();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model) {
            if (ModelState.IsValid) {

                var user = CurrentUser.Register(model.UserNick, model.Password, model.Email);

                if (user.Id > 0) {
                    CurrentUser.Login(model.UserNick, model.Password, false);
                    return Redirect("/");
                }
                else {
                    ModelState.AddModelError("", "Can't create new user");
                }
            }

            model.NewCaptcha();

            return View(model);
        }

        public ActionResult NewPassword() {
            var recovery = GetRecovery();

            if (recovery != null) {
                var model = new NewPasswordModel();
                var user = Users.Get(recovery.UserId);
                model.UserNick = user.Nick;
                return View(model);
            }

            return Redirect("/");
        }

        [HttpPost]
        public ActionResult NewPassword(NewPasswordModel model) {
            var recovery = GetRecovery();

            if (recovery != null) {

                if (ModelState.IsValid) {

                    var user = Users.Get(recovery.UserId);
                    user.Password = CurrentUser.HashPass(model.Password.Trim(), user.Nick);
                    Users.Update(user);

                    var guid = Request.QueryString["id"];
                    Recoveries.Delete(guid);

                    return RedirectToAction("Login", "Account");
                }

                return View(model);
            }

            return Redirect("/");
        }

        private Recovery GetRecovery() {
            var guid = Request.QueryString["guid"];

            if (guid != null) {
                return Recoveries.Get(guid);
            }

            return null;
        }

        public ActionResult ForgotPassword() {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(NickModel model) {
            if (ModelState.IsValid) {
                User user = Users.Get(model.UserNick);
                if (user != null && user.Id > 0) {
                    var recovery = Recoveries.Add(user.Id);
                    bool sended = SendEmail.SendRecoveryEmail(user, recovery.Guid.ToString());
                    if (sended) {
                        Redirect("/");
                    }
                    else {
                        ModelState.AddModelError("", "Письмо не отправлено. Причина записана в логах (увы вам она не видна). Попробуйте еще раз. Если все равно не работает, обратитесь к администрации");
                    }
                }
                else {
                    ModelState.AddModelError("", "Пользователя с таким ником нет");
                }
            }
            return View(model);
        }

    }
}
