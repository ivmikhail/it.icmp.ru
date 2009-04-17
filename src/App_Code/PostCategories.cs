using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// ��������� ��������. 1 ������� �����������
/// </summary>
public static class PostCategories
{

    /// <summary>
    /// ����� �� ��������������
    /// </summary>
    /// <param name="id">������������� ���������</param>
    public static PostCategory GetById(int id)
    {
        //TODO: ������������
        return GetCategoryFromRow(Database.CategoryGetById(id));
    }

    /// <summary>
    /// ����� ��� ��������� ��������
    /// </summary>
    public static List<PostCategory> GetAll()
    {
        //TODO: ������������
        return GetCategoryFromTable(Database.CategoryGetAll());
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
    public static PostCategory Add(PostCategory category)
    {
        //TODO: �������� ���
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
