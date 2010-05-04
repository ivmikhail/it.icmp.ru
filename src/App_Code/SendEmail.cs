using System;
using System.Net.Mail;

namespace ITCommunity {
	/// <summary>
	/// Класс отсылающий емейлы
	/// </summary>
	public static class SendEmail {

		public static bool SendRecoveryEmail(User user, string guid) {
			bool status = true;

			try {
				var message = new MailMessage();
				message.To.Add(new MailAddress(user.Email));
				message.Subject = "Ykt IT Community - восстановление пароля";
				message.Body = "Здравствуй %username% ! \n\n" +
							   "Вы запросили сброс пароля с сайта " + Global.SiteAddress + ", сделать это можно перейдя по ссылке:" + "\n" +
							   " " + Global.SiteAddress + "/recovery.aspx?id=" + guid + " \n\n" +
							   "Ссылка активна в течении 3-4 дней." + "\n\n" +
							   "______ \n" +
							   "С уважением, робот находящийся в рабстве у держателей сайта " + Global.SiteAddress;

				var client = new SmtpClient(); // используются параметры из web.config
				if (client.Host.Contains("gmail.com")) { // Вахаха, простите меня
					client.EnableSsl = true;
				}
				client.Send(message);

				Logger.Log.Info("Успешно запрошен email для смены пароля для пользователя " + user.Login + " с адреса " + CurrentUser.Ip);
			}
			catch (Exception ex) {
				status = false;
				Logger.Log.Error("Невозможно отправить email для восстановления пароля, запрошена смена пароля для пользователя " + CurrentUser.User.Login + " с адреса " + CurrentUser.Ip, ex);
			}

			return status;
		}
	}
}
