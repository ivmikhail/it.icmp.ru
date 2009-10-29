using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using ITCommunity;

namespace ITCommunity
{

    /// <summary>
    /// MenuItem(������ � �������). 2 ������ �����������.
    /// </summary>

    public class MenuItem
    {
        //������� ������ �������� ���� �� ����, ����� ��� ����������� �����������
        private delegate object MenuLoader();

        private int _id;
        private int _parentId; // 0 ������ ��������� ����.
        private string _url;
        private int _sort;
        private string _name;
        private byte _newWindow; // �� 0, ��������� ������ � ����� ����

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

        /// <summary>
        /// ��������� ����, ��� ������������
        /// </summary>
        public void Update()
        {
            Database.MenuItemsUpdate(this._id, this._parentId, this._url, this._sort, this._name, this._newWindow);
            AppCache.Remove(Global.ConfigStringParam("MenuCacheName"));
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
            List<MenuItem> menu_items = GetMenu();
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
            List<MenuItem> menu_items = GetMenu();
            List<MenuItem> result = new List<MenuItem>();
            foreach (MenuItem menu in menu_items)
            {
                if (menu.Parent.Id == parent)
                {
                    result.Add(menu);
                }
            }
            result.Sort(new MenuItemComparator());

            return result;

            //return GetItemsFromTable(Database.MenuItemsGetByParent(parentId));
        }

        /// <summary>
        /// �������� ���� �� ����
        /// </summary>
        /// <returns>������ ������� � ����, ���������� �� �����������</returns>
        private static List<MenuItem> GetMenu()
        {
            MenuLoader loader = new MenuLoader(GetMenuFromDB);
            List<MenuItem> menu = (List<MenuItem>)AppCache.Get(Global.ConfigStringParam("MenuCacheName"), 
                                                               new object(), 
                                                               loader, 
                                                               null, 
                                                               DateTime.Now.AddHours(Global.ConfigDoubleParam("MenuCachePer")));
            
            return menu;
        }

        private static List<MenuItem> GetMenuFromDB()
        {
            List<MenuItem> menu_items = GetItemsFromTable(Database.MenuItemsGetAll());
            return menu_items;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="id">������������� ������ � ����</param>
        public static void Delete(int id)
        {
            Database.MenuItemsDel(id);
            AppCache.Remove(Global.ConfigStringParam("MenuCacheName"));
        }

        /// <summary>
        /// ��������� ����� ����
        /// </summary>
        /// <param name="MenuItem">����������� �����</param>
        /// <returns>������ ��� ����������� ����� � ����(�� ��)</returns>
        public static MenuItem Add(MenuItem item)
        {
            MenuItem new_item = GetItemFromRow(Database.MenuItemsAdd(item.Parent.Id, item.Url, item.Sort, item.Name, item._newWindow));
            AppCache.Remove(Global.ConfigStringParam("MenuCacheName"));

            return new_item; 
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
