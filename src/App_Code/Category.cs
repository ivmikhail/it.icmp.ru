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
    /// ��������� ��������. ����������� 1.
    /// </summary>
    public class Category
    {
        //������� ������ �������� ��������� �� ����, ����� ��� ����������� �����������
        private delegate object CategoriesLoader(); 

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
        /// ���������, ���� �� ��������� � ������ ����������
        /// </summary>
        /// <param name="cat_id">������ ��������</param>
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
        /// ����� �� ��������������
        /// </summary>
        /// <param name="id">������������� ���������</param>
        public static Category GetById(int id)
        {
            List<Category> cats = GetAll();
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
        /// ���������� ��� ��������� � ������ �� ����
        /// </summary>
        public static List<Category> GetAll()
        {
            CategoriesLoader loader = new CategoriesLoader(GetAllCategoriesFromDB);
            List<Category> cats = (List<Category>)AppCache.Get(Global.ConfigStringParam("CategoriesCacheName"), 
                                                               new object(), 
                                                               loader, 
                                                               null, 
                                                               DateTime.Now.AddHours(Global.ConfigDoubleParam("CategoriesCachePer")));
            return cats;
        }

        /// <summary>
        /// �������� ��������� ������ �������
        /// </summary>
        /// <param name="postId">������������� �������</param>
        /// <returns>������ ���������</returns>
        public static List<Category> GetPostCategories(int postId)
        {
            //NOTE: ����� �����
            return GetCategoriesFromTable(Database.PostGetCategories(postId));
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="id">������������� ���������</param>
        public static void Delete(int id)
        {
            Database.CategoryDel(id);
            AppCache.Remove(Global.ConfigStringParam("CategoriesCacheName"));
        }

        /// <summary>
        /// ���������� ��������� � ����(��� ���������)
        /// </summary>
        /// <param name="category">������ ��� ����������� ���������</param>
        public static Category Add(Category category)
        {            
            Category cat = GetCategoryFromRow(Database.CategoryAdd(category.Name, category.Sort));
            AppCache.Remove(Global.ConfigStringParam("CategoriesCacheName"));

            return cat;
        }

        private static List<Category> GetAllCategoriesFromDB()
        {
            return GetCategoriesFromTable(Database.CategoryGetAll());

        }
        private static List<Category> GetCategoriesFromTable(DataTable dt)
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
