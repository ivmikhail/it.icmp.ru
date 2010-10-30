using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ITCommunity.Models;
using System.Web.UI.MobileControls;
using ITCommunity.DB;
using ITCommunity.Core;
using ITCommunity.IndexerLib;
using ITCommunity.DB.Tables;

namespace ITCommunity.Controllers {
    public class SearchController : BaseController {
        public ActionResult Search(string q = "", String page = "1") {
            String query = q.Trim();

            int p = 1;
            int? pageNumber;
            if (Int32.TryParse(page, out p)) {
                pageNumber = p;
            } else {
                pageNumber = null;
            }
            int postsCount = 0;
            List<Post> posts = SearchLucene(query, p, Config.PostsPerPage, ref postsCount);
            var model = new SearchListModel(query, posts, pageNumber, postsCount);
            return View("List", model);
        }
        /// <summary>
        /// Возвращаем посты, удовлетворяющие условию поиска через Lucene
        /// </summary>
        /// <param name="query">условие поиска</param>
        /// <param name="page">текущая страница</param>
        /// <param name="count">кол-во постов на страницу</param>
        /// <param name="posts_count">кол-во найденных постов</param>
        /// <returns>список постов, важно - в Description пишем сниппет ???? </returns>
        // TODO: подумать
        private static List<Post> SearchLucene(String query, int page, int count, ref int posts_count) {

            List<SearchedPost> list = Indexer.GetInstance().Search(query, page, count, ref posts_count);
            List<Post> result = new List<Post>(list.Count);
            for (int i = 0; i < list.Count; i++) {
                Post post = Posts.Get(list[i].Id);
                result.Add(post);
            }
            return result;
        }
    }
}
