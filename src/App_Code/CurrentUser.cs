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
    private static User currentUser;
    public static User User
    {
        get 
        {
            if (isAuth)
            {
                currentUser = (User)HttpContext.Current.Session["CurrentUser"];
            } else
            {
                if (currentUser == null)
                {
                    currentUser = new User();
                }
            }
            return currentUser;
        }

    }

    /// <summary>
    /// Авторизация: запихиваем юзера в сессию
    /// </summary>
    /// <param name="login">Логин, он же nick</param>
    /// <param name="pass">Пароль</param>
    public static User LogIn(string login, string pass, bool remember)
    {
        /*
         Protected Sub authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs)

            Const QS_RETURN_URL As String = "ReturnURL"

            Dim dbConn As OleDbConnection = Nothing
            Dim dCmd As OleDbCommand = Nothing
            Dim dr As OleDbDataReader = Nothing
            Dim strConnection As String
            Dim strSQL As String
            Dim nextPage As String
            Dim ticket As FormsAuthenticationTicket
            Dim cookie As HttpCookie
            Dim encryptedStr As String


            Dim strPass As String = "nhfv,kth"
            Dim md5Hasher As New MD5CryptoServiceProvider()
            Dim hashedBytes As Byte()
            Dim encoder As New UTF8Encoding()
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(strPass))

            Response.Write(hashedBytes)


            Try
                'get the connection string from web.config and open a connection
                'to the database
                strConnection = ConfigurationManager. _
                ConnectionStrings("dbConn").ConnectionString

                dbConn = New OleDb.OleDbConnection(strConnection)
                dbConn.Open()

                'check to see if the user exists in the database
                strSQL = "SELECT nick AS UserName, Role FROM tblUsers " & _
                  "WHERE nick=? AND Password=?"
                dCmd = New OleDbCommand(strSQL, dbConn)
                dCmd.Parameters.Add(New OleDbParameter("nick", _
                       Login1.UserName))
                dCmd.Parameters.Add(New OleDbParameter("Password", _
                       Login1.Password))
                dr = dCmd.ExecuteReader()

                Dim isPersistent As Boolean = Login1.RememberMeSet
                If (dr.Read()) Then
                    'user credentials were found in the database so notify the system
                    'that the user is authenticated

                    'create an authentication ticket for the user with an expiration
                    'time of 30 minutes and placing the user's role in the userData
                    'property

                    If (Login1.RememberMeSet) Then
                        ticket = New FormsAuthenticationTicket(1, _
                      CStr(dr.Item("UserName")), _
                      DateTime.Now(), _
                      DateTime.Now().AddYears(10), _
                      Login1.RememberMeSet, _
                      CStr(dr.Item("Role")))
                        'cookie.Expires = ticket.IssueDate.AddYears(10)
                    Else
                        ticket = New FormsAuthenticationTicket(1, _
                      CStr(dr.Item("UserName")), _
                      DateTime.Now(), _
                      DateTime.Now().AddMinutes(20), _
                      Login1.RememberMeSet, _
                      CStr(dr.Item("Role")))
                    End If
                    encryptedStr = FormsAuthentication.Encrypt(ticket)

                    'add the encrypted authentication ticket in the cookies collection
                    'and if the cookie is to be persisted, set the expiration for
                    '10 years from now. Otherwise do not set the expiration or the
                    'cookie will be created as a persistent cookie.
                    cookie = New HttpCookie(FormsAuthentication.FormsCookieName, encryptedStr)
                    cookie.Expires = ticket.IssueDate.AddYears(10)

                    Response.Cookies.Add(cookie)

                    'get the next page for the user
                    If (Not IsNothing(Request.QueryString(QS_RETURN_URL))) Then
                        'user attempted to access a page without logging in so redirect
                        'them to their originally requested page
                        nextPage = Request.QueryString(QS_RETURN_URL)
                    Else
                        'user came straight to the login page so just send them to the
                        'home page
                        nextPage = Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING")

                    End If

                    'Redirect user to the next page
                    'NOTE: This must be a Response.Redirect to write the cookie to the
                    '	user's browser. Do NOT change to Server.Transfer which
                    '	does not cause around trip to the client browser and thus
                    '	will not write the authentication cookie to the client
                    '	browser.
                    Response.Redirect(nextPage, True)
                    'Response.Redirect(FormsAuthentication.GetRedirectUrl(Login1.UserName, True))
                    'Response.Redirect("http://it.icmp.ru:8080/newsdetails.aspx?news_id=2851")
                Else
                    Login1.FailureText = "Отказано в доступе"
                End If

            Finally
                'cleanup
                If (Not IsNothing(dr)) Then
                    dr.Close()
                End If

                If (Not IsNothing(dbConn)) Then
                    dbConn.Close()
                End If
            End Try


        End Sub
        */

        
        User user = Users.GetUserByLogin(login);

        string hashedPass = HashPass(pass);        

        if(user.Id > 0 && user.Pass == hashedPass)
        {
            HttpContext.Current.Session.Add("CurrentUser", user);
            //TODO: Переделать!
            /*
            DateTime ticketExpiration = DateTime.Now;
            if (remember)
            {
                DateTime.Now.AddYears(20);
            } else
            {
                DateTime.Now.AddMinutes(20);
            }
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Nick, DateTime.Now, ticketExpiration, remember, Convert.ToString((int)user.Role));
            //FormsAuthentication.RenewTicketIfOld(ticket);

            string encrypt = FormsAuthentication.Encrypt(ticket);
           HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);
            cookie.Expires = ticket.IssueDate.AddYears(20);
            HttpContext.Current.Response.Cookies.Add(cookie);
             */
            
        }

        return user;
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
