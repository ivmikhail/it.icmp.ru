using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;

/// <summary>
/// Статичный класс обслуживающий обьект User
/// </summary>
/// 
public static class Users
{
    /// <summary>
    /// Получаем пользователя из базы по логину
    /// </summary>
    /// <param name="login">login он же nick</param>
    public static User GetUserByLogin(string login)
    {
        return GetUserFromRow(Database.UserGetByLogin(login));
    }

    /// <summary>
    /// Получаем пользователя по идентификатору
    /// </summary>
    /// <param name="userId">Идентификатор</param>
    public static User GetById(int userId)
    {
        return GetUserFromRow(Database.UserGetById(userId));
    }

    /// <summary>
    /// Получаем пользователей по ролям
    /// </summary>
    /// <param name="role">Роль получаемых пользователей</param>
    public static List<User> GetByRole(User.Roles role)
    {
        return GetUsersFromTable(Database.UserGetByRole((int)role));
    }

    /// <summary>
    /// Получаем последних зарегистрировашихся
    /// </summary>
    /// <param name="count">Количество нужных пользователей</param>
    public static List<User> GetLastRegistered(int count)
    {
        return GetUsersFromTable(Database.UserGetLastRegistered(count));

    }

    /// <summary>
    /// Получаем самых активных постеров
    /// </summary>
    /// <param name="count">Кол-во нужных пользователей</param>
    public static KeyValuePair<string, string> GetTopPosters(int count)
    {
        throw new NotImplementedException();
        //return GetUsersFromTable(Database.UserGetTopPosters(count));
    }

    /// <summary>
    /// Удаляем аккаунт
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    public static void Delete(int userId)
    {
        Database.UserDel(userId);
    }

    /// <summary>
    /// Добавляем аккаунт в бд
    /// </summary>
    /// <param name="User">Пользователь которого хотим добавить в базу</param>
    public static User Add(User user)
    {
        DataRow dr = Database.UserAdd(user.Nick, user.Pass, (byte)user.Role, user.Email);
        return GetUserFromRow(dr); 
    }

    private static List<User> GetUsersFromTable(DataTable dt)
    {
        List<User> users = new List<User>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            users.Add(GetUserFromRow(dt.Rows[i]));
        }
        return users;
    }

    private static User GetUserFromRow(DataRow dr)
    {
        User user;
        if (dr == null)
        {
            user = new User();
        }
        else
        {
            user = new User(Convert.ToInt32(dr["id"]),
                                 Convert.ToString(dr["nick"]),
                                 Convert.ToString(dr["pass"]),
                                 Convert.ToDateTime(dr["cdate"]),
                                 (User.Roles)Convert.ToInt16(dr["role"]),
                                 Convert.ToString(dr["email"]));
        }
        return user;
    }
}
