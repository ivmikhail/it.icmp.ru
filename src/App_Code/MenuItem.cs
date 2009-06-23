using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using ITCommunity;
using System.Web.Caching;

namespace ITCommunity
{

    /// <summary>
    /// MenuItem(������ � �������). 2 ������ �����������.
    /// </summary>

    public class MenuItem
    {
        private int _id;
        private int _parentId; // 0 ������ ��������� ����.
        private string _url;
        private int _sort;
        private string _name;
        private byte _newWindow; // �� 0 ��������� ������ � ����� ����

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
                } else
                {
                    return MenuItem.GetById(_parentId);
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
        public bool NewWindow {
            get {
                return _newWindow != 0;
            }
            set {
                _newWindow = value ? (byte)1 : (byte)0;
            }
        }

        public void Update()
        {
            Database.MenuItemsUpdate(this._id, this._parentId, this._url, this._sort, this._name, this._newWindow);
            RemoveMenuCache();
        }

        public MenuItem(int id, int parentId, string url, int sort, string name, byte isInNewWindow)
        {
            _id = id;
            _parentId = parentId;
            _url = url;
            _sort = sort;
            _name = name;
            _newWindow = isInNewWindow;
        }

        public MenuItem()
        {
            _id = -1;
            _parentId = -1;
            _url = "";
            _sort = -1;
            _name = "";
        }
        /// <summary>
        /// �������� ������� � ����
        /// </summary>
        /// <param name="id">�������������</param>
        public static MenuItem GetById(int id)
        {
            List<MenuItem> menu_items = GetMenuFromCache();
            MenuItem result = new MenuItem();
            foreach (MenuItem menu in menu_items)
            {
                if (menu.Id == id)
                {
                    result = menu;
                    break;
                }
            }
            return result;
           // return GetItemFromRow(Database.MenuItemsGetById(id));
        }



        /// <summary>
        /// �������� �������� �� ��������
        /// 
        /// ����� �������� ������� ������� ����� �������� � �������� �������������� �������� 0.
        /// </summary>
        /// <param name="parentId">������������� ������������� ������</param>
        /// <returns>������ ����������. ���� ������������� �� ����������� ����� ������������ ������� �������</returns>
        public static List<MenuItem> GetByParent(int parentId)
        {
            int parent = -1;
            if (parentId > 0)
            {
                parent = parentId;
            }
            List<MenuItem> menu_items = GetMenuFromCache();
            List<MenuItem> result = new List<MenuItem>();
            foreach (MenuItem menu in menu_items)
            {
                if (menu.Parent.Id == parent)
                {
                    result.Add(menu);
                }
            }
            return result;

            //return GetItemsFromTable(Database.MenuItemsGetByParent(parentId));
        }

        private static List<MenuItem> LoadMenuItemsToCache()
        {
            List<MenuItem> menu_items = GetItemsFromTable(Database.MenuItemsGetAll());
            //���������, ��������� �� ���� ��� ����?
            HttpContext.Current.Cache.Add("menu_items", menu_items, null, DateTime.Now.Add(new TimeSpan(7, 0, 0, 0, 0)), Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            return menu_items;
        }

        private static void RemoveMenuCache()
        {
            HttpContext.Current.Cache.Remove("menu_items");
        }

        private static List<MenuItem> GetMenuFromCache()
        {
            List<MenuItem> menu_items = (List<MenuItem>)HttpContext.Current.Cache.Get("menu_items");
            if (menu_items == null)
            {
                menu_items = LoadMenuItemsToCache();
            }
            return menu_items;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="id">������������� ������ � ����</param>
        public static void Delete(int id)
        {
            Database.MenuItemsDel(id);
            RemoveMenuCache();
        }

        /// <summary>
        /// ��������� �����
        /// </summary>
        /// <param name="MenuItem">����������� �����</param>
        public static MenuItem Add(MenuItem item)
        {
            RemoveMenuCache();
            return GetItemFromRow(Database.MenuItemsAdd(item.Parent.Id, item.Url, item.Sort, item.Name, item._newWindow));
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
            } else
            {
                item = new MenuItem(Convert.ToInt32(dr["id"]),
                                     Convert.ToInt32(dr["parent_id"]),
                                     Convert.ToString(dr["url"]),
                                     Convert.ToInt32(dr["sort"]),
                                     Convert.ToString(dr["name"]),
                                     Convert.ToByte(dr["new_window"]));
            }
            return item;
        }
    }
}
