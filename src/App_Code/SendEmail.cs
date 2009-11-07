using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using ITCommunity;

namespace ITCommunity
{

    /// <summary>
    /// Класс отсылающий емейлы
    /// </summary>
    public static class SendEmail
    {
        public static bool SendRecoveryEmail(User user, string guid)
        {
            bool status = true;
            try
            {

                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(user.Email));
                message.Subject = "Ykt IT Community - восстановление пароля";
                message.Body = "Здравствуй %username% ! \n\n" +
                               "Вы запросили сброс пароля с сайта " + Global.SiteAddress + ", сделать это можно перейдя по ссылке:" + "\n" +
                               " " + Global.SiteAddress + "/recovery.aspx?id=" + guid + " \n\n" +
                               "Ссылка активна в течении 3-4 дней." + "\n\n" + 
                               "______ \n" +
                               "С уважением, робот находящийся в рабстве у держателей сайта " + Global.SiteAddress;
                SmtpClient client = new SmtpClient(); // используются параметры из web.config
                //client.EnableSsl = true;
                client.Send(message);
                Logger.Log.Info("Успешно запрошен email для смены пароля для пользователя " + user.Nick + " с адреса " + CurrentUser.Ip);
            } catch (Exception ex)
            {
                status = false;
                Logger.Log.Error("Невозможно отправить email для восстановления пароля, запрошена смена пароля для пользователя " + CurrentUser.User.Nick + " с адреса " + CurrentUser.Ip, ex);
            }
            return status;
        }
    }
}
