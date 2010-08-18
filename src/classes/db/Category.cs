using ITCommunity.Db.Tables;


namespace ITCommunity.Db {

    public partial class Category {

        public int PostsCount {
            get;
            private set;
        }

        partial void OnLoaded() {
            PostsCount = Categories.GetPostsCount(Id);
        }
    }
}
