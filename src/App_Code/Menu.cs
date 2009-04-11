using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

public class MenuItem
{
    private int    _id; // 0 значит родителей нету.
    private int    _parentId;
    private string _url;
    private int    _sort;

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

    public MenuItem Parent
    {
        get
        {
            if (_parentId == 0)
            {
                return new MenuItem();
            }
            else
            {
                return MenuItems.GetById(_parentId);
            }
        }
        set
        {
            _parentId = value._id;
        }
    }

    public string Url
    {
        get
        {
           return _url;
        }
        set
        {
            _url = value;
        }
    }

    public int Sort
    {
        get
        {
            return _sort;
        }
        set
        {
            _sort = value;
        }
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public MenuItem(int id, int parentId, string url, int sort)
    {
        _id = id;
        _parentId = parentId;
        _url = url;
        _sort = sort;
    }

    public MenuItem()
    {
        _id = -1;
        _parentId = -1;
        _url = "";
        _sort = -1;
    }
}
