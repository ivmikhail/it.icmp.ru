using System;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Specialized;

/// <summary>
/// Текущий пользователь
/// </summary>
/// 
public static class CurrentUser
{
    /// <summary>
    /// Возвращаем обьект юзер из Сессии, если авторизован.
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
    /// Авторизация: запихиваем юзера в сессию
    /// </summary>
    /// <param name="login">Логин, он же nick</param>
    /// <param name="pass">Пароль</param>
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
    /// Выход
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
    /// Валидируем логин
    /// </summary>
    private static bool ValidateLogin(string login)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Регистрируем нового пользователя
    /// </summary>
    /// <param name="login">login=nick</param>
    /// <param name="pass">пароль</param>
    /// <param name="email">электропочта</param>
    public static User Register(string login, string pass, string email)
    {
        User user = new User();
        user.Nick = login;
        user.Pass = HashPass(pass);
        user.Email = email;

        return Users.Add(user);
    }

    /// <summary>
    /// Проверяем забанен ли текущий пользователь по IP
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
