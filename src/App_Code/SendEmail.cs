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
    /// Summary description for SendEmail
    /// </summary>
    public static class SendEmail
    {
        public static bool SendRecoveryEmail(string email, string guid)
        {
            bool status = true;
            try
            {

                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(email));
                message.Subject = "Ykt IT Community - восстановление парол€";
                message.Body = "«дравствуй %username% ! \n\n" +
                               "¬ы запросили сброс парол€ с сайта http://it.icmp.ru, сделать это можно перейд€ по ссылке:" + "\n" +
                               " http://it.icmp.ru/recovery.aspx?id=" + guid + " \n\n" +
                               "—сылка активна в течении 3-4 дней." + "\n\n" + 
                               "______ \n" +
                               "— уважением, робот наход€щийс€ в рабстве у держателей сайта http://it.icmp.ru";
                SmtpClient client = new SmtpClient(); // используютс€ параметры из web.config
                client.EnableSsl = true;
                client.Send(message);
            } catch (Exception ex)
            {
                string bla = ex.Message;
                status = false;
            }
            return status;
        }
    }
}
