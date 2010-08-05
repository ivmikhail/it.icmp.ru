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

namespace ITCommunity.Controllers {

    [HandleError]
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
                    ModelState.AddModelError("", "Вы неправильно ввели ник или пароль, попробуйте еще");
                }
            }

            return View(model);
        }

        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model, int? captchaanswer, int? captchaquestion) {
            if (ModelState.IsValid && Captcha.IsRightAnswer(captchaanswer, captchaquestion)) {

                var user = CurrentUser.Register(model.UserNick, model.Password, model.Email);

                if (user.Id > 0) {
                    CurrentUser.Login(model.UserNick, model.Password, false);
                    return Redirect("/");
                }
                else {
                    ModelState.AddModelError("", "Can't create new user");
                }
            }

            return View(model);
        }

        public ActionResult Recover() {
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
        public ActionResult Recover(NewPasswordModel model) {
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

            return Redirect("/");
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
        public ActionResult Send(NickModel model) {
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
                    ModelState.AddModelError("", "The user name not exist");
                }
            }
            return View(model);
        }

    }
}
