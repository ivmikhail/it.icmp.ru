using System;
using System.Collections.Generic;
using System.Data;

namespace ITCommunity {
	/// <summary>
	/// MenuItem(Ссылка в менюшке). 2 уровня вложенности.
	/// </summary>
	public class MenuItem {

		//делегат метода загрузки меню из базы, нужен для организации кеширования
		private delegate object MenuLoader();

		#region Properties

		private static MenuLoader _menuLoader = new MenuLoader(GetMenuFromDB);

		private int _id;
		private int _parentId; // 0 значит родителей нету.
		private string _url;
		private int _sort;
		private string _name;
		private byte _newWindow; // не 0, открывать ссылку в новом окне

		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public MenuItem Parent {
			get {
				if (_parentId == 0) {
					return new MenuItem();
				}
				else {
					return MenuItem.GetById(_parentId);
				}
			}
			set {
				_parentId = value._id;
			}
		}

		public string Url {
			get { return _url; }
			set { _url = value; }
		}
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		public int Sort {
			get { return _sort; }
			set { _sort = value; }
		}
		public bool NewWindow {
			get {
				return _newWindow != 0;
			}
			set {
				_newWindow = value ? (byte)1 : (byte)0;
			}
		}

		#endregion

		public MenuItem() {
			_id = -1;
			_parentId = -1;
			_url = "";
			_sort = -1;
			_name = "";
		}

		public MenuItem(int id, int parentId, string url, int sort, string name, byte isInNewWindow) {
			_id = id;
			_parentId = parentId;
			_url = url;
			_sort = sort;
			_name = name;
			_newWindow = isInNewWindow;
		}

		/// <summary>
		/// Добавляем пункт меню
		/// </summary>
		/// <param name="MenuItem">Добавляемый пункт</param>
		/// <returns>Только что добавленный пункт в меню(из БД)</returns>
		public static MenuItem Add(MenuItem item) {
			MenuItem new_item = GetItemFromRow(Database.MenuItemsAdd(item.Parent.Id, item.Url, item.Sort, item.Name, item._newWindow));
			AppCache.Remove(Config.String("MenuCacheName"));

			return new_item;
		}

		/// <summary>
		/// Обновляем меню, кеш сбрасывается
		/// </summary>
		public void Update() {
			AppCache.Remove(Config.String("MenuCacheName"));
			Database.MenuItemsUpdate(this._id, this._parentId, this._url, this._sort, this._name, this._newWindow);
		}

		/// <summary>
		/// Удаляем
		/// </summary>
		/// <param name="id">Идентификатор пункта в меню</param>
		public static void Delete(int id) {
			Database.MenuItemsDel(id);
			AppCache.Remove(Config.String("MenuCacheName"));
		}

		/// <summary>
		/// Получаем пунктик в меню
		/// </summary>
		/// <param name="id">Идентификатор</param>
		public static MenuItem GetById(int id) {
			List<MenuItem> menuItems = GetMenu();
			MenuItem result = new MenuItem();
			foreach (MenuItem menu in menuItems) {
				if (menu.Id == id) {
					result = menu;
					break;
				}
			}
			return result;
		}

		/// <summary>
		/// Получаем пунктики по родителю
		/// Чтобы получить верхний уровень нужно передать в качестве идентификатора родителя 0.
		/// </summary>
		/// <param name="parentId">Идентификатор родительского пункта</param>
		/// <returns>Список подпунктов. Если идентификатор не натуральное число возвращается верхний уровень</returns>
		public static List<MenuItem> GetByParent(int parentId) {
			List<MenuItem> result = new List<MenuItem>();

			int parent = -1;
			if (parentId > 0) {
				parent = parentId;
			}
			List<MenuItem> menuItems = GetMenu();
			foreach (MenuItem menu in menuItems) {
				if (menu.Parent.Id == parent) {
					result.Add(menu);
				}
			}
			result.Sort(new MenuItemComparator());

			return result;
		}

		/// <summary>
		/// Забираем меню из кеша
		/// </summary>
		/// <returns>Список пунктов в меню, независимо от вложенности</returns>
		private static List<MenuItem> GetMenu() {
			object menu = AppCache.Get(
				Config.String("MenuCacheName"),
				_menuLoader,
				null,
				Config.Double("MenuCachePer"));
			return (List<MenuItem>)menu;
		}

		private static List<MenuItem> GetMenuFromDB() {
			return GetItemsFromTable(Database.MenuItemsGetAll());
		}

		private static List<MenuItem> GetItemsFromTable(DataTable dt) {
			List<MenuItem> items = new List<MenuItem>();
			for (int i = 0; i < dt.Rows.Count; i++) {
				items.Add(GetItemFromRow(dt.Rows[i]));
			}
			return items;
		}

		private static MenuItem GetItemFromRow(DataRow dr) {
			MenuItem item;
			if (dr == null) {
				item = new MenuItem();
			}
			else {
				item = new MenuItem(
					Convert.ToInt32(dr["id"]),
					Convert.ToInt32(dr["parent_id"]),
					Convert.ToString(dr["url"]),
					Convert.ToInt32(dr["sort"]),
					Convert.ToString(dr["name"]),
					Convert.ToByte(dr["new_window"])
				);
			}
			return item;
		}
	}
}
