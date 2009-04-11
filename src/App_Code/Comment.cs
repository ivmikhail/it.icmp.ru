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

    public int Id
    {
        get
        {
            return _id;
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

    public User Author
    {
        get
        {
            return Users.GetById(_userId);
        }
    }

    public DateTime CreateDate
    {
        get
        {
            return _cdate;
        }
    }

    public string Ip
    {
        get
        {
            return _ip;
        }
    }
    public Comment(int id, int postId, int userId, DateTime cdate, string ip)
    {
        _id = id;
        _postId = postId;
        _userId = userId;
        _cdate = cdate;
        _ip = ip;
    }
    public Comment() {
        _id = -1;
        _postId = -1;
        _userId = -1;
        _cdate = DateTime.Now;
        _ip = "";
    }
}
