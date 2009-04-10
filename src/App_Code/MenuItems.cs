using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// ��������� ����� ������������� ������ MenuItem(������ � �������). 2 ������ �����������.
/// </summary>

// TODO: ������������ ��� ��� ������� � ���������� ������ � ����.

public static class MenuItems
{
    /// <summary>
    /// �������� ������� � ����
    /// </summary>
    /// <param name="id">�������������</param>
    public static MenuItem GetById(int id)
    {
        return GetItemFromRow(Database.MenuItemsGetById(id));
    }

    /// <summary>
    /// �������� �������� �� ������������� ����
    /// </summary>
    /// <param name="parentId">��������</param>
    public static List<MenuItem> GetByParent(int parentId)
    {
        return GetItemsFromTable(Database.MenuItemsGetByParent(parentId));
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="id">������������� ������ � ����</param>
    public static void Delete(int id)
    {
        Database.MenuItemsDel(id);
    }

    /// <summary>
    /// ��������� �����
    /// </summary>
    /// <param name="MenuItem">����������� �����</param>
    public static MenuItem Add(MenuItem item)
    {
        return GetItemFromRow(Database.MenuItemsAdd(item.Parent.Id, item.Url, item.Sort));
    }

    private static List<MenuItem> GetItemsFromTable(DataTable dt)
    {
        List<MenuItem> items = new List<MenuItem>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            items.Add(GetItemFromRow(dt.Rows[i]));
        }
        return items;
    }

    private static MenuItem GetItemFromRow(DataRow dr)
    {
        MenuItem item;
        if (dr == null)
        {
            item = new MenuItem();
        }
        else
        {
            item = new MenuItem(Convert.ToInt32(dr["id"]),
                                 Convert.ToInt32(dr["parent_id"]),
                                 Convert.ToString(dr["url"]),
                                 Convert.ToInt32(dr["sort"]));
        }
        return item;
    }
}
