using System.Collections.Generic;

using ITCommunity.Db;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class UserPostsModel : PostListModel {

        public User User {
            get;
            private set;
        }

        protected override List<Post> GetList() {
            return Posts.GetPagedByUser(User.Id, Page, PerPage, ref TotalCount);
        }

        public UserPostsModel(User user, int? page) :
            base(SortBy.Date, page) {
            User = user;
            Load();
        }
    }
}
