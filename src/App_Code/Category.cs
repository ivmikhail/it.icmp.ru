using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Web.Caching;

using ITCommunity;

namespace ITCommunity
{
    /// <summary>
    /// Категории новостей. Вложенность 1.
    /// </summary>
    public class Category
    {
        private int _id;
        private string _name;
        private int _sort;

        public int Id
        {
            get
            {
                return _id;
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
            throw new System.NotImplementedException();
            //RemoveCatsCache();
        }

        public Category(int id, string name, int sort)
        {
            _id = id;
            _name = name;
            _sort = sort;
        }

        public Category()
        {
            _id = -1;
            _name = "";
            _sort = -1;
        }

        /// <summary>
        /// Проверяем, есть ли категория с данным айдишником
        /// </summary>
        /// <param name="cat_id">Данный айдишник</param>
        public static bool IsCategoryExist(int cat_id)
        {
            bool is_exist = false;
            List<Category> cats = GetAll();
            foreach (Category cat in cats)
            {
                if (cat.Id == cat_id)
                {
                    is_exist = true;
                    break;
                }
            }
            return is_exist;
        }

        /// <summary>
        /// Берем по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        public static Category GetById(int id)
        {
            List<Category> cats = GetCatsFromCache();
            Category result = new Category();
            foreach (Category cat in cats)
            {
                if (cat.Id == id)
                {
                    result = cat;
                    break;
                }
            }
            return result;
            //return GetCategoryFromRow(Database.CategoryGetById(id));
        }

        /// <summary>
        /// Берем все категории новостей
        /// </summary>
        public static List<Category> GetAll()
        {
            return GetCatsFromCache();
        }

        /// <summary>
        /// Получаем категории данной новости
        /// </summary>
        /// <param name="postId">Идентификатор новости</param>
        /// <returns>Список категорий</returns>
        public static List<Category> GetPostCategories(int postId)
        {
            //NOTE: Узкое место
            return GetCategoryFromTable(Database.PostGetCategories(postId));
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        public static void Delete(int id)
        {
            RemoveCatsCache();
            Database.CategoryDel(id);
        }

        /// <summary>
        /// Добавление категории в базу
        /// </summary>
        /// <param name="category">Сама категория</param>
        public static Category Add(Category category)
        {
            RemoveCatsCache();
            return GetCategoryFromRow(Database.CategoryAdd(category.Name, category.Sort));
        }

        private static List<Category> LoadCatsToCache()
        {
            List<Category> cats = GetCategoryFromTable(Database.CategoryGetAll());
            HttpContext.Current.Cache.Add("categories", cats, null, DateTime.Now.Add(new TimeSpan(7, 0, 0, 0, 0)), Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            return cats;
        }

        private static void RemoveCatsCache()
        {
            HttpContext.Current.Cache.Remove("categories");
        }

        private static List<Category> GetCatsFromCache()
        {
            List<Category> cats = (List<Category>)HttpContext.Current.Cache.Get("categories");
            if (cats == null)
            {
                cats = LoadCatsToCache();
            }
            return cats;
        }

        private static List<Category> GetCategoryFromTable(DataTable dt)
        {
            List<Category> cats = new List<Category>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cats.Add(GetCategoryFromRow(dt.Rows[i]));
            }
            return cats;
        }

        private static Category GetCategoryFromRow(DataRow dr)
        {
            Category cat;
            if (dr == null)
            {
                cat = new Category();
            } else
            {
                cat = new Category(Convert.ToInt32(dr["id"]),
                                     Convert.ToString(dr["name"]),
                                     Convert.ToInt32(dr["sort"]));
            }
            return cat;
        }
    }
}
