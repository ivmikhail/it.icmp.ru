using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

/// <summary>
/// Пользователь хранящийся в БД
/// </summary>

public class User
{
    private int _id;
    private string _pass;
    private string _nick;
    private string _email;
    private int _role;
    private DateTime _cdate;

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public enum Roles
    {
        Admin = 0,
        Poster = 1,
        User = 2,
    }

    public int Id
    {
        get
        {
            return _id;
        }
    }

    public string Pass
    {
        get
        {
            return _pass;
        }
        set
        {
            _pass = value;
        }
    }

    public string Email
    {
        get
        {
            return _email;
        }
        set
        {
            _email = value;
        }
    }

    public User.Roles Role
    {
        get
        {
            Roles result;
            switch (_role)
            {
                case 0:
                    result = Roles.Admin;
                    break;
                case 1:
                    result = Roles.Poster;
                    break;
                default:
                    result = Roles.User;
                    break;
            }
            return result;
        }
        set
        {
            _role = (int)value;
        }
    }

    public string Nick
    {
        get
        {
            return _nick;
        }
        set
        {
            _nick = value;
        }
    }

    public DateTime CreateDate
    {
        get
        {
            return _cdate;
        }
        set
        {
            _cdate = value;
        }
    }

    public User(int id, string nick, string pass, DateTime cdate, User.Roles role, string email)
    {
        _id    = id;
        _nick  = nick;
        _pass  = pass;
        _cdate = cdate;
        _role  = (int)role;
        _email = email;
    }

    public User() { }
}
