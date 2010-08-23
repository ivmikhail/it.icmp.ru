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
                message.Subject = "Ykt IT Community - задание нового пароля";
                message.Body =
                    "Здравствуйте " + user.Nick + "!\n\n" +

                    "Вы запросили сброс пароля с сайта " + Config.SiteAddress + "\n," +
                    "сделать это можно перейдя по ссылке:\n" +
                    Config.SiteAddress + "/newpassword?guid=" + recovery.Guid.ToString() + "\n" +
                    "Ссылка активна в течении 3-4 дней." +
                    "______" +
                    "С уважением, робот находящийся в рабстве у держателей сайта" + Config.SiteAddress;

                var client = new SmtpClient(); // используются параметры из web.config

                if (client.Host.Contains("gmail.com")) { // Вахаха, простите меня
                    client.EnableSsl = true;
                }

                client.Send(message);

                Logger.Log.Info("Успешно запрошен email для смены пароля для пользователя " + user.Nick + " с адреса " + CurrentUser.Ip);

                return true;
            } catch (Exception ex) {
                Logger.Log.Error("Невозможно отправить email для восстановления пароля, запрошена смена пароля для пользователя " + CurrentUser.User.Nick + " с адреса " + CurrentUser.Ip, ex);
            }

            return false;
        }
    }
}
