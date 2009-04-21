using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// ��������� ��������. ����������� 1.
/// </summary>
public class Category
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

    public Category(int id, string name, int sort)
    {
        _id   = id;
        _name = name;
        _sort = sort;
    }

    public Category() {
        _id = -1;
        _name = "";
        _sort = -1;
    }

    /// <summary>
    /// ����� �� ��������������
    /// </summary>
    /// <param name="id">������������� ���������</param>
    public static Category GetById(int id)
    {
        //TODO: ������������
        return GetCategoryFromRow(Database.CategoryGetById(id));
    }

    /// <summary>
    /// ����� ��� ��������� ��������
    /// </summary>
    public static List<Category> GetAll()
    {
        //TODO: ������������
        return GetCategoryFromTable(Database.CategoryGetAll());
    }

    /// <summary>
    /// ����� ��� ��������� �������
    /// </summary>
    public static List<Category> GetPostCategrories(int postId)
    {
        return GetCategoryFromTable(Database.PostGetCategories(postId));
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="id">������������� ���������</param>
    public static void Delete(int id)
    {
        //TODO: �������� ���
        Database.CategoryDel(id);
    }

    /// <summary>
    /// ���������� ��������� � ����
    /// </summary>
    /// <param name="category">���� ���������</param>
    public static Category Add(Category category)
    {
        //TODO: �������� ���
        return GetCategoryFromRow(Database.CategoryAdd(category.Name, category.Sort));
    }

    private static List<Category> GetCategoryFromTable(DataTable dt)
    {
        List<Category> cats = new List<Category>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            cats.Add(GetCategoryFromRow(dt.Rows[i]));
        }
        return cats;
    }

    private static Category GetCategoryFromRow(DataRow dr)
    {
        Category cat;
        if (dr == null)
        {
            cat = new Category();
        } else
        {
            cat = new Category(Convert.ToInt32(dr["id"]),
                                 Convert.ToString(dr["name"]),
                                 Convert.ToInt32(dr["sort"]));
        }
        return cat;
    }
}
