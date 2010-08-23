using ITCommunity.Core;


namespace ITCommunity.DB {

    public partial class Header {

        public static int MaxLength {
            get { return Config.GetInt("HeaderMaxLength"); }
        }

        public static int ShowingHours {
            get { return Config.GetInt("HeaderShowingHours"); }
        }

        public static int RequiredPostsCount {
            get { return Config.GetInt("HeaderRequiredPostsCount"); }
        }

        partial void OnLoaded() {
            var loadUser = User;
        }
    }
}
