using System.Collections.Generic;

using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class CategoryPostsModel : PostListModel {

        public Category Category { get; set; }

        protected override List<Post> GetList() {
            return Posts.GetPagedByCategory(Page, PerPage, Category.Id, ref TotalCount);
        }

        public CategoryPostsModel(int id, int? page) :
            base(SortBy.Date, page) {
            Category = Categories.Get(id);
        }
    }
}
