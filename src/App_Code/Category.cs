using System;
using System.Collections.Generic;
using System.Data;

namespace ITCommunity {
	/// <summary>
	/// Категории новостей. Вложенность 1.
	/// </summary>
	public class Category {

		//делегат метода загрузки категорий из базы, нужен для организации кеширования
		private delegate object CategoriesLoader();

		#region Properties

		private static CategoriesLoader _categoriesLoader = new CategoriesLoader(GetAllCategoriesFromDB);

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

		/// <summary>
		/// Проверяем, есть ли категория с данным айдишником
		/// </summary>
		/// <param name="cat_id">Данный айдишник</param>
		public static bool IsExist(int catId) {
			bool isExists = false;
			List<Category> cats = GetAll();
			foreach (Category cat in cats) {
				if (cat.Id == catId) {
					isExists = true;
					break;
				}
			}
			return isExists;
		}

		/// <summary>
		/// Берем по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор категории</param>
		public static Category GetById(int id) {
			List<Category> cats = GetAll();
			Category result = new Category();
			foreach (Category cat in cats) {
				if (cat.Id == id) {
					result = cat;
					break;
				}
			}
			return result;
		}

		/// <summary>
		/// Возвращаем все категории в списке из кеша
		/// </summary>
		public static List<Category> GetAll() {
			List<Category> cats = (List<Category>)AppCache.Get(
				Config.String("CategoriesCacheName"),
				_categoriesLoader,
				null,
				Config.Double("CategoriesCachePer"));
			return cats;
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
			// Что делать с новостями?
			Database.CategoryDel(id);
			AppCache.Remove(Config.String("CategoriesCacheName"));
		}

		/// <summary>
		/// Добавление категории в базу(кеш удаляется)
		/// </summary>
		/// <param name="category">Только что добавленная категория</param>
		public static Category Add(Category category) {
			Category cat = GetCategoryFromRow(Database.CategoryAdd(category.Name, category.Sort));
			AppCache.Remove(Config.String("CategoriesCacheName"));
			return cat;
		}

		/// <summary>
		/// Изменяем данную категорию, кеш сбрасывается.
		/// </summary>
		public void Update() {
			Database.CategoryUpdate(_id, _sort, _name);
			AppCache.Remove(Config.String("CategoriesCacheName"));
		}

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
	}
}
