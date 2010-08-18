using ITCommunity.Core;


namespace ITCommunity.Db {

    public partial class Recovery {

        partial void OnLoaded() {
            var loadUser = User;
        }
    }
}
