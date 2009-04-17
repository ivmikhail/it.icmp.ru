using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

/// <summary>
/// Собственно новость.
/// </summary>
public class Post
{
    private int _id;
    private string _title;
    private string _description;
    private string _text;
    private DateTime _cdate;
    private int _userId;
    private int _catId;
    private int _attached;
    private int _views;
    private string _source;
    private int _commentsCount;

    public int Id
    {
        get
        {
            return _id;
        }
    }

    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            _title = value;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
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

    public User Author
    {
        get
        {
            return Users.GetById(_userId);
        }
        set
        {
            _userId = value.Id;
        }
    }

    public PostCategory Category
    {
        get
        {
            return PostCategories.GetById(_catId);
        }
        set
        {
            _catId = value.Id;
        }
    }

    public bool Attached
    {
        get
        {
            return !(_attached == 0);
        }
        set
        {
           _attached = value ? 1 : 0;
        }
    }

    public int Views
    {
        get
        {
            return _views;
        }
    }

    public string Source
    {
        get
        {
            return _source;
        }
        set
        {
            _source = value;
        }
    }
    public int CommentsCount
    {
        get
        {
            return _commentsCount;
        }
        set
        {
            _commentsCount = value;
        }
    }

    /// <summary>
    /// Обновляем
    /// </summary>
    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public Post(int id, string title, string description, string text, DateTime cdate, int userId, int catId, bool attached, int views, string source, int commentsCount)
    {
        _id = id;
        _title = title;
        _description = description;
        _text = text;
        _cdate = cdate;
        _userId = userId;
        _catId = catId;
        _attached = attached ? 1 : 0;
        _views = views;
        _source = source;
        _commentsCount = commentsCount;
    }

    public Post() {
        _id = -1;
        _title = "";
        _description = "";
        _text = "";
        _cdate = DateTime.Now;
        _userId = -1;
        _catId = -1;
        _attached = 0;
        _views = 0;
        _source = "";
        _commentsCount = 0;
    }
}
