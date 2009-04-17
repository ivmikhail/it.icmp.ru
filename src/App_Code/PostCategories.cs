using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Категории новостей. 1 уровень вложенности
/// </summary>
public static class PostCategories
{

    /// <summary>
    /// Берем по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    public static PostCategory GetById(int id)
    {
        //TODO: закешировать
        return GetCategoryFromRow(Database.CategoryGetById(id));
    }

    /// <summary>
    /// Берем все категории новостей
    /// </summary>
    public static List<PostCategory> GetAll()
    {
        //TODO: Закешировать
        return GetCategoryFromTable(Database.CategoryGetAll());
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
    public static PostCategory Add(PostCategory category)
    {
        //TODO: сбросить кеш
        return GetCategoryFromRow(Database.CategoryAdd(category.Name, category.Sort));
    }

    private static List<PostCategory> GetCategoryFromTable(DataTable dt)
    {
        List<PostCategory> cats = new List<PostCategory>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            cats.Add(GetCategoryFromRow(dt.Rows[i]));
        }
        return cats;
    }

    private static PostCategory GetCategoryFromRow(DataRow dr)
    {
        PostCategory cat;
        if (dr == null)
        {
            cat = new PostCategory();
        }
        else
        {
            cat = new PostCategory(Convert.ToInt32(dr["id"]),
                                 Convert.ToString(dr["name"]),
                                 Convert.ToInt32(dr["sort"]));
        }
        return cat;
    }
}
