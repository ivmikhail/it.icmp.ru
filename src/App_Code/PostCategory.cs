using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

/// <summary>
/// Категории новостей. Вложенность 1.
/// </summary>
public class PostCategory
{
    private int _id;
    private string _name;
    private int _sort;

    public int Id
    {
        get
        {
            return _id;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
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

    public PostCategory(int id, string name, int sort)
    {
        _id   = id;
        _name = name;
        _sort = sort;
    }

    public PostCategory() { }
}
