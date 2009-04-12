using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;

/// <summary>
/// ��������� ����� ������������� ������ User
/// </summary>
/// 
public static class Users
{
    /// <summary>
    /// �������� ������������ �� ���� �� ������
    /// </summary>
    /// <param name="login">login �� �� nick</param>
    public static User GetUserByLogin(string login)
    {
        return GetUserFromRow(Database.UserGetByLogin(login));
    }

    /// <summary>
    /// �������� ������������ �� ��������������
    /// </summary>
    /// <param name="userId">�������������</param>
    public static User GetById(int userId)
    {
        return GetUserFromRow(Database.UserGetById(userId));
    }

    /// <summary>
    /// �������� ������������� �� �����
    /// </summary>
    /// <param name="role">���� ���������� �������������</param>
    public static List<User> GetByRole(User.Roles role)
    {
        return GetUsersFromTable(Database.UserGetByRole((int)role));
    }

    /// <summary>
    /// �������� ��������� �������������������
    /// </summary>
    /// <param name="count">���������� ������ �������������</param>
    public static List<User> GetLastRegistered(int count)
    {
        return GetUsersFromTable(Database.UserGetLastRegistered(count));

    }

    /// <summary>
    /// �������� ����� �������� ��������
    /// </summary>
    /// <param name="count">���-�� ������ �������������</param>
    public static KeyValuePair<string, string> GetTopPosters(int count)
    {
        throw new NotImplementedException();
        //return GetUsersFromTable(Database.UserGetTopPosters(count));
    }

    /// <summary>
    /// ������� �������
    /// </summary>
    /// <param name="userId">������������� ������������</param>
    public static void Delete(int userId)
    {
        Database.UserDel(userId);
    }

    /// <summary>
    /// ��������� ������� � ��
    /// </summary>
    /// <param name="User">������������ �������� ����� �������� � ����</param>
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
