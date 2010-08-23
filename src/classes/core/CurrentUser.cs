using System;
using System.Web;
using System.Web.Security;

using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Core {

    /// <summary>
	/// Текущий пользователь
	/// </summary>
	public static class CurrentUser {

        public const string SESSION_NAME = "CurrentUser";

		public static bool IsAuth {
			get {
				return HttpContext.Current.User.Identity.IsAuthenticated;
			}
		}

        public static bool IsAdmin {
            get {
                return IsAuth && User.Role == User.Roles.Admin;
            }
        }

		/// <summary>
		/// IP текущего пользователя
		/// </summary>
		public static string Ip {
			get {
				var serverVars = HttpContext.Current.Request.ServerVariables;
				return serverVars["HTTP_X_FORWARDED_FOR"] ?? serverVars["REMOTE_ADDR"];
			}
		}

		/// <summary>
		/// Возвращаем обьект юзер из сессии/куки, если авторизован.
		/// </summary>
		public static User User {
			get {
				var currentUser = User.Anonymous;

				if (IsAuth) {
					if (HttpContext.Current.Session != null) {
                        currentUser = (User)HttpContext.Current.Session[SESSION_NAME];
					}

					if (currentUser == null) {
						currentUser = GetUserFromCookie();
                        HttpContext.Current.Session[SESSION_NAME] = currentUser;
					}

					if (currentUser.Role == User.Roles.Banned) {
						CurrentUser.Logout();
					}
				}

				return currentUser;
			}
		}

		/// <summary>
		/// Шифруем пароль пользователя, в дальнейшем используем выходное значение функции
		/// в качестве пароля пользователя.
		/// 
		/// Таким образом гарантируется что истинный пароль знает только сам пользователь.
		/// </summary>
		/// <param name="password">Истинный пароль</param>
        /// <param name="nick">Ник пользователя</param>
		/// <returns>Хэшированный пароль</returns>
        public static string HashPassword(string password, string nick) {
            var preparedPass = nick.Trim().ToUpper() + password;
			var hashedPass = FormsAuthentication.HashPasswordForStoringInConfigFile(preparedPass, "SHA1");
			return hashedPass;
		}

		/// <summary>
		/// Авторизация: запихиваем юзера в сессию и куки
		/// </summary>
        /// <param name="nick">Ник пользователя</param>
        /// <param name="password">Пароль</param>
        /// <param name="remember">Запоминать в куки на долгое время или нет</param>
        /// <returns>Успешно авторизовался или нет</returns>
        public static bool Login(string nick, string password, bool remember) {
            var user = Users.Get(nick);
            var hashedPass = HashPassword(password, nick);

			if (user.IsAnonymous == false && user.Password == hashedPass) {
                HttpContext.Current.Session[SESSION_NAME] = user;

				var ticketExpiration = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout);;
				if (remember) {
					ticketExpiration = DateTime.Now.AddYears(50);
				}

                var newTicket = new FormsAuthenticationTicket(
                    1, 
                    nick, 
                    DateTime.Now, 
                    ticketExpiration, 
                    true, 
                    user.Role.ToString(), 
                    FormsAuthentication.FormsCookiePath
                );

                var encryptedTicket = FormsAuthentication.Encrypt(newTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

				authCookie.Expires = ticketExpiration;
				HttpContext.Current.Response.Cookies.Add(authCookie);

				return true;
			}
			return false;
		}

		/// <summary>
		/// Выход
		/// </summary>
		public static void Logout() {
			HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
			HttpContext.Current.Session.Abandon();
			FormsAuthentication.SignOut();
		}

		/// <summary>
		/// Регистрируем нового пользователя
		/// </summary>
        /// <param name="nick">Ник пользователя</param>
        /// <param name="password">Пароль</param>
		/// <param name="email">Электропочта</param>
        /// <returns>Возвращает созданного пользователя</returns>
        public static User Register(string nick, string password, string email) {
			var user = new User();

            user.Nick = nick;
            user.Password = HashPassword(password, nick);
			user.Email = email;
			user.Role = User.Roles.Poster;

			return Users.Add(user);
		}

		private static User GetUserFromCookie() {
			var user = new User();

			var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
			if (authCookie != null) {
				var ticket = FormsAuthentication.Decrypt(authCookie.Value);
				if (ticket.Expired) {
					Logout();
				} else {
					user = Users.Get(ticket.Name);
				}
			}

			return user;
		}
	}
}
