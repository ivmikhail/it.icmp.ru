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
    /// Возвращаем обьект юзер из Сессии, если авторизован. Иначе null
    /// </summary>
    public static User GetUser()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Авторизация: запихиваем юзера в сессию
    /// </summary>
    /// <param name="login">Логин, он же nick</param>
    /// <param name="pass">Пароль</param>
    public static bool LogIn(string login, string pass)
    {
        bool status = false;
        User candidate = Users.GetUserByLogin(login);

        string true_pass = "magicword1" + pass + "magicword"; //TODO: Вынести в конфиг
        string hashedPass = FormsAuthentication.HashPasswordForStoringInConfigFile( true_pass, "MD5");

        if(candidate.Id != 0 && candidate.Pass == HashPass(pass))
        {
            HttpContext.Current.Session.Add("ITCurrentUser", candidate);
            status = true;
        }

        return status;
    }

    /// <summary>
    /// Выход
    /// </summary>
    /// <param name="returnUrl">Урл, куда перенаправим пользователя после выхода</param>
    public static void LogOut(string returnUrl)
    {
        throw new System.NotImplementedException();
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
    /// Хешируем пароль
    /// </summary>
    private static string HashPass(string pass)
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
        throw new System.NotImplementedException();
    }
}
