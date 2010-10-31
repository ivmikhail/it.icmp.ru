using System.Web.Mvc;

using ITCommunity.Models;
using ITCommunity.DB;
using System.Collections.Generic;
using System;
using ITCommunity.Core;
using ITCommunity.IndexerLib;
using ITCommunity.DB.Tables;

namespace ITCommunity.Controllers {

    public class PostController : BasePostController {

        [Authorize]
        public ActionResult Add() {
            return base.BaseAdd();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(PostEditModel model) {
            return base.BaseAdd(model);
        }

        [Authorize]
        public ActionResult Edit(int? id) {
            return base.BaseEdit(id);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int? id, PostEditModel model) {
            return base.BaseEdit(id, model);
        }

        public ActionResult List(int? page) {
            var model = new PostListModel(PostListModel.SortBy.Date, page);

            return View("ListPage", model);
        }

        public ActionResult PopularList(string period, int? page) {
            var model = new PostListModel(PostListModel.SortBy.Views, period, page);

            return View(model);
        }

        public ActionResult DiscussibleList(string period, int? page) {
            var model = new PostListModel(PostListModel.SortBy.Comments, period, page);

            return View(model);
        }

        public ActionResult Search(string q = "", string page = "1") {
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
            //var model = new SearchListModel(query, posts, pageNumber, postsCount);
            var model = new PostListModel(PostListModel.SortBy.Date, pageNumber);
            model.List = posts;
            model.TotalCount = postsCount;
            model.Query = "&q=" + query;
            
            ViewData["Searched"] = query;
            return View("ListPage", model);
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
        private static List<Post> SearchLucene(string query, int page, int count, ref int posts_count) {

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
