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
/// Мега-глобал класс
/// </summary>
public class Global : System.Web.HttpApplication
{
    public Global()
    {
    }

    public void Application_AuthenticateRequest(Object src, EventArgs e)
    {
        if (!(HttpContext.Current.User == null))
        {
            if (HttpContext.Current.User.Identity.AuthenticationType == "Forms")
            {
                System.Web.Security.FormsIdentity id;
                id = (System.Web.Security.FormsIdentity)HttpContext.Current.User.Identity;
                string[] roles = id.Ticket.UserData.Split(','); // на самом деле у чела не может быть по 2 роли одновременно
                HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
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

    private static string _magicWord = "";
    public static string MagicWord
    {
        get
        {
            if (_magicWord == "")
            {
                string word = ConfigurationManager.AppSettings["MagicWord"];
                _magicWord = word.Length < 3 ? "do you like porno?" : ConfigurationManager.AppSettings["MagicWord"];      
            }
            return _magicWord;
        }
    }

}
