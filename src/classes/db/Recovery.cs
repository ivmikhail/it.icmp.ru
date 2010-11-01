using ITCommunity.Core;


namespace ITCommunity.DB {

    public partial class Recovery {

        partial void OnLoaded() {
            var loadUser = User;
        }
    }
}
