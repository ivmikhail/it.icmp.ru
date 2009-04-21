using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Категории новостей. Вложенность 1.
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
    /// Берем по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    public static Category GetById(int id)
    {
        //TODO: закешировать
        return GetCategoryFromRow(Database.CategoryGetById(id));
    }

    /// <summary>
    /// Берем все категории новостей
    /// </summary>
    public static List<Category> GetAll()
    {
        //TODO: Закешировать
        return GetCategoryFromTable(Database.CategoryGetAll());
    }

    /// <summary>
    /// Берем все категории новости
    /// </summary>
    public static List<Category> GetPostCategrories(int postId)
    {
        return GetCategoryFromTable(Database.PostGetCategories(postId));
    }

    /// <summary>
    /// Удаление
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    public static void Delete(int id)
    {
        //TODO: сбросить кеш
        Database.CategoryDel(id);
    }

    /// <summary>
    /// Добавление категории в базу
    /// </summary>
    /// <param name="category">Сама категория</param>
    public static Category Add(Category category)
    {
        //TODO: сбросить кеш
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
