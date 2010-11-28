using System;
using System.Net.Mail;

using ITCommunity.DB;


namespace ITCommunity.Core {

    /// <summary>
    /// Класс отсылающий емейлы
    /// </summary>
    public static class EmailSender {

        public static bool NewPasswordEmail(User user, Recovery recovery) {
            try {
                var message = new MailMessage();
                message.To.Add(new MailAddress(user.Email));
                message.Subject = "Ykt IT Community - восстановление пароля";
                message.Body =
                    "Здравствуйте " + user.Nick + "!\n\n" +

                    "Вы запросили сброс пароля с сайта " + Config.SiteAddress + "," +
                    "сделать это можно перейдя по ссылке:\n\n" +
                    Config.SiteAddress + "/user/newpassword?guid=" + recovery.Guid.ToString() + "\n\n" +
                    "Ссылка активна в течении 3-4 дней." + "\n\n\n\n" +
                    "------------\n" +
                    "С уважением, робот находящийся в рабстве у держателей сайта" + Config.SiteAddress;

                var client = new SmtpClient(); // используются параметры из web.config

                if (client.Host.Contains("gmail.com")) { // Вахаха, простите меня
                    client.EnableSsl = true;
                }

                client.Send(message);

                Logger.Log.Warn("Успешно отправлено сообщение на email пользователя для смены пароля" + Logger.GetUserInfo());

                return true;
            } catch (Exception ex) {
                Logger.Log.Error("Невозможно отправить email для восстановления пароля, запрошена смена пароля для пользователя " + user.Nick + Logger.GetUserInfo(), ex);
            }

            return false;
        }
    }
}
