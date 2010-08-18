using System;
using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class UserController : BaseController {

        public ActionResult Profile(string nick) {
            var userNick = nick ?? CurrentUser.User.Nick;
            var user = Users.Get(userNick);

            if (user.IsAnonymous) {
                return NotFound();
            }

            return View(user);
        }

        public ActionResult Posts(string nick, int? page) {
            var userNick = nick ?? CurrentUser.User.Nick;
            var user = Users.Get(userNick);

            if (user.IsAnonymous) {
                return NotFound();
            }

            var model = new UserPostsModel(user, page);

            return View(model);
        }

        public ActionResult Comments(string nick, int? page) {
            var userNick = nick ?? CurrentUser.User.Nick;
            var user = Users.Get(userNick);

            if (user.IsAnonymous) {
                return NotFound();
            }

            var model = new UserCommentsModel(user, page);

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult List(string role, int? page) {
            var model = new UserListModel(role, page);

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ChangeRole(string nick, string role) {
            if (nick == null) {
                return NotFound();
            }

            var user = Users.Get(nick);

            if (user.IsAnonymous) {
                return NotFound();
            }

            User.Roles changingRole;

            if (Enum.TryParse(role, true, out changingRole)) {
                user.Role = changingRole;
                Users.Update(user);
            }


            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Logout() {
            CurrentUser.Logout();

            return Redirect("/");
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel model, string returnUrl) {
            if (ModelState.IsValid) {
                if (CurrentUser.Login(model.UserNick, model.Password, model.RememberMe)) {
                    if (!String.IsNullOrEmpty(returnUrl)) {
                        return Redirect(returnUrl);
                    } else {
                        return Redirect("/");
                    }
                } else {
                    ModelState.AddModelError("LoginPassword", "Error");
                }
            }

            return View(model);
        }

        public ActionResult Register() {
            var model = new UserRegisterModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(UserRegisterModel model) {
            if (ModelState.IsValid) {
                var user = CurrentUser.Register(model.UserNick, model.Password, model.Email);

                CurrentUser.Login(model.UserNick, model.Password, false);

                return Redirect("/");
            }

            model.NewCaptcha();

            return View(model);
        }

        public ActionResult NewPassword() {
            var recovery = GetRecovery();

            if (recovery == null) {
                return View("ForgotPassword");
            }

            var model = new UserNewPasswordModel(recovery);

            return View(model);
        }

        [HttpPost]
        public ActionResult NewPassword(UserNewPasswordModel model) {
            var recovery = GetRecovery();

            if (recovery == null) {
                return View("ForgotPassword");
            }

            if (ModelState.IsValid) {
                recovery.User.Password = CurrentUser.HashPassword(model.Password, recovery.User.Nick);
                Users.Update(recovery.User);
                Recoveries.Delete(recovery.Guid);

                CurrentUser.Login(recovery.User.Nick, model.Password, false);

                return Redirect("/");
            }

            return View(model);
        }

        public ActionResult ForgotPassword() {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(UserNickModel model) {
            if (model == null) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                var user = Users.Get(model.UserNick);
                var recovery = Recoveries.Add(user.Id);

                if (EmailSender.NewPasswordEmail(user, recovery)) {
                    return View("ForgotPasswordSent", model);
                } else {
                    ModelState.AddModelError("Error", "Can't send e-mail");
                }
            }

            return View(model);
        }

        private Recovery GetRecovery() {
            var guid = Request.QueryString["guid"];

            if (guid != null) {
                return Recoveries.Get(guid);
            }

            return null;
        }
    }
}
