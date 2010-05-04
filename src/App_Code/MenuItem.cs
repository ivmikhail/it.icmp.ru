using System;
using System.Collections.Generic;
using System.Data;

namespace ITCommunity {
	/// <summary>
	/// MenuItem(Ссылка в менюшке). 2 уровня вложенности.
	/// </summary>
	public class MenuItem {

		private class Comparer : IComparer<MenuItem> {

			public int Compare(MenuItem x, MenuItem y) {
				return x.Sort.CompareTo(y.Sort);
			}

		}

		#region For caching

		public const string MENU_CACHE_KEY = "Menu";

		//делегат метода загрузки меню из базы, нужен для организации кеширования
		private delegate object MenuLoader();

		private static MenuLoader _menuLoader = GetMenuFromDB;

		#endregion

		#region Properties

		private static Comparer _comparer = new Comparer();

		private int _id;
		private int _parentId; // 0 значит родителей нету.
		private string _url;
		private int _sort;
		private string _name;
		private bool _onNewWindow;

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
					return MenuItem.Get(_parentId);
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
		public bool OnNewWindow {
			get { return _onNewWindow; }
			set { _onNewWindow = value; }
		}

		#endregion

		#region Constructors

		public MenuItem() {
			_id = -1;
			_parentId = -1;
			_url = "";
			_sort = -1;
			_name = "";
		}

		public MenuItem(int id, int parentId, string url, int sort, string name, bool onNewWindow) {
			_id = id;
			_parentId = parentId;
			_url = url;
			_sort = sort;
			_name = name;
			_onNewWindow = onNewWindow;
		}

		#endregion

		/// <summary>
		/// Обновляем меню, кеш сбрасывается
		/// </summary>
		public void Update() {
			AppCache.Remove(MENU_CACHE_KEY);

			byte onNewWindow = (byte)(_onNewWindow ? 1 : 0);

			Database.MenuItemsUpdate(_id, _parentId, _url, _sort, _name, onNewWindow);
		}

		#region Public static methods

		/// <summary>
		/// Добавляем пункт меню
		/// </summary>
		/// <param name="MenuItem">Добавляемый пункт</param>
		/// <returns>Только что добавленный пункт в меню(из БД)</returns>
		public static MenuItem Add(MenuItem item) {
			AppCache.Remove(MENU_CACHE_KEY);

			byte onNewWindow = (byte)(item.OnNewWindow ? 1 : 0);

			return GetItemFromRow(Database.MenuItemsAdd(item.Parent.Id, item.Url, item.Sort, item.Name, onNewWindow));
		}

		/// <summary>
		/// Удаляем
		/// </summary>
		/// <param name="id">Идентификатор пункта в меню</param>
		public static void Delete(int id) {
			AppCache.Remove(MENU_CACHE_KEY);

			Database.MenuItemsDel(id);
		}

		/// <summary>
		/// Получаем пунктик в меню
		/// </summary>
		/// <param name="id">Идентификатор</param>
		public static MenuItem Get(int id) {
			var menuItems = GetMenu();

			foreach (var menu in menuItems) {
				if (menu.Id == id) {
					return menu;
				}
			}

			return null;
		}

		/// <summary>
		/// Получаем пунктики по родителю
		/// Чтобы получить верхний уровень нужно передать в качестве идентификатора родителя 0.
		/// </summary>
		/// <param name="parentId">Идентификатор родительского пункта</param>
		/// <returns>Список подпунктов. Если идентификатор не натуральное число возвращается верхний уровень</returns>
		public static List<MenuItem> GetByParent(int parentId) {
			var result = new List<MenuItem>();

			var menuItems = GetMenu();
			foreach (var menu in menuItems) {
				if (menu._parentId == parentId) {
					result.Add(menu);
				}
			}
			result.Sort(_comparer);

			return result;
		}

		#endregion

		#region Private static methods

		/// <summary>
		/// Забираем меню из кеша
		/// </summary>
		/// <returns>Список пунктов в меню, независимо от вложенности</returns>
		private static List<MenuItem> GetMenu() {
			var menu = AppCache.Get(MENU_CACHE_KEY, _menuLoader);

			return (List<MenuItem>)menu;
		}

		private static List<MenuItem> GetMenuFromDB() {
			return GetItemsFromTable(Database.MenuItemsGetAll());
		}

		private static List<MenuItem> GetItemsFromTable(DataTable dt) {
			var items = new List<MenuItem>();

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
				bool onNewWindow = Convert.ToByte(dr["new_window"]) != 0;

				item = new MenuItem(
					Convert.ToInt32(dr["id"]),
					Convert.ToInt32(dr["parent_id"]),
					Convert.ToString(dr["url"]),
					Convert.ToInt32(dr["sort"]),
					Convert.ToString(dr["name"]),
					onNewWindow
				);
			}

			return item;
		}

		#endregion
	}
}
