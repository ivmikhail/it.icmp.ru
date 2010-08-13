using System;
using System.Net.Mail;
using System.Web;
using ITCommunity.Db;
using ITCommunity.Core;

namespace ITCommunity {
	/// <summary>
	/// Класс отсылающий емейлы
	/// </summary>
	public static class SendEmail {

        /// <summary>
        /// Адрес сайта, например: http://it.icmp.ru (без завершающего слеша).
        /// </summary>
        private static string SiteAddress
        {
            get
            {
                if (HttpContext.Current.Request.Url.Host == "localhost")
                {
                    String url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
                    String siteAddr = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    return url.Substring(0, url.IndexOf("/", siteAddr.Length + 1));
                }
                else
                {
                    return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                }
            }
        }

		public static bool SendRecoveryEmail(User user, string guid) {
			bool status = true;

			try {
				var message = new MailMessage();
				message.To.Add(new MailAddress(user.Email));
				message.Subject = "Ykt IT Community - восстановление пароля";
				message.Body = "Здравствуй %username% ! \n\n" +
							   "Вы запросили сброс пароля с сайта " + SiteAddress + ", сделать это можно перейдя по ссылке:" + "\n" +
							   " " + SiteAddress + "/newpassword?guid=" + guid + " \n\n" +
							   "Ссылка активна в течении 3-4 дней." + "\n\n" +
							   "______ \n" +
							   "С уважением, робот находящийся в рабстве у держателей сайта " + SiteAddress;
                
				var client = new SmtpClient(); // используются параметры из web.config
				if (client.Host.Contains("gmail.com")) { // Вахаха, простите меня
					client.EnableSsl = true;
				}
				client.Send(message);

				Logger.Log.Info("Успешно запрошен email для смены пароля для пользователя " + user.Nick + " с адреса " + CurrentUser.Ip);
			}
			catch (Exception ex) {
				status = false;
                Logger.Log.Error("Невозможно отправить email для восстановления пароля, запрошена смена пароля для пользователя " + CurrentUser.User.Nick + " с адреса " + CurrentUser.Ip, ex);
			}

			return status;
		}
	}
}
