using System;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Specialized;

/// <summary>
/// ������� ������������
/// </summary>
/// 
public static class CurrentUser
{
    /// <summary>
    /// ���������� ������ ���� �� ������, ���� �����������.
    /// </summary>
    public static User User
    {
        get 
        {
            User dbUser;
            if (isAuth)
            {
                dbUser = (User)HttpContext.Current.Session["ITCurrentUser"];
            } else
            {
                dbUser = new User();
            }
            return dbUser;
        }

    }

    /// <summary>
    /// �����������: ���������� ����� � ������
    /// </summary>
    /// <param name="login">�����, �� �� nick</param>
    /// <param name="pass">������</param>
    public static User LogIn(string login, string pass)
    {
        User candidate = Users.GetUserByLogin(login);

        string hashedPass = HashPass(pass);        

        if(candidate.Id > 0 && candidate.Pass == hashedPass)
        {
            HttpContext.Current.Session.Add("ITCurrentUser", candidate);
        }

        return candidate;
    }

    private static string HashPass(string pass)
    {
        string preparedPass = Global.MagicWord.Substring(0, 2) + pass + Global.MagicWord.Substring(2);
        string hashedPass = FormsAuthentication.HashPasswordForStoringInConfigFile(preparedPass, "MD5");
        return hashedPass;
    }

    /// <summary>
    /// �����
    /// </summary>
    public static void LogOut()
    {
        HttpContext.Current.Session.Remove("ITCurrentUser");
        FormsAuthentication.SignOut();
    }

    public static string Ip
    {
        get
        {
            NameValueCollection serverVars = HttpContext.Current.Request.ServerVariables;
            return serverVars["HTTP_X_FORWARDED_FOR"] ?? serverVars["REMOTE_ADDR"];
        }
    }

    /// <summary>
    /// ���������� �����
    /// </summary>
    private static bool ValidateLogin(string login)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// ������������ ������ ������������
    /// </summary>
    /// <param name="login">login=nick</param>
    /// <param name="pass">������</param>
    /// <param name="email">������������</param>
    public static User Register(string login, string pass, string email)
    {
        User user = new User();
        user.Nick = login;
        user.Pass = HashPass(pass);
        user.Email = email;

        return Users.Add(user);
    }

    /// <summary>
    /// ��������� ������� �� ������� ������������ �� IP
    /// </summary>
    public static bool IsBanned()
    {
        throw new System.NotImplementedException();
    }

    public static bool isAuth
    {
        get
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}
