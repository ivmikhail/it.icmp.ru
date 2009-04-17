using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// ����-������ �����
/// </summary>
public class Global : System.Web.HttpApplication
{
    public Global()
    {
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

    private static string _connectionString = "";
    public static string ConnectionString
    {
        get
        {
            if (_connectionString == "")
            {
                _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            return _connectionString;
        }
    }
    private static int _postsCount = -1;
    public static int PostsCount
    {
        get
        {
            if (_postsCount == -1)
            {
                _postsCount = Convert.ToInt32(ConfigurationManager.AppSettings["PostsCount"].ToString()); ;
            }
            return _postsCount;
        }
    }
}
