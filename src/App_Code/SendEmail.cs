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
    ///  ласс отсылающий емейлы
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
                message.Subject = "Ykt IT Community - восстановление парол€";
                message.Body = "«дравствуй %username% ! \n\n" +
                               "¬ы запросили сброс парол€ с сайта " + Global.SiteAddress + ", сделать это можно перейд€ по ссылке:" + "\n" +
                               " " + Global.SiteAddress + "/recovery.aspx?id=" + guid + " \n\n" +
                               "—сылка активна в течении 3-4 дней." + "\n\n" + 
                               "______ \n" +
                               "— уважением, робот наход€щийс€ в рабстве у держателей сайта " + Global.SiteAddress;
                SmtpClient client = new SmtpClient(); // используютс€ параметры из web.config
                //client.EnableSsl = true;
                client.Send(message);
                Logger.Log.Info("”спешно запрошен email дл€ смены парол€ дл€ пользовател€ " + user.Nick + " с адреса " + CurrentUser.Ip);
            } catch (Exception ex)
            {
                status = false;
                Logger.Log.Error("Ќевозможно отправить email дл€ восстановлени€ парол€, запрошена смена парол€ дл€ пользовател€ " + CurrentUser.User.Nick + " с адреса " + CurrentUser.Ip, ex);
            }
            return status;
        }
    }
}
