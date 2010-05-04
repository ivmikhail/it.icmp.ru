using System;
using System.Collections.Generic;
using System.Data;

namespace ITCommunity {
	/// <summary>
	/// Категории новостей. Вложенность 1.
	/// </summary>
	public class Category {

		#region For caching

		public const string CATEGORIES_CAHCE_KEY = "Categories";

		//делегат метода загрузки категорий из базы, нужен для организации кеширования
		private delegate object CategoriesLoader();

		private static CategoriesLoader _categoriesLoader = GetAllCategoriesFromDB;

		#endregion

		#region Properties

		private int _id;
		private string _name;
		private int _sort;

		public int Id {
			get { return _id; }
		}

		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		public int Sort {
			get { return _sort; }
			set { _sort = value; }
		}

		#endregion

		#region Constructors

		public Category() {
			_id = -1;
			_name = "";
			_sort = -1;
		}

		public Category(int id, string name, int sort) {
			_id = id;
			_name = name;
			_sort = sort;
		}

		#endregion

		/// <summary>
		/// Изменяем данную категорию, кеш сбрасывается.
		/// </summary>
		public void Update() {
			AppCache.Remove(CATEGORIES_CAHCE_KEY);

			Database.CategoryUpdate(_id, _sort, _name);
		}

		#region Public static methods

		/// <summary>
		/// Добавление категории в базу
		/// </summary>
		/// <param name="category">Только что добавленная категория</param>
		public static Category Add(Category category) {
			AppCache.Remove(CATEGORIES_CAHCE_KEY);

			return GetCategoryFromRow(Database.CategoryAdd(category.Name, category.Sort));
		}

		/// <summary>
		/// Берем по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор категории</param>
		public static Category Get(int id) {
			var categories = GetCategories();

			foreach (Category category in categories) {
				if (category.Id == id) {
					return category;
				}
			}

			return null;
		}

		/// <summary>
		/// Возвращаем все категории в списке из кеша
		/// </summary>
		public static List<Category> GetCategories() {
			var categories = AppCache.Get(CATEGORIES_CAHCE_KEY, _categoriesLoader);

			return (List<Category>)categories;
		}

		/// <summary>
		/// Получаем категории данной новости
		/// </summary>
		/// <param name="postId">Идентификатор новости</param>
		/// <returns>Список категорий</returns>
		public static List<Category> GetPostCategories(int postId) {
			//NOTE: Узкое место
			return GetCategoriesFromTable(Database.PostGetCategories(postId));
		}

		/// <summary>
		/// Удаление
		/// </summary>
		/// <param name="id">Идентификатор категории</param>
		public static void Delete(int id) {
			AppCache.Remove(CATEGORIES_CAHCE_KEY);

			// Что делать с новостями?
			Database.CategoryDel(id);
		}

		/// <summary>
		/// Проверяем, есть ли категория с данным айдишником
		/// </summary>
		/// <param name="id">Данный айдишник</param>
		public static bool IsExist(int id) {
			return Get(id) != null;
		}

		#endregion

		#region Private static methods

		private static List<Category> GetAllCategoriesFromDB() {
			return GetCategoriesFromTable(Database.CategoryGetAll());
		}

		private static List<Category> GetCategoriesFromTable(DataTable dt) {
			List<Category> cats = new List<Category>();
			for (int i = 0; i < dt.Rows.Count; i++) {
				cats.Add(GetCategoryFromRow(dt.Rows[i]));
			}
			return cats;
		}

		private static Category GetCategoryFromRow(DataRow dr) {
			Category cat;
			if (dr == null) {
				cat = new Category();
			}
			else {
				cat = new Category(
					Convert.ToInt32(dr["id"]),
					Convert.ToString(dr["name"]),
					Convert.ToInt32(dr["sort"])
				);
			}
			return cat;
		}

		#endregion
	}
}
