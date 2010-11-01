using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class UserCommentsModel : PaginatedModel {

        private List<Comment> _comments;

        public List<Comment> List {
            get {
                if (_comments == null) {
                    Load();
                }
                return _comments;
            }
        }

        public User User {
            get;
            private set;
        }

        public UserCommentsModel(User user, int? page)
            : base(page) {
            User = user;
            PerPage = Config.UserCommentsPerPage;
            Load();
        }

        public void Load() {
            _comments = GetList();
        }

        protected virtual List<Comment> GetList() {
            return Comments.GetPagedByUser(User.Id, Page, PerPage, ref TotalCount);
        }
    }
}
