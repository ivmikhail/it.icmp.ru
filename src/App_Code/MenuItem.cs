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
    /// MenuItem(Ссылка в менюшке). 2 уровня вложенности.
    /// </summary>

    // TODO: закешировать статичное барахло и обращаться только к кешу.

    public class MenuItem
    {
        private int _id;
        private int _parentId; // 0 значит родителей нету.
        private string _url;
        private int _sort;
        private string _name;

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

        public void Update()
        {
            Database.MenuItemsUpdate(this._id, this._parentId, this._url, this._sort, this._name);
        }

        public MenuItem(int id, int parentId, string url, int sort, string name)
        {
            _id = id;
            _parentId = parentId;
            _url = url;
            _sort = sort;
            _name = name;
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
        /// Получаем пунктик в меню
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public static MenuItem GetById(int id)
        {
            return GetItemFromRow(Database.MenuItemsGetById(id));
        }

        /// <summary>
        /// Получаем пунктики по родительскому меню
        /// </summary>
        /// <param name="parentId">Родитель</param>
        public static List<MenuItem> GetByParent(int parentId)
        {
            return GetItemsFromTable(Database.MenuItemsGetByParent(parentId));
        }

        /// <summary>
        /// Удаляем
        /// </summary>
        /// <param name="id">Идентификатор пункта в меню</param>
        public static void Delete(int id)
        {
            Database.MenuItemsDel(id);
        }

        /// <summary>
        /// Добавляем пункт
        /// </summary>
        /// <param name="MenuItem">Добавляемый пункт</param>
        public static MenuItem Add(MenuItem item)
        {
            return GetItemFromRow(Database.MenuItemsAdd(item.Parent.Id, item.Url, item.Sort, item.Name));
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
                                     Convert.ToString(dr["name"]));
            }
            return item;
        }
    }
}
