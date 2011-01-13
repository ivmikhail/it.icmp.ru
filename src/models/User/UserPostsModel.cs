using System.Collections.Generic;

using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class UserPostsModel : PostListModel {

        public User User {
            get;
            private set;
        }

        public UserPostsModel(User user, int? page) :
            base(SortBy.Date, page) {
            User = user;
            Load();

            if (user.PostsCount != TotalCount) {
                user.PostsCount = TotalCount;
                Users.Update(user);
            }
        }

        protected override List<Post> GetList() {
            return Posts.GetPagedByUser(User.Id, Page, PerPage, ref TotalCount);
        }
    }
}
