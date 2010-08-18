using System.Collections.Generic;

using ITCommunity.Db;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class FavoritePostsModel : PostListModel {

        protected override List<Post> GetList() {
            return Posts.GetPagedFavorite(Page, PerPage, ref TotalCount);
        }

        public FavoritePostsModel(int? page) :
            base(SortBy.Date, page) {
        }
    }
}
