using System.Collections.Generic;

using ITCommunity.DB;
using ITCommunity.DB.Tables;


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
