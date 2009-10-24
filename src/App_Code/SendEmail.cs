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
    /// ����� ���������� ������
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
                message.Subject = "Ykt IT Community - �������������� ������";
                message.Body = "���������� %username% ! \n\n" +
                               "�� ��������� ����� ������ � ����� " + Global.SiteAddress + ", ������� ��� ����� ������� �� ������:" + "\n" +
                               " " + Global.SiteAddress + "/recovery.aspx?id=" + guid + " \n\n" +
                               "������ ������� � ������� 3-4 ����." + "\n\n" + 
                               "______ \n" +
                               "� ���������, ����� ����������� � ������� � ���������� ����� " + Global.SiteAddress;
                SmtpClient client = new SmtpClient(); // ������������ ��������� �� web.config
                //client.EnableSsl = true;
                client.Send(message);
                Logger.Log.Info("������� �������� email ��� ����� ������ ��� ������������ " + user.Nick + " � ������ " + CurrentUser.Ip);
            } catch (Exception ex)
            {
                status = false;
                Logger.Log.Error("���������� ��������� email ��� �������������� ������, ��������� ����� ������ ��� ������������ " + CurrentUser.User.Nick + " � ������ " + CurrentUser.Ip, ex);
            }
            return status;
        }
    }
}
