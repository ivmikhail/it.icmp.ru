

namespace ITCommunity.DB {

    public partial class Vote {

        partial void OnLoaded() {
            var loadUser = User;
        }
    }
}
