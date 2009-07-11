using System;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Specialized;
using ITCommunity;

namespace ITCommunity
{
    /// <summary>
    /// Текущий пользователь
    /// </summary>
    /// 
    public static class CurrentUser
    {
        /// <summary>
        /// Возвращаем обьект юзер из сессии/куки, если авторизован.
        /// </summary>
        public static User User
        {
            get
            {
                User currentUser = new User();
                if (isAuth)
                {
                    currentUser = (User)HttpContext.Current.Session["CurrentUser"];
                    if (currentUser == null)
                    {
                        currentUser = GetUserFromCookie();
                    }

                    if (currentUser.Role == User.Roles.Banned)
                    {
                        CurrentUser.LogOut();
                    }
                }
                return currentUser;
            }

        }

        /// <summary>
        /// Авторизация: запихиваем юзера в сессию
        /// </summary>
        /// <param name="login">Логин, он же nick</param>
        /// <param name="pass">Пароль</param>
        public static User LogIn(string login, string pass, bool remember)
        {
            User user = User.GetByLogin(login);
            string hashedPass = HashPass(pass, login);

            if (user.Id > 0 && user.Pass == hashedPass)
            {
                HttpContext.Current.Session.Add("CurrentUser", user);

                DateTime ticketExpiration = DateTime.Now;
                if (remember)
                {
                    ticketExpiration = DateTime.Now.AddYears(50);
                } else
                {
                    ticketExpiration = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout); // хмм
                }
                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(1, login, DateTime.Now, ticketExpiration, remember, user.Role.ToString());

                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(login, false);
                authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                authCookie.Expires = ticketExpiration;
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
            return user;
        }

        /// <summary>
        /// Шифруем пароль пользователя, в дальнейшем используем выходное значение функции
        /// в качестве пароля пользователя.
        /// 
        /// Таким образом гарантируется что истинный пароль знает только сам пользователь.
        /// </summary>
        /// <param name="pass">Истинный пароль</param>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        public static string HashPass(string pass, string login)
        {
            string preparedPass = login + pass;
            string hashedPass = FormsAuthentication.HashPasswordForStoringInConfigFile(preparedPass.ToUpper(), "SHA1");
            return hashedPass;
        }

        /// <summary>
        /// Выход
        /// </summary>
        public static void LogOut()
        {
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            HttpContext.Current.Session.Remove("CurrentUser");
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// IP текущего пользователя
        /// </summary>
        public static string Ip
        {
            get
            {
                NameValueCollection serverVars = HttpContext.Current.Request.ServerVariables;
                return serverVars["HTTP_X_FORWARDED_FOR"] ?? serverVars["REMOTE_ADDR"];
            }
        }

        /// <summary>
        /// Регистрируем нового пользователя
        /// </summary>
        /// <param name="login">login=nick</param>
        /// <param name="pass">пароль</param>
        /// <param name="email">электропочта</param>
        public static User Register(string login, string pass, string email)
        {
            User user  = new User();
            user.Nick  = login;
            user.Pass  = HashPass(pass, login);
            user.Email = email;
            user.Role  = User.Roles.Poster;

            return User.Add(user);
        }

        public static bool isAuth
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        static private User GetUserFromCookie()
        {

            User user = new User();
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(HttpContext.Current.User.Identity.Name, false);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            if (ticket.Expired)
            {
                LogOut();
            } else
            {
                user = User.GetByLogin(ticket.Name);
            }
            return user;
        }
    }
}