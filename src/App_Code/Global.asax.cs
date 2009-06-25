using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ITCommunity;
using System.IO;

namespace ITCommunity
{
    /// <summary>
    /// ����-������ �����
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        public Global()
        {
        }

        /// <summary>
        /// ����� �����, ��������: http://it.icmp.ru (��� ������������ �����).
        /// </summary>
        public static string SiteAddress
        {
            get { return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string ConfigStringParam(string param)
        {
            try
            {
                return ConfigurationManager.AppSettings[param];
            } catch (Exception ex)
            {
                throw (new Exception("������ ��� ������ ������������(web.config ������ appSettings), �������� " + param + "", ex));
            }
        }

        public static int ConfigNumParam(string param)
        {
            try
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings[param]);
            } catch (Exception ex)
            {
                throw (new Exception("������ ��� ������ ������������(web.config ������ appSettings), �������� " + param + "", ex));
            }
        }

        public void Application_AuthenticateRequest(Object src, EventArgs e)
        {
            //Note: ����� ������ �������� ������ � ������
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity.AuthenticationType == "Forms")
                {
                    System.Web.Security.FormsIdentity id = (System.Web.Security.FormsIdentity)HttpContext.Current.User.Identity;
                    string[] roles = id.Ticket.UserData.Split(','); // �� ����� ���� � ���� �� ����� ���� �� 2 ���� ������������
                    Context.User = new System.Security.Principal.GenericPrincipal(id, roles);

                }
            }
        }
    }
}