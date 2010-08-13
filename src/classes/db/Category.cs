using ITCommunity.Db.Tables;


namespace ITCommunity.Db {

    public partial class Category {

        private int _postsCount;

        public int PostsCount {
            get {
                return _postsCount;
            }
        }

        partial void OnLoaded() {
            _postsCount = Categories.GetPostsCount(Id);
        }
    }
}
