using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

/// <summary>
/// Комментарий к посту
/// </summary>
/// 
public class Comment
{
    private int _id;
    private int _postId;
    private int _userId;
    private DateTime _cdate;
    private string _ip;
    private string _text;

    public int Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    public Post Post
    {
        get
        {
            return Posts.GetById(_postId);
        }
        set
        {
            _postId = value.Id;
        }
    }

    /// <summary>
    /// Пустой юзер если коммент оставил аноним
    /// </summary>
    public User Author
    {
        get
        {
            User user = new User();
            if (_userId > 0)
            {
                user = Users.GetById(_userId);
            }
            return user;
        }
        set
        {
            _userId = value.Id;
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

    public string Ip
    {
        get
        {
            return _ip;
        }
        set
        {
            _ip = value;
        }
    }
    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;
        }
    }

    public Comment(int id, int postId, int userId, DateTime cdate, string ip, string text)
    {
        _id = id;
        _postId = postId;
        _userId = userId;
        _cdate = cdate;
        _ip = ip;
        _text = text;
    }
    public Comment() {
        _id = -1;
        _postId = -1;
        _userId = -1;
        _cdate = DateTime.Now;
        _ip = "";
        _text = "";
    }
}
