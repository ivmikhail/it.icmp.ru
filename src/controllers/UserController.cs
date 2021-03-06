﻿using System;
using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Models;
using System.Collections.Generic;
using System.Collections.Specialized;


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

        [Authorize]
        public ActionResult Edit() {
            var model = new UserEditModel();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(UserEditModel model) {
            if (ModelState.IsValid) {
                var user = model.ToUser();

                Users.Update(user);

                return View("Edited");
            }

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


            return Redirect();
        }

        public ActionResult Logout() {
            CurrentUser.Logout();

            return Redirect("/");
        }

        public ActionResult Login() {
            if (CurrentUser.IsAuth) {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel model, string returnUrl) {
            if (CurrentUser.IsAuth) {
                return Redirect("/");
            }
            if (ModelState.IsValid) {
                if (CurrentUser.Login(model.UserNick, model.Password, model.RememberMe)) {
                    if (!String.IsNullOrEmpty(returnUrl)) {
                        return Redirect(returnUrl);
                    } else {
                        return Redirect("/");
                    }
                } else {
                    ModelState.AddModelError("", "Вы неправильно ввели ник или пароль, попробуйте еще");
                }
            }

            return View(model);
        }

        public ActionResult Register() {
            if (CurrentUser.IsAuth) {
                return Redirect("/");
            }
            var model = new UserRegisterModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(UserRegisterModel model) {
            if (CurrentUser.IsAuth) {
                return Redirect("/");
            }
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
            if (CurrentUser.IsAuth) {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(UserNickModel model) {
            if (CurrentUser.IsAuth) {
                return Redirect("/");
            }
            if (model == null) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                var user = Users.Get(model.UserNick);
                var recovery = Recoveries.Add(user.Id);

                if (EmailSender.NewPasswordEmail(user, recovery)) {
                    return View("ForgotPasswordSent", model);
                } else {
                    ModelState.AddModelError("", "Письмо не отправлено. Попробуйте еще раз. Если все равно не работает, обратитесь к администрации");
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

        public ActionResult Whoami() {   
            ViewData["httpHeaders"] = CurrentUser.HttpHeaders;
            return View("Whoami");
        }
    }
}
