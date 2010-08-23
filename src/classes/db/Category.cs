using ITCommunity.DB.Tables;


namespace ITCommunity.DB {

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
